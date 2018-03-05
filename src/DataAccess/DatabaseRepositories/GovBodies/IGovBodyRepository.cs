using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GM.DataAccess.Model;

namespace GM.DataAccess.DatabaseRepositories
{
    public interface IGovBodyRepository
    {
        GovernmentBody Get(long govBodyId);
    }
}
