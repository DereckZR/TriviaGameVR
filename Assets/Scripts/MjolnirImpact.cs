using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MjolnirImpact : MonoBehaviour
{
    [SerializeField] ThrowMjolnir throwMjolnir;
    [SerializeField] TriviaManager triviaManager;
    [SerializeField] AudioClip mjolnirImpact;
    private void OnTriggerEnter(Collider other) {
        StartCoroutine(delayReturnMjolnir());
        StartCoroutine(triviaManager.Shaking());
        AudioManager.Instance.PlaySound(mjolnirImpact);
    }

    private IEnumerator delayReturnMjolnir()
    {
        yield return new WaitForSeconds(1f);
        throwMjolnir.ThrowMjolnirToPlayer();
    }
}
