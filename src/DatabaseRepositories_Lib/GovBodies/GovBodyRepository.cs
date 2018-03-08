using System;
using System.Collections.Generic;
using System.Linq;
using GM.DatabaseAccess;
using GM.DatabaseModel;

namespace GM.DatabaseRepositories
{
    public class GovBodyRepository : IGovBodyRepository
    {
        public GovernmentBody Get(long governmentBodyId)
        {
            dBOperations dbo = new dBOperations();
            GovernmentBody govBody = dbo.GetGovernmentBody(governmentBodyId);
            return govBody;
        }
    }
}
