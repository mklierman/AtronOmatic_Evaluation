using System.Collections.ObjectModel;
using AtronOmatic_Evaluation.Models;
using AtronOmatic_Evaluation.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

namespace AtronOmatic_Evaluation.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
        // Ensure that the MainPage is only created once, and cached during navigation.
        this.NavigationCacheMode = Microsoft.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        var rootElement = this.Content as FrameworkElement;
        if (rootElement != null)
        {
            rootElement.Loaded += Root_Loaded;
        }
    }

    //Need to set the XamlRoot as a variable in the View Model so we can show a dialog from it
    private void Root_Loaded(object sender, RoutedEventArgs e) => ViewModel.XRoot = this.XamlRoot;

    private VideoModel selectedVideo;
    private MoviesPayload payload;

    private void collection_ItemClick(object sender, ItemClickEventArgs e)
    {
        // Get the collection item corresponding to the clicked item.
        if (collection.ContainerFromItem(e.ClickedItem) is ListViewItem container)
        {
            //Build the payload to send to the detail page
            payload = new MoviesPayload();
            selectedVideo = container.Content as VideoModel;
            var listView = sender as ListView;
            if (listView?.ItemsSource is ObservableCollection<VideoModel> list)
            {
                payload.VideoList = list;
                payload.SelectedVideo = selectedVideo;
            }
        }

        // Navigate to the DetailedInfoPage.
        Frame.Navigate(typeof(DetailPage), payload, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
    }

}
