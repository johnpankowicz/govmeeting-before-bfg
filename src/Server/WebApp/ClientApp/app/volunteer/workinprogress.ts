import { WorkItem } from './workitem';
import { Meeting } from '../shared/models/meeting'
import { Volunteer } from './volunteer';
import { Location } from '../shared/models/location';

export class WorkInProgress {
    locationId: number;
    meeting: Meeting;
    workitems: WorkItem[];
    volunteers: Volunteer[];
    locations: Location[];
}