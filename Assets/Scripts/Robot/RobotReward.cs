using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotReward : MonoBehaviour
{
    [SerializeField] bool giveReward;
    private void Start() {
        giveReward = false;

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
            Vector3.Lerp(transform.localPosition, new Vector3(-0.7f,5.5f,-24.5f), 1f * Time.deltaTime);
        if(transform.localPosition.x <= -0.69f)
        {
            giveReward = false;
            transform.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
