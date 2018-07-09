using MobileApp.API;
using MobileApp.Models;
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
    public partial class ResourcesTabbedPage : TabbedPage
    {
        List<ResourceItem> Resource;
        public ResourcesTabbedPage (List<ResourceItem> Resource)
        {
            InitializeComponent();
            this.Resource = Resource;
            MaterialListView.ItemsSource = this.Resource.Where(x=> (x.ResourceType == ResourceType.Materials));
            MaterialListView.ItemTapped += GetInfo.TappedItem;
            CarsListView.ItemsSource = this.Resource.Where(x => (x.ResourceType == ResourceType.Device));
            CarsListView.ItemTapped += GetInfo.TappedItem;
            JobResListView.ItemsSource = this.Resource.Where(x => (x.ResourceType == ResourceType.Workforce));
            JobResListView.ItemTapped += GetInfo.TappedItem;
        }
    }
}