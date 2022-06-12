using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumeMusicBrainzWebAPI.Models
{
    public class MusicBrainzSongModel
    {
        public string? ArtistName { get; set; }
        public string? TitleName { get; set; }
        public string? AlbumName { get; set; }
        public string Word { get; set; }
        public bool IsFound { get; set; }

        public MusicBrainzSongModel(string artistName, string titleName, string albumName, string word, bool isFound = true)
        {
            ArtistName = artistName;
            TitleName = titleName;
            AlbumName = albumName;
            Word = word;
            IsFound = isFound;
        }

        public MusicBrainzSongModel(string word, bool isFound = false)
        {
            Word = word;
            IsFound = isFound;
        }
    }
}
