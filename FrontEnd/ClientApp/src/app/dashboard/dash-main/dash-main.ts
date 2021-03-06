import { Component, OnInit, OnDestroy, Input} from '@angular/core';
import { GetDashboardTitle } from '../dashboard-titles'
import { UserSettingsService, UserSettings, LocationType } from '../../user-settings.service';

const NoLog = true;  // set to false for console logging

@Component({
  selector: 'gm-dash-main',
  templateUrl: './dash-main.html',
  styleUrls: ['./dash-main.scss']
})
export class DashMainComponent implements OnInit{
  private ClassName: string = this.constructor.name + ": ";
  language: string = "en";
  location: string;
  agency: string;
  isMunicipal: boolean;
  isCounty: boolean;
  isState: boolean;
  isCountry: boolean;

  // page titles
  govinfoTitle: string;
  billsTitle: string;
  meetingsTitle: string;
  newsTitle: string;
  fixasrTitle: string;
  addtagsTitle: string;
  viewMeetingTitle: string;
  issuesTitle: string;
  officialsTitle: string;
  virtualMeetingTitle: string;
  chatTitle: string;
  chartsTitle: string;
  notesTitle: string;
  minutesTitle: string;
  workitemsTitle: string;
  alertsTitle: string;

  constructor(private userSettingsService: UserSettingsService) {
   }

   ngOnInit() {
    this.userSettingsService.subscribeSettings(message => {
      // NoLog || console.log(this.ClassName + "receive message: " + message)
      let newSettings = this.userSettingsService.settings;
      NoLog || console.log(this.ClassName + "SCAO ", newSettings);
      this.changeLocation(newSettings);
    })

    this.userSettingsService.subscribeLanguage(language => {
      this.language = language;
      this.changeTitles();
    })

    this.changeTitles();
  }

  private changeLocation(item: UserSettings) {
    this.location = item.location;
    this.agency = item.agency;
    NoLog || console.log(this.ClassName + "location:" + this.location);

    this.isCounty = (this.location == "Lincoln County")
  }

  private changeTitles() {
    this.govinfoTitle = GetDashboardTitle("Politics", this.language);
    this.billsTitle = GetDashboardTitle("Legislation", this.language);
    this.meetingsTitle = GetDashboardTitle("Meetings", this.language);
    this.newsTitle = GetDashboardTitle("Govmeeting News", this.language);
    this.fixasrTitle = GetDashboardTitle("Proofread Transcript", this.language);
    this.addtagsTitle = GetDashboardTitle("Add Tags to Transcript", this.language);
    this.viewMeetingTitle = GetDashboardTitle("View Latest Meeting", this.language);
    this.issuesTitle = GetDashboardTitle("Issues", this.language);
    this.officialsTitle = GetDashboardTitle("Officials", this.language);
    this.virtualMeetingTitle = GetDashboardTitle("Virtual Meeting", this.language);
    this.chatTitle = GetDashboardTitle("Chat", this.language);
    this.chartsTitle = GetDashboardTitle("Charts", this.language);
    this.notesTitle = GetDashboardTitle("Notes", this.language);
    this.minutesTitle = GetDashboardTitle("Meeting Minutes", this.language);
    this.workitemsTitle = GetDashboardTitle("Work Items", this.language);
    this.alertsTitle = GetDashboardTitle("Alerts", this.language);
  }


}
