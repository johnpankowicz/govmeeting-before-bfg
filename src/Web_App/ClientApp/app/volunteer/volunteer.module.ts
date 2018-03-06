import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VolunteerComponent } from './volunteer.component';
import { GmSharedModule } from '../gmshared/gmshared.module'
import { RouterModule } from '@angular/router';


@NgModule({
  imports: [
    CommonModule,
      GmSharedModule,
      RouterModule
  ],
  declarations: [VolunteerComponent],
  exports: [VolunteerComponent]
})
export class VolunteerModule { }
