using System;
using System.Collections.Generic;
using System.Linq;
using GM.DatabaseModel;

namespace GM.DatabaseRepositories
{
    public class GovBodyRepository : IGovBodyRepository
    {
        public GovernmentBody Get(long locationId)
        {
            GovernmentBody location = GetTestMeeting(locationId);
            return location;
        }

        //////////////////////////////////////////////////
        // Use list as test/development repository.
        // We will normally be accessing the database.

        private GovernmentBody GetTestMeeting(long govBodyId)
        {
            GovernmentBody govBody = testGovBodies.Single(m => m.Id == govBodyId);
            return govBody;
        }

        private List<GovernmentBody> testGovBodies = new List<GovernmentBody>
        {
            new GovernmentBody()
            {
                Id = 1,
                Name = "Selectmen",
                Country = "USA",
                State = "ME",
                County = "LincolnCounty",
                Municipality = "BoothbayHarbor",
                Languages = new List<Language> {new Language {Id = 1, Name = "en"} }
            },
            new GovernmentBody()
            {
                Id = 2,
                Name = "CityCouncil",
                Country = "USA",
                State = "PA",
                County = "Philadelphia",
                Municipality = "Philadelphia",
                Languages = new List<Language> {new Language {Id = 1, Name = "en"} }
            }
        };

    }
}
