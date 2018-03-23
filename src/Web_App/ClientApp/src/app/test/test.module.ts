import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

import { TestComponent } from './test.component';
import { FetchDataComponent } from './fetchdata/fetchdata.component';
import { CounterComponent } from './counter/counter.component';


@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
 ],
  declarations: [TestComponent, FetchDataComponent, CounterComponent]
})
export class TestModule { }
