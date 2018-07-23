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
	public partial class ResourcePage : ContentPage
    {
        List<Models.ResourceItem> Resource;
        public ResourcePage (List<Models.ResourceItem> Resource)
		{
			InitializeComponent();
            this.Resource = Resource;
            MaterialListView.ItemsSource = this.Resource;
            MaterialListView.ItemTapped += API.GetInfo.TappedItem;
        }
	}
}