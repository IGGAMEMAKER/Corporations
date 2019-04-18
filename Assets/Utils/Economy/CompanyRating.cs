namespace Assets.Utils
{
    public static partial class CompanyEconomyUtils
    {
        public static int GetCompanyRating(int companyId)
        {
            return UnityEngine.Random.Range(1, 6);
        }
    }
}
