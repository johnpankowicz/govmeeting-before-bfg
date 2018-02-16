When we were using "angular-seed", this folder contained the client side application (Typescript/Angular code).
This allowed the client and server app to be kept completely separate. 
The WebApp accessed the client app by creating a PhysicalFileProvider in WebApp/Startup.cs which resolved to the client app folder.

But now that the client was changed from using "angular-seed" to using "SpaTemplates", the client app is now in WebApp\ClientApp.
This was  easiest at the time to get the SpaTemplates version working.
But it would be useful if we can once again put them in their own separate projects.