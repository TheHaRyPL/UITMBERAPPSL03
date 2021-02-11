using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace UITMBER.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {


            var mapSpan = Xamarin.Forms.Maps.MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(50.043604d, 22.0261172d), Xamarin.Forms.Maps.Distance.FromKilometers(3));


            MapControl = new Xamarin.Forms.Maps.Map(mapSpan);
            MapControl.MapClicked += MapControl_MapClicked;
            MapControl.IsShowingUser = true;

        }

        private void MapControl_MapClicked(object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
        {
            MapControl.Pins.Clear();
            MapControl.Pins.Add(new Xamarin.Forms.Maps.Pin()
            {
                Position = e.Position,
                Label = "Clicked",
                Type = Xamarin.Forms.Maps.PinType.SavedPin
            });

             
        }

        private string infoText;
        public string InfoText
        {
            get => infoText;
            set => SetProperty(ref infoText, value);
        }
        public ICommand ActionCommand => new Command(async () => await GetMyCurrentLocation());


        private Xamarin.Forms.Maps.Map mapControl;
        public Xamarin.Forms.Maps.Map MapControl
        {
            get => mapControl;
            set => SetProperty(ref mapControl, value);
        }


        private async Task GetMyCurrentLocation()
        {
            try
            {
                //var location = await Geolocation.GetLastKnownLocationAsync();


                var location = await Geolocation.GetLocationAsync( new GeolocationRequest() { DesiredAccuracy = GeolocationAccuracy.Default });

                if (location != null)
                {
                    InfoText = $"LAT: {location.Latitude} LONG: {location.Longitude}";


                    var mapSpan = Xamarin.Forms.Maps.MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(location.Latitude, location.Longitude), Xamarin.Forms.Maps.Distance.FromKilometers(1.5));

                    MapControl.MoveToRegion(mapSpan);
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


        }
    }
}
