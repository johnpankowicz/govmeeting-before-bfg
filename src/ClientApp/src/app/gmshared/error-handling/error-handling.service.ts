import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
//import { Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from 'rxjs';
//import { ErrorObservable } from 'rxjs/observable/ErrorObservable';
import { throwError } from "rxjs";
import { catchError } from 'rxjs/operators';
import { HttpHeaders } from '@angular/common/http';


@Injectable()
export class ErrorHandlingService {
    constructor(private http: HttpClient) {
        // console.log('ViewMeetingService - constructor');
    }

    // This method is copied from https://angular.io/guide/http
    public handleError(error: HttpErrorResponse) {
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
        //return ErrorObservable.create('Something bad happened; please try again later.');
        // return new ErrorObservable(
        //    'Something bad happened; please try again later.');

        console.log('Something bad happened; please try again later.')
        return throwError(error);
    }
}

