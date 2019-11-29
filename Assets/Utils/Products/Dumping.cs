namespace Assets.Utils
{
    public static partial class ProductUtils
    {
        public static void ToggleDumping(GameContext gameContext, GameEntity product)
        {
            product.isDumping = !product.isDumping;
        }

        public static void StartDumping(GameContext gameContext, GameEntity product)
        {
            product.isDumping = true;
        }

        public static void StopDumping(GameContext gameContext, GameEntity product)
        {
            product.isDumping = false;
        }
    }
}
