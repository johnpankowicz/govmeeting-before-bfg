using Govmeeting.Backend.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoadDb
{
    class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=GovmeetingLoadDb;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        /// <summary>
        /// Gets or sets the government entities.
        /// </summary>
        /// <value>
        /// The government entities.
        /// </value>
        public DbSet<GovernmentBody> GovernmentBodies { get; set; }

        /// <summary>
        /// Gets or sets the meetings.
        /// </summary>
        /// <value>
        /// The meetings.
        /// </value>
        public DbSet<Meeting> Meetings { get; set; }

        /// <summary>
        /// Gets or sets the talks.
        /// </summary>
        /// <value>
        /// The talks.
        /// </value>
        public DbSet<Talk> Talks { get; set; }

        /// <summary>
        /// Gets or sets the topic discussions.
        /// </summary>
        /// <value>
        /// The topic discussions.
        /// </value>
        public DbSet<TopicDiscussion> TopicDiscussions { get; set; }

        /// <summary>
        /// Gets or sets the topics.
        /// </summary>
        /// <value>
        /// The topics.
        /// </value>
        public DbSet<Topic> Topics { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the speakers.
        /// </summary>
        /// <value>
        /// The speakers.
        /// </value>
        public DbSet<Speaker> Speakers { get; set; }

        /// <summary>
        /// Gets or sets the representatives.
        /// </summary>
        /// <value>
        /// The representatives.
        /// </value>
        public DbSet<Representative> Representatives { get; set; }

        /// <summary>
        /// Gets or sets the citizens.
        /// </summary>
        /// <value>
        /// The citizens.
        /// </value>
        public DbSet<Citizen> Citizens { get; set; }

        /// <summary>
        /// Gets or sets the languages.
        /// </summary>
        /// <value>
        /// The languages.
        /// </value>
        public DbSet<Language> Languages { get; set; }

    }
}
