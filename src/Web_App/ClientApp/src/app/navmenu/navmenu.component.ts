import { Component } from '@angular/core';
//import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
    selector: 'gm-navbar',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
    isCollapsed: boolean = true;
}
