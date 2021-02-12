using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UITMBER.ViewModels;

namespace UITMBER.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendApplicationPage : ContentPage
    {
        public SendApplicationPage()
        {
            InitializeComponent();
        }

        //docelowo strona do wysyłania aplikacji będzie dostępna ze strony w której sa wyświetlane wszystkie aplikacje wiec
        //po klikniecie Cancel użytkownik zostanie przeniesiony z powrotem do strony ze wszystkimi jego aplikacjami
        //aktualnie po kliknięciu w przycisk wyrzuca błąd
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}