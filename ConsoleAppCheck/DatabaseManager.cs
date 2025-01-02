using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace ConsoleAppCheck
{
    internal class DatabaseManager
    {
        internal void CreateTable(string connectionString){
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using( var tabledCmd = connection.CreateCommand())
                {
                    tabledCmd.CommandText =
                    @"create table if not exists coding(
                            Id integer primary key autoincrement,
                            Date text,
                            Duration text
                        )";
                    tabledCmd.ExecuteNonQuery();
                }
            }
        }
    }
}