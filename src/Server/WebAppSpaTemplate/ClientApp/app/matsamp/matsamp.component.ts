import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'gm-matsamp',
  templateUrl: './matsamp.component.html',
  styleUrls: ['./matsamp.component.css']
})
export class MatsampComponent implements OnInit {
  title = 'matsamp works!';
  isDarkTheme : boolean = false;
  progress : number = 50;

  constructor() { }

  ngOnInit() {
  }

}
