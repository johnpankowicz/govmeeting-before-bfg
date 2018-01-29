import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ServerdemoComponent } from './serverdemo.component';
import { SharedModule } from '../shared/shared.module'


@NgModule({
  imports: [
    CommonModule,
    SharedModule
  ],
  declarations: [ServerdemoComponent],
  exports: [ServerdemoComponent]
})
export class ServerdemoModule { }
