using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace AggregatorRSS.Context;

public partial class RssContext : DbContext
{

    public RssContext()
    {}

    public RssContext(DbContextOptions<RssContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public virtual DbSet<Channel> Channels { get; set; } = null!;

    public virtual DbSet<Feed> Feeds { get; set; } = null!;

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Channel>(entity =>
        {
            entity.HasKey(e => e.guid).HasName("channel_pk");

            entity.ToTable("channel");

            entity.Property(e => e.guid)
                .ValueGeneratedNever()
                .HasColumnName("guid");
            entity.Property(e => e.address).HasColumnName("address");
            entity.Property(e => e.alias).HasColumnName("alias");
            entity.Property(e => e.enabled)
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.lastStart).HasColumnName("laststart"); 
            entity.Property(e => e.lastEnd).HasColumnName("lastend");  
        });

        modelBuilder.Entity<Feed>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("feed_pk");

            entity.ToTable("feed");

            entity.Property(e => e.Guid)
                .ValueGeneratedNever()
                .HasColumnName("guid");
            entity.Property(e => e.Author).HasColumnName("author");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.Channel).HasColumnName("channel");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.GuidRss).HasColumnName("guid_rss");
            entity.Property(e => e.Link).HasColumnName("link");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.pubDate).HasColumnName("pubdate");
            
            entity.HasOne(d => d.ChannelNavigation).WithMany(p => p.feeds)
                .HasForeignKey(d => d.Channel)
                .HasConstraintName("feed_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
