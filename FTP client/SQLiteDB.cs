using FTP_client.Models;
using SQLite;

namespace FTP_client;

public class SQLiteDB
{
    private readonly SQLiteAsyncConnection _connection = new(Path.Combine(FileSystem.AppDataDirectory, "connections.db3"));

    public SQLiteDB()
    {
        _connection.CreateTableAsync<ConnectionParameters>().Wait();
        _connection.CreateTableAsync<DownloadsPath>().Wait();
        if(_connection.Table<DownloadsPath>().FirstOrDefaultAsync().Result is null)
        {
            _connection.InsertAsync(new DownloadsPath() { Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) }).Wait();
        }
    }

    public async Task<bool> AddConnectionParametersAsync(ConnectionParameters connectionParameters)
    {
        if(await _connection.Table<ConnectionParameters>().FirstOrDefaultAsync(x => x.Name == connectionParameters.Name) is not null)
        {
            return false;
        }

        await _connection.InsertAsync(connectionParameters);
        return true;
    }

    public async Task DeleteConnectionParametersAsync(ConnectionParameters connectionParameters)
        => await _connection.DeleteAsync(connectionParameters);

    public async Task<List<ConnectionParameters>> GetConnectionParametersAsync() 
        => await _connection.Table<ConnectionParameters>().ToListAsync();

    public async Task<string> GetDownloadsPathAsync() => (await _connection.Table<DownloadsPath>().FirstAsync()).Path;

    public async Task SetDownloadsPathAsync(string path)
    {
        DownloadsPath downloadsPath = await _connection.Table<DownloadsPath>().FirstAsync();
        downloadsPath.Path = path;
        await _connection.InsertOrReplaceAsync(downloadsPath);
    }
}