using Assets.Utils;
using Entitas;
using UnityEngine;

public class BaseClass : MonoBehaviour
{
    // data
    public static GameContext GameContext => Contexts.sharedInstance.game;

    public int CurrentIntDate => ScheduleUtils.GetCurrentDate(GameContext);



    public GameEntity SelectedCompany   => ScreenUtils.GetSelectedCompany(GameContext);

    public NicheType  SelectedNiche     => ScreenUtils.GetSelectedNiche(GameContext);
    public IndustryType SelectedIndustry => ScreenUtils.GetSelectedIndustry(GameContext);

    public GameEntity SelectedHuman     => ScreenUtils.GetSelectedHuman(GameContext);
    public GameEntity SelectedInvestor  => ScreenUtils.GetSelectedInvestor(GameContext);

    public ScreenMode CurrentScreen     => ScreenUtils.GetMenu(GameContext).menu.ScreenMode;



    public GameEntity Me => GameContext.GetEntities(GameMatcher.Player)[0];


    public GameEntity MyGroupEntity     => Companies.GetPlayerControlledGroupCompany(GameContext);
    public GameEntity MyCompany => MyGroupEntity ?? null;
    public bool HasCompany => MyCompany != null;








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
        gameObject.GetComponent<IsChosenComponent>().Toggle(isChosen);
    }
}
