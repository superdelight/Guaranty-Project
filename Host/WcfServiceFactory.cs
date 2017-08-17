using Microsoft.Practices.Unity;
using Ponzi.Implementation.Contract;
using Ponzi.Implementation.Implementation;
using PonziBussinessLogic.Implementation;
using PonziBussinessLogic.Interface;
using PonziRepostiory.Implementation;
using PonziRepostiory.Interface;
using Unity.Wcf;

namespace Host
{
    public class WcfServiceFactory : UnityServiceHostFactory
    {
        protected override void ConfigureContainer(IUnityContainer container)
        {
            // register all your components with the container here
            // container


            //Repositories  and Entities
            container.RegisterType<IUnitofWork, UnitofWork>();
            container.RegisterType<NobleDBEntities>(new HierarchicalLifetimeManager());
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>));

            //Business logic
            container.RegisterType<IPonziAdminLogic, PonziAdminLogin>();

        }
    }
}  
