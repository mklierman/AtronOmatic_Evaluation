using AtronOmatic_Evaluation.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;

namespace AtronOmatic_Evaluation.ViewModels;

public partial class DetailViewModel : ObservableRecipient
{
    public DetailViewModel()
    {
    }

    private ObservableCollection<VideoModel> videoList;
    public ObservableCollection<VideoModel> VideoList
    {
        get => videoList;
        set => SetProperty(ref videoList, value);

    }

    private VideoModel selectedVideo;
    public VideoModel SelectedVideo
    {
        get => selectedVideo;
        set => SetProperty(ref selectedVideo, value);
    }
}
