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
        public class Genre
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
        public class Images
        {
            public Jpg? jpg { get; set; }
            public Webp? webp { get; set; }
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
        public class Entry
        {
            public int? mal_id { get; set; }
            public string? type { get; set; }
            public string? name { get; set; }
            public string? url { get; set; }
        }



        public class External
        {
            public string? name { get; set; }
            public string? url { get; set; }
        }
        public class Items
        {
            public int? count { get; set; }
            public int? total { get; set; }
            public int? per_page { get; set; }
        }

        public class Author
        {
            public int? mal_id { get; set; }
            public string? type { get; set; }
            public string? name { get; set; }
            public string? url { get; set; }
        }
        public class Pagination
        {
            public int? last_visible_page { get; set; }
            public bool? has_next_page { get; set; }
            public int? current_page { get; set; }
            public Items? items { get; set; }
        }
        public class Published
        {
            public DateTime? from { get; set; }
            public DateTime? to { get; set; }
            public Prop? prop { get; set; }
            public string? @string { get; set; }
        }
        
        public class Serialization
        {
            public int? mal_id { get; set; }
            public string? type { get; set; }
            public string? name { get; set; }
            public string? url { get; set; }
        }
        public class Aired
        {
            public string? from { get; set; }
            public string? to { get; set; }
            public Prop? prop { get; set; }
        }

        public class Broadcast
        {
            public string? day { get; set; }
            public string? time { get; set; }
            public string? timezone { get; set; }
            public string? @string { get; set; }
        }


        public class Licensor
        {
            public int? mal_id { get; set; }
            public string? type { get; set; }
            public string? name { get; set; }
            public string? url { get; set; }
        }

        public class Producer
        {
            public int? mal_id { get; set; }
            public string? type { get; set; }
            public string? name { get; set; }
            public string? url { get; set; }
        }



        public class Relation
        {
            public string? relation { get; set; }
            public List<Entry>? entry { get; set; }
        }
        public class Streaming
        {
            public string? name { get; set; }
            public string? url { get; set; }
        }


        public class Theme2
        {
            public List<string?> openings { get; set; }
            public List<string?> endings { get; set; }
        }

        public class Trailer
        {
            public string? youtube_id { get; set; }
            public string? url { get; set; }
            public string? embed_url { get; set; }
        }
        public class Theme
        {
            public int? mal_id { get; set; }
            public string? type { get; set; }
            public string? name { get; set; }
            public string? url { get; set; }
        }
        public class Studio
        {
            public int? mal_id { get; set; }
            public string? type { get; set; }
            public string? name { get; set; }
            public string? url { get; set; }
        }
    }
}
