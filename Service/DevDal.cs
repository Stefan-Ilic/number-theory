using System;
using System.Data;
using System.Data.SQLite;
using Interfaces;

namespace Service
{
    public class DevDal : IDataAccessLayer
    {
        public DevDal(string connectionString)
        {
            _connection = new SQLiteConnection(connectionString);
            _connection.Open();
        }

        private readonly SQLiteConnection _connection;

        public bool? IsPrime(long numberToTest)
        {
            if (numberToTest > LargestTestedNumber) { return null; }
            var sql = new SQLiteCommand("SELECT number FROM primes WHERE number = @num", _connection);
            sql.Parameters.Add("num", DbType.Int64).Value = numberToTest;
            return sql.ExecuteScalar() != null;
        }

        public long LargestTestedNumber
        {
            get
            {
                var sql = new SQLiteCommand("SELECT number FROM largesttestednumber", _connection);
                return Convert.ToInt64(sql.ExecuteScalar());
            }
            set
            {
                var sql = new SQLiteCommand("UPDATE number SET @new", _connection);
                sql.Parameters.Add("new", DbType.Int64).Value = value;
                sql.ExecuteNonQuery();
            }
        }

        public void AddNewPrimeNumber(long newPrime)
        {
            var sql = new SQLiteCommand("INSERT primes (number) VALUES (@value)", _connection);
            sql.Parameters.Add("value", DbType.Int64).Value = newPrime;
            sql.ExecuteNonQuery();
        }
    }
}
