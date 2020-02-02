import { Component, OnInit, Input } from '@angular/core';

console.log = function() {}  // comment this out for console logging

@Component({
  selector: 'gm-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.scss']
})
export class NewsComponent implements OnInit {
  private ClassName: string = this.constructor.name;
  public _location = '';
  public _agency = '';
  public datetime;

  @Input()
    set location(location: string) {
      this._location = location;
      console.log(this.ClassName +"set location=" + location)
    }
    get location(): string { return this._location; }

    @Input()
    set agency(agency: string) {
      this._agency = agency;
      console.log(this.ClassName +"set agency=" + agency)
    }
    get agency(): string { return this._agency; }

  constructor() { }

  ngOnInit() {
    this.datetime = new Date()
  }


}
