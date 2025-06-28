using ZL.Unity.Unimo;

public class RelicCardPool : GenericPool<RelicCard>
{
    private static RelicCardPool _instance;

    protected override void Awake()
    {
        base.Awake();

        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static RelicCardPool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<RelicCardPool>();
            }
            return _instance;
        }
    }
}
