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

    class Feature
    {
        const int RELEVANCY_RELEVANT = 0;
        const int RELEVANCY_SLIGHTLY_OUTDATED = -1;
        const int RELEVANCY_VASTLY_OUTDATED = -2;

        int Relevancy;
        FeatureStatus Status;

        public Feature(int relevancy, FeatureStatus status)
        {
            Status = status;
            Relevancy = relevancy;
        }

        public void Explore ()
        {
            Status = FeatureStatus.Explored;
        }

        public void Update ()
        {
            Status = FeatureStatus.NeedsExploration;
            Relevancy = RELEVANCY_RELEVANT;
        }

        public void Outdate ()
        {
            if (Relevancy == RELEVANCY_RELEVANT)
                Relevancy = RELEVANCY_SLIGHTLY_OUTDATED;
            else
                Relevancy = RELEVANCY_VASTLY_OUTDATED;

            Status = FeatureStatus.NeedsExploration;
        }

        public bool IsRelevant ()
        {
            return Relevancy == RELEVANCY_RELEVANT;
        }
    }
}
