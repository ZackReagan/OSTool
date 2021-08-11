using System;

namespace OSTool.Core
{
    internal enum RegType
    {
        Instance,
        Singleton
    };

    internal class RegistrationModel
    {
        internal Type ObjectType { get; set; }
        internal RegType RegType { get; set; }
    }
}