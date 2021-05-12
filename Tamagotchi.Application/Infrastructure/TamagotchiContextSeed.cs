using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tamagotchi.Domain.AggregatesModel.DragonAggregate;
using Tamagotchi.Domain.Common;
using Tamagotchi.Infrastructure;

namespace Tamagotchi.Application.Infrastructure
{
    public class TamagotchiContextSeed
    {
        public async Task SeedAsync(TamagotchiContext context, IWebHostEnvironment env, ILogger<TamagotchiContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(TamagotchiContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                using (context)
                {
                    context.Database.Migrate();

                    //Seed data
                    if (!context.LifeStages.Any())
                    {
                        context.LifeStages.AddRange(Enumeration.GetAll<LifeStage>());

                        await context.SaveChangesAsync();
                    }
                }
            });
        }

        private AsyncRetryPolicy CreatePolicy(ILogger<TamagotchiContextSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }
    }
}
