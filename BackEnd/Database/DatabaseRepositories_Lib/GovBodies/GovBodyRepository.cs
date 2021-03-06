﻿using System;
using System.Collections.Generic;
using System.Linq;
using GM.DatabaseAccess;
using GM.DatabaseModel;

namespace GM.DatabaseRepositories
{
    public class GovBodyRepository : IGovBodyRepository
    {
        dBOperations dBOperations;

        public GovBodyRepository(dBOperations _dBOperations)
        {
            dBOperations = _dBOperations;
        }

        public GovernmentBody Get(long governmentBodyId)
        {
            GovernmentBody govBody = dBOperations.GetGovernmentBody(governmentBodyId);
            return govBody;
        }
        public long GetId(string country, string state, string county, string municipality)
        {
            // TODO - implement - return ID of body based on country, state, county & municipality.
            return -1;
        }
    }
}
