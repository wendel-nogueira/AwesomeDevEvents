using AwesomeDevEvents.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace AwesomeDevEvents.API.Persistence
{
    public class DevEventsDbContext : DbContext
    {
        public DevEventsDbContext(DbContextOptions<DevEventsDbContext> options) : base(options)
        {

        }

        public DbSet<DevEvent> DevEvents { get; set; }
        public DbSet<DevEventSpeaker> DevEventSpeakers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DevEvent>(entity => {
                entity.HasKey(devEvent => devEvent.Id);

                entity.Property(devEvent => devEvent.Title)
                    .HasColumnName("Title")
                    .HasMaxLength(100)
                    .HasColumnType("varchar(100)")
                    .IsRequired();

                entity.Property(devEvent => devEvent.Description)
                    .HasColumnName("Description")
                    .HasMaxLength(200)
                    .HasColumnType("varchar(200)")
                    .IsRequired(false);

                entity.Property(devEvent => devEvent.StartDate)
                    .HasColumnName("Start_Date")
                    .IsRequired();

                entity.Property(devEvent => devEvent.EndDate)
                    .HasColumnName("End_Date")
                    .IsRequired();

                entity.HasMany(devEvent => devEvent.Speakers)
                    .WithOne()
                    .HasForeignKey(speaker => speaker.DevEventId);
            });

            builder.Entity<DevEventSpeaker>(entity => {
                entity.HasKey(devEventSpeaker => devEventSpeaker.Id);

                entity.Property(devEventSpeaker => devEventSpeaker.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(100)
                    .HasColumnType("varchar(100)")
                    .IsRequired();

                entity.Property(devEventSpeaker => devEventSpeaker.TalkTitle)
                    .HasColumnName("Talk_Title")
                    .HasMaxLength(100)
                    .HasColumnType("varchar(100)")
                    .IsRequired();

                entity.Property(devEventSpeaker => devEventSpeaker.TalkDescription)
                    .HasColumnName("Talk_Description")
                    .HasMaxLength(200)
                    .HasColumnType("varchar(200)")
                    .IsRequired(false);

                entity.Property(devEventSpeaker => devEventSpeaker.LinkedinProfile)
                    .HasColumnName("Linkedin_Profile")
                    .HasMaxLength(256)
                    .HasColumnType("varchar(256)")
                    .IsRequired(false);
            });
        }
    }
}
