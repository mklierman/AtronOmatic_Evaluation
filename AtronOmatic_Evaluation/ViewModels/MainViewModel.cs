using CommunityToolkit.Mvvm.ComponentModel;
using Flurl.Http;
using AtronOmatic_Evaluation.Models;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AtronOmatic_Evaluation.ViewModels;

public partial class MainViewModel : ObservableRecipient
{
    private const string url = @"https://assets.acmeaom.com/interview-project/uwpvideos.json";
    public MainViewModel()
    {
        LoadVideoList();
    }

    private XamlRoot xRoot;
    public XamlRoot XRoot
    {
        get => xRoot; 
        set => xRoot = value;
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

    private async Task LoadVideoList()
    {
        try
        {
            var response = await url.AllowAnyHttpStatus().GetAsync();
            if (response != null)
            {
                if (response.ResponseMessage.IsSuccessStatusCode)
                {
                    var result = await response.GetJsonAsync<ObservableCollection<VideoModel>>();
                    if (result != null)
                    {
                        // Order by titles
                        var orderedVids = new ObservableCollection<VideoModel>(result.OrderBy(p => p.title));
                        result.Clear();
                        foreach (var vid in orderedVids)
                        {
                            //Since we're iterating over everything here anyways, populate runningMinutes
                            vid.runningMinutes = $"{vid.runningTime / 60} minutes";

                            result.Add(vid);
                        }
                        await LoadImages(result);
                        VideoList = result;
                    }
                    else
                    {
                        //Inform user of empty list
                        ShowError("No Movies found!");
                    }
                }
                else
                {
                    //Inform user of bad response
                    ShowError($"Error {response.ResponseMessage.StatusCode}: {response.ResponseMessage.ReasonPhrase}");
                }
            }
        }
        catch (FlurlHttpTimeoutException)
        {
            //Inform user of timeout
            ShowError("Request timed out");
        }
        catch (Exception ex)
        {
            ShowError("Unhandled Exception");
            //If I had logging this is where I would put it
        }
    }

    private async Task LoadImages(ObservableCollection<VideoModel> result)
    {
        foreach (var video in result)
        {
            var url = video.artUrl;
            try
            {
                //Check if image at url is valid
                using var stream = await url.GetStreamAsync();
                stream.Close();
            }
            catch (FlurlHttpException ex)
            {
                //Url isn't valid, set as fallback image
                video.artUrl = "/Assets/NoImageAvailable.png";
            }
            catch (Exception ex)
            {
                ShowError("Unhandled Exception");
                //If I had logging this is where I would put it
            }
        }
    }

    private async void ShowError(string message)
    {
        var errorDialog = new ContentDialog
        {
            Title = "Error",
            Content = message,
            PrimaryButtonText = "Retry",
            SecondaryButtonText = "Cancel",
            XamlRoot = XRoot
        };

        var result = await errorDialog.ShowAsync();
        if (result == ContentDialogResult.Primary)
        {
            LoadVideoList();
        }

    }
}
