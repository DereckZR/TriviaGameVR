using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThorReward : MonoBehaviour
{
    [SerializeField] GameObject grabPoint;
    [SerializeField] bool giveReward;
    private void Start() {
        giveReward = false;
        grabPoint.transform.GetComponent<CapsuleCollider>().enabled = false;
        transform.GetComponent<BoxCollider>().enabled = false;
    }
    private void Update() {
        if(giveReward) Reward();
    }
    public void GiveReward()
    {
        giveReward = true;
    }

    private void Reward()
    {
        transform.localPosition = 
            Vector3.Lerp(transform.localPosition, new Vector3(196.5f,22,23.7f), 1f * Time.deltaTime);
        transform.localRotation = 
            Quaternion.Lerp(transform.localRotation, Quaternion.Euler(-90, 90, 90), 0.5f * Time.deltaTime);
        if(transform.localPosition.x >= 196.4f)
        {
            giveReward = false;
            grabPoint.transform.GetComponent<CapsuleCollider>().enabled = true;
            transform.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
