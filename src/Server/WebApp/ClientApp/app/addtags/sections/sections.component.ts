import { Component, OnInit } from '@angular/core';
import { AddtagsService } from '../addtags.service';

@Component({
  selector: 'gm-sections',
  templateUrl: './sections.component.html',
  styleUrls: ['./sections.component.css'],
})
export class SectionsComponent implements OnInit {
    errorMessage: string;
    sections : string[];

  constructor(private _addtagsService: AddtagsService) {
  }

    ngOnInit() { this.getSections(); }

getSections() {
    this._addtagsService.getSections()
        .subscribe(
        sections => this.sections = sections,
            error => this.errorMessage = <any>error);
}

 OnChange(newValue: any) {
      console.log(newValue);
  }
}
