using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GM.FileDataModel;


namespace GM.FileDataRepositories
{
    public interface IViewMeetingRepository
    {
        ViewmeetingView Get(long meetingId);
    }
}
