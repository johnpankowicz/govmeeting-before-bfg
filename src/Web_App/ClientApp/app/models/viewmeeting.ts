//import { WorkItem } from './workitem';
import { Meeting } from './meeting'
//import { Volunteer } from './volunteer';
//import { Location } from '../models/location';

export class ViewMeeting {
    //meetingInfo: MeetingInfo;
    meeting: Meeting;
    topicNames: string[];
    speakerNames: string[];
    topicDiscussions: TopicDiscussion[];
}

export class TopicDiscussion {
    name: string;               // change field name to "topicName".
    talks: Talk[];
}

export class Talk {
    Speaker: string;
    Text: string;
}