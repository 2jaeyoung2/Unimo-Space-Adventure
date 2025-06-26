using JDG;
using UnityEngine;

namespace JDG
{
    public class Bootstrapper : MonoBehaviour
    {
        private static bool _initialized = false;
        private GameObject _sceneLoaderPrefab;
        private GameObject _stateManagerPrefab;

        private void Awake()
        {
            if (_initialized)
            {
                Destroy(gameObject);
                return;
            }

            _initialized = true;
            DontDestroyOnLoad(gameObject);

            _sceneLoaderPrefab = Resources.Load<GameObject>("WorldMap/SceneLoader");
            _stateManagerPrefab = Resources.Load<GameObject>("WorldMap/GameStateManager");

            if (SceneLoader.Instance == null)
            {
                Instantiate(_sceneLoaderPrefab);
            }

            if (GameStateManager.Instance == null)
            {
                Instantiate(_stateManagerPrefab);
            }
        }
    }
}
