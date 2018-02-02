using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    /* This is the repository for the transcripts which are being
     * edited. The edits mostly involve adding tags to the "tags".
     * The tags represent:
     *  Topic   - that the topic of discussion is changing
     *  Section - that the meeting section is changing
     *  Bill    - that a specific bill is now being discussed
     */
    public interface IAddtagsRepository
    {
        Addtags Get(string username, string country, string state, string county, string city, string govEntity, string language, string meetingDate);

        bool Put(Addtags value, string username, string country, string state, string county, string city, string govEntity, string language, string meetingDate);
    }
}
