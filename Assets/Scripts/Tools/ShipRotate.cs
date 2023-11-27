using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRotate : MonoBehaviour
{
    [SerializeField] float rotateVelocity;
    private void Update() {
        transform.Rotate(new Vector3(0, rotateVelocity, 0) * Time.deltaTime);
    }
}
