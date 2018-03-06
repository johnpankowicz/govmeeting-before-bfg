using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GM.FileDataModel;


namespace GM.FileDataRepositories
{
    public interface IViewMeetingRepository
    {
        MeetingView Get(long meetingId);
    }
}
