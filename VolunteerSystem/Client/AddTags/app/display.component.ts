import {Component, ElementRef, OnInit} from 'angular2/core';

@Component({
  selector:'display',
  template:`
  <input #myname (input) = "updateName(myname.value)"/>
  <p> My name : {{myName}}</p>
`
})
export class DisplayComponent implements OnInit {
  myName:string;
  el;
  //updateName: Function;
  
  constructor(public element: ElementRef) {
    this.el = this.element.nativeElement // <- your direct element reference 
    this.myName = "Aman";
    //this.updateName  = function(input:String){
    //    this.myName = input;
    //}
  }
  
  updateName(input: string) {
      this.myName = input
  }
  
  ngOnInit() {
  }
}