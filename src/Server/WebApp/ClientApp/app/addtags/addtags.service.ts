import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { catchError } from 'rxjs/operators';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';

import { Addtags } from './addtags';
import { Talk } from './talks/talk';


@Injectable()
export class AddtagsService {

    // Todo-g - change to use this query string.
    //private getUrl = 'api/addtags/USA/PA/Philadelphia/Philadelphia/CityCouncil/2014-09-25';
    private getUrl: string = 'api/addtags';
    private putUrl: string = 'api/addtags';
    private addtags: Addtags;

    constructor(private http: HttpClient) {
        console.log('TalksService - constructor');
    }

    getTalks(): Observable<Addtags> {
        return this.http.get<Addtags>(this.getUrl)
            .pipe(catchError(this.handleError));
    }

    postChanges(addtags: Addtags): Observable<any> {
        console.log('postChanges in talks.service');
        //return Observable.of(this.addtags);
        return this.postData(this.putUrl, addtags);
    }

    private postData(url: string, addtags: Addtags): Observable<Addtags> {
        const httpOptions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
            })
        };
        console.log('postData in fixasr.service');
        return this.http.post<Addtags>(url, addtags, httpOptions)
            .pipe(catchError(this.handleError));
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

    // This method is copied from https://angular.io/guide/http
    private handleError(error: HttpErrorResponse) {
        if (error.error instanceof ErrorEvent) {
            // A client-side or network error occurred. Handle it accordingly.
            console.error('An error occurred:', error.error.message);
        } else {
            // The backend returned an unsuccessful response code.
            // The response body may contain clues as to what went wrong,
            console.error(
                `Backend returned code ${error.status}, ` +
                `body was: ${error.error}`);
        }
        // return an ErrorObservable with a user-facing error message
        return new ErrorObservable(
            'Something bad happened; please try again later.');
    };
}
