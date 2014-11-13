using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using FlickrApi;

namespace FavoInsta
{
    class SQLiteManager
    {
        public SQLiteManager(){
            /*
            string db_file = "sample.db";

            using (var conn = new SQLiteConnection("Data Source=" + db_file))
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    command.CommandText = "create table sample(id INTEGER  PRIMARY KEY AUTOINCREMENT, Name TEXT, Age INTEGER)";
                    command.ExecuteNonQuery();
                }
                conn.Close();
            }
             */
        }

        public void InsertInstagram(List<InstagramPhoto> list) {
            string dbConnectionString = "Data Source=photo.db";
            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                try
                {
                    cn.Open();
                    Console.WriteLine("connection is OK");
                    using (SQLiteTransaction trans = cn.BeginTransaction())
                    {
                        SQLiteCommand cmd = cn.CreateCommand();

                        cmd.CommandText = "insert into photo (Url, Text) values (@Url,@Text)";
                        cmd.Parameters.Add("Url", System.Data.DbType.String);
                        cmd.Parameters.Add("Text", System.Data.DbType.String);

                        foreach (var ip in list)
                        {
                            cmd.Parameters["Url"].Value = ip.PhotoUrl;
                            cmd.Parameters["Text"].Value = ip.Text;
                            cmd.ExecuteNonQuery();
                        }
                        trans.Commit();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("missing",e);
                }
            }
        }

        public void InsertFlickr(List<FlickrImage> list)
        {
            string dbConnectionString = "Data Source=photo.db";
            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                try
                {
                    cn.Open();
                    Console.WriteLine("connection is OK");
                    using (SQLiteTransaction trans = cn.BeginTransaction())
                    {
                        SQLiteCommand cmd = cn.CreateCommand();

                        cmd.CommandText = "insert into photo (Url, Text) values (@Url,@Text)";
                        cmd.Parameters.Add("Url", System.Data.DbType.String);
                        cmd.Parameters.Add("Text", System.Data.DbType.String);

                        foreach (var fp in list)
                        {
                            cmd.Parameters["Url"].Value = fp.ImageUrl;
                            cmd.Parameters["Text"].Value = String.Join(" ", fp.Tags.ToArray());
                            cmd.ExecuteNonQuery();
                        }
                        trans.Commit();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("missing",e);
                }
            }
        }

        public List<PhotoUrl> GetData()
        {
            List<PhotoUrl> result = new List<PhotoUrl>();

            string dbConnectionString = "Data Source=photo.db";
            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                cn.Open();
                SQLiteCommand cmd = cn.CreateCommand();
                cmd.CommandText = "select * from photo";
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Console.WriteLine(reader["Url"].ToString());
                        // Console.WriteLine(reader["Text"].ToString());
                        result.Add(new PhotoUrl(reader["Url"].ToString(), reader["Text"].ToString()));
                    }
                }
            }
            return result;
        }
    }

    class PhotoUrl
    {
        private string _url, _text;

        public string Url
        {
            get { return _url; }
        }

        public string Text
        {
            get { return _text; }
        }

        public PhotoUrl(string url, string text) {
            _url = url;
            _text = text;
        }
    }
}
