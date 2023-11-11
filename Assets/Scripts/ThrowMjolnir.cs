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
    [SerializeField] Animator thorAnimator;
    [SerializeField] float throwVelocity = 5;
    [SerializeField] AudioClip fall;
    [SerializeField] ThorDefeated thorDefeated;
    bool isThrow;
    bool isInHand;
    System.Random random;
    private void Start() {
        random = new System.Random();
        isThrow = false;
        isInHand = true;
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
    //Se usa en un evento de la animacion de ataque de thor
    public void ThrowMjolnirToPlayer()
    {
        objetiveAtacck = objetives[random.Next(0, objetives.Length)]; 
        isThrow = !isThrow;
    }

    public void ReturnMjolnirToHand()
    {
        thorAnimator.SetBool(TriviaManager.BossActions.IsAttack, false);
        isInHand = true;
        isThrow = false;
    }

    public void Damaged()
    {
        thorAnimator.SetBool(TriviaManager.BossActions.IsDamaged, false);
    }

    public void Fall()
    {
        AudioManager.Instance.PlaySound(fall);
    }
    public void IsDefeated()
    {
        thorDefeated.StartFinalCinematic();
    }
    
}
