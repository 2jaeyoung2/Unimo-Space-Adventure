namespace ZL.Unity.Pooling
{
    public abstract class ManagedPooledObject<TKey> : PooledObject
    {
        private TKey key;

        public TKey Key
        {
            get => key;

            set => key = value;
        }
    }
}