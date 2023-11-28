using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistReward : MonoBehaviour
{
    [SerializeField] GameObject grabPoint;
    [SerializeField] bool giveReward;
    private void Start() {
        giveReward = false;
        grabPoint.transform.GetComponent<BoxCollider>().enabled = false;
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
            Vector3.Lerp(transform.localPosition, new Vector3(-1f,32,-35.5f), 1f * Time.deltaTime);
        if(transform.localPosition.x <= -0.99f)
        {
            giveReward = false;
            grabPoint.transform.GetComponent<BoxCollider>().enabled = true;
            transform.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
