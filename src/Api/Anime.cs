﻿using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Kitsu.Api
{
    public class Anime
    {
        public static async Task<dynamic> GetSeason(Season season, int year, string customFilter = null)
        {
            var filter = !string.IsNullOrWhiteSpace(customFilter) ? customFilter : $"?filter[seasonYear]={year}&filter[season]={season}";
            
            var json = await Kitsu.Client.GetStringAsync($"{Kitsu.BaseUri}/anime{filter}&page[limit]=20&page[offset]=0");

            if (season == Season.year && !string.IsNullOrWhiteSpace(customFilter))
            {
                json = await Kitsu.Client.GetStringAsync($"{Kitsu.BaseUri}/anime?filter[seasonYear]={year}&page[limit]=20&page[offset]=0");
            }
            
            return JsonConvert.DeserializeObject(json);
        }

        public static async Task<dynamic> GetSeasonNextPage(string next)
        {
            var json = await Kitsu.Client.GetStringAsync(next);
            return JsonConvert.DeserializeObject(json);
        }

        public static async Task<dynamic> GetAnime(int animeId)
        {
            var json = await Kitsu.Client.GetStringAsync($"{Kitsu.BaseUri}/anime/{animeId}");
            return JsonConvert.DeserializeObject(json);
        }
    }
}
