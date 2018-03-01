import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { catchError } from 'rxjs/operators';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';
import { ErrorHandlingService } from '../gmshared/error-handling/error-handling.service';

import { Addtags } from './addtags';
import { Talk } from './talks/talk';


@Injectable()
export class AddtagsService {
    private addtagsUrl: string = 'api/addtags';
    private addtags: Addtags;

    // Normally the meetingId will be passed to the getTalks method.
    // But we did not yet write the component for the user to select a meeting.
    // We will use id "2" for now. This maps to a meeting of Philadelphia on the server.
    private meetingId: number = 2;

    constructor(private http: HttpClient, private errHandling: ErrorHandlingService) {
        console.log('TalksService - constructor');
    }

    getTalks(): Observable<Addtags> {
        // See notes above for "meetingId".
        let url: string = this.addtagsUrl;
        url = url + "/" + this.meetingId;

        return this.http.get<Addtags>(url)
            .pipe(catchError(this.errHandling.handleError));
    }

    postChanges(addtags: Addtags): Observable<any> {
        console.log('postChanges in talks.service');
        //return Observable.of(this.addtags);
        return this.postData(this.addtagsUrl, addtags);
    }

    private postData(url: string, addtags: Addtags): Observable<Addtags> {
        const httpOptions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
            })
        };
        console.log('postData in fixasr.service');
        return this.http.post<Addtags>(url, addtags, httpOptions)
            .pipe(catchError(this.errHandling.handleError));
    }

    // Todo - This needs to call the WebApi for the data.
    getSections(): Observable<string[]> {
        return Observable.of(this.sections);
    }

    // Todo - This needs to call the WebApi for the data.
    getTopics(): Observable<string[]> {
        return Observable.of(this.topics);
    }

    private topics: string[] = [
        "Topic1",
        "Topic2",
        "Topic3",
        "Topic4"
    ];

    private sections: string[] = [
        'Invocation',
        'Approval of Journal',
        'Leaves of Absense',
        'Presentations',
        'Communications',
        'Introductions of Bills',
        'Reports',
        'Bills on Second Reading',
        'Public Comment',
        'Second Reading',
        'Speeches',
        'Adjournment'
    ];

    // The way that HTTP Post works in Asp.Net Core has changed from prior Asp.Net.
    // Some good sources of information are:
    //    http://andrewlock.net/model‐binding‐json‐posts‐in‐asp‐net‐core/
    //    https://docs.asp.net/en/latest/mvc/models/model-binding.html
    //    https://lbadri.wordpress.com/2014/11/23/web-api-model-binding-in-asp-net-mvc-6-asp-net-5/
}
