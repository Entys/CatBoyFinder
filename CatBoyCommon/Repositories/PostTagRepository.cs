using CatBoyCommon.Models;
using Npgsql;

namespace CatBoyCommon;

public class PostTagRepository : IRepository<PostTag>
{
    public bool Add(ref PostTag obj)
    {
        //TODO: Change id
        using (var conn = CatBoyContext.instance.GetConnection())
        {
            using (var cmd = new NpgsqlCommand("INSERT INTO post_tag (post_id, tag_id) VALUES ($1, $2)", conn))
            {
                cmd.Parameters.AddWithValue(obj.PostId);
                cmd.Parameters.AddWithValue(obj.TagId);
                cmd.ExecuteNonQuery();
            }
        }
        return true;
    }

    public bool AddAll(IEnumerable<PostTag> obj)
    {
        //TODO: Do a Transaction
        throw new NotImplementedException();
    }

    public bool Update(PostTag obj)
    {
        using (var conn = CatBoyContext.instance.GetConnection())
        {
            using (var cmd = new NpgsqlCommand("UPDATE post_tag SET (post_id, tag_id) = ($1, $2) WHERE id = $3", conn))
            {
                cmd.Parameters.AddWithValue(obj.PostId);
                cmd.Parameters.AddWithValue(obj.TagId);
                cmd.Parameters.AddWithValue(obj.Id);
                cmd.ExecuteNonQuery();
            }
        }
        return true;
    }

    public bool Delete(PostTag obj)
    {
        using (var conn = CatBoyContext.instance.GetConnection())
        {
            using (var cmd = new NpgsqlCommand("DELETE FROM post_tag WHERE id = $1);", conn))
            {
                cmd.Parameters.AddWithValue(obj.Id);
                cmd.ExecuteNonQuery();
            }
        }
        return true;
    }

    public IEnumerable<PostTag> GetAll()
    {
        using (var cmd = new NpgsqlCommand("SELECT * FROM post_tag", CatBoyContext.instance.GetConnection()))
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                yield return new PostTag()
                {
                    Id = reader.GetInt32(0),
                    PostId = reader.GetInt32(1),
                    TagId = reader.GetInt32(2)
                };
            }
        }
    }

    public PostTag GetById(int id)
    {
        using (var cmd = new NpgsqlCommand("SELECT * FROM post_tag WHERE id = $1", CatBoyContext.instance.GetConnection()))
        {
            cmd.Parameters.AddWithValue(id);
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new PostTag()
                    {
                        Id = reader.GetInt32(0),
                        PostId = reader.GetInt32(1),
                        TagId = reader.GetInt32(2)
                    };
                }
            }
        }
        return null;
    }
}