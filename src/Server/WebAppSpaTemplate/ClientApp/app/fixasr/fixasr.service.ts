import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { AsrSegment } from './asrsegment';
import { AsrText } from './asrtext';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

// AppData is configuration which is passed in from index.html.
import { AppData } from '../appdata';

@Injectable()
export class FixasrService {

    isServerRunning: boolean = this.appData.isServerRunning;
    isDataFromMemory: boolean = this.appData.isDataFromMemory;

    // private _Url_NoServer = 'testdata/2016-10-11 Boothbay Harbor Selectmen.json';
    // private _Url_NoServer = 'testdata/2016-10-11 Boothbay Harbor Selectmen (3 minutes).json';
    private _Url_NoServer = 'assets/2016-10-11 Boothbay Harbor Selectmen (3 minutes).json';

    //private _UrlServer = 'api/fixasr/USA/ME/LincolnCounty/BoothbayHarbor/Selectmen/2016-10-11';
    private _UrlServer = 'api/fixasr';

    private testData: AsrText = {
        'lastedit': 0,
        'asrsegments':     [
        { startTime: '0:00', said: 'the tuesday october $YEAR 11 selectmen' },
        { startTime: '0:02', said: 'meeting i will apologize apologize for' },
        { startTime: '0:06', said: 'my voice i can hardly speak i woke up' },
        { startTime: '0:08', said: 'Saturday with a terrible cold so if you' },
        { startTime: '0:10', said: 'can\'t hear me just speak up and i\'ll try' },
        { startTime: '0:13', said: 'to speak louder and you may want to stay' },
        { startTime: '0:14', said: 'in the back yeah you guys today in the' },
        { startTime: '0:17', said: 'background is yeah I\'m like have our' },
        { startTime: '0:20', said: 'full board with us tonight and we have' },
        { startTime: '0:24', said: 'our recording secretary Kelly the ghost' },
        { startTime: '0:26', said: 'town manager Tom women and selected' },
        { startTime: '0:30', said: 'Trish Warren wendy wolf myself Denise' }
   ]};

    constructor (private http: Http, private appData:AppData) {
        console.log('FixasrService - ',appData);
    }

    // The property "IsServerRunning" on AppData
    // tells us if the ASP.NET server is running. If so we will call the API to get the data.
    // Otherwise, we will get the data via a file read from our assets folder.
    getAsr(): Observable<AsrText> {

        console.log('isServerRunning=' + this.isServerRunning + '   isDataFromMemory=' + this.isDataFromMemory);
        // if data from memory, just return data as an `Observable`
        if (this.isDataFromMemory) {
            console.log('getAsrFromMemory');
            return Observable.of(this.testData);
        } else if (this.isServerRunning) {
            console.log('get data from ' + this._UrlServer);
            return this.http.get(this._UrlServer)
                .map(this.extractData)
                .catch(this.handleError);
        } else {
            console.log('get data from ' + this._Url_NoServer);
            return this.http.get(this._Url_NoServer)
                .map(this.extractData)
                .catch(this.handleError);
        }
    }

    // Todo-g - What should we return from the postChanges call?
    // We need to handle errors.
    postChanges(asrtext : AsrText): Observable<any> {
        // if data from memory, don't post it back.
        if (this.isDataFromMemory) {
            return Observable.of(this.testData);

        // If the asp.net server is not running, don't post
        } else if (!this.isServerRunning) {
            return Observable.of(this.testData);
        }
        console.log('postChanges in fixasr.service  ' + this._UrlServer);
        return this.postData(this._UrlServer, asrtext);
    }

    private postData(url: string, asrtext: AsrText): Observable<any> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        console.log('postData in fixasr.service');
        //return this.http.post(url, asrtext, headers) // this was there before
        return this.http.post(url, asrtext, options)
        .map(res => console.info(res))
        .catch(this.handleError);
    }

    private extractData(res: Response) {
        if (res.status < 200 || res.status >= 300) {
        throw new Error('Bad response status: ' + res.status);
        }
        let body = res.json();
        console.log('extractData: ' + body);
        return body || { };
    }

    private handleError (error: any) {
        // In a real world app, we might send the error to remote logging infrastructure
        console.log('fixasr.service handleError ' + error);
        let errMsg = error.message || 'Server error';
        console.error(errMsg); // log to console instead
        return Observable.throw(errMsg);
    }
}

