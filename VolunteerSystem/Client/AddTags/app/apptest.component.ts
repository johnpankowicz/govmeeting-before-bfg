import {Component} from 'angular2/core';
import {Directive, ElementRef, Input, Output, EventEmitter} from 'angular2/core';

/*
Use clicks Child1.
Child1 emits an event "emitc1" containing the contents of property "c1"
Parent1 captures this event because it has "the following in its template:
    <child1 (emitc1)="catchEmit($event)"></child1>
Parent1 stores the string in its "myc1" property.
Parent1 sends Child2 the string via this code in its template:
    <child2 [newc1]="myc1"></child2>
Child2 receives the string because it has declared a property:
    @Input() newc1: string;
*/

@Directive({
    selector: '[childdir]',    
    host: {
        '(mouseup)':'onMouseUp()'
    }
})
export class Childdir {
    d1: string = "d1 string"
    @Output() emitd1 = new EventEmitter<string>();    
    onMouseUp() {
        console.log("EMIT this.d1");
        this.emitd1.emit(this.d1);
    }
}


@Component({
    selector: 'child1',
    template: `
        <div (click)="doEmit()">
        child1<br>
        </div>
        `
})
export class Child1 {
    c1: string = "c1 string"
    @Output() emitc1 = new EventEmitter<string>();    
    doEmit() {
        console.log("EMIT this.c1");
        this.emitc1.emit(this.c1);
    }
}

@Component({
    selector: 'child2',
    template: `
        child2 newc1={{newc1}}<br>
        `
})
export class Child2 {
    c2: string = "c2 string"
    @Input() newc1: string;
}

@Component({
    selector: 'parent2',
    template: `
        parent 2<br>
        `
})
export class parent2 {
    p2: string = "p2 string";
}

@Component({
    selector: 'parent1',
    template: `
        parent 1<br>
        <div childdir (emitd1)="catchdEmit($event)">Click me here</div>
        <child1 (emitc1)="catchEmit($event)"></child1>
        <child2 [newc1]="myc1"></child2>
        `,
    directives: [Child1, Child2, Childdir]
})
export class parent1 {
    p1: string = "p1 string"
    myc1: string;
    catchEmit(c1: string) {
        console.log("CATCH c1");
        this.myc1 = c1;
    }
    catchdEmit(d1: string) {
        console.log("CATCH d1");
        this.myc1 = d1;
    }
}

@Component({
    selector: 'my-app',
    template: `
        <h1>Apptest</h1>
        <parent1></parent1>
        <parent2></parent2>
         `,
    directives: [parent1, parent2]
})
export class ApptestComponent {
    apptestvar1: string = "app string";
}


/*



*/