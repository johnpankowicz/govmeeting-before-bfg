using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GM.WebApp.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GM.WebApp.Features.Viewmeetings
{
    [Route("api/[controller]")]
    public class ViewMeetingController : Controller
    {
        public IViewMeetingRepository meetings { get; set; }

        public ViewMeetingController(IViewMeetingRepository meetings)
        {
            this.meetings = meetings;
        }

        [HttpGet("{meetingId}")]
        public MeetingView Get(int meetingId)
        {
            MeetingView ret = meetings.Get(meetingId);
            return ret;
        }
    }
}
