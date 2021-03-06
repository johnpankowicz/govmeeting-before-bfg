import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DemoMaterialModule } from '../material';

import { ChatComponent } from './chat';

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
