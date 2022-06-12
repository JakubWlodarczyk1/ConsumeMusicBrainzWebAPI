using ConsumeMusicBrainzWebAPI.Helpers;
using ConsumeMusicBrainzWebAPI.Models;
using Newtonsoft.Json;

namespace ConsumeMusicBrainzWebAPI.Processors
{
    public class RandomWordsProcessor 
    {
        public static void ShowListElements(List<string> wordsList)
        {
            Console.WriteLine("List of random words: \n{");
            foreach (var words in wordsList)
            {
                Console.WriteLine(words);
            }
            Console.WriteLine("}");
        }
        public static int AddToList(string word, int i, int wordsNumber, RandomWordsData randomWordsData)
        {
            if (!randomWordsData.wordsList.Contains(word))
            {
                randomWordsData.wordsList.Add(word);              
                if (i == wordsNumber - 1) ShowListElements(randomWordsData.wordsList);
            }
            else
            {
               return i--;
            }
            return i;
        }

        public static async Task LoadRandomWords(int wordsNumber = 0)
        {
            RandomWordsData randomWordsData = new RandomWordsData();
            string url = "";

            if (wordsNumber >= 5 && wordsNumber <= 20)
            {
                url = "https://random-words-api.vercel.app/word";
                for (int i = 0; i < wordsNumber; i++)
                {
                    using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string jsonResponse = await response.Content.ReadAsStringAsync();
                            var randomWords = JsonConvert.DeserializeObject<RandomWordsModel[]>(jsonResponse);
                            AddToList(randomWords[0].Word, i, wordsNumber, randomWordsData);
                        }
                        else
                        {
                            throw new Exception(response.ReasonPhrase);
                        }
                    }
                }
                randomWordsData.wordsList.Sort();
                await MusicBrainzProcessor.DoSearch(randomWordsData.wordsList);
            }
            else
            {
                Console.WriteLine("Wrong words number");
            }
        }
    }
}
