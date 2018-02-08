import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Headers, RequestOptions } from '@angular/http';
import { Talk } from './talk';
import { Observable } from 'rxjs/Observable';
import { Addtags } from '../addtags';

// AppData is configuration which is passed in from index.html.
import { AppData } from '../../appdata';
import { catchError } from 'rxjs/operators';

import { ErrorObservable } from 'rxjs/observable/ErrorObservable';


@Injectable()
export class TalksService {

    // Todo-g - change to use this query string.
    //private getUrl = 'api/addtags/USA/PA/Philadelphia/Philadelphia/CityCouncil/2014-09-25';
    private getUrl : string = 'api/addtags';
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
        return Observable.of(this.addtags);
        //return this.postData(this.putUrl, addtags);
    }

    // The way that HTTP Post works in Asp.Net Core has changed from prior Asp.Net.
    // Some good sources of information are:
    //    http://andrewlock.net/model‐binding‐json‐posts‐in‐asp‐net‐core/
    //    https://docs.asp.net/en/latest/mvc/models/model-binding.html
    //    https://lbadri.wordpress.com/2014/11/23/web-api-model-binding-in-asp-net-mvc-6-asp-net-5/


    //// The following will post JSON data (Addtags instance) to an Asp.Net MVC 6 controller
    //// On the server side, you must do the following.
    //// In startup.cs, add this to ConfigureServices:
    ////     services.AddMvc().AddXmlSerializerFormatters();
    //// In the controller, define the action method as:
    ////     [HttpPost]
    ////     public void Post([FromBody]Addtags value) { ... }
    //postData(url: string, data: any): Observable<any> {
    //    let headers = new Headers({ 'Content-Type': 'application/json' });
    //    let options = new RequestOptions({ headers: headers });
    //    console.log('postData in talks.service');
    //    //return this.http.post(url, data, headers)
    //    return this.http.post(url, data, options)
    //    .map(res => console.info(res))
    //    .catch(this.handleError);
    //}

    //private extractData(res: Response) {
    //    if (res.status < 200 || res.status >= 300) {
    //    throw new Error('Bad response status: ' + res.status);
    //    }
    //    let body = res.json();
    //    //return body.data || { };
    //    return body || { };
    //}

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
