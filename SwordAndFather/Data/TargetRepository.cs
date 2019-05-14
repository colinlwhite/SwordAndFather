using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwordAndFather.Controllers;
using Dapper;
using System.Data.SqlClient;
using SwordAndFather.Models;

namespace SwordAndFather.Data
{
    public class TargetRepository
    {

        const string ConnectionString = "Server=localhost;Database=SwordAndFather;Trusted_Connection=True;";
        public Target AddTarget(string name, string location, FitnessLevel fitnessLevel, int userId)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var insertQuery = @"
                                    INSERT INTO [dbo].[Targets]
                                           ([Location]
                                           ,[Name]
                                           ,[FitnessLevel]
                                           ,[UserId])
                                    output inserted.*
                                        VALUES
                                           (@location
                                           ,@name
                                           ,@fitnessLevel
                                           ,@userId)";
                var parameters = new { Name = name,
                                       Location = location,
                                       FitnessLevel = fitnessLevel,
                                       UserId = userId
                };


                // <Target> I want to automatically map it to this
                var newTarget = db.QueryFirstOrDefault<Target>(insertQuery, parameters);

                if (newTarget != null)
                {
                    return newTarget;
                }

                throw new Exception("Could not create target");
            }
        }
    }
}
