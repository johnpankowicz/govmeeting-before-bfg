import {Injectable} from 'angular2/core';
import {Http, Response} from 'angular2/http';
import {Observable} from 'rxjs/Observable';
import {Headers, RequestOptions} from 'angular2/http';

@Injectable()
export class BackendService {
    
    //private _meetingUrl = 'app/testdata/BBH-2014-09-08.json';
    private _meetingUrl = 'http://birdw8.com:57233/api/transcript';
    
    // private _meetingData: any = {};
    private data: any;
    private observable: Observable<any>;
    
    
    constructor (private http: Http) {}

    getMeeting(): Observable<any> {
        return this.getData(this._meetingUrl);
    }
       
    getData(url): Observable<any> {
        if(this.data) {
        // if `data` is available just return it as `Observable`
        return Observable.of(this.data);
        } else if(this.observable) {
        // if `this.observable` is set then the request is in progress
        // return the `Observable` for the ongoing request
        return this.observable;
        } else {
        // create the request, store the `Observable` for subsequent subscribers
        this.observable = this.http.get(url)
            .map(res => res.json())
            .do(val => {
                this.data = val;
                // when the cached data is available we don't need the `Observable` reference anymore
                this.observable = null;
            })
            // make it shared so more than one subscriber can get the result
            .share();
        return this.observable;
        }
    }  

    
/*       
    getMeetingFromFile(): Observable<{}[]> {
        return this.http.get(this._meetingUrl)
        .map(this.extractData)
        .catch(this.handleError);
    }
*/
    
    private extractData(res: Response) {
        if (res.status < 200 || res.status >= 300) {
        throw new Error('Bad response status: ' + res.status);
        }
        let body = res.json();
        return body.data || { };
    }
    
    private handleError (error: any) {
        // In a real world app, we might send the error to remote logging infrastructure
        let errMsg = error.message || 'Server error';
        console.error(errMsg); // log to console instead
        return Observable.throw(errMsg);
    }
}

