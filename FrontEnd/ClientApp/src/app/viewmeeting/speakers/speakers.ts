import { Component, OnInit } from '@angular/core';
import { ViewMeetingService } from '../viewmeeting.service';
import { UserchoiceService } from '../userchoice.service';

const NoLog = true;  // set to false for console logging

@Component({
  selector: 'gm-speakers',
  templateUrl: './speakers.html',
  styleUrls: ['./speakers.css']
})
export class SpeakersComponent implements OnInit {
  private ClassName: string = this.constructor.name + ": ";
  speakerNames: string[];
  errorMessage: string;
  selectedSpeaker: number = 0;

  constructor(private _viewMeetingService: ViewMeetingService,
      private _userChoice: UserchoiceService) { }

  ngOnInit() {
    this.getSpeakerNames();
    this._userChoice.setSpeaker('SHOW ALL');
}

  getSpeakerNames() {
    this._viewMeetingService.getMeeting(null)
    .subscribe(
    meeting => {
        this.speakerNames = meeting.speakerNames;
        NoLog || console.log(this.ClassName, this.speakerNames);
      },
    error => this.errorMessage = <any>error);
  }

  IsSelectedSpeaker(i: number) : boolean {
    return(this._userChoice.getSpeaker() === this.speakerNames[i]);
  }

  FilterBySpeaker(i: number) {
    this.selectedSpeaker = i;
    NoLog || console.log(this.ClassName, this.speakerNames[i])
    this._userChoice.setSpeaker(this.speakerNames[i]);
    if (i!==0) {
        this._userChoice.setTopic('SHOW ALL');
    }
  }

}
