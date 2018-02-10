import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';
import { catchError } from 'rxjs/operators';
import { ViewMeeting } from './viewmeeting';
//import { MeetingService } from './imeeting.service';

@Injectable()
export class MeetingServiceStub {
    constructor() {
      console.log('MeetingService stub constructor');
    }

    public getMeeting(): Observable<ViewMeeting> {
        console.log('getMeeting from memory');
        return Observable.of(this.viewMeeting);
    }

    private viewMeeting: ViewMeeting = {
        "meetingInfo": {
            "name": "Boothbay Harbor Selectmen meeting",
            "date": "Sept. 8, 2014"
        },
        "topicNames": [
            "SHOW ALL",
            "Introductions",
            "Anouncements",
            "Treasurer Report",
            "Prior Minutes",
            "Public Works",
            "Wharves & Wiers",
            "Public Restrooms",
            "Music",
            "Executive Session"
        ],
        "speakerNames": [
            "SHOW ALL",
            "Denise Griffin",
            "Jay Warren",
            "Wendy Wolf",
            "Russell Hoffman",
            "William Hamblen",
            "Thomas Woodin",
            "Kellie Bigos",
            "Julia Latter",
            "Man 1",
            "Mike"
        ],
        "topicDiscussions": [
            {
                "name": "Introductions", "speakers": [
                    { "name": "Denise Griffin", "text": "Meeting to order. Today is Monday, Sept. 8th. Welcome to the selectmen’s meeting, the first meeting in September. On my left is Kellie Bigos, Recording Secretary and Tom Woodin, our town manager, selectmens Jay Warren, Russ Hoffman, myself Denise Griffin, Will Hamblen and Wendy Wolf. And in the audience, we have our financial treasurer, Julia Latter. Okay, Tom, town manager announcements?" }
                ]
            },
            {
                "name": "Anouncements", "speakers": [
                    { "name": "Thomas Woodin", "text": "Thank You. The only one I have right off is just that Bike Maine is coming up. As of Wednesday morning there will be support vehicles showing up with all their gear. The riders will start to come into town between 1 and 4 on Wednesday. There is about 300 riders coming into town and about 50 volunteers. And they’ll be staying through Thursday. There is going to be a lobster bake for them at the footbridge parking lot at 5 to 7. After that there’s live music at the Opera House, that’s open to the public. The next day, they wake up, they have a full breakfast at the elementary school and then they start to pack up and ride on to the next destination which is Bath. So you will be seeing a lot of cyclists coming Wednesday afternoon. That’s all I’ve got." }
                ]
            },
            {
                "name": "Treasurer Report", "speakers": [
                    { "name": "Denise Griffin", "text": "Good. Julia, financials." },
                    { "name": "Julia Latter", "text": "?? Total revenue is 82 million, ?? thousand, 868 dollars and 75 cents. Total expenses year to date are ??? Does anyone have any questions on ?? ?? ??? You do not have your graph sheet again. ??? issues with scheduling. Hopefully, ??? copies for you guys before the next selectmen’s meeting.?? presentation then. They assured me they will be bringing the ?? accounts payable is 296 thousand, 156 dollars and 32 cents. And the bank balance is 2 million, 631 thousand, 175 dollars and 95 cents.   (Julia Latter was not using a mic.)" },
                    { "name": "Denise Griffin", "text": "Questions?" },
                    { "name": "Wendy Wolf", "text": "Julia, maybe you can tell me: what’s the last time we competed the audit?" },
                    { "name": "Julia Latter", "text": "Completed it?" },
                    { "name": "Wendy Wolf", "text": "No, competed it for change of auditor or new audit?" },
                    { "name": "Man 1", "text": "This is my fifth year so it’s been at least 5 years." },
                    { "name": "Julia Latter", "text": "Yeah, so it’s been about 5 years." },
                    { "name": "Wendy Wolf:", "text": "Yeah, that’s something we should look at. That’s a long time to have the same audit firm." },
                    { "name": "Man 1", "text": "Yeah, we plan to do that anyway for the next audit season. There was a change in personnel at the audit company. It was late in the process so we didn’t have time to requite it out, so we will do that for the next one." },
                    { "name": "Wendy Wolf", "text": "OK, great." },
                    { "name": "Denise Griffin", "text": "You’re thinking that every 5 years, we should put it out for bid?" },
                    { "name": "Wendy Wolf", "text": "Yes" },
                    { "name": "Julia Latter", "text": "??" }
                ]
            },
        ]
    }
}

