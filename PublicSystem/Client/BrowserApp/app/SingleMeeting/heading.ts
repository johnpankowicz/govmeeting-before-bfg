/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />

/**
 * <summary>
 *  A single meeting.
 * </summary>
**/
module SingleMeeting {

    /**
     * <summary>
     *  AngularJS controller for displaying the meeting header.
     * </summary>
    **/
    export class headingCtrl {

        _backEnd: Utilities.IBackEnd;

        static $inject = ['BackEndSrv'];

        /**
         * <summary>
         *  Constructor.
         * </summary>
         * <param name="backEnd">   The back end service. </param>
        **/
        constructor(backEnd: Utilities.IBackEnd) {
            this._backEnd = backEnd;
            this._backEnd.getMeetingInfo(this);
        }
    }
}
