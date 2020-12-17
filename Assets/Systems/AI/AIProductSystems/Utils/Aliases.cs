

//// utils, logs and aliases
//public partial class AutoUpgradeProductsSystem
//{
//    static T PickMostImportantValue<T>(Dictionary<T, long> values)
//    {
//        long value = 0;
//        T goal = default;

//        foreach (var pair in values)
//        {
//            if (pair.Value > value)
//            {
//                value = pair.Value;
//                goal = pair.Key;
//            }
//        }

//        return goal;
//    }

//    int GetDate()
//    {
//        return ScheduleUtils.GetCurrentDate(gameContext);
//    }

//    // logging
//    TestComponent GetLogs()
//    {
//        return gameContext.GetEntities(GameMatcher.Test)[0].test;
//    }

//    bool GetLog(LogTypes logTypes)
//    {
//        return GetLogs().logs[logTypes];
//    }
//}
