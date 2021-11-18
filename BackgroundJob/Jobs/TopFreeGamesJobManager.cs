using Application.BackgroundService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundJob.Jobs
{
    public class TopFreeGamesJobManager
    {
        private readonly ITopGameService _topGameService;

        public TopFreeGamesJobManager(ITopGameService topGameService)
        {
            _topGameService = topGameService;
        }

        public async Task Run()
        {
           await _topGameService.AddTopTenFreeGameToDb();
        }
    }
}
