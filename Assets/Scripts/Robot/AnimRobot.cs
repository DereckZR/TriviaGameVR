using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimRobot : MonoBehaviour
{
    [SerializeField] Animator robotAnimator;
    [SerializeField] RobotReward scientistReward;
    [SerializeField] AudioClip blasterSound;
    public void ReturnIdleafterAttack()
    {
        robotAnimator.SetBool(TriviaManager.BossActions.IsAttack, false);
    }
    public void Damaged()
    {
        robotAnimator.SetBool(TriviaManager.BossActions.IsDamaged, false);
    }
    public void IsDefeated()
    {
        scientistReward.GiveReward();
    }
    public void PlayBlasterSound()
    {
        AudioManager.Instance.PlaySound(blasterSound);
    }


    private void Update()
    {
        if(!robotAnimator.GetBool(TriviaManager.BossActions.IsDefeated))
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0, 0), 1 * Time.deltaTime);
            transform.localRotation = Quaternion.Euler(0, 310, 0);
        }
    }
}
