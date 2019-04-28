using System;
using System.Linq;

namespace src.BL
{
    public class ShortLinkGenerator : IShortLinkGenerator
    {
        private const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        
        public string GetShortLinkName(int length)
        {
            var random = new Random();
            return new string(Enumerable.Repeat(ALPHABET, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}