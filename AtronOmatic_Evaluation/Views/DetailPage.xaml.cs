using AtronOmatic_Evaluation.Models;
using AtronOmatic_Evaluation.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace AtronOmatic_Evaluation.Views;

public sealed partial class DetailPage : Page
{
    private MoviesPayload payload;
    public DetailViewModel ViewModel
    {
        get;
    }

    public DetailPage()
    {
        ViewModel = App.GetService<DetailViewModel>();
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        //Unpack the payload
        payload = (MoviesPayload)e.Parameter;
        ViewModel.SelectedVideo = payload.SelectedVideo;
        ViewModel.VideoList = payload.VideoList;
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        Frame.GoBack();
    }

}
