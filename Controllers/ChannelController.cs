using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using AggregatorRSS.Context;
using AggregatorRSS.RSS;

[ApiController]
[Route("api/[controller]")]
public class ChannelController : ControllerBase
{
    private RssContext _context;
    private ILogger<ChannelController> _logger;
    private IRSSClient _rssClient;

    public ChannelController(RssContext context, ILogger<ChannelController> logger, IRSSClient rssClient)
    {
        _context = context;
        _logger = logger;
        _rssClient = rssClient;
    }
    /* для получения подписок на каналы */
    [HttpGet]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<Channel>>> GetChannels()
    {
        return await _context.Channels.ToListAsync();
    }
    /* для запуска запросов */
    [HttpPost]
    [Route("/api/Run")]
    public ActionResult PostRun()
    {
        Task.Run (() => _rssClient.SearchAllFeed());
        return Ok();
    }
    /*  для новой подписки на канал
        достаточно ввести адрес и имя
     */
    [HttpPost]
    public async Task<ActionResult<Channel>> PostCreate([FromBody] Channel сhannel)
    {
        _context.Channels.Add(сhannel);
        await _context.SaveChangesAsync();
        /* */
        Task.Run (() => _rssClient.SearchAllFeed());
        return Ok();
    }
}