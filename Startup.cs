using Newtonsoft.Json;
using AggregatorRSS.Context;
using AggregatorRSS.RSS;

namespace AggregatorRSS.Core;

public class Startup
{

    //private void OnShutdown() {}

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers().AddNewtonsoftJson();
        services.AddSwaggerGen();
        services.AddDbContext<RssContext>();
        services.AddHttpClient();
        services.AddSingleton<IRSSClient, RSSClientService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
    {
        //applicationLifetime.ApplicationStopping.Register(OnShutdown);
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        /* Swagger оставляем, для упрощения просмотра ТЗ */
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}