import { Component, OnInit } from '@angular/core';
import { VgAPI } from 'videogular2/core';
import { Observable } from 'rxjs/Rx';

// AppData is configuration which is passed in from index.html.
import { AppData } from '../appdata';

@Component({
  moduleId: module.id,
  selector: 'gm-video',
  templateUrl: 'video.component.html',
  styleUrls: ['video.component.css']
})
export class VideoComponent {
    sources:Array<Object>;
    api:VgAPI;

    onPlayerReady(api:VgAPI) {
        this.api = api;
        console.log('In video OnPlayerReady');
    }

    ngOnInit() {
    }

    // TODO - When the Firefox developer tools are open showing console output,
    // an error appears when switching away from the "Fix Error" page where the
    // videogular video is displayed. If we switch to the Home page, we get the
    // error "Invalid URI. Load of media resource  failed." If we switch to any
    // page (About, Add Tags, etc), then we get the error "Cannot play media. No
    // decoders for requested formats: text/html". There are questions on stackoverflow
    // about this problem, and it is often just occurring in Firefox.
    // https://stackoverflow.com/questions/47003569/firefox-returning-content-type-text-html-not-supported-for-video
    // https://stackoverflow.com/questions/30810955/loading-html-video-with-jquery-not-working-anymore



    constructor(private appData: AppData) {
        console.log('VideoComponent constructor isServerRunning=' + appData.isServerRunning);
        // Todo-g Full videos will be split into pieces which will be processed seperately.
        //      See Backend/ AutoTranscription / SplitVideo
        // Todo-g Compose URL to handle production and development
        //var location: string = appData.isServerRunning ? 'http://localhost:58880/assets/video/' : 'assets/video/';
        //var location: string = appData.isServerRunning ? 'http://localhost:58880/assets/video/' : 'assets/shortvideo/';
        //var location: string = 'assets/shortvideo/';
        var location: string = 'assets/';
        //var location: string = 'api/video/';

        this.sources = [
            {
              src: location + '2016-10-11 Boothbay Harbor Selectmen (3 minutes).mp4',
              //src: location,
              type: 'video/mp4'
            },
            {
              src: location + '2016-10-11 Boothbay Harbor Selectmen (3 minutes).ogg',
              type: 'video/ogg'
            }
/*            {
                src: 'http://static.videogular.com/assets/videos/videogular.mp4',
                type: 'video/mp4'
            },
            {
                src: 'http://static.videogular.com/assets/videos/videogular.ogg',
                type: 'video/ogg'
            },
            {
                src: 'http://static.videogular.com/assets/videos/videogular.webm',
                type: 'video/webm'
            }
*/
        ];
    }

    playPhrase(start : number, duration : number) {
        console.log('In video playPhrase, start=' + start + ' duration=' + duration);
        let timer = Observable.timer(duration * 1000);
        timer.subscribe(t=>this.api.pause());
        //timer.subscribe(t=>console.log('done with timeout'));
        this.api.seekTime(start);
        this.api.play();
        console.log('exiting video playPhrase');
    }
 }
