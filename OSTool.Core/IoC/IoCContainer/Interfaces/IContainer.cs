namespace OSTool.Core
{
    public interface IContainer
    {
        #region Non-Binding Interfaces

        void RegisterInstanceType<T>()
            where T : class;

        void RegisterSingleType<T>()
            where T : class;

        #endregion

        #region Binding Interfaces

        void RegisterInstanceType<T, V>()
            where T : class
            where V : class;

        void RegisterSingleType<T, V>()
            where T : class
            where V : class;

        #endregion

        T Resolve<T>();
    }
}