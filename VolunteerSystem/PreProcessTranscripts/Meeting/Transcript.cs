using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBO
{
    public class Transcript
    {
        Meeting meeting;
    }
    public class Meeting
    {
        string name;
        string location;
        DateTime date;
        Officer[] officers;
    }
    public class Officer
    {
        string name;
        string title;
    }
    public class Topic
    {
        string name;
        Speaker[] speakers;
    }
    public class Speaker
    {
        
    }
}
