import { Component, OnInit } from '@angular/core';
import { UserchoiceService } from './userchoice.service';

@Component({
  selector: 'gm-meeting',
  templateUrl: './viewmeeting.component.html',
  styleUrls: ['./viewmeeting.component.css'],
  providers: [
    UserchoiceService
  ]
})
export class ViewMeetingComponent implements OnInit {
  ngOnInit() {
  }
}
