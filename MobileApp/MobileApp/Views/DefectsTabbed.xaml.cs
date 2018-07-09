using MobileApp.API;
using MobileApp.Models;
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
    public partial class DefectsTabbed : TabbedPage
    {
        List<Defect> Source;
        public DefectsTabbed (List<Defect> Source)
        {
            InitializeComponent();
            this.Source = Source;
            Refresh();
            listView1.ItemTapped += GetInfo.TappedItem;
            listView2.ItemTapped += async (object sender, ItemTappedEventArgs e) =>
            {
                if (e == null) return;
                await Navigation.PushAsync(new CreateDefect(Source,this,listView2.SelectedItem as Defect));// de-select the row
                ((ListView)sender).SelectedItem = null;
            };
        }

        public void Refresh()
        {
            listView1.ItemsSource = null;
            listView2.ItemsSource = null;
            listView1.ItemsSource = this.Source.Where(x => (!x.IsNew));
            listView2.ItemsSource = this.Source.Where(x => (x.IsNew));
        }

        private async System.Threading.Tasks.Task Button_ClickedAsync(object sender, EventArgs e)
        { 
            await Navigation.PushAsync(new CreateDefect(Source,this));// de-select the row
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            string res1 = JsonConvert.SerializeObject(GetInfo.tasks);
            App.Current.Properties["obj"] = res1;
        }
    }
}