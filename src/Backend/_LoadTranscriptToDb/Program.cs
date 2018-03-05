using System;
using GM.Database.DbAccess;
using Microsoft.EntityFrameworkCore.Design;

namespace GM.Database.LoadTranscriptToDb
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ApplicationDbContext appContext = new ApplicationDbContext();
            appContext.Database.EnsureCreated();

        }
    }
}
