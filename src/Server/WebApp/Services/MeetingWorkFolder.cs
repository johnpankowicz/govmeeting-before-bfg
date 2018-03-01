using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Govmeeting.Backend.Model;
using Webapp.Features.Govbodies;
using GM.WebApp.Features.Meetings;


// Work folders under Datafiles are named as follows:
//    <country>_<state>_<county>_<town-or-city>_<gov-entity>_<language>/<date>
// Example:
//     "USA", "PA", "Philadelphia", "Philadelphia", "CityCouncil", "en", "2016-03-17"
// uses this folder:
//     "Datafiles/USA_PA_Philadelphia_Philadelphia_CityCouncil_en/2016-03-17"
//


namespace GM.WebApp.Services
{
    public interface IMeetingWorkFolder
    {
        string GetPath(long meetingId);
    }


    public class MeetingWorkFolder : IMeetingWorkFolder
    {
        IMeetingRepository meetingRepository;
        IGovBodyRepository govBodyRepository;

        public MeetingWorkFolder(IMeetingRepository _meetingRepository, IGovBodyRepository _locationRepository)
        {
            meetingRepository = _meetingRepository;
            govBodyRepository = _locationRepository;
        }

        public string GetPath(long meetingId)
        {
            Meeting meeting = meetingRepository.Get(meetingId);
            GovernmentBody govBody = govBodyRepository.Get(meeting.GovernmentBodyId);

            //DateTime dt = DateTime.Parse(meeting.date, null, System.Globalization.DateTimeStyles.RoundtripKind);
            string date = string.Format("{0:yyyy-MM-dd}", meeting.Date);

            string path = govBody.Country + "_" + govBody.State + "_" + govBody.County + "_" + govBody.Municipality +
                "_" + govBody.Name + "_" + govBody.Languages[0].Name + "\\" + date;

            return path;
        }
    }
}
