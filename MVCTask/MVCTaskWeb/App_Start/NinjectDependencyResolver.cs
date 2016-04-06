using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using MVCTaskModel.UnitOfWork;
using Ninject;

namespace MVCTask.App_Start
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            _kernel.Bind<IMapper>()
                .ToMethod(x => GameStoreMapperConfig.CreateMapper())
                .Named("GameStoreMapper");
        }
    }
}