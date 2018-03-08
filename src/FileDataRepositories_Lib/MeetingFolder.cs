using System;
using System.Collections.Generic;
using System.Text;
using GM.DatabaseModel;
using GM.DatabaseRepositories;

namespace GM.FileDataRepositories
{
    // Work folders under Datafiles are named as follows:
    //    <country>_<state>_<county>_<town-or-city>_<gov-entity>_<language>/<date>
    // Example:
    //     "USA", "PA", "Philadelphia", "Philadelphia", "CityCouncil", "en", "2016-03-17"
    // uses this folder:
    //     "Datafiles/USA_PA_Philadelphia_Philadelphia_CityCouncil_en/2016-03-17"
    //

    public class MeetingFolder
    {
        IMeetingRepository _meetingRepository;
        IGovBodyRepository _govBodyRepository;

        public MeetingFolder(
            IMeetingRepository meetingRepository,
            IGovBodyRepository govBodyRepository
        )
        {
            _meetingRepository = meetingRepository;
            _govBodyRepository = govBodyRepository;
        }

        public string Get(long meetingId)
        {
            Meeting meeting = _meetingRepository.Get(meetingId);
            GovernmentBody govBody = _govBodyRepository.Get(meeting.GovernmentBodyId);

            string date = string.Format("{0:yyyy-MM-dd}", meeting.Date);
            string path = govBody.Country + "_" + govBody.State + "_" + govBody.County + "_" + govBody.Municipality +
                "_" + govBody.Name + "_" + govBody.Languages[0].Name + "\\" + date;

            return path;
        }

    }
}
