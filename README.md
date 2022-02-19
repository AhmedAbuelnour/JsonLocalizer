# JsonLocalizer

#Nuget

You can install JsonLocalizer [from Nuget](https://www.nuget.org/packages/JsonLocalizer/) 


# Introduction
Json Localizer is a library, which make it easy to implement Json as a localization solution to your ASP.NET Core application.

# Getting started

- You need to make a folder named "Resources" in your wwwroot folder <br /><br />
![alt text](https://i.ibb.co/hmjvYk8/11.png)

- You list your json files, each file represents a language <br /> <br />
  - for English example <br />
![alt text](https://i.ibb.co/3WJj0L5/22.png)

  - for Arabic example <br />
![alt text](https://i.ibb.co/2jfnXNQ/33.png)

- You need to register the localizer in your services 
```
// you can pass as many as Cultures as you want. for this example, we only showing for English and Arabic
builder.Services.AddJsonLocalizer(builder.Environment.WebRootPath, new CultureInfo("en-US"), new CultureInfo("ar-EG"));
```
- example for using Jsonlocalizer on the default template 
```
public class WeatherForecastController : ControllerBase
{
    // the text here in this array, represents a key value in the json, the corresponding value will be retried by the Json Localize Manager
    private static readonly string[] Summaries = new[]
    {
       "freezing", "bracing", "chilly", "cool", "mild", "warm", "balmy", "hot", "sweltering", "scorching"
    };

    private readonly JsonLocalizerManager _resourceJsonManager;
    public WeatherForecastController(JsonLocalizerManager resourceJsonManager)
    {
       _resourceJsonManager = resourceJsonManager;
    }
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
       return Enumerable.Range(1, 5).Select(index => new WeatherForecast
       {
           Date = DateTime.Now.AddDays(index),
           TemperatureC = Random.Shared.Next(-20, 55),
           Summary = _resourceJsonManager[Summaries[Random.Shared.Next(Summaries.Length)]]
        }).ToArray();
    }
}
```
