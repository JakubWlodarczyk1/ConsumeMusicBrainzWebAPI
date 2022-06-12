using Newtonsoft.Json;

namespace ConsumeMusicBrainzWebAPI.Models
{
    public class RandomWordsModel
    {
        [JsonProperty("word")]
        public string Word { get; set; }

    }
}
