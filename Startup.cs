using ConsumeMusicBrainzWebAPI.Helpers;
using ConsumeMusicBrainzWebAPI.Processors;

ApiHelper.InitializeClient();

Console.WriteLine("Type number of words between 5 and 20:");
await RandomWordsProcessor.LoadRandomWords(Convert.ToInt32(Console.ReadLine()));

Console.WriteLine("Press any key to close console");

Console.ReadKey();