using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace ConsoleAppCheck
{
    internal class CodingController
    {
        string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
        
        internal void Post(Coding coding){
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = $"insert into coding (date, duration) values ('{coding.Date}', '{coding.Duration}')";
                    tableCmd.ExecuteNonQuery();
                }
            }
        }

        internal Coding GetById(int id){
            using (var connection = new SqliteConnection(connectionString))
            {
                using(var tableCmd = connection.CreateCommand()){
                    connection.Open();
                    tableCmd.CommandText=$"select * from coding where Id = '{id}'";

                    using (var reader = tableCmd.ExecuteReader())
                    {
                        Coding coding = new();
                        if (reader.HasRows){
                            reader.Read();
                            coding.Id= reader.GetInt32(0);
                            coding.Date=reader.GetString(1);
                            coding.Duration=reader.GetString(2);

                        }
                        Console.WriteLine("\n\n");
                        return coding;
                    }
                }
            }
        }

        internal void Get(){
            List<Coding> tableDate = new List<Coding>();
            
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = "select * from coding";

                    using (var reader = tableCmd.ExecuteReader())
                    {
                        if(reader.HasRows)
                        {
                            while(reader.Read()){
                                tableDate.Add(
                                    new Coding{
                                        Id = reader.GetInt32(0),
                                        Date = reader.GetString(1),
                                        Duration = reader.GetString(2)
                                    }
                                );
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n\nNo rows found.\n\n");
                        }

                        TableVisualisation.ShowTable(tableDate);
                    }
                }
            }
            }

            internal void Delete(int id){
                using (var connection = new SqliteConnection(connectionString))
                {
                    using (var tableCmd = connection.CreateCommand())
                    {
                        connection.Open();
                        tableCmd.CommandText=$"delete from coding where Id = '{id}'";
                        tableCmd.ExecuteNonQuery();
                        Console.WriteLine($"\n\nRecord with Id {id} was deleted. \n\n");
                    }

                }
            }

            internal void Update(Coding coding)
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    using (var tableCmd = connection.CreateCommand())
                    {
                        connection.Open();
                        tableCmd.CommandText=
                        $@"update coding set
                        Date = '{coding.Date}',
                        Duration = '{coding.Duration}' 
                        where id = {coding.Id}";
                        
                        tableCmd.ExecuteNonQuery();
                        Console.WriteLine($"\n\nRecord with Id {coding.Id} was updated. \n\n");
                    }

                }
            }
        }
    }
