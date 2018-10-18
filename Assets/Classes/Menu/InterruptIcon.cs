using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Classes.Menu
{
    class InterruptIcon
    {
        InterruptImportance Importance;
        InterruptInfo Info;

        public InterruptIcon (InterruptInfo info, InterruptImportance importance)
        {
            Importance = importance;
            Info = info;
        }

        public string PrefabName
        {
            get
            {
                switch (Importance)
                {
                    case InterruptImportance.Danger:
                        return "DangerInterrupt";
                    case InterruptImportance.Positive:
                        return "PossibilityInterrupt";
                    case InterruptImportance.Warning:
                        return "WarningInterrupt";
                    default:
                        return "UnknownInterrupt";
                }
            }
        }
    }

    public enum InterruptImportance
    {
        Danger,
        Warning,
        Positive
    }

    public enum InterruptInfo
    {

    }
}
