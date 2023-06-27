namespace PlatinumBot.Data; 

public class EightBallResponse
{
    public string Response { get; set; } = string.Empty;
}

public class EightBallResponses
{
    public List<EightBallResponse> Responses { get; set; } = new List<EightBallResponse>();
}