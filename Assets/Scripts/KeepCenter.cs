using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepCenter : MonoBehaviour
{
    Vector3 PosicionInicial;
    // Start is called before the first frame update
    void Start()
    {
        PosicionInicial=transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=Vector3.Lerp(transform.position, PosicionInicial,Time.deltaTime);
    }
}
