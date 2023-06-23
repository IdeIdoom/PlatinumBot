namespace PlatinumBot.Services;

public interface ICommandHandler
{
    Task InitializeAsync();
    void SetPrefix(String prefix);

}
