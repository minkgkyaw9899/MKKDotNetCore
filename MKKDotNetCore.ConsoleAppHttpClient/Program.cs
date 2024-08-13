// See https://aka.ms/new-console-template for more information

using MKKDotNetCore.ConsoleAppHttpClient;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

var client = new HttpClient()
{
    BaseAddress = new Uri("http://localhost:5247/api/LatHtaukBayDin")
};


var response = await client.GetAsync("/Questions");

if (response.IsSuccessStatusCode)
{
    var jsonString = await response.Content.ReadAsStringAsync();
    
    var model = JsonConvert.DeserializeObject<RootDto>(jsonString);

    Console.WriteLine(model?.questions);
}

Console.ReadLine();

namespace MKKDotNetCore.ConsoleAppHttpClient
{
    public class RootDto
    {
        public Questions[] questions { get; set; }
        public Answers[] answers { get; set; }
        public string[] numberList { get; set; }
    }

    public class Questions
    {
        public int questionNo { get; set; }
        public string questionName { get; set; }
    }

    public class Answers
    {
        public int questionNo { get; set; }
        public int answerNo { get; set; }
        public string answerResult { get; set; }
    }
}