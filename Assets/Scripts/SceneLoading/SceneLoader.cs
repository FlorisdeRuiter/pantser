using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void SceneLoad(SceneLoadData sceneData)
    {
        if (SceneManager.GetSceneByName(sceneData.SceneReference.SceneName).isLoaded)
            return;

        if (sceneData.isAdditive)
            SceneManager.LoadScene(sceneData.SceneReference.SceneName, LoadSceneMode.Additive);
        else
            SceneManager.LoadScene(sceneData.SceneReference.SceneName);
    }

    public void SceneUnload(SceneLoadData sceneData)
    {
        sceneData.SceneReference.UnloadSceneAsync();
    }
}