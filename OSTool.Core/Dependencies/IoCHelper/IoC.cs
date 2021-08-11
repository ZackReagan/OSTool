﻿namespace OSTool.Core
{
    public static class IoC
    {
        public static IContainer IoCContainer = new Container();

        public static void Setup()
        {
            IoCContainer.RegisterSingletonType<ApplicationViewModel>();
            XmlFile.xmlCreate();
        }

        public static T Get<T>()
        {
            return IoCContainer.Resolve<T>();
        }
    }
}