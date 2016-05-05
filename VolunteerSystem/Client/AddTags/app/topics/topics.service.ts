import {Injectable} from 'angular2/core';
import {Http, Response} from 'angular2/http';
import {Observable} from 'rxjs/Observable';
import {Headers, RequestOptions} from 'angular2/http';

@Injectable()
export class TopicsService {
    
    private _topicsUrl = 'app/topics/topics.json';
    
    constructor (private http: Http) {}
    
    getTopics() : string[] {
        return [
            "Topic1",
            "Topic2",
            "Topic3",
            "Topic4"
            ];
    }
    
    getTopicsFromFile(): Observable<string[]> {
        return this.http.get(this._topicsUrl)
        .map(this.extractData)
        .catch(this.handleError);
    }
    
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