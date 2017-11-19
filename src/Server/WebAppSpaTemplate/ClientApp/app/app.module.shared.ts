import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing/app-routing.module';
import { MeetingModule } from './meeting/meeting.module'
import { AddtagsModule } from './addtags/addtags.module'
//import { FixasrModule } from './fixasr/fixasr.module'
import { SharedModule } from './shared/shared.module'
import { HomeModule } from './home/home.module';
import { AboutModule } from './about/about.module';
//import { MatsampModule } from './matsamp/matsamp.module';
import { NavbarComponent } from './navbar/navbar.component';

import { AppData } from './appdata';


@NgModule({
    declarations: [
        AppComponent,
        NavbarComponent
],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        AppRoutingModule,
        HomeModule,
        AboutModule,
        MeetingModule,
        AddtagsModule,
//        FixasrModule,
//        MatsampModule,
        SharedModule
    ],
    providers: [AppData,
        {
            provide: AppData,
            useValue: { isServerRunning: false, isDataFromMemory: false }
            //useValue: window.APPDATA
            //useValue: window['APP_DATA']
        }
    ],
    bootstrap: [AppComponent]
})
export class AppModuleShared {
}
