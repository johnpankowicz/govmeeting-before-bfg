using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GM.DatabaseModel;

namespace GM.DatabaseRepositories
{
    public interface IMeetingRepository
    {
        Meeting Get(long meetingId);
        string GetMeetingFolder(long meetingId);
    }
}
