using Assets.Core;
using Entitas;
using UnityEngine;

public class BaseClass : MonoBehaviour
{
    // data
    public static GameContext Q => Contexts.sharedInstance.game;

    public int CurrentIntDate => ScheduleUtils.GetCurrentDate(Q);



    public GameEntity SelectedCompany   => ScreenUtils.GetSelectedCompany(Q);

    public NicheType  SelectedNiche     => ScreenUtils.GetSelectedNiche(Q);
    public IndustryType SelectedIndustry => ScreenUtils.GetSelectedIndustry(Q);

    public GameEntity SelectedHuman     => ScreenUtils.GetSelectedHuman(Q);
    public GameEntity SelectedInvestor  => ScreenUtils.GetSelectedInvestor(Q);

    public ScreenMode CurrentScreen     => ScreenUtils.GetMenu(Q).menu.ScreenMode;



    public GameEntity Hero => Q.GetEntities(GameMatcher.Player)[0];


    public GameEntity MyGroupEntity     => Companies.GetPlayerControlledGroupCompany(Q);
    public GameEntity MyCompany => MyGroupEntity ?? null;
    public GameEntity Group => MyCompany;
    public bool HasCompany => MyCompany != null;

    //public GameEntity Flagship => Companies.






    // GameObjects
    public bool Contains<T>()
    {
        return gameObject.GetComponent<T>() != null;
    }

    public T AddIfAbsent<T>() where T : Component
    {
        if (!Contains<T>())
            return gameObject.AddComponent<T>();

        return gameObject.GetComponent<T>();
    }

    public void RemoveIfExists<T>() where T : Component
    {
        if (Contains<T>())
            Destroy(gameObject.GetComponent<T>());
    }

    public void AddOrRemove<T>() where T : Component
    {
        if (Contains<T>())
            Destroy(gameObject.GetComponent<T>());
    }



    internal void ToggleIsChosenComponent(bool isChosen)
    {
        if (Contains<IsChosenComponent>())
            gameObject.GetComponent<IsChosenComponent>().Toggle(isChosen);
    }
}
