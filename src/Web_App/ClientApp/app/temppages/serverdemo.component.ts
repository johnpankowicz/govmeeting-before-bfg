import { Component } from '@angular/core';

@Component({
  selector: 'gm-serverdemo',
  templateUrl: './serverdemo.component.html'
})
export class ServerdemoComponent{
    showtranscript: boolean = false;
    showhidetranscript: string = "Show";

    showindex: boolean = false;
    showhideindex: string = "Show";

    CheckShowTranscript(): boolean {
        return this.showtranscript;
    }
    ToggleTranscript() {
        this.showhidetranscript = this.showtranscript ? "Show" : "Hide";
        this.showtranscript = !this.showtranscript;
    }

    CheckShowIndex(): boolean {
        return this.showindex;
    }
    ToggleIndex() {
        this.showhideindex = this.showindex ? "Show" : "Hide";
        this.showindex = !this.showindex;
    }
}
