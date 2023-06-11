using PlatinumBot.Modules;

namespace PlatinumBot.Modules;

public class JSONLoader
{
    public void Load(string path)
    {

    }

    public Operator GetOperator(string name)
    {
        Operator? arknightsOp = null;
        return arknightsOp;
    }
}

public class Operator
{
    public string Name { get; private set;}
    public string Description { get; private set;}

    public Operator(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
