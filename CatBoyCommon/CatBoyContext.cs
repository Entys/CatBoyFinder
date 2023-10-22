using Npgsql;

namespace CatBoyCommon;

public class CatBoyContext
{
    public PostRepository Posts { get; set; }
    public TagRepository Tags { get; set; }
    public PostTagRepository PostTags { get; set; }
    public PostDetailsRepository PostDetails { get; set; }
    private String _connectionString;

    public static CatBoyContext instance;

    public CatBoyContext(String connectionString)
    {
        _connectionString = connectionString;
        Posts = new PostRepository();
        Tags = new TagRepository();
        PostTags = new PostTagRepository();
        PostDetails = new PostDetailsRepository();
        instance = this;
    }

    public NpgsqlConnection GetConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}