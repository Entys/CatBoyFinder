namespace CatBoyCommon.Models;

public class Post
{
    public int PostId { get; set; }
    public int Rating { get; set; }
    public int Score { get; set; }
    public string Source { get; set; }
    public DateTime CreatedAt { get; set; }
}