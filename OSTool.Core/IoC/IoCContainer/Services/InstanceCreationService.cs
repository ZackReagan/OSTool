using System;

namespace OSTool.Core
{
    internal class InstanceCreationService
    {
        static InstanceCreationService instance = null;

        static InstanceCreationService()
        {
            instance = new InstanceCreationService();
        }

        public static InstanceCreationService GetInstance()
        {
            return instance;
        }

        public object GetNewObject(Type t)
        {
            object obj;

            try
            {
                obj = Activator.CreateInstance(t);
            }
            catch 
            {
                throw new ApplicationException("Instance creation failed.");
            }

            return obj;
        }
    }
}