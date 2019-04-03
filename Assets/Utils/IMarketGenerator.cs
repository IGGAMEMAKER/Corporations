public interface IMarketGenerator
{
    // Attach niche to industry as independent chain
    // ----
    //          | -- niche1
    // Industry | -- niche
    //          | -- niche2
    // ----
    void AttachNicheToIndustry(NicheType niche, IndustryType industry);

    void ForkNiche(NicheType parent, NicheType child);
    
    // all child niches will try to be the next big thing
    // this means that clients will move from one niche to another
    //
    // NOTE: this sets parent AND its nearest childs as competing
    void SetChildsAsCompetingNiches(NicheType parent);

    // This means, that this niche will not outgrow sourceNiche by number of clients
    // dependency specifies percentage of clients,
    // aka if dependency is 20, max size of this niche will be less than 20% of sourceNiche
    void SetNicheAsDependant(NicheType niche, NicheType sourceNiche, int dependency = 100);

    // Helpful for cross promotions
    // Companies in these niches will be able to exchange their audiences way more effectively
    // compatibility 0 - 100
    void SetNichesAsSynergic(NicheType niche1, NicheType niche2, int compatibility);
}
