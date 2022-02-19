using System.Text.Json.Nodes;
namespace JsonLocalizer;
public class JsonLocalizerManager
{
    private readonly JsonNode? JsonReader;
    public JsonLocalizerManager(string WebRootPath, string defaultLang)
    {
        JsonReader ??= JsonNode.Parse(json: File.ReadAllText(Path.Combine(WebRootPath, "Resources", $"{defaultLang}.json")));
    }
    public string this[string text]
    {
        get => JsonReader[text]?.ToString() ?? text;
    }
}

