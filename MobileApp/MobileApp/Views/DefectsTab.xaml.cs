using MobileApp.API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DefectsTab : ContentPage
    {
        Models.Task Source;
        bool isNew = false;
        public DefectsTab (Models.Task Source, bool isNew)
		{
			InitializeComponent ();
            this.isNew = isNew;
            this.Source = Source;
            Refresh();
            if (!isNew)
                listView1.ItemTapped += GetInfo.TappedItem;
            else
                listView1.ItemTapped += async (object sender, ItemTappedEventArgs e) =>
                {
                    if (e == null) return;
                    await Navigation.PushAsync(new CreateDefect(Source, this, listView1.SelectedItem as Models.Defect));// de-select the row
                    ((ListView)sender).SelectedItem = null;
                };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Refresh();
        }

        public void Refresh()
        {
            listView1.ItemsSource = null;
            if (isNew)
                listView1.ItemsSource = this.Source.Defects.Where(x => (x.IsNew));
            else
                listView1.ItemsSource = this.Source.Defects.Where(x => (!x.IsNew));
        }

        private async System.Threading.Tasks.Task Button_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateDefect(Source, this));// de-select the row
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            string res1 = JsonConvert.SerializeObject(GetInfo.tasks);
            App.Current.Properties["obj"] = res1;
        }
    }
}