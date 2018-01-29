import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VolunteerComponent } from './volunteer.component';
import { SharedModule } from '../shared/shared.module'


@NgModule({
  imports: [
    CommonModule,
    SharedModule
  ],
  declarations: [VolunteerComponent],
  exports: [VolunteerComponent]
})
export class VolunteerModule { }
