using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tamagotchi.Application.Application.Exceptions;

namespace Tamagotchi.Application.Application.Queries
{
    public class DragonQueries : IDragonQueries
    {
        private string _connectionString = string.Empty;

        public DragonQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<DragonViewModel> GetDragonAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<DragonViewModel>(
                   @"SELECT d.[Id] DragonId
                          ,d.[Name] Name
                          ,[Age]
                          ,[Happiness]
                          ,[Hunger]
                          ,ls.Name LifeStage
                      FROM [TamagotchiDB].[tamagotchi].[dragons] d
                      INNER JOIN [TamagotchiDB].[tamagotchi].[lifestages] ls ON d.LifeStageId = ls.Id
                      WHERE d.Id = @id"
                        , new { id }
                    );

                if (result.AsList().Count == 0)
                    throw new DragonNotFoundException();

                return MapDragon(result);
            }
        }

        public async Task<IEnumerable<DragonViewModel>> GetDragonsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return await connection.QueryAsync<DragonViewModel>(
                    @"SELECT d.[Id] DragonId
                          ,d.[Name] Name
                          ,[Age]
                          ,[Happiness]
                          ,[Hunger]
                          ,ls.Name LifeStage
                      FROM [TamagotchiDB].[tamagotchi].[dragons] d
                      INNER JOIN [TamagotchiDB].[tamagotchi].[lifestages] ls ON d.LifeStageId = ls.Id");
            }
        }

        private static DragonViewModel MapDragon(dynamic result)
        {
            var dargon = new DragonViewModel
            {
                DragonId = result[0].DragonId,
                Name = result[0].Name,
                Age = result[0].Age,
                Happiness = result[0].Happiness,
                Hunger = result[0].Hunger,
                LifeStage = result[0].LifeStage
            };

            return dargon;
        }
    }
}
