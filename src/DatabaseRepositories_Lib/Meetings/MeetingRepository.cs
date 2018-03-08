using System;
using System.Collections.Generic;
using GM.DatabaseModel;
using System.Linq;
using GM.DatabaseAccess;

namespace GM.DatabaseRepositories
{
    public class MeetingRepository : IMeetingRepository
    {
        IGovBodyRepository _govBodyRepository;

        public MeetingRepository(IGovBodyRepository govBodyRepository)
        {
            _govBodyRepository = govBodyRepository;
        }

        public Meeting Get(long meetingId)
        {
            dBOperations dbo = new dBOperations(); 
            Meeting meeting = dbo.GetMeeting(meetingId);
            return meeting;
        }
    }
}
