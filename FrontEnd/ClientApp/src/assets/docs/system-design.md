# System Flowcharts

The diagrams below show the interaction between software components. The ClientApp is the Angular Single Page Application that runs in the browser. The other components run on the server.

Each server component is a  separate Visual Studio project. Workflow_App and Web_App are console applications . The others are C# libraries.

There are separate diagrams for the Web_app and ClientApp internals.

![System flowchart](http://localhost:4200/assets/images/FlowchartSystem.png#thumbnail)

The components in the above diagram are:

```
* ClientApp            - Angular SPA
* Web_App              - Asp.Net  web server process
* Workflow_App         - Workflow control process
* GetOnlineFiles       - Retrieve online transcripts and recordings
* ProcessRecording     - Extract & transcribe audio. Create work segments.
* ProcessTranscript    - Transform raw transcripts 
* LoadDatabase         - Populate database with data from completed transcript
* OnlineAccess         - Routines to retrieve files from remote sites.
* GoogleCloudAccess    - Routines to access Google Cloud services
* FileDataRepositories - Store & Get JSON work file data
* DatabaseRepositories - Store & Get Model data from database
* DatabaseAccess       - Access database using Entity Framework
```

### ClientApp Flowchart

![ClientApp Flowchart](http://localhost:4200/assets/images/FlowchartClientApp.png)

```
* appmodule               - tell Angular how to construct and bootstrap the app
* app-routing & navmenu   - Angular component router and  navigation bar
* home                    - default first page
* volunteer               - first page for registered user
* fixasr                  - page to fix Auto Speech Recognition text
* video                   - component to play video file for fixasr
* addtags                 - page to add tag metadata to transcript
* register                - page to register a government body
* viewmeeting             - public page to view a completed transcript
* searchmeeting           - public page to search meeting databae
* setalerts               - public page to set alerts on future meeting events
* gmshared and models     - shared components and data models
```

### WebApp Flowchart

![WebApp Flowchart](http://localhost:4200/assets/images/FlowchartWebApp.png)

```
* Home Controller         - Home controller serves page with root Angular tag
* Account Controller      - Process user registration and login
* Manage Controller       - Users can manage their profiles
* Admin Controller        - Administrator can manage users, policies and claims
* Web API Controllers     - Each of these are small and merely get or put data to the
                            database or filesystem.
* Email Service           - Handle registration email confirmation
* Message Service         - Handle registration confirmation via text message 
```

# Frameworks

The Front-end is written in Typescript using Angular (2+).
The web server and backend are in C# using [DotNet Core](https://github.com/dotnet/core)  and [Asp.Net Core](https://github.com/aspnet/home)





# Application Environment

ASP.NET Core references a particular environment variable, ASPNETCORE_ENVIRONMENT to describe the environment the application is currently running in. This variable can be set to any value you like, but three values are used by convention: Development, Staging, and Production.

This value is set in the project properties under the Debug tab. It is used often in the Views\Shared cshtml files.

# User Secrets

When you clone the govmeeting repository from Github, you get everything except the "_SECRETS" folder. This folder resides outside the repository. It contains the following "secret" information:

* ClientId and ClientSecret for the Google external authorization service.
* SiteKey and Secret for the Google ReCaptcha service.
* Credentials for the Google Cloud Platform.
* Database connection string.
* Admin username and password.

The _SECRETS folder may contain four files.

* appsettings.Development.json
* appsettings.Production.json
* appsettings.Staging.json
* TranscribeAudio.json

TranscribeAudio.json contains the Google Cloud Platform credentials. Each of other three files may contain settings for each of the other secrets. appsettings.Production.json should contain all of the setting for production. Whatever settings are in these file will overide those that are in Server/WebApp/app.settings.json. This file is inlcluded in the repository.

If you want your local machine to have access to the Google services, you need to create a local folder "../_SECRETS in relation to where the repository is located. Then, for example, you can add a file "appsettings.Development.json" to it, which contains keys that you obtain from Google. See: [Google API Keys](home#google-api-keys)
