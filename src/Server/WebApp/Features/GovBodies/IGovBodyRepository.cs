using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Govmeeting.Backend.Model;

namespace WebApp.Models
{
    public interface IGovBodyRepository
    {
        GovernmentBody Get(long govBodyId);
    }
}
