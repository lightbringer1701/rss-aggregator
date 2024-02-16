using System;
using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;
using System.Globalization;
using System.Timers;

using AggregatorRSS.Context;

namespace AggregatorRSS.RSS;

/* RSS клиент, поиск новостей */
public class RSSClientService : IRSSClient
{
    private IHttpClientFactory _httpClientFactory;
    private RssContext _context;
    private ILogger<RSSClientService> _logger;

    private async void TimerEventProcessor(Object source, ElapsedEventArgs e)
    {
        await Task.Run(() => SearchAllFeed());
    }

    public RSSClientService(IHttpClientFactory httpClientFactory, RssContext context, ILogger<RSSClientService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _context = context;
        _logger = logger;
        var timer = new System.Timers.Timer(60000);
        timer.Elapsed += TimerEventProcessor!;
        timer.AutoReset = true;
        timer.Enabled = true;
    }

    public async Task SearchFeed(Channel channel) 
    {
        var httpClient = _httpClientFactory.CreateClient();
        /* Запрос на RSS-канал */
        try
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(channel.address)
            };
            using (var response = await httpClient.SendAsync(request))
            {
                await _context.SaveChangesAsync();
                var stream = await response.Content.ReadAsStreamAsync();
                
                XmlSerializer serializer = new XmlSerializer(typeof(RssXML));
                RssXML rss = (RssXML) serializer.Deserialize(stream)!;
                foreach (Feed feed in rss.Channel!.feeds!)
                {
                    feed.Channel = channel.guid;
                    /* Проверяем совпадения по guid (rss)
                       Не очень эффективно, да и не на всех rss есть guid, но для небольшого объема с определенными rss подойдет
                       Если нет совпадений в БД, то создаем новую запись
                    */
                    if (_context.Feeds.FirstOrDefault(r => r.GuidRss == feed.GuidRss) is null) 
                    {
                        _context.Feeds.Add(feed);
                    }                    
                }
                await _context.SaveChangesAsync();
            }
        } 
        catch (Exception e)
        {
            /*  */
            _logger.LogError(e.ToString());
        }
    }

    public async void SearchAllFeed()
    {
        /* Получим только те RSS-каналы, что отмечены как активные */
        List<Channel> channels = await _context.Channels.Where(o => o.enabled == true).ToListAsync();
        /* Для каждого активного начнем задачу по запросу */
        foreach (Channel channel in channels)
        {
            /* Проверка, что задача не выполняется */
            if (channel.lastEnd <= DateTime.UtcNow || channel.lastEnd is null)
            {
                /* Установим время начала последнего запуска (для избежания повторных запросов */
                channel.lastEnd = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                /* Вызываем задачу на получение актуальных записей с RSS */
                await SearchFeed(channel);
            }
        }       
    }
}    
    