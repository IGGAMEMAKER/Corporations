using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract partial class ButtonController : MonoBehaviour
{
    Button Button;

    public abstract void Execute();

    public GameEntity SelectedCompany
    {
        get
        {
            var data = MenuUtils.GetMenu(GameContext).menu.Data;

            if (data == null)
            {
                //Debug.LogError("SelectedCompany does not exist!");

                return CompanyUtils.GetAnyOfControlledCompanies(GameContext);
            }

            return CompanyUtils.GetCompanyById(GameContext, (int)data);
        }
    }

    public ScreenMode CurrentScreen
    {
        get
        {
            return MenuUtils.GetMenu(GameContext).menu.ScreenMode;
        }
    }

    public ProductComponent MyProduct
    {
        get
        {
            return MyProductEntity?.product;
        }
    }

    public GameEntity MyProductEntity
    {
        get
        {
            return CompanyUtils.GetPlayerControlledProductCompany(GameContext);
        }
    }

    public GameEntity MyGroupEntity
    {
        get
        {
            return CompanyUtils.GetPlayerControlledGroupCompany(GameContext);
        }
    }

    public GameContext GameContext
    {
        get
        {
            return Contexts.sharedInstance.game;
        }
    }

    void Start()
    {
        Button = GetComponent<Button>();

        Button.onClick.AddListener(Execute);
    }

    void OnDestroy()
    {
        RemoveListener();
    }

    void RemoveListener()
    {
        if (Button)
            Button.onClick.RemoveListener(Execute);
        else
            Debug.LogWarning("This component is not assigned to Button. It is assigned to " + gameObject.name);
    }

    public void SetSelectedCompany(int companyId)
    {
        MenuUtils.SetSelectedCompany(GameContext, companyId);
    }
}
