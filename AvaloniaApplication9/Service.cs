using AvaloniaApplication9.Models;

namespace AvaloniaApplication9;

public class Service
{
    private static AvaloniaContext? _db;
    
    public static AvaloniaContext  GetDbContext()
    {
        if (_db == null)
        {
            _db = new AvaloniaContext();
        }
        return _db;
    }
}