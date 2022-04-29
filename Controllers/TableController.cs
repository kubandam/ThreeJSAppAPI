using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace BCAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TableController : Controller
    {
        [HttpGet(Name = "getInt")]
        [Produces("application/json", "application/xml", Type = typeof(List<string>))]
        public IActionResult Get()
        {
            SqliteConnection sqlite_conn;
            sqlite_conn = CreateConnection();
            List<List<string>> list = ReadData(sqlite_conn); 
            return Ok(list);
        }

        static SqliteConnection CreateConnection()
        {

            SqliteConnection sqlite_conn = new SqliteConnection("Data Source= bc.db");
            sqlite_conn.Open();
            return sqlite_conn;
        }
        static List<List<string>> ReadData(SqliteConnection conn)
        {
            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT id,text FROM bc";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            List<List<string>> list = new List<List<string>>();
            while (sqlite_datareader.Read())
            {
                List<string> vs = new List<string>();
                for (int i = 0; i < sqlite_datareader.FieldCount; i++)
                {
                    vs.Add(sqlite_datareader.GetString(i));
                }
                list.Add(vs);
            }
            conn.Close();
            return list;
        }
    }
}
