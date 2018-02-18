import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { NavMenuComponent } from './navmenu/navmenu.component';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing/app-routing.module';
import { ViewMeetingModule } from './viewmeeting/viewmeeting.module'
import { AddtagsModule } from './addtags/addtags.module'
import { FixasrModule } from './fixasr/fixasr.module'
import { GmSharedModule } from './gmshared/gmshared.module'
import { HomeModule } from './home/home.module';
import { AboutModule } from './about/about.module';
import { VolunteerModule } from './volunteer/volunteer.module';
import { TemppagesModule } from './temppages/temppages.module';
import { TestModule } from './test/test.module';
//import { MatsampModule } from './matsamp/matsamp.module

import { ViewMeetingService } from './viewmeeting/viewmeeting.service';
import { ViewMeetingServiceStub } from './viewmeeting/viewmeeting.service-stub';
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
        ViewMeetingModule,
        AddtagsModule,
        FixasrModule,
        VolunteerModule,
        TemppagesModule,
        //MatsampModule,
        GmSharedModule,
        TestModule
    ],
    providers: [
        // To use the stubs, uncomment the stubs and comment out the real.

        // The real services
        { provide: ViewMeetingService, useClass: ViewMeetingService },
        { provide: AddtagsService, useClass: AddtagsService},
        { provide: FixasrService, useClass: FixasrService },

        // The stub services
        //{ provide: ViewMeetingService, useClass: ViewMeetingServiceStub },
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
