import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DemoMaterialModule } from './material';
import { FlexLayoutModule } from "@angular/flex-layout";

// TODO - eliminate duplicate imports
// I was getting errors when ReactiveFormsModule was not in dashboard.module.ts
// When do I need this both here and in other modules?.

import {FormsModule, ReactiveFormsModule} from '@angular/forms';

import { AppComponent } from './app.component';

import { ErrorHandlingService } from './shared/error-handling/error-handling.service';

import { AppRoutingModule } from './app-routing/app-routing.module';
import { ViewMeetingModule } from './viewmeeting/viewmeeting.module';
import { AddtagsModule } from './addtags/addtags.module';
import { FixasrModule } from './fixasr/fixasr.module';
import { SharedModule } from './shared/shared.module';

import { ViewMeetingService } from './viewmeeting/viewmeeting.service';
import { ViewMeetingServiceStub } from './viewmeeting/viewmeeting.service-stub';
import { AddtagsService } from './addtags/addtags.service';
import { AddtagsServiceStub } from './addtags/addtags.service-stub';
import { FixasrService } from './fixasr/fixasr.service';
import { FixasrServiceStub } from './fixasr/fixasr.service-stub';
import { AppData } from './appdata';
import { DashboardModule } from './dashboard/dashboard.module';
import { AboutProjectModule } from './about-project/about-project.module';
import { TestmatComponent } from './testmat/testmat.component';

// import { DashboardComponent } from './dashboard/dashboard.component';


// Is the Asp.Net server running
const _isAspServerRunning = true;

@NgModule({
  declarations: [
    AppComponent,
    TestmatComponent
    // DashboardComponent
  ],
  imports: [
    BrowserModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    //NgbModule.forRoot(),
    NgbModule,
    DemoMaterialModule,
    AppRoutingModule,
    ViewMeetingModule,
    AddtagsModule,
    FixasrModule,
    SharedModule,
    FlexLayoutModule,
    DashboardModule,
    AboutProjectModule
  ],
  exports: [
    //DemoMaterialModule
  ],
  providers: [
    ErrorHandlingService,

    // If you use the stubs for these services, it will not call the Asp.Net
    // server , but will instead return static data.
    { provide: ViewMeetingService, useClass: _isAspServerRunning ? ViewMeetingService : ViewMeetingServiceStub },
    { provide: AddtagsService, useClass: _isAspServerRunning ? AddtagsService : AddtagsServiceStub },
    { provide: FixasrService, useClass: _isAspServerRunning ? FixasrService : FixasrServiceStub },

    AppData,
    {
      provide: AppData,
      // We can read APP_DATA from the html.
      // useValue: window['APP_DATA']    // Get settings from html
      useValue: { isAspServerRunning: _isAspServerRunning },
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

