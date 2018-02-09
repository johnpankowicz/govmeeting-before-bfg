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
    private _Url_NoServer = 'assets/2016-10-11 Boothbay Harbor Selectmen (3 minutes).json';
    // Todo - switch to full api call:
    //private _UrlServer = 'api/fixasr/USA/ME/LincolnCounty/BoothbayHarbor/Selectmen/2016-10-11';
    private _UrlServer = 'api/fixasr';

    private asrtext: AsrText;
    private error = { message: "not defined yet" };

    constructor(private http: HttpClient) {
        console.log('FixasrService - constructor');
    }

    getAsr(): Observable<AsrText> {
        console.log('get data from ' + this._UrlServer);
        return this.http.get<AsrText>(this._UrlServer)
            .pipe(catchError(this.handleError));
    }

    // Todo-g - What should we return from the postChanges call?
        postChanges(asrtext : AsrText): Observable < any > {
        //return Observable.of(this.asrtext);
        console.log('postChanges in fixasr.service  ' + this._UrlServer);
        return this.postData(this._UrlServer, asrtext);
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

