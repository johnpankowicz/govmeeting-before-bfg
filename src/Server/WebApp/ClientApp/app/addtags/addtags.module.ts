import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
//import { ReactiveFormsModule } from '@angular/forms'

import { AddtagsComponent } from './addtags.component';
import { TalksComponent } from './talks/talks.component';
import { TopicsComponent } from './topics/topics.component';
import { SectionsComponent } from './sections/sections.component';
import { GmSharedModule } from '../gmshared/gmshared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    //ReactiveFormsModule
    GmSharedModule
  ],
  declarations: [AddtagsComponent, TalksComponent, TopicsComponent, SectionsComponent]
})
export class AddtagsModule { }
