using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OSTool.Core
{
    public class Container : IContainer
    {
        Dictionary<Type, RegistrationModel> instanceRegistry = new Dictionary<Type, RegistrationModel>();

        public void RegisterInstanceType<T>()
            where T : class
        {
            RegisterType<T>(RegType.Instance);
        }

        public void RegisterSingletonType<T>()
            where T : class
        {
            RegisterType<T>(RegType.Singleton);
        }

        private void RegisterType<T>(RegType type)
        {
            if (instanceRegistry.ContainsKey(typeof(T)) == true)
            {
                instanceRegistry.Remove(typeof(T));
            }

            instanceRegistry.Add(
                typeof(T),
                    new RegistrationModel
                    {
                        RegType = type,
                        ObjectType = typeof(T)
                    }
                );
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
                    Type typeToCreate = model.ObjectType;

                    ConstructorInfo[] consInfo = typeToCreate.GetConstructors();

                    var dependentCtor = consInfo.FirstOrDefault(item => item.CustomAttributes.FirstOrDefault(att => att.AttributeType == typeof(TinyDependencyAttribute)) != null);

                    if(dependentCtor == null)
                    {
                        obj = CreateInstance(model);
                    }
                    else
                    {
                        ParameterInfo[] parameters = dependentCtor.GetParameters();
                        if (parameters.Count() == 0)
                        {
                            obj = CreateInstance(model);
                        }
                        else
                        {
                            List<object> arguments = new List<object>();
                            foreach (var param in parameters)
                            {
                                Type type = param.ParameterType;
                                arguments.Add(Resolve(type));
                            }

                            obj = CreateInstance(model, arguments.ToArray());
                        }
                    }
                }
            }

            return obj;
        }

        private object CreateInstance(RegistrationModel model, object[] arguments = null)
        {
            object returnedObj = null;
            Type typeToCreate = model.ObjectType;

            if (model.RegType == RegType.Instance)
            {
                returnedObj = InstanceCreationService.GetInstance().GetNewObject(typeToCreate, arguments);
            }
            else if (model.RegType == RegType.Singleton)
            {
                returnedObj = SingleCreationService.GetInstance().GetSingleton(typeToCreate, arguments);
            }

            return returnedObj;
        }
    }
}