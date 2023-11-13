using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThorReward : MonoBehaviour
{
    [SerializeField] bool giveReward;
    private void Start() {
        giveReward = false;
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
            Vector3.Lerp(transform.localPosition, new Vector3(196.5f,22,23.75f), 1f * Time.deltaTime);
        transform.localRotation = 
            Quaternion.Lerp(transform.localRotation, Quaternion.Euler(-90, 90, 0), 0.5f * Time.deltaTime);
    }
}
