using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EvernoteClone.ViewModel.Helpers
{
    enum CUDAction
    {
        Insert,
        Update,
        Delete
    }
    public class DatabaseHelper
    {
        private static readonly string dbFile = Path.Combine(Environment.CurrentDirectory, "notesDb.db3");
        
        private static bool ExecuteCUDAction<T>(CUDAction action, T item)
        {
            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                conn.CreateTable<T>();
                var actionMethod = conn.GetType().GetMethod(action.ToString()).MakeGenericMethod(typeof(T));
                var rows = (int)actionMethod.Invoke(conn, new object[] { item });
                return rows > 0;
                //switch (action)
                //{
                //    case CUDAction.Insert:
                //        return conn.Insert(item) > 0;
                //    case CUDAction.Update:
                //        return conn.Update(item) > 0;
                //    case CUDAction.Delete:
                //        return conn.Delete(item) > 0;
                //}
                //return false;
            }
        }

        public static bool Insert<T>(T item)
        {
            return ExecuteCUDAction(CUDAction.Insert, item);
        }

        public static bool Update<T>(T item)
        {
            return ExecuteCUDAction(CUDAction.Update, item);
        }

        public static bool Delete<T>(T item)
        {
            return ExecuteCUDAction(CUDAction.Delete, item);
        }

        public static List<T> Read<T>() where T : new()
        {
            List<T> items;

            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                conn.CreateTable<T>();
                items = conn.Table<T>().ToList();
            }

            return items;
        }
        public static List<T> GetByColumnName<T>(string column, object value) where T : new()
        {

            var query = $"SELECT * FROM {typeof(T).Name} WHERE {column} = ?";
            List<T> items;

            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                conn.CreateTable<T>();
                items = conn.Query<T>(query, value);
            }

            return items;
        }
    }
}
