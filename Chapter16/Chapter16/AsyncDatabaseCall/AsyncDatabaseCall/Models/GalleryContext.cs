using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace AsyncDatabaseCall.Models {

    public class GalleryContext {

        readonly string _spName = "sp$GetCars";

        readonly string _connectionString = 
            ConfigurationManager.ConnectionStrings["CarGalleryConnStr"].ConnectionString;

        public IEnumerable<Car> GetCarsViaSP() {

            using (var conn = new SqlConnection(_connectionString)) {
                using (var cmd = new SqlCommand()) {

                    cmd.Connection = conn;
                    cmd.CommandText = _spName;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader()) {

                        return reader.Select(r => carBuilder(r)).ToList();
                    }
                }
            }
        }

        public async Task<IEnumerable<Car>> GetCarsViaSPAsync() {

            using (var conn = new SqlConnection(_connectionString)) {
                using (var cmd = new SqlCommand()) {

                    cmd.Connection = conn;
                    cmd.CommandText = _spName;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (var reader = await cmd.ExecuteReaderAsync()) {
                        
                        return reader.Select(r => carBuilder(r)).ToList();
                    }
                }
            }
        }

        //private helpers

        private Car carBuilder(SqlDataReader reader) {

            return new Car {

                Id = int.Parse(reader["Id"].ToString()),
                Make = reader["Make"] is DBNull ? null : reader["Make"].ToString(),
                Model = reader["Model"] is DBNull ? null : reader["Model"].ToString(),
                Year = int.Parse(reader["Year"].ToString()),
                Price = float.Parse(reader["Price"].ToString()),
            };
        }
    }
}