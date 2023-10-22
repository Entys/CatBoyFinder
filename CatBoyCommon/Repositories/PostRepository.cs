using CatBoyCommon.Models;
using Npgsql;

namespace CatBoyCommon;

public class PostRepository : IRepository<Post>
{
    public bool Add(ref Post obj)
    {
        using (var conn = CatBoyContext.instance.GetConnection())
        {
            using (var cmd = new NpgsqlCommand("INSERT INTO post (id, rating, score, source, created_at) VALUES ($1, $2, $3, $4, $5)", conn))
            {
                cmd.Parameters.AddWithValue(obj.PostId);
                cmd.Parameters.AddWithValue(obj.Rating);
                cmd.Parameters.AddWithValue(obj.Score);
                cmd.Parameters.AddWithValue(obj.Source);
                cmd.Parameters.AddWithValue(obj.CreatedAt);
                cmd.ExecuteNonQuery();
            }
        }
        return true;
    }

    public bool AddAll(IEnumerable<Post> obj)
    {
        //TODO: Do a Transaction
        throw new NotImplementedException();
    }

    public bool Update(Post obj)
    {
        using (var conn = CatBoyContext.instance.GetConnection())
        {
            using (var cmd = new NpgsqlCommand("UPDATE post SET (rating, score, source, created_at) = ($1, $2, $3, $4) WHERE id = $5", conn))
            {
                cmd.Parameters.AddWithValue(obj.Rating);
                cmd.Parameters.AddWithValue(obj.Score);
                cmd.Parameters.AddWithValue(obj.Source);
                cmd.Parameters.AddWithValue(obj.CreatedAt);
                cmd.Parameters.AddWithValue(obj.PostId);
                cmd.ExecuteNonQuery();
            }
        }
        return true;
    }

    public bool Delete(Post obj)
    {
        using (var conn = CatBoyContext.instance.GetConnection())
        {
            using (var cmd = new NpgsqlCommand("DELETE FROM post WHERE id = $1);", conn))
            {
                cmd.Parameters.AddWithValue(obj.PostId);
                cmd.ExecuteNonQuery();
            }
        }
        return true;
    }

    public IEnumerable<Post> GetAll()
    {
        using (var cmd = new NpgsqlCommand("SELECT * FROM post", CatBoyContext.instance.GetConnection()))
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                yield return new Post()
                {
                    PostId = reader.GetInt32(0),
                    Rating = reader.GetInt32(1),
                    Score = reader.GetInt32(2),
                    Source = reader.GetString(3),
                    CreatedAt = reader.GetDateTime(4)
                };
            }
        }
    }

    public Post GetById(int id)
    {
        using (var cmd = new NpgsqlCommand("SELECT * FROM post WHERE id = $1", CatBoyContext.instance.GetConnection()))
        {
            cmd.Parameters.AddWithValue(id);
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Post()
                    {
                        PostId = reader.GetInt32(0),
                        Rating = reader.GetInt32(1),
                        Score = reader.GetInt32(2),
                        Source = reader.GetString(3),
                        CreatedAt = reader.GetDateTime(4)
                    };
                }
            }
        }
        return null;
    }
}