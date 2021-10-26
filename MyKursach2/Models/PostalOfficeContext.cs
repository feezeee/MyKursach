using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Models
{
    public class PostalOfficeContext : DbContext
    {

        public PostalOfficeContext(DbContextOptions<PostalOfficeContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Position> Positions { get; set; }


        //public string ConnectionString { get; set; }

        //public PostalOfficeContext(string connectionString)
        //{
        //    this.ConnectionString = connectionString;
        //}

        //private MySqlConnection GetConnection()
        //{
        //    return new MySqlConnection(ConnectionString);
        //}

        //public List<Workers>

        //public List<Position> GetAllPositions()
        //{
        //    List<Position> list = new List<Position>();

        //    using (MySqlConnection conn = GetConnection())
        //    {
        //        conn.Open();
        //        MySqlCommand cmd = new MySqlCommand("select * from position;", conn);

        //        using (var reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                list.Add(new Position()
        //                {
        //                    id = Convert.ToInt32(reader["id"]),
        //                    position_name = reader["position_name"].ToString()
        //                });
        //            }
        //        }
        //    }
        //    return list;
        //}
    }
}
