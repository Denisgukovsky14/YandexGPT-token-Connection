using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpeedReaderTRY
{

    public class YandexGptClient
    {
        private static readonly string MyToken = "Ваш токен";
        private static readonly string MyFolderId = "Ваша директория";
        private static readonly string ApiUrl = "https://llm.api.cloud.yandex.net/foundationModels/v1/completion";

        public async Task<string> AskYandexGpt(string prompt)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Api-Key {MyToken}");
                client.DefaultRequestHeaders.Add("x-folder-id", MyFolderId);

                var requestBody = new
                {
                    modelUri = $"gpt://{MyFolderId}/yandexgpt-lite",
                    completionOptions = new
                    {
                        stream = false,
                        temperature = 0.6,
                        maxTokens = 2000
                    },
                    messages = new[]
                    {
                    new
                    {
                        role = "user",
                        text = prompt
                    }
                }
                };

                var json = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(ApiUrl, content);
                var responseJson = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Ошибка: {responseJson}");
                }

                dynamic result = JsonConvert.DeserializeObject(responseJson);
                return result.result.alternatives[0].message.text;
            }
        }

    }

    class Program
    {
        static async Task Main(string[] args)
        {


            await TryToAsk();

            Console.ReadKey();

        }

        public static async Task TryToAsk()
        {
            var gpt = new YandexGptClient();
            string answer = await gpt.AskYandexGpt("Ваш запрос пишется сюда");
            Console.WriteLine(answer);
        }

    }
}
