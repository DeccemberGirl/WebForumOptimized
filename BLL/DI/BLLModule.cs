using DAL;
using Ninject.Modules;

namespace BLL.DI
{
    /// <summary>
    /// Ninject module for registering needed dependencies for BLL
    /// </summary>
    public class BLLModule : NinjectModule
    {
        /// <summary>
        /// Binds <see cref="IUnitOfWork">interface</see> to <see cref="UnitOfWork">class</see>
        /// </summary>
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}
