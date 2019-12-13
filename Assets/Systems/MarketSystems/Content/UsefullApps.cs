using Entitas;

public partial class MarketInitializerSystem : IInitializeSystem
{
    private void InitializeUsefulAppsIndustry()
    {
        var niches = new NicheType[] {
            NicheType.Qol_PdfReader, // pdf readers
            NicheType.Qol_DocumentEditors, // micr office
            NicheType.Qol_GraphicalEditor, // Photoshop
            NicheType.Qol_3DGraphicalEditor, // 3d max
            NicheType.Qol_VideoEditingTool, // average


            NicheType.Qol_AdBlocker, // small
            NicheType.Qol_DiskFormattingUtils, // small
            NicheType.Qol_RSSReader, // small
            NicheType.Qol_Archivers, // small-average

            NicheType.Qol_MusicPlayers, // small-average
            NicheType.Qol_VideoPlayers, // average

            //NicheType.Qol_Encyclopedia,
            //NicheType.Qol_Antivirus,

            //NicheType.Qol_OnlineEducation
        };
        AttachNichesToIndustry(IndustryType.WorkAndLife, niches);

        var smallUtil = new MarketProfile(AudienceSize.Million, Monetisation.Adverts, Margin.Low, AppComplexity.Solo, NicheSpeed.Year);

        var officeDocumentEditor = new MarketProfile(AudienceSize.Million100, Monetisation.Service, Margin.Mid, AppComplexity.Average, NicheSpeed.Year);

        SetMarkets(NicheType.Qol_PdfReader, 2000, 2050, officeDocumentEditor);
        SetMarkets(NicheType.Qol_DocumentEditors, 1995, 2040, officeDocumentEditor);
        SetMarkets(NicheType.Qol_GraphicalEditor, 1980, 2040, officeDocumentEditor);
        SetMarkets(NicheType.Qol_3DGraphicalEditor, 1990, 2030, officeDocumentEditor);
        SetMarkets(NicheType.Qol_VideoEditingTool, 1990, 2030, officeDocumentEditor);


        SetMarkets(NicheType.Qol_DocumentEditors, 1995, 2040, smallUtil);
        SetMarkets(NicheType.Qol_GraphicalEditor, 1980, 2040, smallUtil);
        SetMarkets(NicheType.Qol_3DGraphicalEditor, 1990, 2030, smallUtil);
        SetMarkets(NicheType.Qol_VideoEditingTool, 1990, 2030, smallUtil);


    }
}
