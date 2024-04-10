using System;
using System.Text;

using Microsoft.Data.Sqlite;

namespace WebSpatialDBTest
{
    public class SpatialDBTest
    {
        public SpatialDBTest()
        {
        }
        private static string DB_PATH = null;
        public static void Test()
        {
            DB_PATH = "/home/shatam-server/eclipse-workspace/hazard_zone.sqlite";
           
            Console.WriteLine(DB_PATH);
            SqliteConnection conn = new SqliteConnection("Data Source=" + DB_PATH);//DBConnection.GetConnection(DB_PATH);
            conn.Open();
            conn.EnableExtensions(true);
            log("loading .....");


            using (SqliteCommand mycommand = new SqliteCommand("SELECT load_extension('mod_spatialite')", conn))
            {
                mycommand.ExecuteNonQuery();
            }

            log("Loaded extension");

            string str = "SELECT sqlite_version(), spatialite_version(), spatialite_target_cpu()";
            using (SqliteCommand cmd = new SqliteCommand(str, conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        String msg = "SQLite version: ";
                        msg += reader.GetString(0);
                        log(msg);
                        msg = "SpatiaLite version: ";
                        msg += reader.GetString(1);
                        log(msg);
                        msg = "target CPU: ";
                        msg += reader.GetString(2);
                        log(msg);
                    }
                }
            }
            StringBuilder queryBuilder = new StringBuilder();
            //
            //Alquist_Priolo_Earthquake_Fault_Zone
            queryBuilder.Append("select object_id, ST_AsText(the_geom) from Seismic_Hazard_Zone where object_id in (1, 5);"); // where object_id=76
            //queryBuilder.Append("select * from Seismic_Hazard_Zone where object_id in (1, 5);");

            //TestToConvertSKBToWKT(conn, queryBuilder.ToString());
            ShowQueryOutput(conn, queryBuilder.ToString());
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
            }
        }

        private static void ShowQueryOutput(SqliteConnection conn, string query)
        {
            Console.WriteLine("Insider show function.");
            using (SqliteCommand cmd = new SqliteCommand(query, conn))
            {
                using (SqliteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        log("Index :" + reader.GetString(0));
                        //Console.WriteLine(byteArray.Length);

                        //Console.WriteLine("Geo :" + reader.GetString(1));
                        Console.WriteLine("Geo :" + reader["object_id"]);

                        Console.WriteLine("Geo1 :" + reader["ST_AsText(the_geom)"]);
                    }//EOF while

                    reader.Close();
                }
            }
        }

        public static void log(Object o)
        {
            Console.WriteLine(o);
        }
    }
}
