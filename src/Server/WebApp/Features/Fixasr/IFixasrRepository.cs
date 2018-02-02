using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    /* This is the repository for the Automatic Speech Recogniton transcripts which are being
     * corrected for errors.
     */
    public interface IFixasrRepository
    {
        Fixasr Get(string username, string country, string state, string county, string city, string govEntity, string language, string meetingDate, int part);

        bool Put(Fixasr value, string username, string country, string state, string county, string city, string govEntity, string language, string meetingDate, int part);
    }
}
