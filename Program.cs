using System;
using System.Data;
using System.Data.SqlClient;

namespace Console_Ado_Protra
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string connectionString = "Server=localhost;Database=ProTrailers_V2.0.1;Trusted_Connection=True;";
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
                consultar(connection);
            }


            

            Console.ReadKey();
        }
        static public void consultar(SqlConnection connection)
        {
            var laVaria = "Drama";
            using (var command = new SqlCommand())
            {
                command.Parameters.Add(new SqlParameter("@Genero", laVaria));
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = @"SELECT Titulo, Genero, Director, Fecha
                                        FROM Movies
                                        WHERE Genero = @Genero";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine("\n{0}\t\t\t\t{1}\t\t\t\t{2}\t\t\t\t{3}",
                        reader.GetString(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetDateTime(3));
                }
            }
        }
    }
}
