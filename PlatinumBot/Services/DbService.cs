using Newtonsoft.Json;
using PlatinumBot.Data;

namespace PlatinumBot.Services;

public class DbService 
{
    public IDictionary<String, ArknightsOperator> ArknightsOperators { get; private set; } = new Dictionary<String, ArknightsOperator>(); 
    public List<String> EightBallResponses {get; private set;} = new List<String>();    
    public DbService()
    {

    }

    public void Init() 
    {
        var jsonLoader = new JSONLoader();
        try 
        {
            jsonLoader.Load("./data/eightball.json");
            if(jsonLoader.JsonData is not null && jsonLoader.JsonData != string.Empty)
            {
                EightBallResponses? responses = JsonConvert.DeserializeObject<EightBallResponses>(jsonLoader.JsonData);
                if(responses is not null)
                {
                    foreach(var response in responses.Responses)
                    {
                        EightBallResponses.Add(response.Response);
                    }
                }
            }
            
            
            //jsonLoader.Load("./data/operator_overviews.json");
            //We do stuff here for ops
        }
        catch(Exception ex) 
        {
            throw new Exception(ex.Message);
        }
            
    }
}

public class JSONLoader
{
    public string JsonData {get; private set;} = string.Empty;
    public void Load(string path)
    {
        using StreamReader streamReader = new(path);
        JsonData = streamReader.ReadToEnd();
    }
}