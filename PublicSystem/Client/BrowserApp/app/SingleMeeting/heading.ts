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

        meetingInfo: { name: string; date: string};
        _backEnd: IBackEnd;

        static $inject = ["BackEndSrv"];

        /**
         * <summary>
         *  Constructor.
         * </summary>
         * <param name="backEnd">   The back end service. </param>
        **/
        constructor(backEnd : IBackEnd) {
            this._backEnd = backEnd;
            this._backEnd.getMeetingInfo(this);
        }
    }
}
