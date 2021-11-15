using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _loadingScreen;
    [SerializeField]
    private Slider _slider;
    private AsyncOperation _async;
    public void PlayGame()
    {
        //SceneManager.LoadScene("Game");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(LoadingScreen());
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    IEnumerator LoadingScreen()
    {
        _loadingScreen.SetActive(true);
        _async = SceneManager.LoadSceneAsync(1);
        _async.allowSceneActivation = false;

        while (!_async.isDone)
        {
            _slider.value = _async.progress;
            
            if (_async.progress == 0.9f)
            {
                _slider.value = 1f;
                _async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
