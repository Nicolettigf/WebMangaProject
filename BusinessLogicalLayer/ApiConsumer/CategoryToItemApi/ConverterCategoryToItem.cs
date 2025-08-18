using Entities;
using static Entities.MediaBase;

namespace BusinessLogicalLayer.ApiConsumer.MangaApi.MangaCategoryApi
{
    public class ConverterGenreToItem
    {
        public static List<Genre> CovertiMangaCate(RootMA Cate)
        {
            List<Genre> category = new();
            foreach (var item in Cate.data)
            {
                Genre c = new Genre();
                c.Id = Convert.ToInt32(item.id);
                category.Add(c);
            }
            return category;
        }
    }
}
