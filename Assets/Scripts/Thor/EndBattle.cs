using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBattle : MonoBehaviour
{
    [SerializeField] OVRGrabbable grab;
    
    bool flag = true;
    private void Update() {
        if(grab.isGrabbed && flag)
        {
            flag = false;
            StartCoroutine(GoToLobby());
        }
    }
    private IEnumerator GoToLobby()
    {
        yield return new WaitForSeconds(5);
        ChangeSceneManager.Instance.GoToScene(0);
        AudioManager.Instance.ChangeMusic(ChangeSceneManager.Instance.sountrack[0]);
    }
}
