import { Component, OnInit } from '@angular/core';
import { VgAPI } from 'videogular2/core';
import { Observable } from 'rxjs/Rx';

@Component({
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
        api.play();
    }

    ngOnInit() {
    }

    constructor() {
        console.log('VideoComponent constructor');
        // Todo - use the server API to return the video
        //var location: string = 'api/video/';

        // Todo - location is now just hardcoded. This will be chosen by user.
        var location: string = "datafiles/USA_ME_LincolnCounty_BoothbayHarbor_Selectmen_en/2017-02-15/R4-FixText/part01/"
        var fileBasename: string = "ToFix"

        this.sources = [
            {
                //src: location + '2016-10-11 Boothbay Harbor Selectmen (3 minutes).mp4',
                src: location + fileBasename + '.mp4',
                type: 'video/mp4'
            }
/*            // Todo - provide .ogg and .webm versions of the videos
            {
              src: location + location + fileBasename + '.ogg',
              type: 'video/ogg'
            },
            {
              src: location + location + fileBasename + '.webm',
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
