/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />

/**
 * <summary>
 *  This code belongs to the module for handling browsing a single meeting.
 * </summary>
**/
module SingleMeeting {

    /**
     * <summary>
     *  AngularJS controller to get user's options on what to display.
     * </summary>
    **/
    export class MeetingOptionsCtrl {

        _backEnd: Utilities.IBackEnd;
        _userChoice: Utilities.IUserChoiceSrv;

        static $inject = ['BackEndSrv', 'UserChoiceSrv'];

        /**
         * <summary>
         *  MeetingOptionsCtrl Constructor.
         * </summary>
         * <param name="backEnd">       The back end service. </param>
         * <param name="userChoiceSrv"> The user choice service. </param>
        **/
        constructor(backEnd: Utilities.IBackEnd, userChoiceSrv: Utilities.IUserChoiceSrv) {
            this._backEnd = backEnd;
            this._userChoice = userChoiceSrv;

            this._backEnd.getSpeakerNames(this);
            this._backEnd.getTopicNames(this);
        }

        /**
         * <summary>
         *  My speaker.
         * </summary>
         * <param name="newSpeaker">    The new speaker. </param>
         * <returns>
         *  .
         * </returns>
        **/
        mySpeaker(newSpeaker: string) {
        	if (angular.isDefined(newSpeaker)) {
        	    this._userChoice.setSpeaker(newSpeaker);
        	}
        	return this._userChoice.getSpeaker()
        }

        /**
         * <summary>
         *  My topic.
         * </summary>
         * <param name="newTopic">  The new topic. </param>
         * <returns>
         *  .
         * </returns>
        **/
        myTopic(newTopic: string) {
        	if (angular.isDefined(newTopic)) {
        	    this._userChoice.setTopic(newTopic);
        	}
        	return this._userChoice.getTopic()
        }
    }
}
