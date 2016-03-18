/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />

/**
 * <summary>
 *  An utilities.
 * </summary>
**/
module Utilities {

    /**
     * <summary>
     *  Interface for user choice server.
     * </summary>
    **/
    export interface IUserChoiceSrv {
        getSpeaker(): string;
        setSpeaker(speaker: string): void;
        getTopic(): string;
        setTopic(topic: string): void;
    }

    /**
     * <summary>
     *  A user choice server.
     * </summary>
    **/
    export class UserChoiceSrv {

        _speaker : string;
        _topic: string;

        constructor() {
        }

        /**
         * <summary>
         *  Gets the speaker.
         * </summary>
         * <returns>
         *  The speaker.
         * </returns>
        **/
        getSpeaker() : string {
            return this._speaker;
        }

        /**
         * <summary>
         *  Sets a speaker.
         * </summary>
         * <param name="speaker">   The speaker. </param>
         * <returns>
         *  .
         * </returns>
        **/
        setSpeaker(speaker : string) {
            this._speaker = speaker;
        }

        /**
         * <summary>
         *  Gets the topic.
         * </summary>
         * <returns>
         *  The topic.
         * </returns>
        **/
        getTopic() : string {
            return this._topic;
        }

        /**
         * <summary>
         *  Sets a topic.
         * </summary>
         * <param name="topic"> The topic. </param>
         * <returns>
         *  .
         * </returns>
        **/
        setTopic(topic: string) {
            this._topic = topic;
        }
    }
}

// The above Typescript is the equivilent of the Javascript
// below, which had been written prior to this.

//(function () {

//	angular.module('utilities')
//	.factory('UserChoiceSrv',
//		userChoiceServer);

//	function userChoiceServer() {
//		var _speaker;
//		var _topic;
//		var service = {
//			getSpeaker: function () {
//				return _speaker;
//			},
//			setSpeaker: function (speaker) {
//				_speaker = speaker;
//			},
//			getTopic: function () {
//				return _topic;
//			},
//			setTopic: function (topic) {
//				_topic = topic;
//			}
//		};
//		return service;
//}

//})();