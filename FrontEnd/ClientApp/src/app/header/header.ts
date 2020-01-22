import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import { NavService } from '../dash-sidenav/sidenav-menu/service';
import { MessageService } from '../message.service';

@Component({
  selector: 'gm-header',
  templateUrl: './header.html',
  styleUrls: ['./header.scss']
})
export class HeaderComponent implements OnInit {
  messages: any[] = [];
  subscription: Subscription;
  location: string;
  backgroundStyle: any;

  constructor(public navService: NavService, private messageService: MessageService) {

    this.subscription = this.messageService.getMessage().subscribe(message => {
      if (message) {
        this.messages.push(message);
        console.log("header: message=")
        console.log(message.text);
        this.parseMessage(message.text);
      } else {
        // clear messages when empty message received
        this.messages = [];
      }
    });
  }

  ngOnInit() {
  }

  openNav() {
    this.navService.openNav();
  }

  parseMessage(message: string) {
    let mes = message.split(':');
    if (mes[0] == 'AgencySelected') {
      this.location = mes[1];
      if (this.location == "Non-Government") {
        this.location = mes[2];
      }
      console.log("location:" + this.location);
      this.changeBackground(this.location)
    }
  }

  changeBackground(location: string) {
    let background;

    switch (location) {
      case 'Austin': { background = "url('/assets/images/Austin.png')"; break; }
      case 'Travis County': { background = "url('/assets/images/Travis_County.png')"; break; }
      case 'State of Texas': { background = "url('/assets/images/Texas_State_Capitol.png')"; break; }
      case 'United States': { background = "url('/assets/images/United_States_Capitol.png')"; break; }
      case 'Glendale HOA': { background = "url('/assets/images/condominiums.png')"; break; }
    }
    this.backgroundStyle = { 'background-image': background };
  }
}
