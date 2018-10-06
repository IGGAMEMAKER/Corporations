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

        public Feature(RelevancyStatus relevancy, FeatureStatus status)
        {
            Status = status;
            Relevancy = relevancy;
        }

        public void Explore()
        {
            Status = FeatureStatus.Explored;
        }

        public void Update()
        {
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
    }
}
