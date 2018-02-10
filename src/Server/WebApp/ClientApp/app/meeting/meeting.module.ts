import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { MeetingComponent } from './meeting.component';
import { BrowsemeetingComponent } from './browsemeeting/browsemeeting.component';
import { HeadingComponent } from './heading/heading.component';
import { SpeakersComponent } from './speakers/speakers.component';
import { TopicsComponent } from './topics/topics.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    HttpClientModule,
    SharedModule
 ],
  declarations: [MeetingComponent, BrowsemeetingComponent,
     HeadingComponent, SpeakersComponent, TopicsComponent]
})
export class MeetingModule { }
