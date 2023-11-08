using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MjolnirImpact : MonoBehaviour
{
    [SerializeField] ThrowMjolnir throwMjolnir;
    private void OnTriggerEnter(Collider other) {
        StartCoroutine(delayReturnMjolnir());
    }

    private IEnumerator delayReturnMjolnir()
    {
        yield return new WaitForSeconds(1f);
        throwMjolnir.ThrowMjolnirToPlayer();
    }
}
