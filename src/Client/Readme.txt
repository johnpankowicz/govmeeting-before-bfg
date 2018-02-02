This folder was intended to contain a "BrowserApp" project, which contains the client side application (Typescript/Angular code).
When we were using "angular-seed", it was set up to work this way. The server application, "WebApp, and BrowserApp were able to
developed separately, if desired. 
The WebApp accessed the BrowserApp by creating a PhysicalFileProvider in WebApp/Startup.cs which resolved to src\ClientBrowserApp.

But now that BrowserApp was changed from using "angular-seed" to using "SpaTemplates", BrowserApp now resides in WebApp\ClientApp.
This was just easiest at the time to get the SpaTemplates version working.

But it would be helpful if we can once again put them in their own separate projects.