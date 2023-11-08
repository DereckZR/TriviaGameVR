using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowMjolnir : MonoBehaviour
{
    [SerializeField] GameObject mjolnir;
    [SerializeField] GameObject[] objetives;
    GameObject objetiveAtacck;
    [SerializeField] GameObject parentIdle;
    [SerializeField] GameObject parentThrow;
    Rigidbody rigibbodyMjolnir;
    [SerializeField] float throwVelocity = 5;
    bool isThrow;
    bool isInHand;
    System.Random random;
    private void Start() {
        random = new System.Random();
        isThrow = false;
        isInHand = true;
        //rigibbodyMjolnir = mjolnir.GetComponent<Rigidbody>();
        //rigibbodyMjolnir.useGravity = false;
    }
    private void Update() {
        if(isThrow)
        {
            isInHand = false;
            mjolnir.transform.SetParent(parentThrow.transform, true);
            mjolnir.transform.localRotation = 
                Quaternion.Lerp(mjolnir.transform.localRotation, Quaternion.Euler(-17,-260,185), 2 * Time.deltaTime);
            mjolnir.transform.localPosition =
                Vector3.Lerp(mjolnir.transform.localPosition, objetiveAtacck.transform.localPosition, throwVelocity * Time.deltaTime);
        }
        else if(!isThrow && !isInHand)
        {
            mjolnir.transform.SetParent(parentIdle.transform, true);
            mjolnir.transform.localRotation = 
                Quaternion.Lerp(mjolnir.transform.localRotation, Quaternion.Euler(0,0,0), 5 * Time.deltaTime);
            mjolnir.transform.localPosition =
                Vector3.Lerp(mjolnir.transform.localPosition, new Vector3(0.001f, 0.002f, 0.001f), 5 * Time.deltaTime);
        }
        else if(isInHand)
        {
            mjolnir.transform.SetParent(parentIdle.transform, true);
            mjolnir.transform.localRotation = Quaternion.Euler(0,0,0);
            mjolnir.transform.localPosition = new Vector3(0.001f, 0.002f, 0.001f);
        }
    }
    public void ThrowMjolnirToPlayer()
    {
        objetiveAtacck = objetives[random.Next(0, objetives.Length)]; 
        isThrow = !isThrow;
    }

    public void ReturnMjolnirToHand()
    {
        isInHand = true;
        isThrow = false;
    }

    /*public void ThrowMjolnirToPlayer()
    {
        rigibbodyMjolnir.useGravity = true;
        mjolnir.transform.SetParent(parentThrow.transform, true);
        rigibbodyMjolnir.velocity = CalculateVelocity();
    }

    private Vector3 CalculateVelocity()
    {
        float gravity = Physics.gravity.y;
        Vector3 range = objetive.transform.position - mjolnir.transform.position;
        float velocityX, velocityY, velocityZ;
        velocityY = Mathf.Sqrt(-2 * gravity * h);
        velocityX = range.x / ((-velocityY / gravity) + Mathf.Sqrt(2 * (range.y - h) / gravity));
        velocityZ = range.z / ((-velocityY / gravity) + Mathf.Sqrt(2 * (range.y - h) / gravity));
        return new Vector3(velocityX, velocityY, velocityZ);
    }*/

    
}
