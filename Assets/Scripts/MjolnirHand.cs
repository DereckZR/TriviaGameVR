using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MjolnirHand : MonoBehaviour
{
    [SerializeField] ThrowMjolnir throwMjolnir;
    private void OnTriggerEnter(Collider other) {
        throwMjolnir.ReturnMjolnirToHand();
        //Debug.LogError("asdasdasd");
    }
}
