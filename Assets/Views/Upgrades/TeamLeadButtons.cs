using UnityEngine;

public class TeamLeadButtons : RoleRelatedButtons
{
    public GameObject WebCheckbox;
    public GameObject DesktopCheckbox;
    public GameObject MobileIOSCheckbox;
    public GameObject MobileAndroidCheckbox;

    internal override void Render(GameEntity company)
    {
        bool isProductManager = HasWorker(WorkerRole.ProductManager, company);

        // release stuff
        // -------------
        Draw(WebCheckbox, CanEnable(company, ProductUpgrade.PlatformWeb) && isProductManager);
        Draw(MobileIOSCheckbox, CanEnable(company, ProductUpgrade.PlatformMobileIOS) && isProductManager);
        Draw(MobileAndroidCheckbox, CanEnable(company, ProductUpgrade.PlatformMobileAndroid) && isProductManager);
        Draw(DesktopCheckbox, CanEnable(company, ProductUpgrade.PlatformDesktop) && isProductManager);
    }
}
