using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Essentials;
using System.Reflection;
using UITMBER.Services.Drivers;
using UITMBER.Services.Location;
using UITMBER.Services.Authentication;
using UITMBER.Enums;
using UITMBER.Services.UserFavouriteLocation;
using UITMBER.Models.UserFavouriteLocation;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using UITMBER.Helpers;

namespace UITMBER.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        public IDriversService DriversService => DependencyService.Get<IDriversService>();
        public ILocationService LocationService => DependencyService.Get<ILocationService>();

        public IAuthenticationService AuthService => DependencyService.Get<IAuthenticationService>();

        public IUserFavouriteLocationService UserFavouriteLocationService => DependencyService.Get<IUserFavouriteLocationService>();


        public MainViewModel()
        {
            PageStatusEnum = PageStatusEnum.Default;
            OrderStateEnum = OrderStateEnum.StartPicker;

            CanCalculate = false;

            URLItems = new ObservableCollection<LocationsResponse>();

            var mapSpan = Xamarin.Forms.GoogleMaps.MapSpan.FromCenterAndRadius(new Xamarin.Forms.GoogleMaps.Position(50.043604d, 22.0261172d), Xamarin.Forms.GoogleMaps.Distance.FromKilometers(3));


            MapControl = new Xamarin.Forms.GoogleMaps.Map();

            MapControl.MoveToRegion(mapSpan);

            MapControl.MyLocationEnabled = true;

            MapControl.MapClicked += MapControl_MapClicked;

            AddMapStyle();
        }

        private async void MapControl_MapClicked(object sender, MapClickedEventArgs e)
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                if (PageStatusEnum == PageStatusEnum.Searching)
                {

                    var mapSpan = Xamarin.Forms.GoogleMaps.MapSpan.FromCenterAndRadius(
                        new Xamarin.Forms.GoogleMaps.Position(e.Point.Latitude, e.Point.Longitude),
                        Xamarin.Forms.GoogleMaps.Distance.FromKilometers(1));

                    MapControl.MoveToRegion(mapSpan);

                    //Location Geocoding
                    var locations = await Geocoding.GetPlacemarksAsync(new Location(e.Point.Latitude, e.Point.Longitude));
                    var locationDecoded = locations?.FirstOrDefault();


                    //Pobieranie lokalizacji z adresu (tekstu)
                    //var loactionsFromAddress = await Geocoding.GetLocationsAsync("Paderewskiego");
                    //var loactionFromAddress = loactionsFromAddress?.FirstOrDefault();
                    //var mapSpan = Xamarin.Forms.GoogleMaps.MapSpan.FromCenterAndRadius(
                    //   new Xamarin.Forms.GoogleMaps.Position(loactionFromAddress.Latitude, loactionFromAddress.Longitude),
                    //   Xamarin.Forms.GoogleMaps.Distance.FromKilometers(1));

                    //MapControl.MoveToRegion(mapSpan);

                    //return;

                    //Setting Pin
                    if (OrderStateEnum == OrderStateEnum.StartPicker)
                    {
                        _startLocation = e.Point;

                        var pin = MapControl.Pins.FirstOrDefault(x => x.Label == "Start");
                        if (pin != null)
                        {
                            pin.Position = new Position(_startLocation.Latitude, _startLocation.Longitude);
                        }
                        else
                        {
                            MapControl.Pins.Add(new Xamarin.Forms.GoogleMaps.Pin
                            {
                                Type = PinType.Place,
                                Position = new Position(_startLocation.Latitude, _startLocation.Longitude),
                                Label = "Start",
                                Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("ic_pickuplocation.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "ic_pickuplocation.png", WidthRequest = 25, HeightRequest = 25 }),
                                Tag = 1
                            });
                        }



                        StartText = locationDecoded.Thoroughfare + " " + locationDecoded.FeatureName + ", " + locationDecoded.SubAdminArea;

                    }
                    else
                    {
                        _destinationLocation = e.Point;

                        var pin = MapControl.Pins.FirstOrDefault(x => x.Label == "Destination");
                        if (pin != null)
                        {
                            pin.Position = new Position(_destinationLocation.Latitude, _destinationLocation.Longitude);
                        }
                        else
                        {
                            MapControl.Pins.Add(new Xamarin.Forms.GoogleMaps.Pin
                            {
                                Type = PinType.Place,
                                Position = new Position(_destinationLocation.Latitude, _destinationLocation.Longitude),
                                Label = "Destination",
                                Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("ic_pickuplocation.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "ic_pickuplocation.png", WidthRequest = 25, HeightRequest = 25 }),
                                Tag = 2
                            });
                        }

                        DestinationText = locationDecoded.Thoroughfare + " " + locationDecoded.FeatureName + ", " + locationDecoded.SubAdminArea;
                    }

                    CanCalculate = _startLocation != null && _destinationLocation != null;
                }
            }
            catch (Exception ex)
            {


            }
            finally
            {
                IsBusy = false;
            }
        }

        private void AddMapStyle()
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream($"UITMBER.MapStyle.json");
            string styleFile;
            using (var reader = new System.IO.StreamReader(stream))
            {
                styleFile = reader.ReadToEnd();
            }

            MapControl.MapStyle = MapStyle.FromJson(styleFile);
        }

        private Position _destinationLocation;
        private Position _startLocation;


        private string _startText;
        public string StartText
        {
            get => _startText;
            set => SetProperty(ref _startText, value);
        }



        private string _destinationText;
        public string DestinationText
        {
            get => _destinationText;
            set => SetProperty(ref _destinationText, value);
        }



        private PageStatusEnum _pageStatusEnum;
        public PageStatusEnum PageStatusEnum
        {
            get => _pageStatusEnum;
            set => SetProperty(ref _pageStatusEnum, value);
        }

        private OrderStateEnum _orderStateEnum;
        public OrderStateEnum OrderStateEnum
        {
            get => _orderStateEnum;
            set => SetProperty(ref _orderStateEnum, value);
        }


        private bool hasUFL;
        public bool HasUFL
        {
            get => hasUFL;
            set => SetProperty(ref hasUFL, value);
        }

        private bool _canCalculate;
        public bool CanCalculate
        {
            get => _canCalculate;
            set => SetProperty(ref _canCalculate, value);
        }


        private Xamarin.Forms.GoogleMaps.Map mapControl;


        public Xamarin.Forms.GoogleMaps.Map MapControl
        {
            get => mapControl;
            set => SetProperty(ref mapControl, value);
        }

        public ObservableCollection<LocationsResponse> URLItems { get; }

        public ICommand GetCurrentLocationCommand => new Command(async () => await GetMyCurrentLocation());


        public ICommand StartCalculatingCommand => new Command(async () => await StartCalculating());



        public ICommand ChangePageStatusCommand => new Command<PageStatusEnum>((param) =>
        {
            PageStatusEnum = param;

            if (PageStatusEnum == PageStatusEnum.Default)
            {
                MapControl.Pins.Clear();
                MapControl.Polylines.Clear();
                GetMyCurrentLocation();
            }
            else if (PageStatusEnum == PageStatusEnum.Searching)
            {
                MapControl.Pins.Clear();
                MapControl.Polylines.Clear();
                if (_startLocation != null)
                {
                    MapControl.Pins.Add(new Xamarin.Forms.GoogleMaps.Pin
                    {
                        Type = PinType.Place,
                        Position = new Position(_startLocation.Latitude, _startLocation.Longitude),
                        Label = "Driver",
                        Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("ic_pickuplocation.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "ic_pickuplocation.png", WidthRequest = 25, HeightRequest = 25 }),
                        Tag = 1
                    });
                }
            }
        });


        private async Task GetMyCurrentLocation()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                //var result = await AuthService.AuthenticateAsync(new Models.Authentication.AuthenticationRequest()
                //{
                //    Login = "test2@test.pl",
                //    Password = "Sm1shn3"
                //});



                //UFL

                var uflItems = await UserFavouriteLocationService.GetMyLocationsAsync();

                foreach (var item in uflItems)
                {
                    URLItems.Add(item);
                }
         

                HasUFL = uflItems.Count > 0;




                //LOCATION

                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync();
                }

                if (location != null)
                {

                    var mapSpan = Xamarin.Forms.GoogleMaps.MapSpan.FromCenterAndRadius(new Xamarin.Forms.GoogleMaps.Position(location.Latitude, location.Longitude), Xamarin.Forms.GoogleMaps.Distance.FromKilometers(1.5));

                    MapControl.MoveToRegion(mapSpan);


                    await LoadNearDrivers(location);
                }

            }
            catch (FeatureNotSupportedException ex)
            {
                //Nie obsługiwana lokalizacja
            }
            catch (FeatureNotEnabledException ex)
            {
                //lokalizacja wyłączona
            }
            catch (PermissionException ex)
            {
                //Brak uprawnien do lokalizacji
            }
            catch (Exception ex)
            {


            }
            finally
            {
                IsBusy = false;
            }


        }

        private async Task LoadNearDrivers(Location location)
        {

            MapControl.Polylines.Clear();
            MapControl.Pins.Clear();

            //SERVER
            var drivers = await DriversService.GetNerbyDriveres(new Models.LatLong() { Lat = location.Latitude, Long = location.Longitude });

            foreach (var driver in drivers)
            {
                MapControl.Pins.Add(new Xamarin.Forms.GoogleMaps.Pin
                {
                    Type = PinType.Place,
                    Position = new Position(driver.Lat, driver.Long),
                    Label = "Driver",
                    Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("ic_car.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "ic_car.png", WidthRequest = 25, HeightRequest = 25 }),
                    Tag = string.Empty
                });
            }


            ////MOCKDATA
            //for (int i = 0; i < 10; i++)
            //{
            //    var random = new Random();

            //    MapControl.Pins.Add(new Xamarin.Forms.GoogleMaps.Pin
            //    {
            //        Type = PinType.Place,
            //        Position = new Position(location.Latitude + (random.NextDouble() * 0.04), location.Longitude + (random.NextDouble() * 0.04)),
            //        Label = "Driver",
            //        Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("ic_car.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "ic_car.png", WidthRequest = 25, HeightRequest = 25 }),
            //        Tag = string.Empty
            //    });
            //}


        }


        private async Task StartCalculating()
        {
            try
            {

                var distance = Location.CalculateDistance(new Location(_startLocation.Latitude, _startLocation.Longitude), new Location(_destinationLocation.Latitude, _destinationLocation.Longitude), DistanceUnits.Kilometers);
                DrawRoute(_startLocation, _destinationLocation);
                PageStatusEnum = PageStatusEnum.ShowingRoute;


                //TODO: CALCULATIONG COST AND SELECT FOR CAR TYPE



            }
            catch (Exception ex)
            {

            }
        }

        private void DrawRoute(Position startLocation, Position destinationLocation)
        {
            MapControl.Polylines.Clear();
            MapControl.Pins.Clear();

            var polyline = new Xamarin.Forms.GoogleMaps.Polyline();
            polyline.StrokeColor = Color.Black;
            polyline.StrokeWidth = 3;


            polyline.Positions.Add(startLocation);
            polyline.Positions.Add(destinationLocation);


            MapControl.Polylines.Add(polyline);
            MapControl.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(polyline.Positions[0].Latitude, polyline.Positions[0].Longitude), Xamarin.Forms.GoogleMaps.Distance.FromMiles(0.50f)));
            {
                var pin = new Xamarin.Forms.GoogleMaps.Pin
                {
                    Type = PinType.SearchResult,
                    Position = startLocation,
                    Label = "Start",
                    Address = "Start",
                    Tag = "CirclePoint",
                    Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("ic_circle_point.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "ic_circle_point.png", WidthRequest = 25, HeightRequest = 25 })

                };
                MapControl.Pins.Add(pin);
            }
            {
                var pin = new Xamarin.Forms.GoogleMaps.Pin
                {
                    Type = PinType.SearchResult,
                    Position = destinationLocation,
                    Label = "End",
                    Address = "End",
                    Tag = "CirclePoint",
                    Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("ic_circle_point.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "ic_circle_point.png", WidthRequest = 25, HeightRequest = 25 })

                };
                MapControl.Pins.Add(pin);
            }
        }





    }


}
