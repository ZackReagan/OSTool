using System;
using System.Linq;
using System.Management;

namespace OSTool.CoreExtension
{
    public class ManagementObject
    {
        public static int Query(string query, string property)
        {
            return new ManagementObjectSearcher(query).Get().Cast<ManagementBaseObject>().Sum(x => Convert.ToInt32(x.Properties[property].Value));
        }
    }
}