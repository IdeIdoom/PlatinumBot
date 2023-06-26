using PlatinumBot.Services;

namespace PlatinumBot.Data;

public class ArknightsOperator
{
    public string Name { get; private set;}
    public string Description { get; private set;}

    public ArknightsOperator() 
    {
        Name = "";
        Description = "";
    }

    public ArknightsOperator(string name, string description)
    {
        Name = name;
        Description = description;
    }
    public void LoadFromJson(JSONLoader jsonLoader)
    {
        
    }
}