using System.Collections.Generic;

public interface ICommandHandler
{
    void HandleCommand(string eventName, Dictionary<string, object> parameters);
}
