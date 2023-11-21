using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MjolnirImpact : MonoBehaviour
{
    [SerializeField] ThrowMjolnir throwMjolnir;
    [SerializeField] TriviaManager triviaManager;
    [SerializeField] AudioClip mjolnirImpact;
    private void OnTriggerEnter(Collider other) {
        transform.GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(delayReturnMjolnir());
        StartCoroutine(EnableTarget());
        StartCoroutine(triviaManager.Shaking());
        AudioManager.Instance.PlaySound(mjolnirImpact);
    }

    private IEnumerator delayReturnMjolnir()
    {
        yield return new WaitForSeconds(1f);
        throwMjolnir.ThrowMjolnirToPlayer();
    }
    private IEnumerator EnableTarget()
    {
        yield return new WaitForSeconds(2.5f);
        transform.GetComponent<BoxCollider>().enabled = true;
    }
}
