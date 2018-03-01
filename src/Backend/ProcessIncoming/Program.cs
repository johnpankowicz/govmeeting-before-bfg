using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GM.Backend.ProcessIncoming
{

    /*  This program processes raw transcripts and recordings of government meetings.
        For development, it is simplier to keep it as process from the WebApp.
        But it will eventually will become part of that app.

        It watches the "INCOMING" folder for new files and processes them.
        It creates a work folder in the Datafiles folder based on the name of the file.

        For transcript files, it produces a file in the subfolder "T3-ToTag" of the meeting folder.
        This file becomes input for the next step (step 4) of the transcript processing, adding tags.

        For recordings, it produces a group of files in the subfolder "R4-FixText" of the meeting folder.
        Each of these files contain a segment of the meeting text which are ready for the next step (step 5) of
        the recording processings, fixing the voice recognition text.
     */
    class Program
    {
        // Todo-g - This should come from configuration
        static string datafilesPath = Environment.CurrentDirectory + @"\..\..\Datafiles";


        static void Main(string[] args)
        {
            // https://andrewlock.net/using-dependency-injection-in-a-net-core-console-application/
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                //.AddSingleton<IFooService, FooService>()
                //.AddSingleton<IBarService, BarService>()
                .BuildServiceProvider();

            //configure console logging
            serviceProvider
                .GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Debug);

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");

            // For development only. If a work folder for this meeting exists, delete it.
            bool deleteMeetingFolderIfExists = true;

            ProcessFiles processFiles = new ProcessFiles(deleteMeetingFolderIfExists);
            processFiles.WatchIncoming();
            string s = Console.ReadLine();
        }
    }
}
