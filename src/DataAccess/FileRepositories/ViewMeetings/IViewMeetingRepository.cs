using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GM.DataAccess.FileDataModel;


namespace GM.DataAccess.FileRepositories
{
    public interface IViewMeetingRepository
    {
        MeetingView Get(long meetingId);
    }
}
