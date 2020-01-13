import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'gm-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.scss']
})
export class NewsComponent implements OnInit {
  public _location = '';
  public _agency = '';
  public datetime;

  @Input()
    set location(location: string) {
      this._location = location;
      console.log("news set location=" + location)}
    get location(): string { return this._location; }

    @Input()
    set agency(agency: string) {
      this._agency = agency;
      console.log("news set agency=" + agency)}
    get agency(): string { return this._agency; }

  constructor() { }

  ngOnInit() {
    this.datetime = new Date()
  }

}
