using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadLevelScreen : MonoBehaviour
{

    public GameObject loadingScreen;
    public Slider loadingScreenSlider;
    public Text progressText;
    //public GameObject loadingSlider;

    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadAsynchronously(sceneName));
    }


    IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {

            float progress = Mathf.Clamp01(operation.progress / 0.9f);
             Debug.Log(progress);

            loadingScreenSlider.value = progress;
            progressText.text =  progress * 100f + "%";  

            yield return null;
        }
    }

}