namespace CatBoyCommon.Models;

public class PostDetails
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public virtual Post Post { get; set; }
    public String PreviewUrl { get; set; }
    public String FileUrl { get; set; }
}