namespace OSTool.Core
{
    public interface IContainer
    {
        void RegisterInstanceType<T>()
            where T : class;

        void RegisterSingletonType<T>()
            where T : class;

        T Resolve<T>();
    }
}