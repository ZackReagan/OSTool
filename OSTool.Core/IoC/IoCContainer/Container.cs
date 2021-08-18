using System;
using System.Collections.Generic;

namespace OSTool.Core
{
    public class Container : IContainer
    {
        Dictionary<Type, RegistrationModel> instanceRegistry = new Dictionary<Type, RegistrationModel>();

        #region Non-Binding Interfaces

        public void RegisterInstanceType<T>()
            where T : class
        {
            Register<T, T>(RegisterType.Singleton);
        }

        public void RegisterSingleType<T>()
            where T : class
        {
            Register<T, T>(RegisterType.Singleton);
        }

        #endregion

        #region Binding Interfaces

        public void RegisterInstanceType<T, V>()
            where T : class
            where V : class
        {
            Register<T, V>(RegisterType.Singleton);
        }

        public void RegisterSingleType<T, V>()
            where T : class
            where V : class
        {
            Register<T, V>(RegisterType.Singleton);
        }

        #endregion

        private void Register<T, V>(RegisterType type)
        {
            if (instanceRegistry.ContainsKey(typeof(T)) == true)
            {
                instanceRegistry.Remove(typeof(T));
            }

            instanceRegistry.Add(typeof(T), new RegistrationModel
            {
                RegType = type,
                ObjectType = typeof(V)
            });
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        private object Resolve(Type t)
        {
            object obj = null;

            if (instanceRegistry.ContainsKey(t) == true)
            {
                RegistrationModel model = instanceRegistry[t];

                if (model != null)
                {
                    obj = CreateInstance(model);
                }
            }

            return obj;
        }

        private object CreateInstance(RegistrationModel model)
        {
            object returnedObj;
            Type typeToCreate = model.ObjectType;

            if (model.RegType == RegisterType.Instance)
            {
                returnedObj = InstanceCreationService.GetInstance().GetNewObject(typeToCreate);
            }
            else
            {
                returnedObj = SingleCreationService.GetInstance().GetSingleton(typeToCreate);
            }

            return returnedObj;
        }
    }
}