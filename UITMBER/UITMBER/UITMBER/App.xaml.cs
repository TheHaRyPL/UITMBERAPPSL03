using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UITMBER.Services;
using UITMBER.Views;
using UITMBER.Services.Request;
using UITMBER.Services.Authentication;
using UITMBER.Services.Car;
using UITMBER.Services.Register;
using UITMBER.Services.Location;


using UITMBER.Services.UserFavouriteLocation;



using UITMBER.Services.Application;


using UITMBER.Services.Orders;
using UITMBER.Services.Clients;
using UITMBER.Services.Drivers;

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

        protected override async void OnStart()
        {
            if(Settings.TokenExpire < DateTime.Now)
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }


        }

        protected override void OnSleep()
        {
        }

        protected override async void OnResume()
        {
            if (Settings.TokenExpire < DateTime.Now)
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
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

            DependencyService.Register<IRegisterService, RegisterService>();
            DependencyService.Register<ILocationService, LocationService>();



            DependencyService.Register<IClinetsService, ClientsService>();


            DependencyService.Register<IDriversService, DriversService>();

            



        }



    }
}
