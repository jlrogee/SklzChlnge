using System;
using MongoDB.Bson.Serialization.Attributes;

namespace src.DAL
{
    public class Link
    {
        public string ShortLink { get; set; }
        public string OriginalLink { get; set; }
        public int HitCount { get; set; }
        public string UserId { get; set; }
    }
}