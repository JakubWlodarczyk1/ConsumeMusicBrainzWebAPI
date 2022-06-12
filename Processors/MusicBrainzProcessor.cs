﻿using ConsumeMusicBrainzWebAPI.Models;
using MetaBrainz.MusicBrainz;

namespace ConsumeMusicBrainzWebAPI.Processors
{
    public class MusicBrainzProcessor
    {      
        public static async Task DoSearch(List<string> wordsList)
        {
            var songs = new List<MusicBrainzSongModel>();

            foreach (var word in wordsList)
            {
                if (!string.IsNullOrEmpty(word))
                {
                    var query = new Query("ConsumeMusicBrainzWebAPI", "1.0", "kubawlo1@gmail.com");
                    var recordings = await query.FindRecordingsAsync(word, 1);

                    if (recordings.Results.Count >= 1)
                    {
                        if (recordings.Results[0].Item != null && recordings.Results[0].Item.Releases != null && recordings.Results[0].Item.ArtistCredit[0].Name != null)
                        {
                            string? album = Convert.ToString(recordings.Results[0].Item.Releases[0].Title);
                            string? title = recordings.Results[0].Item.Title;
                            string? artist = recordings.Results[0].Item.ArtistCredit[0].Name;
                            songs.Add(new MusicBrainzSongModel(artist, title, album, word));
                        }
                        else
                        {
                            songs.Add(new MusicBrainzSongModel(word, false));
                        }
                    } 
                    else
                    {
                        songs.Add(new MusicBrainzSongModel(word, false));
                    }
                }
            }
            foreach (var item in songs)
            {
                if (item.IsFound != false)
                {
                    Console.WriteLine($"Search word: {item.Word} (Title: {item.TitleName} Artist: {item.ArtistName} Album: {item.AlbumName})");       
                } 
                else
                {
                    Console.WriteLine($"Search word: {item.Word} - No recording found!");
                }            
            }
            
        }
    }
}

//Parallel.For(0, number, async i =>
//{
//    using (HttpResponseMessage response = client.GetAsync(url).Result)
//    {
//        using (HttpContent content = response.Content)
//        {
//            var json = content.ReadAsStringAsync().Result;
//            dynamic dobj = JsonConvert.DeserializeObject<dynamic>(json);
//            //string word = dobj[0]["word"].ToString();
//            string[] word = json.Split("/n/n");
//            //JObject loObject = JObject.Parse(json);
//            //string word = loObject[0]["word"].ToString();
//            wordsList.Add(word[0]);
//        }
//    }
//string str = await client.DownloadString(url);
//dynamic dobj = JsonConvert.DeserializeObject<dynamic>(json);
//string word = dobj[0]["word"].ToString();
//wordsList.Add(word);
//});