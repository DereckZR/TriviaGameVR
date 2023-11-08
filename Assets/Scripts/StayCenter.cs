using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayCenter : MonoBehaviour
{
    private void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0, 0), 1 * Time.deltaTime);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, 0), 1 * Time.deltaTime);
    }
}
