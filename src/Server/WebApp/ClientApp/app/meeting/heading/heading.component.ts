import { Component, OnInit } from '@angular/core';
//import { MeetingService } from '../meeting.service-stub';
import { MeetingService } from '../meeting.service';

@Component({
  selector: 'gm-heading',
  templateUrl: './heading.component.html',
  styleUrls: ['./heading.component.css']
})
export class HeadingComponent implements OnInit {

    meeting: any = {
        meetingId: 0,
        locationId: 0,
        governmentBody: '',
        language: "",
        date: '',
        meetingLength: 0
    };

  errorMessage: string;

  constructor(private _meetingService: MeetingService) { }

  ngOnInit() {this.getMeeting();}

  getMeeting() {
    this._meetingService.getMeeting()
    .subscribe(
    (viewMeeting: any) => {
        this.meeting = viewMeeting.meeting;
        //console.log(this.meeting);
    },
    (error: any) => this.errorMessage = <any>error);
}

}
