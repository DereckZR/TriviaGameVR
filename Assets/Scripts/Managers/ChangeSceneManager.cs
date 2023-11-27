using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneManager : MonoBehaviour
{
    [SerializeField] public AudioClip[] sountrack;
    public static ChangeSceneManager Instance { get; private set;}
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
    public void GoToScene(int sceneIndex)
    {
        StartCoroutine(WaitForFade(sceneIndex));
    }
    private IEnumerator WaitForFade(int sceneIndex)
    {
        FadeScreen.Instance.FadeOut();
        yield return new WaitForSeconds(FadeScreen.Instance.fadeDuration);
        SceneManager.LoadScene(sceneIndex);
        AudioManager.Instance.ChangeMusic(sountrack[sceneIndex]);
    }
}
