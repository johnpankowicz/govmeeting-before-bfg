import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module'
import { ServerdemoComponent } from './serverdemo.component';
import { HomedemoComponent } from './homedemo.component';
import { OtherfeaturesComponent } from './otherfeatures.component';

@NgModule({
  imports: [
        CommonModule,
        RouterModule,
        SharedModule
  ],
    declarations: [
        ServerdemoComponent,
        HomedemoComponent,
        OtherfeaturesComponent
    ],
    exports: [
        ServerdemoComponent,
        HomedemoComponent,
        OtherfeaturesComponent
    ]
})
export class TemppagesModule { }
