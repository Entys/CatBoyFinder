using CatBoyCommon;
using CatBoyCommon.Models;

internal class Program
{
    static Dictionary<String, Tag> tags = new Dictionary<String, Tag>();

    public static void Main(String[] args)
    {
        new CatBoyContext("Host=db;Username=postgres;Password=postgres;Database=postgres");
        foreach (var tag in CatBoyContext.instance.Tags.GetAll())
        {
            tags.Add(tag.Name, tag);
        }

        var booru = new BooruSharp.Booru.Safebooru();
        for (int i = 0; i <= 100; i++)
        {
            var result = booru.GetRandomPostAsync("catboy").Result;
            if (CatBoyContext.instance.Posts.GetAll().Any(post => post.PostId == result.ID)) continue;
            var post = new Post
            {
                PostId = result.ID,
                Rating = (int)result.Rating,
                Score = result.Score.HasValue ? result.Score.Value : 0,
                Source = result.FileUrl.AbsoluteUri,
                CreatedAt = result.Creation ?? DateTime.Now
            };


            CatBoyContext.instance.Posts.Add(ref post);

            foreach (var tag in result.Tags)
            {
                if (tag == null) continue;
                var tagModel = GetOrCreate(tag);
                var postTag = new PostTag
                {
                    PostId = post.PostId,
                    TagId = tagModel.Id
                };

                CatBoyContext.instance.PostTags.Add(ref postTag);
            }
        }

        static Tag GetOrCreate(string name)
        {
            Tag tag = tags.GetValueOrDefault(name);
            if (tag == null)
            {
                tag = new Tag
                {
                    Name = name
                };
                tags.Add(name, tag);
                CatBoyContext.instance.Tags.Add(ref tag);
            }

            return tag;
        }
    }
}