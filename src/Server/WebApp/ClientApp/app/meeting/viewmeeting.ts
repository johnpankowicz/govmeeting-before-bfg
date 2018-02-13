//import { WorkItem } from './workitem';
//import { Meeting } from '../models/meeting'
//import { Volunteer } from './volunteer';
//import { Location } from '../models/location';

export class ViewMeeting {
    meetingInfo: MeetingInfo;
    topicNames: string[];
    speakerNames: string[];
    topicDiscussions: TopicDiscussion[];
}

export class MeetingInfo {
    name: string;
    date: string;
}

export class TopicDiscussion {
    name: string;               // change field name to "topicName".
    speakers: Contribution[];   // change field name to "contributions".
}

export class Contribution {
    name: string;              // change field name to "speaker".
    text: string;
}