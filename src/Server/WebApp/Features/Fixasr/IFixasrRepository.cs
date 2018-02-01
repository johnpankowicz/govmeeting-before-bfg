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
        //void Add(Fixasr item);
        //IEnumerable<Fixasr> GetAll();

        Fixasr Get(string username, string country, string state, string county, string city, string govEntity, string language, string meetingDate, int part);
        void Put(Fixasr value, string username, string country, string state, string county, string city, string govEntity, string language, string meetingDate, int part);
        Fixasr GetByPath(string path);
        string GetStringByPath(string path);
        void PutByPath(Fixasr value);
        //void PutByPath(string path, string value);
        Fixasr Find(string key);
        //Fixasr Remove(string key);
        void Update(Fixasr item);
        void SetAssets(string _wwwroot);
    }
}
