using PlatinumBot.Modules;

namespace PlatinumBot.Modules;

public class Operator
{
    public string Name { get; private set;}
    public string Description { get; private set;}

    public Operator() 
    {
        Name = "";
        Description = "";
    }

    public Operator(string name, string description)
    {
        Name = name;
        Description = description;
    }
    public void LoadFromJson(JSONLoader jsonLoader)
    {

    }
}