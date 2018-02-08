import { Injectable } from '@angular/core';
//import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class TopicsService {

    private _topicsUrl = 'assets/topics.json';


    private topics: string[] = [
        "Topic1",
        "Topic2",
        "Topic3",
        "Topic4"
    ];
    getTopics(): Observable<string[]> {
        return Observable.of(this.topics); 
    }


    //constructor(private http: Http) { }

    //getTopics(): Observable<any> {
    //    if (this.data) {
    //        // if `data` is available just return it as `Observable`
    //        return Observable.of(this.data);
    //    } else if (this.observable) {
    //        // if `this.observable` is set then the request is in progress
    //        // return the `Observable` for the ongoing request
    //        return this.observable;
    //    } else {
    //        // create the request, store the `Observable` for subsequent subscribers
    //        this.observable = this.http.get(this._topicsUrl)
    //            //.map(res => res.json())     // No longer needed for HttpClientModule
    //            .do(val => {
    //                this.data = val;
    //                // when the cached data is available we don't need the `Observable` reference anymore
    //                this.observable = null;
    //            })
    //            // make it shared so more than one subscriber can get the result
    //            .share();
    //        return this.observable;
    //    }
    //}

}