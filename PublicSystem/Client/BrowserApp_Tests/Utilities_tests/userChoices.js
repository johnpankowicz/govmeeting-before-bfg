var Utilities;
(function (Utilities) {
    var UserChoiceSrv = (function () {
        function UserChoiceSrv() {
        }
        UserChoiceSrv.prototype.getSpeaker = function () {
            return this._speaker;
        };
        UserChoiceSrv.prototype.setSpeaker = function (speaker) {
            this._speaker = speaker;
        };
        UserChoiceSrv.prototype.getTopic = function () {
            return this._topic;
        };
        UserChoiceSrv.prototype.setTopic = function (topic) {
            this._topic = topic;
        };
        return UserChoiceSrv;
    })();
    Utilities.UserChoiceSrv = UserChoiceSrv;
})(Utilities || (Utilities = {}));
//# sourceMappingURL=userChoices.js.map