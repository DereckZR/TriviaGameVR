using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfo : MonoBehaviour
{
    [SerializeField] GameObject principalScreen;
    [SerializeField] GameObject infoScreen;
    [SerializeField] AudioClip openInfo;
    [SerializeField] AudioClip closeInfo;
    
    private void Start() {
        infoScreen.SetActive(false);
        principalScreen.SetActive(true);
    }
    public void ShowInfoScreen()
    {
        infoScreen.SetActive(true);
        principalScreen.SetActive(false);
        AudioManager.Instance.PlaySound(openInfo);
    }
    public void ShowPrincipalScreen()
    {
        infoScreen.SetActive(false);
        principalScreen.SetActive(true);
        AudioManager.Instance.PlaySound(closeInfo);
    }
    public void StartBattle(int sceneIndex)
    {
        ChangeSceneManager.Instance.GoToScene(sceneIndex);
    }
}
