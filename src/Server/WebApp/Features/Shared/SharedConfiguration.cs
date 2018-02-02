using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Features.Shared
{
    // This static class has properties that can be written only once, but can be read many time.
    // If an attempt is made to write to them again, an exception is thrown.
    // For non-static classes, we can obtain this by setting private values in the constructor 
    //  and giving just read access in the public property. But a static class does not have a constructor.
    // Though this method works, it has the disadvantage that if an invalid access is attempted, the error won't be found til run time.
    public static class SharedConfiguration
    {
        static string _DatafilesPath;
        static string _Assets;

        public static string DatafilesPath
        {
            get { return _DatafilesPath; }
            set { SetIfNotSet(ref _DatafilesPath, value); }
        }

        public static string Assets
        {
            get { return _Assets; }
            set { SetIfNotSet(ref _Assets, value); }
        }
        static void SetIfNotSet(ref string property, string value)
        {
            if (property == null)
            {
                property = value;
            }
            else
            {
                throw new InvalidOperationException("Configuration can be assigned only once");
            }
        }
    }

}
