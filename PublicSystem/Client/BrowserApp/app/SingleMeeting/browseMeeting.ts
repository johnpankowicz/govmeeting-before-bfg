/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />

/**
 * <summary>
 *  This code belongs to the module for handling browsing a single meeting.
 * </summary>
**/
module SingleMeeting {

    /**
     * <summary>
     *  AngularJS controller for displaying a single meeting.
     *  It will display a subset the information based on what
     *  the user has selected.
     * </summary>
    **/
    export class BrowseMeetingCtrl {

        _backEnd: IBackEndWithCtrl;
        _userChoice: Utilities.IUserChoiceSrv;

        static $inject = ['BackEndSrvWithCtrl', 'UserChoiceSrv'];

        /**
         * <summary>
         *  BrowseMeetingCtrl Constructor.
         * </summary>
         * <param name="backEnd">       The back end service. </param>
         * <param name="userChoiceSrv"> The user choice service. </param>
        **/
        constructor(backEnd: IBackEndWithCtrl, userChoiceSrv: Utilities.IUserChoiceSrv) {
            this._backEnd = backEnd;
            this._userChoice = userChoiceSrv;

            this._backEnd.getTopicDiscussions(this);

            this._userChoice.setSpeaker("SHOW ALL");
            this._userChoice.setTopic("SHOW ALL");
        }

        /**
         * <summary>
         *  Check whether to display this topic - only if it or "SHOW ALL" was selected.
         * </summary>
         * <param name="topicName"> Name of the topic. </param>
         * <returns>
         *  Returns true if this topic should be displayed
         * </returns>
        **/
        CheckShowTopic(topicName: string) {
            var _topic = this._userChoice.getTopic();
            return ((_topic == "SHOW ALL") || (_topic == topicName));
        }

        /**
         * <summary>
         *  Check whether to display this speaker - only if it or "SHOW ALL" was selected.
         * </summary>
         * <param name="speakerName">   Name of the speaker. </param>
         * <returns>
         *  Returns true if this speaker should be displayed
         * </returns>
        **/
        CheckShowSpeaker(speakerName: string) {
            var _speaker = this._userChoice.getSpeaker();
            return ((_speaker == "SHOW ALL") || (_speaker == speakerName))
        }
    }
}

