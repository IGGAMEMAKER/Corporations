using Assets;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    AudioManager AudioManager;
    ViewManager ViewManager;


    // Start is called before the first frame update
    void Start()
    {
        AudioManager = gameObject.GetComponent<AudioManager>();
        ViewManager = new ViewManager();
    }

    void Update()
    {
        ToggleScreensIfNecessary();
    }

    void ToggleScreensIfNecessary()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            RenderTechnologyScreen();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            RenderInvestmentScreen();

        if (Input.GetKeyDown(KeyCode.Alpha3))
            RenderBusinessScreen();

        if (Input.GetKeyDown(KeyCode.Alpha4))
            RenderTeamScreen();

        if (Input.GetKeyDown(KeyCode.Alpha5))
            RenderStatsScreen();

        if (Input.GetKeyDown(KeyCode.Alpha6))
            RenderMarketingScreen();

        if (Input.GetKeyDown(KeyCode.Alpha7))
            RenderManagerScreen();
    }

    void PlayToggleScreenSound()
    {
        AudioManager.PlayToggleScreenSound();
    }

    // Screens
    void RenderInvestmentScreen()
    {
        PlayToggleScreenSound();
        ViewManager.RenderInvestmentsScreen();
    }

    void RenderStatsScreen()
    {
        PlayToggleScreenSound();
        ViewManager.RenderStatsScreen();
    }

    void RenderBusinessScreen()
    {
        PlayToggleScreenSound();
        ViewManager.RenderBusinessScreen();
    }

    void RenderTeamScreen()
    {
        PlayToggleScreenSound();
        ViewManager.RenderTeamScreen();
    }

    void RenderMarketingScreen()
    {
        PlayToggleScreenSound();
        ViewManager.RenderMarketingScreen();
    }

    void RenderTechnologyScreen()
    {
        PlayToggleScreenSound();
        ViewManager.RenderTechnologyScreen();
    }

    void RenderManagerScreen()
    {
        PlayToggleScreenSound();
        ViewManager.RenderManagerScreen();
    }
}
