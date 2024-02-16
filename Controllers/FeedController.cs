using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using AggregatorRSS.Context;
using AggregatorRSS.RSS;

[ApiController]
[Route("api/[controller]")]
public class FeedController : ControllerBase
{
    private RssContext _context;
    private ILogger<FeedController> _logger;

    public FeedController(RssContext context, ILogger<FeedController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /* если фильтра нет, то покажем все новости*/
    [HttpGet]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<Feed>>> GetFeeds(string? filter)
    {
        if (filter is null || filter == "")
        {
            return await _context.Feeds.ToListAsync();
        }
        /* и фильтр и заголовок к нижнему регистру, чтобы упростить поиск*/
        return await _context.Feeds.Where( o => o.Title!.ToLower().Contains(filter!)).ToListAsync();
    }
}