using Assets.Core;
using System.Linq;
using UnityEngine;

public partial class BaseClass : MonoBehaviour
{
    // data
    public static GameContext Q => Contexts.sharedInstance.game;

    public int CurrentIntDate => ScheduleUtils.GetCurrentDate(Q);
    public string ShortDate => ScheduleUtils.GetFormattedDate(CurrentIntDate);


    public GameEntity SelectedCompany => ScreenUtils.GetSelectedCompany(Q);

    public NicheType SelectedNiche => ScreenUtils.GetSelectedNiche(Q);

    // TODO REMOVE
    public IndustryType SelectedIndustry => ScreenUtils.GetSelectedIndustry(Q);

    public GameEntity SelectedHuman => ScreenUtils.GetSelectedHuman(Q);
    public int SelectedTeam => ScreenUtils.GetSelectedTeam(Q);

    // TODO REMOVE
    public GameEntity SelectedInvestor => ScreenUtils.GetSelectedInvestor(Q);

    public ScreenMode CurrentScreen => ScreenUtils.GetMenu(Q).menu.ScreenMode;


    public GameEntity Hero => ScreenUtils.GetPlayer(Q);


    public GameEntity MyGroupEntity => Companies.GetPlayerControlledGroupCompany(Q);
    public GameEntity MyCompany => MyGroupEntity;
    public GameEntity Flagship => Companies.GetFlagship(Q, MyCompany);

    public bool HasCompany => MyCompany != null;

    //
    // GameObjects
    public int GetParameter(string key) => ScreenUtils.GetInteger(Q, key);
    public void SetParameter(string key, int value) => ScreenUtils.SetInteger(Q, value, key);

    GameEntity _Company;

    public GameEntity GetFollowableCompany()
    {
        if (_Company == null)
        {
            var c = GetComponentInParent<FollowableCompany>();

            if (c == null)
                return null;
            else
                _Company = c.Company;
        }

        return _Company;
    }

    // MODALS
    public void OpenUrl(string url)
    {
        SimpleUI.SimpleUI.OpenUrl_static(url);
    }

    public void OpenModal(string ModalTag, bool closeOthers = true)
    {
        FindObjectOfType<MyModalManager>().OpenMyModal(ModalTag, closeOthers);
        //var m = GetModal(ModalTag);

        //if (m != null)
        //{
        //    Show(m);
        //    ScheduleUtils.PauseGame(Q);
        //}
    }

    public void CloseModal(string ModalTag)
    {
        FindObjectOfType<MyModalManager>().CloseMyModal(ModalTag);
        //var m = GetModal(ModalTag);

        //if (m != null)
        //{
        //    Hide(m);
        //    ScheduleUtils.PauseGame(Q);
        //}
    }

    // TODO REMOVE
    private MyModalWindow GetModal(string ModalTag)
    {
        var m = FindObjectsOfTypeAll<MyModalWindow>().FirstOrDefault(w => w.ModalTag == ModalTag);

        if (m == null)
        {
            Debug.LogError("Modal " + ModalTag + " not found!");

            return null;
        }

        return m;
    }
}