using PlatinumBot.Services;

namespace PlatinumBot.Data;

public class ArknightsOperator
{
    public string Name { get; set;} = "";
    public string Description { get; set;} = "";
}

public class ArknightsOperatorList
{
    public List<ArknightsOperator> operators = new List<ArknightsOperator>();
}

public class CG 
{
    public string Name { get; set;} = "";
    public string Filename {get; set;} = "";
}

public class OperatorCG
{
    public string Name { get; set;} = "";
    public List<CG> CGs = new List<CG>();
}


public class OperatorCGList
{
    public List<OperatorCG> operators = new List<OperatorCG>();
}
