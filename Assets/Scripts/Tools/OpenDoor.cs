using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] Animator doorAnimator;
    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag.Equals("Player"))
            doorAnimator.SetBool("character_nearby", true);
    }
    private void OnTriggerExit(Collider other) {
        if(other.transform.tag.Equals("Player"))
            doorAnimator.SetBool("character_nearby", false);
    }
}
