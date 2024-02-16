using AggregatorRSS.Context;

namespace AggregatorRSS.RSS;

public interface IRSSClient
{
    /* Поиск новостей по RSS-каналу */
    public Task SearchFeed(Channel channel);
    /* Поиск новостей по всем каналам */
    public void SearchAllFeed();
}