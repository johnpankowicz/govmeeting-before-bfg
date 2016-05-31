import {Directive, ElementRef, Input} from '@angular/core';
import {HostListener} from '@angular/core';

@Directive({
    selector:'[myhighlight]'
//    host: {
//        '(mouseenter)':'onMouseEnter()',
//        '(mouseleave)':'onMouseLeave()'
//    }
})

export class MyHighlightDirective {

    //@Input('myHighlight') highlightColor: string;
    @Input() highlightColor: string;

    private _el:HTMLElement;
    private _defaultColor = 'yellow';

    constructor(el: ElementRef) {
        this._el = el.nativeElement;
    }

    @HostListener('mouseenter')
    onMouseEnter() {
        //console.log('onMouseEnter');
        this._highlight(this.highlightColor || this._defaultColor);
    }
    
//        onMouseEnter() {
//        this._highlight(this.highlightColor || this._defaultColor);
//    }

    @HostListener('mouseleave')
    onMouseLeave() {
        //console.log('onMouseLeave');
        this._highlight('transparent');
    }

    private _highlight(color: string) {
        //console.log('color is ' + color);
        this._el.style.backgroundColor = color;
    }
}

//\GOVMEETING\CODE\SOURCE\Govmeeting\PublicSystem\Client\BrowserApp\src\client\app\shared\myhighlight\myhighlight.directive.ts
//      line 5   col 5  Use @HostBindings and @HostListeners instead of the host property (https://goo.gl/cHMFz7)
//      line 13  col 5  In the class "MyHighlightDirective", the directive input property "highlightColor" should not be renamed.
//      Please, consider the following use "@Input() highlightColor: string"