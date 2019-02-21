using Assets.Classes;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseController: MonoBehaviour
{
    public World model;

    public WorldModel WorldModel;

    void Awake()
    {
        Debug.Log("Loading model...");
        LoadModel();
    }

    //abstract void Load();

    public void LoadModel()
    {
        WorldModel = FindObjectOfType<WorldModel>();

        if (WorldModel.model == null)
        {
            model = new World();
            WorldModel.model = model;
        }
        else
        {
            model = WorldModel.model;
        }
    }
}

[RequireComponent(typeof(Button))]
public class UpgradeFeatureController : BaseController
{
    Button Button;
    // Start is called before the first frame update
    void Start()
    {
        Button = GetComponent<Button>();

        Button.onClick.AddListener(UpgradeFeature);
    }

    void UpgradeFeature()
    {
        LoadModel();

        EventManager.NotifyFeatureUpgraded(0, 0);
    }
}
