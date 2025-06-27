using JDG;

namespace JDG
{
    public enum DifficultyType
    {
        Initiate, Novice, Adept, Expert, Veteran, Elite, Master, Grandmaster, Supreme
    }

    [System.Serializable]
    public class DifficultyEntry
    {
        public DifficultyType _difficultyType;
        public int _distance;
    }
}
