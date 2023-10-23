using CatBoyCommon.Models;
using Npgsql;

namespace CatBoyCommon;

public class PostDetailsRepository : IRepository<PostDetails>
{
    public bool Add(ref PostDetails obj)
    {
        using (var conn = CatBoyContext.instance.GetConnection())
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand("INSERT INTO post_detail (post_id, preview_url, file_url) VALUES ($1, $2, $3)", conn))
            {
                cmd.Parameters.AddWithValue(obj.PostId);
                cmd.Parameters.AddWithValue(obj.PreviewUrl);
                cmd.Parameters.AddWithValue(obj.FileUrl);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }
        return true;
    }

    public bool AddAll(IEnumerable<PostDetails> obj)
    {
        //TODO: Do a Transaction
        throw new NotImplementedException();
    }

    public bool Update(PostDetails obj)
    {
        using (var conn = CatBoyContext.instance.GetConnection())
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand("UPDATE post_detail SET (preview_url, file_url) = ($1, $2) WHERE post_id = $3", conn))
            {
                cmd.Parameters.AddWithValue(obj.PreviewUrl);
                cmd.Parameters.AddWithValue(obj.FileUrl);
                cmd.Parameters.AddWithValue(obj.PostId);
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
            using (var cmd = new NpgsqlCommand("DELETE FROM post_detail WHERE post_id = $1);", conn))
            {
                cmd.Parameters.AddWithValue(id);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }
        return true;
    }

    public IEnumerable<PostDetails> GetAll()
    {
        using (var cmd = new NpgsqlCommand("SELECT * FROM post_detail", CatBoyContext.instance.GetConnection()))
        {
            cmd.Connection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    yield return new PostDetails()
                    {
                        PostId = reader.GetInt32(0),
                        PreviewUrl = reader.GetString(1),
                        FileUrl = reader.GetString(2),
                    };
                }
            }
            cmd.Connection.Open();
        }
    }

    public PostDetails GetById(int id)
    {
        using (var cmd = new NpgsqlCommand("SELECT * FROM post_detail WHERE post_id = $1", CatBoyContext.instance.GetConnection()))
        {
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue(id);
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new PostDetails()
                    {
                        PostId = reader.GetInt32(0),
                        PreviewUrl = reader.GetString(1),
                        FileUrl = reader.GetString(2),
                    };
                }
            }
            cmd.Connection.Close();
        }
        return null;
    }
}