using Autofac;
using PigVisionMobile.Services;
using System;
using System.Globalization;
using System.Reflection;
using PigVisionMobile.Core.Services.Catalog;
using PigVisionMobile.Core.Services.OpenUrl;
using PigVisionMobile.Core.Services.RequestProvider;
using PigVisionMobile.Core.Services.Basket;
using PigVisionMobile.Core.Services.Identity;
using PigVisionMobile.Core.Services.Order;
using PigVisionMobile.Core.Services.User;
using Xamarin.Forms;
using PigVisionMobile.Core.Services.Location;
using PigVisionMobile.Core.Services.Marketing;
using PigVisionMobile.Core.Services.PigVision.Company;
using PigVisionMobile.Core.ViewModels.PigVision;
using PigVisionMobile.Core.Services.PigVision.Sow;

namespace PigVisionMobile.Core.ViewModels.Base
{
    public static class ViewModelLocator
    {
		private static IContainer _container;

		public static readonly BindableProperty AutoWireViewModelProperty =
			BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

		public static bool GetAutoWireViewModel(BindableObject bindable)
		{
			return (bool)bindable.GetValue(ViewModelLocator.AutoWireViewModelProperty);
		}

		public static void SetAutoWireViewModel(BindableObject bindable, bool value)
		{
			bindable.SetValue(ViewModelLocator.AutoWireViewModelProperty, value);
		}

		public static bool UseMockService { get; set; }

        public static void RegisterDependencies(bool useMockServices)
		{
			var builder = new ContainerBuilder();

			// View models
			builder.RegisterType<BasketViewModel>();
			builder.RegisterType<CatalogViewModel>();
			builder.RegisterType<CheckoutViewModel>();
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<MainViewModel>();
			builder.RegisterType<OrderDetailViewModel>();
			builder.RegisterType<ProfileViewModel>();
			builder.RegisterType<SettingsViewModel>();
		    builder.RegisterType<CampaignViewModel>();
		    builder.RegisterType<CampaignDetailsViewModel>();

            // PigVision ViewModels
            builder.RegisterType<PigVisionMobile.Core.ViewModels.PigVision.LoginViewModel>();
            builder.RegisterType<PigVisionMobile.Core.ViewModels.PigVision.MainViewModel>();
            builder.RegisterType<PigVisionMobile.Core.ViewModels.PigVision.CompanyViewModel>();
            builder.RegisterType<PigVisionMobile.Core.ViewModels.PigVision.SowListViewModel>();
            builder.RegisterType<PigVisionMobile.Core.ViewModels.PigVision.SowProfileViewModel>();
            builder.RegisterType<PigVisionMobile.Core.ViewModels.PigVision.SowActionViewModel>();
            builder.RegisterType<PigVisionMobile.Core.ViewModels.PigVision.SowDetailsViewModel>();
            builder.RegisterType<PigVisionMobile.Core.ViewModels.PigVision.SettingsViewModel>();


            // Services
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
			builder.RegisterType<DialogService>().As<IDialogService>();
			builder.RegisterType<OpenUrlService>().As<IOpenUrlService>();
			builder.RegisterType<IdentityService>().As<IIdentityService>();
			builder.RegisterType<RequestProvider>().As<IRequestProvider>();
            builder.RegisterType<LocationService>().As<ILocationService>().SingleInstance();

            if (useMockServices)
			{
				builder.RegisterInstance(new CatalogMockService()).As<ICatalogService>();
				builder.RegisterInstance(new BasketMockService()).As<IBasketService>();
				builder.RegisterInstance(new OrderMockService()).As<IOrderService>();
				builder.RegisterInstance(new UserMockService()).As<IUserService>();
			    builder.RegisterInstance(new CampaignMockService()).As<ICampaignService>();

                // PigVision Mock Services
                builder.RegisterInstance(new CompanyMockService()).As<ICompanyService>();
                builder.RegisterInstance(new SowMockService()).As<ISowService>();

                UseMockService = true;
			}
			else
			{
				builder.RegisterType<CatalogService>().As<ICatalogService>().SingleInstance();
				builder.RegisterType<BasketService>().As<IBasketService>().SingleInstance();
				builder.RegisterType<OrderService>().As<IOrderService>().SingleInstance();
				builder.RegisterType<UserService>().As<IUserService>().SingleInstance();
			    builder.RegisterType<CampaignService>().As<ICampaignService>().SingleInstance();

                // PigVision Mock Services
                builder.RegisterType<CompanyService>().As<ICompanyService>().SingleInstance();
                builder.RegisterType<SowService>().As<ISowService>().SingleInstance();

                UseMockService = false;
			}

			if (_container != null)
			{
				_container.Dispose();
			}
			_container = builder.Build();
		}

		public static T Resolve<T>()
		{
			return _container.Resolve<T>();
		}

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
		{
            try
            {
                var view = bindable as Element;
                if (view == null)
                {
                    return;
                }

                var viewType = view.GetType();
                var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

                var viewModelType = Type.GetType(viewModelName);
                if (viewModelType == null)
                {
                    return;
                }
                var viewModel = _container.Resolve(viewModelType);
                view.BindingContext = viewModel;
            }
            catch(Exception ex)
            {
                throw ex;
            }
		}
	}
}