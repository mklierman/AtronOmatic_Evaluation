using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtronOmatic_Evaluation.Models;
public class VideoModel
{
    public string id { get; set; }
    public string title { get; set; }
    public string bulletText { get; set; }
    public string description { get; set; }
    public int runningTime { get; set; }
    public string runningMinutes { get; set; }
    public string artUrl { get; set; }
    public IEnumerable<string> related { get; set; }
}

public struct MoviesPayload
{
    public ObservableCollection<VideoModel> VideoList { get; set; }
    public VideoModel SelectedVideo { get; set; }
}
