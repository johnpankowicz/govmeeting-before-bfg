export class WorkItem {
    workItemId: number; // unique ID for this work item.
    partId: number;  // part number within the meeting. 
           // If a meeting is split into 8 work items, this is a number from 1 to 8.
    isDone: boolean;  // Is this work item finished.
    volunteerId: number;  // ID of the volunteer who is working on this item.
    lastEdit: number;
    lastEditTime: string;
    length: number;
}