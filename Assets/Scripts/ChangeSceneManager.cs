using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneManager : MonoBehaviour
{
    [SerializeField] FadeScreen fade;
    public void GoToScene(int sceneIndex)
    {
        StartCoroutine(WaitForFade(sceneIndex));
    }
    private IEnumerator WaitForFade(int sceneIndex)
    {
        fade.FadeOut();
        yield return new WaitForSeconds(fade.fadeDuration);
        SceneManager.LoadScene(sceneIndex);
    }
}
