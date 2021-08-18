namespace OSTool.Core
{
    public static class IoC
    {
        public static IContainer IoCContainer = new Container();
        public static IDialogService Dialog => Get<IDialogService>();

        public static void Setup()
        {
            IoCContainer.RegisterSingleType<ApplicationViewModel>();
        }

        public static T Get<T>()
        {
            return IoCContainer.Resolve<T>();
        }
    }
}