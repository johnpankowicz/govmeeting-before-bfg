import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Headers, RequestOptions, Response } from '@angular/http';
import { FixasrText, AsrSegment } from '../models/fixasr-view';
import { Observable } from 'rxjs/Observable';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';
import { catchError } from 'rxjs/operators';
import { HttpHeaders } from '@angular/common/http';
import { ErrorHandlingService } from '../gmshared/error-handling/error-handling.service';

@Injectable()
export class FixasrService {
    private fixasrUrl = 'api/fixasr';
    private asrtext: FixasrText;
    private error = { message: "not defined yet" };

    // Normally the meetingId & part will be passed to the getAsr method.
    // But we did not yet write the component for the user to select this.
    // We will use meeting "3" and part "1" for now.
    // This maps to the first work segment of a meeting of BBH on the server.
    private meetingId: number = 3;
    private part: number = 1;

    constructor(private http: HttpClient, private errHandling: ErrorHandlingService) {
        console.log('FixasrService - constructor');
    }

    getAsr(): Observable<FixasrText> {
        // See notes above for meetingId & part.
        let url: string = this.fixasrUrl;
        url = url + "/" + this.meetingId + "/" + this.part;

        console.log('get data from ' + url);
        return this.http.get<FixasrText>(url)
            .pipe(catchError(this.errHandling.handleError));
    }

    postChanges(asrtext : FixasrText): Observable < any > {
        // See notes above for meetingId & part.
        let url: string = this.fixasrUrl;
        url = url + "/" + this.meetingId + "/" + this.part;

        console.log('postChanges in fixasr.service  ' + url);
        return this.postData(url, asrtext);
    }

    private postData(url: string, asrtext: FixasrText): Observable<FixasrText> {
        const httpOptions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
            })
        };
        console.log('postData in fixasr.service');
        return this.http.post<FixasrText>(url, asrtext, httpOptions)
            .pipe(catchError(this.errHandling.handleError));
    }
}

