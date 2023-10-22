using CatBoyCommon.Models;
using Npgsql;

namespace CatBoyCommon;

public class TagRepository : IRepository<Tag>
{
    public bool Add(ref Tag obj)
    {
        //TODO: Change id
        using (var conn = CatBoyContext.instance.GetConnection())
        {
            using (var cmd = new NpgsqlCommand("INSERT INTO tag (name) VALUES ($1)", conn))
            {
                cmd.Parameters.AddWithValue(obj.Name);
                cmd.ExecuteNonQuery();
            }
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
            using (var cmd = new NpgsqlCommand("UPDATE tag SET (name) = ($1) WHERE id = $2", conn))
            {
                cmd.Parameters.AddWithValue(obj.Name);
                cmd.Parameters.AddWithValue(obj.Id);
                cmd.ExecuteNonQuery();
            }
        }
        return true;
    }

    public bool Delete(Tag obj)
    {
        using (var conn = CatBoyContext.instance.GetConnection())
        {
            using (var cmd = new NpgsqlCommand("DELETE FROM tag WHERE id = $1);", conn))
            {
                cmd.Parameters.AddWithValue(obj.Id);
                cmd.ExecuteNonQuery();
            }
        }
        return true;
    }

    public IEnumerable<Tag> GetAll()
    {
        using (var cmd = new NpgsqlCommand("SELECT * FROM tag", CatBoyContext.instance.GetConnection()))
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
    }

    public Tag GetById(int id)
    {
        using (var cmd = new NpgsqlCommand("SELECT * FROM tag WHERE id = $1", CatBoyContext.instance.GetConnection()))
        {
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
        }
        return null;
    }
}