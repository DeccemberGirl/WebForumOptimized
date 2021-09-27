using Ninject.Modules;

namespace WebForum
{
    /// <summary>
    /// Ninject module for registering needed dependencies for BLL
    /// </summary>
    public class BLLModule : NinjectModule
    {
        public override void Load()
        {
            Bind<UnitOfWork>().To<UnitOfWork>();
        }
    }
}
