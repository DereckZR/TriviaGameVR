using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.Input;
using UnityEngine;

public class AnimScientist : MonoBehaviour
{
    [SerializeField] Animator sciAnimator;
    [SerializeField] ScientistReward scientistReward;
    public void ReturnIdleafterAttack()
    {
        sciAnimator.SetBool(TriviaManager.BossActions.IsAttack, false);
    }
    public void Damaged()
    {
        sciAnimator.SetBool(TriviaManager.BossActions.IsDamaged, false);
    }
    public void IsDefeated()
    {
        scientistReward.GiveReward();
    }
}
