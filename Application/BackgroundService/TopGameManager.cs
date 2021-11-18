using Domain;
using Domain.Entities;
using Domain.IRepositories;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Application.BackgroundService
{
    public class TopGameManager : ITopGameService
    {
        //PLAY STORE TOP FREE GAMES URL
        private const string  Url = "https://play.google.com/store/apps/collection/cluster?clp=0g4cChoKFHRvcHNlbGxpbmdfZnJlZV9HQU1FEAcYAw%3D%3D:S:ANO1ljJ_Y5U&gsr=Ch_SDhwKGgoUdG9wc2VsbGluZ19mcmVlX0dBTUUQBxgD:S:ANO1ljL4b8c";

        private readonly ITopListDetailsCommandMongoRepository _topListDetailsCommandRepository;

        public TopGameManager(ITopListDetailsCommandMongoRepository topListDetailsCommandRepository)
        {
            _topListDetailsCommandRepository = topListDetailsCommandRepository;
        }

        public async Task<bool> AddTopTenFreeGameToDb()
        {
            var topGamesListDetail = await GetFromListTopFreeGames();

            if (topGamesListDetail == null) return false;
            
            await _topListDetailsCommandRepository.AddTopGames(topGamesListDetail);

            return true;
        }
        
        public async Task<GameListDetails> GetFromListTopFreeGames()
        {
            var appListLinks = await TopAppLinkManager.GetTopAppsFromPlayStoreUrl(Url);

            var appsUrlList = await TopAppLinkManager.GetAppsUrlFromHtml(appListLinks);

            var appUrlList = new List<string>();

            foreach (var appUrl in appsUrlList)
            {
                var playStoreAppUrl = appUrl.Insert(0, "https://play.google.com");
                appUrlList.Add(playStoreAppUrl);
            }
            
            return await GetGamesDetailsAndAddGamesToList(appUrlList);
        }
        
        private async Task<GameListDetails> GetGamesDetailsAndAddGamesToList(List<string> appUrlList)
        {

            GameListDetails topGameListDetail = new GameListDetails();
            List<GameDetails> listGameAdd = new List<GameDetails>();
            
            GameDetails gameDetails;
            GameMutableDetails gameMutableDetails;

            var httpClientFor = new HttpClient();
            var htmlDocumentFor = new HtmlDocument();
            var createdDate = DateTime.Now;
            
            foreach (var urlOfGame in appUrlList)
            {
                var htmlFor = await httpClientFor.GetStringAsync(urlOfGame);

                htmlDocumentFor.LoadHtml(htmlFor);

                var gameTitle = htmlDocumentFor.DocumentNode.SelectSingleNode(".//h1[@class='AHFaub']").InnerText;

                var detailDiv = htmlDocumentFor.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "").Equals("DWPxHb")).ToList();

                var detail = detailDiv[0].SelectSingleNode(".//span").InnerText;

                var totalReview = htmlDocumentFor.DocumentNode.SelectSingleNode(".//span[@class='AYi5wd TBRnV']").InnerText;

                //additionalInformation
                var additionalInformation = htmlDocumentFor.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "").Equals("hAyfc")).ToList();

                //Last UPDATE
                var lastUpdate = additionalInformation[0].SelectSingleNode(".//span").InnerText;
                //SIZE
                var size = additionalInformation[1].SelectSingleNode(".//span").InnerText;
                //Install count
                var installCount = additionalInformation[2].SelectSingleNode(".//span").InnerText;
                //Current version
                var currentVersion = additionalInformation[3].SelectSingleNode(".//span").InnerText;
                //Author-Developer
                var author = additionalInformation[5].SelectSingleNode(".//span").InnerText;

                var urlSplit = urlOfGame.Split("id=");
                var trackId = urlSplit[1];

                gameDetails = new GameDetails();
                gameMutableDetails = new GameMutableDetails();

                gameDetails.TrackId = trackId;
                gameDetails.Title = gameTitle;
                gameDetails.Author = author;


                gameMutableDetails.Description = detail;
                gameMutableDetails.TotalReviewCount = totalReview;
                gameMutableDetails.TotalInstallCount = installCount;
                gameMutableDetails.CurrentVersion = currentVersion;
                gameMutableDetails.LastUpdateDate = lastUpdate;
                gameMutableDetails.Size = size;
                gameMutableDetails.AppDetailsCreateDate = createdDate;

                gameDetails.GameMutableDetails = gameMutableDetails;

                listGameAdd.Add(gameDetails);
            }

            topGameListDetail.CreateDate = createdDate;
            topGameListDetail.GameDetailsList = listGameAdd;

            return topGameListDetail;
        }
    }
}
