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

        public string Icon
        {
            get
            {
                switch (Info)
                {
                    case InterruptInfo.CanUpgradeFeature:
                        return "CanUpgradeFeature";
                    default:
                        return "UnknownIcon";
                }
            }
        }

        public InterruptIcon (InterruptInfo info, InterruptImportance importance)
        {
            Importance = importance;
            Info = info;
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
        CanUpgradeFeature,

    }
}
