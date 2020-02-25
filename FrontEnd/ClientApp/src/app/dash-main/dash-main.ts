import { Component, OnInit, OnDestroy, Input} from '@angular/core';
import {Router} from '@angular/router';
import { Subscription } from 'rxjs';
import { UserSettingsService, UserSettings, LocationType } from '../user-settings.service';

const NoLog = false;  // set to false for console logging

@Component({
  selector: 'gm-dash-main',
  templateUrl: './dash-main.html',
  styleUrls: ['./dash-main.scss']
})
export class DashMainComponent implements OnInit, OnDestroy {
  private ClassName: string = this.constructor.name + ": ";
  subscription: Subscription;
  userSettingsService: UserSettingsService;
  location: string;
  agency: string;
  isCounty: boolean;

  // TODO These titles should be set from within the individual components (gov-info, bills, calendar, etc)
  govinfoTitle: string = "Politics";
  billsTitle: string = "Legislation";
  meetingsTitle: string = "Meetings";
  newsTitle: string = "Govmeeting News";
  fixasrTitle: string = "Proofread Transcript";
  addtagsTitle: string = "Add Tags to Transcript"
  viewMeetingTitle: string = "View Latest Meeting";
  issuesTitle: string = "Issues";
  officialsTitle: string = "Officials";
  virtualMeetingTitle: string = "Virtual Meeting";
  chatTitle: string = "Chat";
  chartsTitle: string = "Charts";
  notesTitle: string = "Notes";
  minutesTitle: string = "Meeting Minutes"

  constructor(private _userSettingsService: UserSettingsService) {
    this.userSettingsService = _userSettingsService;

   }

   ngOnInit() {
    this.userSettingsService.SettingsChangeAsObservable().subscribe(message => {
      // NoLog || console.log(this.ClassName + "receive message: " + message)
      let newSettings = this.userSettingsService.settings;
      NoLog || console.log(this.ClassName + "SCAO ", newSettings);
      this.location = newSettings.location;
      this.agency = newSettings.agency;
    })
  }

  ngOnDestroy() {
    // TODO - unsubscribe to ensure no memory leaks
    // this.subscription.unsubscribe();
  }

  private changeLocation(item: UserSettings) {
    this.location = item.location;
    this.agency = item.agency;
    NoLog || console.log(this.ClassName + "location:" + this.location);

    // this.isCounty = (this.location == "Lincoln County")
  }

}
