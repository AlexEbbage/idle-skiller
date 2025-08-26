using UnityEngine;

namespace IdleSkiller.Core
{
    public class Game : MonoBehaviour
    {
        public static Game Instance { get; private set; }

        public GameState State { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void SetState(GameState newState)
        {
            State = newState;
        }
    }
}
