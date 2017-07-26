using FCore.DAL.Entities.Videos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;

namespace FCore.DAL.Entities.Families
{
    public class DapperContext : IDisposable
    {
        IDbConnection sqlConnection { get; set; }

        public bool AddVideo(VideoEntity entity)
        {
            using (sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["FamilyContext"].ConnectionString))
            {
                try
                {
                    sqlConnection.Execute("stp_AddVideo", 
                        new
                        {
                            libId = entity.Libraryid,
                            desc = entity.Description,
                            path = entity.Path
                        }, commandType: CommandType.StoredProcedure);
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }

        public void Dispose()
        {
            if (sqlConnection != null) sqlConnection.Dispose();
        }
    }
}
