import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';
import { catchError } from 'rxjs/operators';
import { HttpHeaders } from '@angular/common/http';
import { ViewMeeting } from './viewmeeting';
import { share } from 'rxjs/operator/share';

@Injectable()
export class MeetingService {

    private meetingUrl = 'api/meeting';
    private observable: Observable<ViewMeeting>;
    private requestInProgress: boolean = false;
    private requestComplete: boolean = false;
    private errorMessage: string;

    // Normally the meetingId will be passed to the getMeeting method.
    // But we did not yet write the component for the user to select a meeting.
    // We will use id "1" for now.
    private meetingId: number = 1;

    constructor(private http: HttpClient) {
      console.log('MeetingService - constructor');
    }

    getMeeting(): Observable<ViewMeeting> {
        if (this.observable != null) {
            return this.observable
        }
        // See notes above for "meetingId".
        let url: string = this.meetingUrl;
        url = url + "/" + this.meetingId;
        //return this.http.get<ViewMeeting>(url)
        //    .pipe(catchError(this.handleError))
        //    .share();     // make it shared so more than one subscriber can get the same result.
         this.observable = this.http.get<ViewMeeting>(url)
            .pipe(catchError(this.handleError))
            .share();     // make it shared so more than one subscriber can get the same result.
        return this.observable;
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

