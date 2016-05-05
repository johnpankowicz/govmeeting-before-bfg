import {Component} from 'angular2/core'
import {SectionsService} from './sections.service'

@Component({
    selector: 'sections',
    templateUrl: 'app/sections/sections.component.html',
    providers: [SectionsService]
})
export class SectionsComponent {
    sections;
    constructor(sectionsService: SectionsService) {
        this.sections = sectionsService.getSections();
    }
    OnChange(newValue: any){
        console.log(newValue)
    }
}