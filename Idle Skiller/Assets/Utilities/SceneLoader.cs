using System.Collections;
using UnityEngine.SceneManagement;

namespace IdleSkiller.Utilities
{
    public static class SceneLoader
    {
        public static void Load(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public static IEnumerator LoadAsync(string sceneName)
        {
            var operation = SceneManager.LoadSceneAsync(sceneName);
            while (!operation.isDone)
            {
                yield return null;
            }
        }
    }
}
