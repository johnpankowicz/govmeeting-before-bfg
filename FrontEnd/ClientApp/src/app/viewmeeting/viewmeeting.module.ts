import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { ViewMeetingComponent } from './viewmeeting';
import { BrowseComponent } from './browse/browse';
import { HeadingComponent } from './heading/heading';
import { SpeakersComponent } from './speakers/speakers';
import { TopicsComponent } from './topics/topics';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    HttpClientModule,
    SharedModule
 ],

  declarations: [
    ViewMeetingComponent,
    BrowseComponent,
    HeadingComponent,
    SpeakersComponent,
    TopicsComponent
  ],

  exports: [
    ViewMeetingComponent
  ]
})
export class ViewMeetingModule { }
