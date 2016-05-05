import {Component} from 'angular2/core';
import {TalksComponent} from './talks/talks.component'

@Component({
    selector: 'my-app',
    template: `
        <h1>Meeting Transcript</h1>
        <talks></talks>
        `,
    // directives: [TalksComponent, MyHighlightDirective]
    directives: [TalksComponent]
})
export class AppComponent {
}