import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { GmSharedModule } from '../gmshared/gmshared.module'
import { ServerdemoComponent } from './serverdemo.component';
import { HomedemoComponent } from './homedemo.component';
import { OtherfeaturesComponent } from './otherfeatures.component';

@NgModule({
  imports: [
        CommonModule,
        RouterModule,
        GmSharedModule
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
