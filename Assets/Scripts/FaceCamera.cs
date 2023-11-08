using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    [SerializeField]
    private Transform mLookAt;
    private Transform localTrans;

    private void Start() {
        localTrans = GetComponent<Transform>();
    }
    private void Update() {
        if(mLookAt) localTrans.LookAt(2 * localTrans.position - mLookAt.position + new Vector3(0, 0, 0));
    }
}
