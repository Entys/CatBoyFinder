using CatBoyCommon.Models;
using Npgsql;

namespace CatBoyCommon;

public class PostTagRepository : IRepository<PostTag>
{
    public bool Add(ref PostTag obj)
    {
        using (var conn = CatBoyContext.instance.GetConnection())
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand("INSERT INTO post_tag (post_id, tag_id) VALUES ($1, $2) RETURNING id;", conn))
            {
                cmd.Parameters.AddWithValue(obj.PostId);
                cmd.Parameters.AddWithValue(obj.TagId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        obj.Id = reader.GetInt32(0);
                    }
                }
            }
            conn.Close();
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
            conn.Open();
            using (var cmd = new NpgsqlCommand("UPDATE post_tag SET (post_id, tag_id) = ($1, $2) WHERE id = $3", conn))
            {
                cmd.Parameters.AddWithValue(obj.PostId);
                cmd.Parameters.AddWithValue(obj.TagId);
                cmd.Parameters.AddWithValue(obj.Id);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }
        return true;
    }

    public bool Delete(int id)
    {
        using (var conn = CatBoyContext.instance.GetConnection())
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand("DELETE FROM post_tag WHERE id = $1);", conn))
            {
                cmd.Parameters.AddWithValue(id);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }
        return true;
    }

    public IEnumerable<PostTag> GetAll()
    {
        using (var cmd = new NpgsqlCommand("SELECT * FROM post_tag", CatBoyContext.instance.GetConnection()))
        {
            cmd.Connection.Open();
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
            cmd.Connection.Close();
        }
    }

    public PostTag GetById(int id)
    {
        using (var cmd = new NpgsqlCommand("SELECT * FROM post_tag WHERE id = $1", CatBoyContext.instance.GetConnection()))
        {
            cmd.Connection.Open();
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
            cmd.Connection.Close();
        }
        return null;
    }
}