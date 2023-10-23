using CatBoyCommon.Models;
using Npgsql;

namespace CatBoyCommon;

public class TagRepository : IRepository<Tag>
{
    public bool Add(ref Tag obj)
    {
        using (var conn = CatBoyContext.instance.GetConnection())
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand("INSERT INTO tag (name) VALUES ($1) RETURNING id;", conn))
            {
                cmd.Parameters.AddWithValue(obj.Name);
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

    public bool AddAll(IEnumerable<Tag> obj)
    {
        //TODO: Do a Transaction
        throw new NotImplementedException();
    }

    public bool Update(Tag obj)
    {
        using (var conn = CatBoyContext.instance.GetConnection())
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand("UPDATE tag SET (name) = ($1) WHERE id = $2", conn))
            {
                cmd.Parameters.AddWithValue(obj.Name);
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
            using (var cmd = new NpgsqlCommand("DELETE FROM tag WHERE id = $1);", conn))
            {
                cmd.Parameters.AddWithValue(id);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }
        return true;
    }

    public IEnumerable<Tag> GetAll()
    {
        using (var cmd = new NpgsqlCommand("SELECT * FROM tag", CatBoyContext.instance.GetConnection()))
        {
            cmd.Connection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    yield return new Tag()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                }
            }
            cmd.Connection.Close();
        }
    }

    public Tag GetById(int id)
    {
        using (var cmd = new NpgsqlCommand("SELECT * FROM tag WHERE id = $1", CatBoyContext.instance.GetConnection()))
        {
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue(id);
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Tag()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                }
            }
            cmd.Connection.Close();
        }
        return null;
    }
}