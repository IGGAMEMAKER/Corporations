namespace Assets.Utils
{
    public static partial class ProductUtils
    {
        public static void ToggleDumping(GameContext gameContext, GameEntity product)
        {
            product.isDumping = !product.isDumping;
        }
    }
}
