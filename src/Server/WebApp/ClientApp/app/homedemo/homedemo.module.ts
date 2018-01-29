import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomedemoComponent } from './homedemo.component';
import { RouterModule, Routes } from '@angular/router';

@NgModule({
  imports: [CommonModule, RouterModule],
  declarations: [HomedemoComponent],
  exports: [HomedemoComponent],
  providers: []
})
export class HomedemoModule { }
