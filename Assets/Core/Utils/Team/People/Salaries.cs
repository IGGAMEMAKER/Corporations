using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static void AddOrReplaceOffer(GameEntity company, HumanFF human, ExpiringJobOffer o)
        {
            int index = human.WorkerOffersComponent.Offers
                .FindIndex(o1 => o1.CompanyId == company.company.Id && o1.HumanId == human.HumanComponent.Id);

            if (index == -1)
                human.WorkerOffersComponent.Offers.Add(o);
            else
                human.WorkerOffersComponent.Offers[index] = o;

            //Debug.Log($"Offer to {Humans.GetFullName(human)} ({human.workerOffers.Offers.Count}): {company.company.Name}");
        }

        public static void SendJobOffer(HumanFF worker, JobOffer jobOffer, GameEntity company, GameContext gameContext)
        {
            var offer = new ExpiringJobOffer
            {
                JobOffer = jobOffer,
                CompanyId = company.company.Id,
                DecisionDate = ScheduleUtils.GetCurrentDate(gameContext) + 30,
                HumanId = worker.HumanComponent.Id
            };

            AddOrReplaceOffer(company, worker, offer);
        }

        public static void SetJobOffer(HumanFF human, GameEntity company, JobOffer offer, int teamId, GameContext gameContext)
        {
            var o = new ExpiringJobOffer
            {
                Accepted = true,

                JobOffer = offer,
                CompanyId = company.company.Id,
                HumanId = human.HumanComponent.Id,
                DecisionDate = -1
            };

            AddOrReplaceOffer(company, human, o);

            Economy.UpdateSalaries(company, gameContext);
        }



        public static float GetPersonalSalaryModifier(HumanFF human)
        {
            float modifier = 0;

            bool isShy = Humans.HasTrait(human, Trait.Shy);
            bool isGreedy = Humans.HasTrait(human, Trait.Greedy);

            if (isShy)
            {
                modifier -= 0.3f;
            }

            if (isGreedy)
            {
                modifier += 0.3f;
            }

            return modifier;
        }

        public static long GetSalaryPerRating(HumanFF human) => GetSalaryPerRating(human, Humans.GetRating(human));
        public static long GetSalaryPerRating(HumanFF human, long rating)
        {
            float modifier = GetPersonalSalaryModifier(human);

            return GetSalaryPerRating(rating, modifier);
        }
        public static long GetSalaryPerRating(long rating, float modifier = 0)
        {
            var baseSalary = Mathf.Pow(500, 1f + rating / 100f);

            return (long)(baseSalary * (1f + modifier) / 4);
        }
    }
}
