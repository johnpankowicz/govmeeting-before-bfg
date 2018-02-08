import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

import { TestComponent } from './test.component';
import { FetchDataComponent } from './fetchdata/fetchdata.component';
import { CounterComponent } from './counter/counter.component';

//import { MeetingComponent } from './meeting.component';
//import { BrowsemeetingComponent } from './browsemeeting/browsemeeting.component';
//import { HeadingComponent } from './heading/heading.component';
//import { SpeakersComponent } from './speakers/speakers.component';
//import { TopicsComponent } from './topics/topics.component';
//import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
    //SharedModule
 ],
  //declarations: [MeetingComponent, BrowsemeetingComponent,
  //    HeadingComponent, SpeakersComponent, TopicsComponent]
  declarations: [TestComponent, FetchDataComponent, CounterComponent]
})
export class TestModule { }
