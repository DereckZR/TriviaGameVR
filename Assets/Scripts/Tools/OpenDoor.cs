using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] Animator doorAnimator;
    [SerializeField] AudioClip doorAudio;
    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag.Equals("Player"))
        {
            doorAnimator.SetBool("character_nearby", true);
            AudioManager.Instance.PlaySound(doorAudio);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.transform.tag.Equals("Player"))
        {
            doorAnimator.SetBool("character_nearby", false);
            AudioManager.Instance.PlaySound(doorAudio);
        }
    }
}
