using Entitas;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ButtonController : MonoBehaviour
{
    public GameContext GameContext;
    public ProductComponent ControlledProduct;
    Button Button;

    public abstract void Execute();

    private void Awake()
    {
        GameContext = Contexts.sharedInstance.game;
    }

    private void Update()
    {
        ControlledProduct = GameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Product, GameMatcher.ControlledByPlayer))[0].product;
    }

    public GameEntity SendEvent()
    {
        // you can attach events to this object
        return GameContext.CreateEntity();
    }

    void Start()
    {
        Button = GetComponent<Button>();

        Button.onClick.AddListener(Execute);
    }

    private void OnDestroy()
    {
        Button.onClick.RemoveListener(Execute);
    }
}
