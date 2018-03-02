using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Govmeeting.Backend.Model;

namespace GM.WebApp.Features.Meetings
{
    public interface IMeetingRepository
    {
        Meeting Get(long meetingId);
        string GetMeetingFolder(long meetingId);
    }
}
