using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public TextMeshProUGUI loadingText;
    public Slider progressBar;
    float time = 0;
    void Start()
    {
        StartCoroutine(LoadSceneAsync("GameScene"));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            progressBar.value = progress;
            string text = $"Loading... {asyncLoad.progress * 100:0.0}%";
            loadingText.SetText(text);
            yield return null;
        }
    }
}
