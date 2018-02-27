import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Headers, RequestOptions, Response } from '@angular/http';
import { AsrSegment } from './asrsegment';
import { AsrText } from './asrtext';
import { Observable } from 'rxjs/Observable';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';
import { catchError } from 'rxjs/operators';
import { HttpHeaders } from '@angular/common/http';

@Injectable()
export class FixasrService {
    private fixasrUrl = 'api/fixasr';
    private asrtext: AsrText;
    private error = { message: "not defined yet" };

    // Normally the meetingId & part will be passed to the getAsr method.
    // But we did not yet write the component for the user to select this.
    // We will use meeting "3" and part "1" for now.
    // This maps to the first work segment of a meeting of BBH on the server.
    private meetingId: number = 3;
    private part: number = 1;

    constructor(private http: HttpClient) {
        console.log('FixasrService - constructor');
    }

    getAsr(): Observable<AsrText> {
        // See notes above for meetingId & part.
        let url: string = this.fixasrUrl;
        url = url + "/" + this.meetingId + "/" + this.part;

        console.log('get data from ' + url);
        return this.http.get<AsrText>(url)
            .pipe(catchError(this.handleError));
    }

    postChanges(asrtext : AsrText): Observable < any > {
        // See notes above for meetingId & part.
        let url: string = this.fixasrUrl;
        url = url + "/" + this.meetingId + "/" + this.part;

        console.log('postChanges in fixasr.service  ' + url);
        return this.postData(url, asrtext);
    }

    private postData(url: string, asrtext: AsrText): Observable<AsrText> {
        const httpOptions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
            })
        };
        console.log('postData in fixasr.service');
        return this.http.post<AsrText>(url, asrtext, httpOptions)
            .pipe(catchError(this.handleError));
    }

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

