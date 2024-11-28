using System.Data;
using Microsoft.Data.SqlClient;

namespace Proj.Infrastructure.Persistent.Dapper;

public class DapperContext
{
    private readonly string _connectionString;

    public DapperContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public string Artists => "[dbo].Artists";
    public string Categories => "[dbo].Categories";
    public string Musics => "[dbo].Musics";
    public string MusicAlbums => "[dbo].MusicAlbums";
}