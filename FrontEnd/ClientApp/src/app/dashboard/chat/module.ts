import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DemoMaterialModule } from '../../material';

import { ChatComponent } from './component';

@NgModule({
  declarations: [ChatComponent],
  imports: [
    CommonModule,
    DemoMaterialModule
  ],
  exports: [
    ChatComponent
]
})
export class ChatModule { }