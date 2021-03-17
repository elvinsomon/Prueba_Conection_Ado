using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Console_Ado_Protra
{
    class Program
    {
        //https://docs.microsoft.com/es-es/sql/connect/ado-net/step-3-connect-sql-ado-net?view=sql-server-ver15
        static void Main(string[] args)
        {
            string connectionString = "Server=localhost;Database=ProTrailers_V2.0.1;Trusted_Connection=True;";

            Console.WriteLine("Hola!");
            Console.Write("Ingresa una categoria: ");
            string busqueda = Console.ReadLine();



            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                consultar(connection, busqueda);
            }

            Console.ReadKey();
        }
        static public void consultar(SqlConnection connection, string busqueda)
        {
            //var laVaria = "Aventura";
            using (var command = new SqlCommand())
            {
                command.Parameters.Add(new SqlParameter("@Genero", busqueda));
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = @"SELECT Titulo, Genero, Director, Fecha
                                        FROM Movies
                                        WHERE Genero = @Genero";

                SqlDataReader reader = command.ExecuteReader();
                //List<string> rows = null;
                //rows = ToCSV(reader, false, ",");                


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

        //public static List<string> ToCSV(this IDataReader dataReader, bool includeHeaderAsFirstRow, string separator)
        //{
        //    List<string> csvRows = new List<string>();
        //    StringBuilder sb = null;

        //    if (includeHeaderAsFirstRow)
        //    {
        //        sb = new StringBuilder();
        //        for (int index = 0; index < dataReader.FieldCount; index++)
        //        {
        //            if (dataReader.GetName(index) != null)
        //                sb.Append(dataReader.GetName(index));

        //            if (index < dataReader.FieldCount - 1)
        //                sb.Append(separator);
        //        }
        //        csvRows.Add(sb.ToString());
        //    }

        //    while (dataReader.Read())
        //    {
        //        sb = new StringBuilder();
        //        for (int index = 0; index < dataReader.FieldCount - 1; index++)
        //        {
        //            if (!dataReader.IsDBNull(index))
        //            {
        //                string value = dataReader.GetValue(index).ToString();
        //                if (dataReader.GetFieldType(index) == typeof(String))
        //                {
        //                    //If double quotes are used in value, ensure each are replaced but 2.
        //                    if (value.IndexOf("\"") >= 0)
        //                        value = value.Replace("\"", "\"\"");

        //                    //If separtor are is in value, ensure it is put in double quotes.
        //                    if (value.IndexOf(separator) >= 0)
        //                        value = "\"" + value + "\"";
        //                }
        //                sb.Append(value);
        //            }

        //            if (index < dataReader.FieldCount - 1)
        //                sb.Append(separator);
        //        }

        //        if (!dataReader.IsDBNull(dataReader.FieldCount - 1))
        //            sb.Append(dataReader.GetValue(dataReader.FieldCount - 1).ToString().Replace(separator, " "));

        //        csvRows.Add(sb.ToString());
        //    }
        //    dataReader.Close();
        //    sb = null;
        //    return csvRows;
        //}
    }
}
