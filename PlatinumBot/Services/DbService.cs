using Newtonsoft.Json;
using PlatinumBot.Data;

namespace PlatinumBot.Services;

public class DbService 
{
    public IDictionary<String, ArknightsOperator> ArknightsOperators { get; private set; }
    public List<String> EightBallResponses {get; private set;}
    public DbService()
    {
        ArknightsOperators = new Dictionary<String, ArknightsOperator>();
        EightBallResponses = new List<String>();
    }

    public void Init() 
    {
        var jsonLoader = new JSONLoader();
        try 
        {
            jsonLoader.Load("./data/eightball.json");
            EightBallResponses? responses = JsonConvert.DeserializeObject<EightBallResponses>(jsonLoader.JsonData);
            foreach(var response in responses.Responses)
            {
                EightBallResponses.Add(response.Response);
            }
            //jsonLoader.Load("./data/operator_overviews.json");
            //We do stuff here for ops
        }
        catch(Exception ex) 
        {

        }
            
    }
}

public class JSONLoader
{
    public string? JsonData {get; private set;}
    public void Load(string path)
    {
        using StreamReader streamReader = new(path);
        JsonData = streamReader.ReadToEnd();
    }
}