using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Classes
{
    public enum FeatureStatus
    {
        NeedsExploration,
        Explored
    }

    public enum RelevancyStatus
    {
        Innovative,
        Relevant,
        Outdated,
        Dinosaur
    }

    public class Feature
    {
        RelevancyStatus Relevancy;
        public FeatureStatus Status
        {
            get;
            internal set;
        }

        public bool IsInnovative
        {
            get
            {
                return Relevancy == RelevancyStatus.Innovative;
            }
        }
        public bool NeedsToUpgrade
        {
            get
            {
                return Relevancy == RelevancyStatus.Dinosaur || Relevancy == RelevancyStatus.Outdated;
            }
        }
        public bool CanMakeBreakthrough
        {
            get
            {
                return Relevancy == RelevancyStatus.Relevant;
            }
        }

        public bool NeedsExploration
        {
            get
            {
                return Status == FeatureStatus.NeedsExploration;
            }
        }

        bool IsImplemented;

        public string name;



        public Feature(string name, RelevancyStatus relevancy, FeatureStatus status, bool isImplemented)
        {
            Status = status;
            Relevancy = relevancy;
            IsImplemented = isImplemented;
            this.name = name;
        }

        public void Explore()
        {
            Status = FeatureStatus.Explored;
        }

        public void Update()
        {
            IsImplemented = true;
            Status = FeatureStatus.NeedsExploration;

            if (Relevancy == RelevancyStatus.Relevant)
                Relevancy = RelevancyStatus.Innovative;
            else
                Relevancy = RelevancyStatus.Relevant;
        }

        public void Outdate()
        {
            if (Relevancy == RelevancyStatus.Innovative)
                Relevancy = RelevancyStatus.Relevant;
            else if (Relevancy == RelevancyStatus.Relevant)
                Relevancy = RelevancyStatus.Outdated;
            else
                Relevancy = RelevancyStatus.Dinosaur;

            Status = FeatureStatus.NeedsExploration;
        }

        public bool IsNotOutdated()
        {
            return Relevancy == RelevancyStatus.Relevant || Relevancy == RelevancyStatus.Innovative;
        }

        public string GetLiteralRelevancy()
        {
            switch (Relevancy)
            {
                case RelevancyStatus.Dinosaur: return "Dinosaur";
                case RelevancyStatus.Innovative: return "Innovative";
                case RelevancyStatus.Outdated: return "Outdated";
                case RelevancyStatus.Relevant: return "Relevant";

                default: return "Unknown";
            }
        }

        string GetLiteralStatus()
        {
            switch (Status)
            {
                case FeatureStatus.Explored: return "Explored";
                case FeatureStatus.NeedsExploration: return "Needs Exploration";

                default: return "Unknown";
            }
        }

        public string GetLiteralFeatureStatus()
        {
            return GetLiteralRelevancy() + " " + GetLiteralStatus();
        }
    }
}
