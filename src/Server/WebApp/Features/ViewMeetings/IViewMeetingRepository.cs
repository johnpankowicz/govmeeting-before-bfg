using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Features.Viewmeetings
{
    public interface IViewMeetingRepository
    {
        MeetingView Get(long meetingId);
    }
}
