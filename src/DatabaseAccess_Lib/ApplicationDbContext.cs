using GM.DatabaseModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace GM.DatabaseAccess
{
    /* There are a few ways to work with a database during development:
     * 1. Use Migrations
     *    Create the database intially with migrations:
     *      dotnet ef  migrations add CreateDatabase
     *      dotnet ef database update
     *    Whenever we change the data model, we create a new migration and apply it:
     *      dotnet ef  migrations add NewCustomerTable
     *      dotnet ef database update
     * 2. Create in code & manually delete when model changes.
     *      Add this to constructor of the DbContext:
     *          Database.EnsureCreated();
     *      Manually delete when model changes
     *          dotnet ef database drop
     * 3. Always drop and create when the app starts.
     *      Add this in constructor of the DbContext.
     *          Database.EnsureDeleted();
     *          context.Database.EnsureCreated();
     */

    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext()
        {
            //Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=GovmeetingDbAccess;Trusted_Connection=True;MultipleActiveResultSets=true");
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
