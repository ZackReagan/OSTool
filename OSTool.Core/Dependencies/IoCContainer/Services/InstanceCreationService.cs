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

        private InstanceCreationService()
        { }

        public static InstanceCreationService GetInstance()
        {
            return instance;
        }

        public object GetNewObject(Type t, object[] arguments = null)
        {
            object obj;

            try
            {
                obj = Activator.CreateInstance(t, arguments);
            }
            catch 
            {
                throw new ApplicationException("Instance creation failed.");
            }

            return obj;
        }
    }
}