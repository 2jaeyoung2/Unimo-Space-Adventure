namespace ZL.Unity
{
    public sealed class EmptyFieldAttribute : FieldAttribute
    {
        #if UNITY_EDITOR

        protected override void Draw(Drawer drawer)
        {
            drawer.DrawEmptyPropertyField();
        }

        #endif
    }
}