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

        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.Euler(0,0,0);

    }
}
