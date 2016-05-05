import {Directive, ElementRef, Input, Output, EventEmitter} from 'angular2/core';

@Directive({
    selector:'[myhighlight]',
    host: {
        '(mouseenter)':'onMouseEnter()',
        '(mouseleave)':'onMouseLeave()',
        '(mouseup)':'onMouseUp()'
    }
})

export class MyHighlightDirective {
    
    private _el:HTMLElement;
    private _defaultColor = 'yellow';
    fullText: string;
    selection: Selection;
    selectedText: string;
    
    @Input('myHighlight') highlightColor: string;
    
    @Output() textSelected: EventEmitter<string>;
    
    constructor(el: ElementRef) {
        this.textSelected = new EventEmitter<string>();
        this._el = el.nativeElement;
    }
    
    onMouseEnter() {
        this._highlight(this.highlightColor || this._defaultColor);
    }

    onMouseLeave() {
        this._highlight(null)   
    }
     
    onMouseUp() {
        console.log(window.getSelection());
        
        this.selection = window.getSelection();
        
        if ((this.selection.anchorNode == null) || 
        (!this.selection.anchorNode.textContent)) {
            return;
        }
        
        this.fullText = this.selection.anchorNode.textContent;
        console.log("FULL TEXT: " + this.fullText);           
        
        this.selectedText = this.fullText.substring(this.selection.anchorOffset,
          this.selection.focusOffset);

        console.log("SELECTED TEXT: " + this.selectedText);
        this.textSelected.emit(this.selectedText);
    }
    
    private _highlight(color: string) {
        this._el.style.backgroundColor = color;
    }
}