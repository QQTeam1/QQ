using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScrene;
    public Slider loadingBar; 
    public void LoadScene(int levelIndex)
    {
       StartCoroutine(LoadSceneAsynchronously(levelIndex));
    }
    IEnumerator LoadSceneAsynchronously(int levelIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
        loadingScrene.SetActive(true);
        while (!operation.isDone) 
        {
            loadingBar.value = operation.progress;
            Debug.Log(operation.progress);
            yield return null;
        }
    }
}
