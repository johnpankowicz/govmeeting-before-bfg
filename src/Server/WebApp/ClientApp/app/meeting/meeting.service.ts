import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';
import { catchError } from 'rxjs/operators';
import { HttpHeaders } from '@angular/common/http';
import { ViewMeeting } from './viewmeeting';

@Injectable()
export class MeetingService {

// The code outlined in main.ts for checking the Name argument need to be used here.
  private _meetingUrl_NoServer = 'assets/BoothbayHarbor_Selectmen_2014-09-08.json';
    //private _meetingUrl = 'assets/data/USA_ME_LincolnCounty_BoothbayHarbor_Selectmen/2014-09-08/Step 5 - processed transcript.json';
    private _meetingUrl = 'api/meeting';
    //private _meetingUrl = 'api/meeting/BoothbayHarbor/Selectmen/2014-09-08';
    //private _meetingUrl = 'api/meeting/USA/ME/LincolnCounty/BoothbayHarbor/Selectmen/2014-09-08';

    // private _meetingData: any = {};
    //private data: any = null;
    private observable: Observable<any>;
    private requestInProgress: boolean = false;
    private requestComplete: boolean = false;

    private errorMessage: string;


    constructor(private http: HttpClient) {
      console.log('MeetingService - constructor');
    }

    getMeeting(): Observable<ViewMeeting> {
        return this.http.get<ViewMeeting>(this._meetingUrl)
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

