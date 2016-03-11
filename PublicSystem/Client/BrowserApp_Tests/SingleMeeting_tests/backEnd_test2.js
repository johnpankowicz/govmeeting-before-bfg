/// <reference path="..\Scripts\jasmine.js" />
/// <reference path="..\Scripts\angular.js" />
/// <reference path="..\scripts\angular-mocks.js" />
/// <reference path="..\..\Client\SingleMeeting\backEnd.js" />
/// <reference path="..\..\Client\SingleMeeting\heading.js" />
/// <reference path="..\..\Client\SingleMeeting\browseMeeting.js" />
/// <reference path="..\..\Client\SingleMeeting\meetingOptions.js" />
/// <reference path="..\..\Client\Utilities\utilities.js" />
/// <reference path="..\..\Client\SingleMeeting\singleMeeting.js" />

// A stripped down test that I am trying to have Jamine run.
describe('SingleMeeting', function () {

    beforeEach(module('singleMeeting'));

    describe('math_', function () {

        it('1+1=2', function () {
            expect(1 + 1).toEqual(2);
        });

        it('1+2=3', function () {
            expect(1 + 2).toEqual(3);
        });

    });
});

/*
describe('Client_SingleMeeting_', function () {
    // beforeEach(module('SingleMeeting'));
    beforeEach(module('SingleMeeting', ['utilities']));
    // beforeEach(module('SingleMeeting', 'utilities'));
    // beforeEach(module('utilities'));

    describe('backEnd ->', function () {

        // it('getMeetingInfo should be defined', (function () {
        it('getMeetingInfo should be defined', inject(function (BackEndSrv) {
                //UserChoiceSrv.setSpeaker("John Henry");
            //expect(UserChoiceSrv.getSpeaker()).toEqual("John Henry");
            expect(1 + 1).toEqual(2);
        }));

        it('Get request for meeting data and return heading', (function () {
        // it('Get request for meeting data and return heading', inject(function (BackEndSrv) {
            //UserChoiceSrv.setTopic("Construction of ice rink");
            //expect(UserChoiceSrv.getTopic()).toEqual("Construction of ice rink");
            expect(1 + 1).toEqual(2);
        }));
    });
});
*/