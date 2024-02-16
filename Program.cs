namespace AggregatorRSS.Core;

public class Program 
{
    public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

    public static IHostBuilder CreateHostBuilder(string[] args)  => Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>().UseKestrel( options =>
            {
                    options.ListenAnyIP(5001);
            })
            .PreferHostingUrls(true);;
        });
}