import {Component, Input, Output, EventEmitter, OnInit} from 'angular2/core'
import {TopicsService} from './topics.service'
import { HTTP_PROVIDERS } from 'angular2/http';

@Component({
    selector: 'topics',
    templateUrl: 'app/topics/topics.component.html',
    providers: [TopicsService]
})
export class TopicsComponent implements OnInit {
    topics: string[];
    errorMessage: string;
    @Input() newTopicName: string;
    
    @Output() topicSelect: EventEmitter<string>;
    @Output() topicEnter: EventEmitter<string>;
    
    constructor(private _topicsService: TopicsService) {
        this.topicSelect = new EventEmitter<string>();
        this.topicEnter = new EventEmitter<string>();
        this.newTopicName = null;
    }
    
    // TODO - We should only get the topics once. This code causes us to 
    // get them every time we create a TopicsComponent, which is for every TalkComponent.
    
    
    // The following would bypass reading from a file and get the list in memory.
    ngOnInit() {this.topics = this._topicsService.getTopics();}
    //ngOnInit() {this.getTopics();}
    
    getTopics() {
        this._topicsService.getTopicsFromFile()
        .subscribe(
        topics => this.topics = topics,
        error => this.errorMessage = <any>error);
    }  
    
    // When the user select a topic, we raise the "topicSelect" event.
    // The parent component (TalksComponent) can then capture this event.
    OnChange(newValue: string){
        this.topicSelect.emit(newValue);
        console.log("topic.component--topicSelect" + newValue)
    }
    
    // When the user enters a new topic, we again raise the "topicSelect" event.
    // We get the new topic name from the bound "newTopicName" property.
    OnEnter(){
        this.topicSelect.emit(this.newTopicName);
        console.log("topic.component--topicSelect " + this.newTopicName)
    }    
}