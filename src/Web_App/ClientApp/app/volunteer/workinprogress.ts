import { WorkItem } from './workitem';
import { Meeting } from '../models/meeting'
import { Volunteer } from './volunteer';
import { Location } from '../models/location';

export class WorkInProgress {
    locationId: number;
    meeting: Meeting;
    workitems: WorkItem[];
    volunteers: Volunteer[];
    locations: Location[];
}