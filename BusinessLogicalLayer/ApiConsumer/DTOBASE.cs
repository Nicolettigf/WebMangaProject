using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.ApiConsumer
{
    public abstract class DTOBASE
    {
        public class Demographic
        {
            public int? mal_id { get; set; }
            public string? type { get; set; }
            public string? name { get; set; }
            public string? url { get; set; }
        }
        public class ExplicitGenre
        {
            public int? mal_id { get; set; }
            public string? type { get; set; }
            public string? name { get; set; }
            public string? url { get; set; }
        }
        public class From
        {
            public int? day { get; set; }
            public int? month { get; set; }
            public int? year { get; set; }
        }
        public class Genre
        {
            public int? mal_id { get; set; }
            public string? type { get; set; }
            public string? name { get; set; }
            public string? url { get; set; }
        }

        public class Images
        {
            public Jpg jpg { get; set; }
            public Webp webp { get; set; }
        }
        public class Jpg
        {
            public string? image_url { get; set; }
            public string? small_image_url { get; set; }
            public string? large_image_url { get; set; }
        }

        public class Prop
        {
            public From? from { get; set; }
            public To? to { get; set; }
            public string? @string { get; set; }
        }
        public class Webp
        {
            public string? image_url { get; set; }
            public string? small_image_url { get; set; }
            public string? large_image_url { get; set; }
        }
        public class To
        {
            public int? day { get; set; }
            public int? month { get; set; }
            public int? year { get; set; }
        }
        public class Title
        {
            public string? type { get; set; }
            public string? title { get; set; }
        }
        public class Theme
        {
            public int? mal_id { get; set; }
            public string? type { get; set; }
            public string? name { get; set; }
            public string? url { get; set; }
        }
    }
}
