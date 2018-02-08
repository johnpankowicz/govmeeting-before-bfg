// import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule  } from '@angular/common/http';
import { FixasrComponent } from './fixasr.component';
import { VideoModule } from '../video/video.module';
import { SharedModule } from '../shared/shared.module'
//import { ReactiveFormsModule } from '@angular/forms'

@NgModule({
  declarations: [
    FixasrComponent,
  ],
  imports: [
//    ReactiveFormsModule,
    CommonModule,
    FormsModule,
    HttpClientModule,
    VideoModule,
    SharedModule
    //MaterialModule.forRoot(),
  ],
  providers: [],
})
export class FixasrModule { }

