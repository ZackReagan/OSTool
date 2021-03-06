using System;
using System.Collections.Generic;

namespace OSTool.Core
{
    internal class SingleCreationService
    {
        static SingleCreationService instance = null;
        static Dictionary<string, object> objectPool = new Dictionary<string, object>();

        static SingleCreationService()
        {
            instance = new SingleCreationService();
        }

        public static SingleCreationService GetInstance()
        {
            return instance;
        }

        public object GetSingleton(Type t)
        {
            object obj;

            try
            {
                if (objectPool.ContainsKey(t.Name) == false)
                {
                    obj = InstanceCreationService.GetInstance().GetNewObject(t);
                    objectPool.Add(t.Name, obj);
                }
                else
                {
                    obj = objectPool[t.Name];
                }
            }
            catch
            {
                throw new ApplicationException("Single creation failed.");
            }

            return obj;
        }
    }
}