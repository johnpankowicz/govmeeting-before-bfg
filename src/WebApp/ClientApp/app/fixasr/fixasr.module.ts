import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule  } from '@angular/common/http';
import { FixasrComponent } from './fixasr.component';
import { VideoModule } from '../video/video.module';
import { GmSharedModule } from '../gmshared/gmshared.module'
import { RouterModule } from '@angular/router';
//import { ReactiveFormsModule } from '@angular/forms'

@NgModule({
  declarations: [
    FixasrComponent,
  ],
  imports: [
    //ReactiveFormsModule,
    CommonModule,
    RouterModule,
    FormsModule,
    HttpClientModule,
    VideoModule,
    GmSharedModule
  ],
  providers: [],
})
export class FixasrModule { }

