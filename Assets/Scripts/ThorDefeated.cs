using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThorDefeated : MonoBehaviour
{
    [SerializeField] GameObject clouds;
    [SerializeField] ThorReward thorReward;
    bool isDefeated;
    private void Start() {
        clouds.SetActive(false);
        clouds.transform.localScale = new Vector3(0,0,0);
        isDefeated = false;
    }
    private void Update() {
        if(isDefeated) Final();
    }
    public void StartFinalCinematic()
    {
        isDefeated = true;
    }
    public void Final()
    {
        clouds.SetActive(true);
        clouds.transform.localScale = 
            Vector3.Lerp(clouds.transform.localScale, new Vector3(2.5f,2.5f,2.5f), 1.5f * Time.deltaTime);
        if(clouds.transform.localScale.x >= 2.3f)
        {
            transform.localPosition = 
                Vector3.Lerp(transform.localPosition, clouds.transform.localPosition, 0.2f * Time.deltaTime);
            transform.Rotate(new Vector3(0, 20, 0) * Time.deltaTime);
            if(transform.localPosition.y >= 200) 
            {
                gameObject.SetActive(false);
                thorReward.GiveReward();
            }
            
        }
    }
}
