using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;

using BusinessLayer.DbInterfaces;

using BusinessLayer.MockDbs;
using BusinessLayer.DbImp;

namespace SDTracker
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }
        private void AddBindings()
        {

            ninjectKernel.Bind<IAdminDb>().To<AdminDb>();
            ninjectKernel.Bind<IEngineerDb>().To<EngineerDb>();
            ninjectKernel.Bind<IHomeDb>().To<HomeDb>();
            ninjectKernel.Bind<IProjectDb>().To<ProjectDb>();
            ninjectKernel.Bind<IReportDb>().To<ReportDb>();
            ninjectKernel.Bind<IUserDb>().To<UserDb>();
            

        }
    }
}