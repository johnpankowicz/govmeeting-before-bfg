import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { NavMenuComponent } from './navmenu/navmenu.component';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing/app-routing.module';
import { MeetingModule } from './meeting/meeting.module'
import { AddtagsModule } from './addtags/addtags.module'
import { FixasrModule } from './fixasr/fixasr.module'
import { SharedModule } from './shared/shared.module'
import { HomeModule } from './home/home.module';
import { AboutModule } from './about/about.module';
import { VolunteerModule } from './volunteer/volunteer.module';
import { TemppagesModule } from './temppages/temppages.module';
import { TestModule } from './test/test.module';
//import { MatsampModule } from './matsamp/matsamp.module

import { MeetingService } from './meeting/meeting.service';
import { MeetingServiceStub } from './meeting/meeting.service-stub';
import { AddtagsService } from './addtags/addtags.service';
import { AddtagsServiceStub } from './addtags/addtags.service-stub';
import { FixasrService } from './fixasr/fixasr.service';
import { FixasrServiceStub } from './fixasr/fixasr.service-stub';

import { AppData } from './appdata';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
    ],
    imports: [
        CommonModule,
        FormsModule,
        NgbModule.forRoot(),

        AppRoutingModule,
        HomeModule,
        AboutModule,
        MeetingModule,
        AddtagsModule,
        FixasrModule,
        VolunteerModule,
        TemppagesModule,
        //MatsampModule,
        SharedModule,
        TestModule
    ],
    providers: [
        // To use the stubs, uncomment the stubs and comment out the real.

        // The real services
        { provide: MeetingService, useClass: MeetingService },
        { provide: AddtagsService, useClass: AddtagsService},
        { provide: FixasrService, useClass: FixasrService },

        // The stub services
        //{ provide: MeetingService, useClass: MeetingServiceStub },
        //{ provide: AddtagsService, useClass: AddtagsServiceStub },
        //{ provide: FixasrService, useClass: FixasrServiceStub },

        // AppData is no longer used. It was used at one time to pass config
        // values from index.html and/or _Layout.cshtml into the Angular App.
        // It allowed us to determine at run-time whether the Angular app was
        // running with or without a backend Asp.Net server.
        // We were able to check these values by injecting AppData into the 
        // constructor of a service or component. For example:
        //    import { AppData } from '../appdata';
        //    ...
        //    constructor(private appData: AppData) { .... }
        // We will leave the code in for this, in case we need to pass
        // in arguments later.
        AppData,
        {
            provide: AppData,
            useValue: { isServerRunning: true, isDataFromMemory: false },
        }
    ],
})
export class AppModuleShared {
}
