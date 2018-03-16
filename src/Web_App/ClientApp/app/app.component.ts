import { Component, ElementRef } from '@angular/core';
import { AppData } from './appdata';

// See aumentations.ts for the explanation of the following.
import 'rxjs/Subject';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
    title = 'app';
    public isCollapsed = true;

// Alternate way of passing arguments from index.html to the Angular app.
// It retrieves an attribute of the root Angular element.
//  constructor(private eltRef:ElementRef) {
//    //let prop = eltRef.getAttribute('clientonly');
//    let native = this.eltRef.nativeElement;
//    let test = native.getAttribute("clientonly");
//    console.log( 'clientonly - ', test);
//  }

  // AppData is no longer used. See the notes in app.module.shared.ts.
  constructor(appData:AppData) {
    console.log('AppComponent - ', appData);
  }
}
