FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY ./bin/Release/net8.0/linux-x64/ ./
ENV ASPNETCORE_URLS=http://+:5001
ENTRYPOINT ["dotnet", "rss-aggregator.dll"]