import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Talk } from './talk';
import { Observable } from 'rxjs/Observable';
import { Addtags } from '../addtags';

@Injectable()
export class TalksServiceStub {

    private addtags: Addtags = {
        data: [
            { speaker: 'Joe', said: 'Waz up', section: 'Invocation', topic: null, showSetTopic: false },
            { speaker: 'Mary', said: 'nutten', section: null, topic: null, showSetTopic: false },
            { speaker: 'Jo', said: 'haiyall', section: null, topic: null, showSetTopic: false },
            { speaker: 'Joe', said: '1111', section: 'Reports', topic: null, showSetTopic: false },
            { speaker: 'Mary', said: '1111111', section: null, topic: 'Topic1', showSetTopic: false },
            { speaker: 'Jo', said: '11111111', section: null, topic: null, showSetTopic: false },
            { speaker: 'Jose', said: '22', section: null, topic: null, showSetTopic: false },
            { speaker: 'Mary', said: '2222', section: null, topic: null, showSetTopic: false },
            { speaker: 'Jo', said: '2222222', section: null, topic: null, showSetTopic: false },
            { speaker: 'Joe', said: '33', section: 'Public Comment', topic: null, showSetTopic: false },
            { speaker: 'Mary', said: '33333', section: null, topic: 'Topic2', showSetTopic: false },
            { speaker: 'Jo', said: '33333333', section: null, topic: null, showSetTopic: false }
        ]
    };

    getTalks(): Observable<Addtags> {
        console.log('getAsr from memory');
        return Observable.of(this.addtags);
    }

    postChanges(addtags: Addtags): Observable<Addtags> {
        console.log('postChanges in talks.service stub');
        return Observable.of(this.addtags);
    }
}