import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'gm-meetings',
  templateUrl: './meetings.component.html',
  styleUrls: ['./meetings.component.scss']
})
export class MeetingsComponent implements OnInit {
  public _location = '';
  public _agency = '';

  @Input()
    set location(location: string) {
      this._location = location;
      console.log("Meetings set location=" + location)}
    get location(): string { return this._location; }

    @Input()
    set agency(agency: string) {
      this._agency = agency;
      console.log("Meetings set agency=" + agency)}
    get agency(): string { return this._agency; }
  constructor() { }

  ngOnInit() {
  }

}
