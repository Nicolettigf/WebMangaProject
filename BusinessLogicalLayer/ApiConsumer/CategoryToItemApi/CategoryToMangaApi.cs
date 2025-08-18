using BusinessLogicalLayer.ApiConsumer.MangaApi.MangaCategoryApi;
using Entities;
using Newtonsoft.Json;
using static Entities.MediaBase;
namespace BusinessLogicalLayer.ApiConsumer.MangaApi
{
    public class GenreToMangaApi
    {
        public static async Task<List<Genre>> MangaCategory(int Id)
        {
            Uri baseAddress = new Uri("https://kitsu.io/api/edge/manga/");
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                using (var response = await httpClient.GetAsync($"{Id}/relationships/categories"))
                {
                    string jsonString = await response.Content.ReadAsStringAsync();
                    if (jsonString.Contains("errors"))
                    {
                    }
                    else
                    {
                        RootMA? mangaRootDTO = JsonConvert.DeserializeObject<RootMA>(jsonString);
                        //Ou pegar em lista ou convert um por um pois ta fazendo lista de um so sempre
                        List<Genre> CateReturn = ConverterGenreToItem.CovertiMangaCate(mangaRootDTO);
                        //BLL
                        return CateReturn;
                    }
                }
            }
            return null;
        }
    }
}
