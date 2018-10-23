using System.Collections.Generic;

public interface CommandHandler
{
    void HandleCommand(string eventName, Dictionary<string, object> parameters);
}
