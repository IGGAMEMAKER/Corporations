﻿using Entitas;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        static float GetRandomColor(int companyId, int mixer)
        {
            return GetHashedRandom(companyId, mixer);
        }

        public static int GetCEOId(GameEntity company)
        {
            return company.cEO.HumanId;
        }

        public static int GetCompanyExpertise (GameEntity company)
        {
            //var CEOId = 
            int companyId = company.company.Id;
            int CEOId = GetCEOId(company);

            return 100 + (int)(30 * GetHashedRandom2(companyId, CEOId));
        }

        public static Color GetCompanyUniqueColor(int companyId)
        {
            var r = GetRandomColor(companyId, companyId);
            var g = GetRandomColor(companyId, companyId + 1);
            var b = GetRandomColor(companyId, companyId + 2);

            return new Color(r, g, b);
        }

        // 0... 1
        static float GetHashedRandom(int companyId, int mixer)
        {
            var C = 0.61f;

            var K = companyId + mixer + GetSeedValue();

            return ((C * K) % 1);
        }

        static float GetHashedRandom2(int companyId, int mixer)
        {
            var C = 0.58f;

            var K = 2 * companyId + mixer + GetSeedValue2();

            return ((C * K) % 1);
        }

        static float GetRandomAcquisitionPriceModifier(int companyId, int shareholderId)
        {
            var min = 0.9f;
            var max = 3f;

            var M = max - min;
            var C = 0.61f;
            var K = companyId + shareholderId + GetSeedValue();

            return min + M * ((C * K) % 1);

            return 1;
            //return Mathf.Clamp(value, 0.9f, 3f);
        }

        static int GetSeedValue()
        {
            return DateTime.Now.Hour;
        }

        static int GetSeedValue2()
        {
            return DateTime.Now.Hour;
        }

        public static long GetRandomValue (long baseValue, int id1, int id2, long min = 40, long max = 135)
        {
            return (long)(baseValue * (min + (max - min) * GetHashedRandom2(id1, id2)));
        }
    }
}
