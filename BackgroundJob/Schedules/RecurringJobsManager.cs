using BackgroundJob.Jobs;
using Hangfire;

namespace BackgroundJob.Schedules
{
    public static class RecurringJobsManager
    {
        public static void TopFreeGamesOperation()
        {
            RecurringJob.AddOrUpdate<TopFreeGamesJobManager>(nameof(TopFreeGamesJobManager),
                job => job.Run(), Cron.Hourly);
        }
    }
}
