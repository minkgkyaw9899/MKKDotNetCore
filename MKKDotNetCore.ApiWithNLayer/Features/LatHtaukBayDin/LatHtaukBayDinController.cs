using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;

namespace MKKDotNetCore.ApiWithNLayer.Features.LatHtaukBayDin;

[Route("api/[controller]")]
[ApiController]
public class LatHtaukBayDinController : Controller
{
    private async Task<LatHtaukBayDinModel?> GetDataAsync()
    {
        var jsonStr = await System.IO.File.ReadAllTextAsync("Db/data.json");
        var model = JsonConvert.DeserializeObject<LatHtaukBayDinModel>(jsonStr);

        return model;
    }

    [HttpGet("Questions")]
    public async Task<IActionResult> GetQuestions()
    {
        var model = await GetDataAsync();
        return Ok(model?.questions);
    }

    [HttpGet("NumbersList")]
    public async Task<IActionResult> GetNumbersList()
    {
        var model = await GetDataAsync();
        return Ok(model?.numberList);
    }

    [HttpGet("{question}/{no}")]
    public async Task<IActionResult> GetAnswer(int question, int no)
    {
        var model = await GetDataAsync();

        var result = model?.answers.FirstOrDefault(item => item.answerNo == no && item.questionNo == question);

        return Ok(result);
    }

    // private int ToNumber(string No)
    // {
    //     No = No.Replace("၀", "0");
    //     No = No.Replace("၁", "1");
    //     No = No.Replace("၂", "2");
    //     No = No.Replace("၃", "3");
    //     No = No.Replace("၄", "4");
    //     No = No.Replace("၅", "5");
    //     No = No.Replace("၆", "6");
    //     No = No.Replace("၇", "7");
    //     No = No.Replace("၈", "8");
    //     No = No.Replace("၉", "9");
    //
    //     return Convert.ToInt32(No);
    // }
}