using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBattle : MonoBehaviour
{
    [SerializeField] OVRGrabbable grab;
    [SerializeField] ChangeSceneManager changeSceneManager;
    bool flag = true;
    private void Update() {
        if(grab.isGrabbed && flag)
        {
            flag = false;
            StartCoroutine(WaitToChange());
        }
    }
    private IEnumerator WaitToChange()
    {
        yield return new WaitForSeconds(5);
        changeSceneManager.GoToScene(0);
    }
}
