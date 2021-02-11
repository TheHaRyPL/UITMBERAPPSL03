using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UITMBER.Services;
using UITMBER.Views;
using UITMBER.Services.Request;
using UITMBER.Services.Authentication;
using UITMBER.Services.Car;

using UITMBER.Services.UserFavouriteLocation;

using UITMBER.Services.Car;


using UITMBER.Services.Application;


using UITMBER.Services.Orders;

namespace UITMBER
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            RegisterServices();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void RegisterServices()
        {
            DependencyService.Register<MockDataStore>();

            DependencyService.Register<IRequestService, RequestService>();
            DependencyService.Register<IAuthenticationService, AuthenticationService>();
            DependencyService.Register<IOrdersServices, OrdersServices>();
            DependencyService.Register<IUserFavouriteLocationService, UserFavouriteLocationService>();
            DependencyService.Register<ICarService, CarService>();
            DependencyService.Register<IApplicationService, ApplicationService>();







        }



    }
}
