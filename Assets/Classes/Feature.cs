using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Classes
{
    enum FeatureStatus
    {
        NeedsExploration,
        Explored
    }

    enum RelevancyStatus
    {
        Innovative,
        Relevant,
        Outdated,
        Dinosaur
    }

    class Feature
    {
        RelevancyStatus Relevancy;
        FeatureStatus Status;
        bool IsImplemented;

        public Feature(RelevancyStatus relevancy, FeatureStatus status, bool isImplemented)
        {
            Status = status;
            Relevancy = relevancy;
            IsImplemented = isImplemented;
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

        string GetLiteralRelevancy()
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
