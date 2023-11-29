using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSubtance : MonoBehaviour
{
    [SerializeField] GameObject[] flasks;
    [SerializeField] GameObject target;
    [SerializeField] GameObject handMockDoc;
    [SerializeField] AudioClip crash;
    bool isAttacking;
    bool isFlaskThrow;
    System.Random random;
    int flaskIndex;
    private void Start() {
        foreach (var flask in flasks) flask.SetActive(false);
        random = new System.Random();
        flaskIndex = 0;
        isAttacking = false;
        isFlaskThrow = false;
    }
    private void Update() {
        if(isAttacking) 
        {
            flaskIndex = random.Next(0, flasks.Length);
            ThrowFlask();
        }
        if(isFlaskThrow) FlaskToTarget();

    }
    public void SciAttack()
    {
        isAttacking = true;
    }
    private void ThrowFlask()
    {
        flasks[flaskIndex].SetActive(true);
        flasks[flaskIndex].transform.SetParent(target.transform);
        isAttacking = false;
        isFlaskThrow = true;
    }
    private void FlaskToTarget()
    {
        flasks[flaskIndex].transform.localPosition = 
            Vector3.Lerp(flasks[flaskIndex].transform.localPosition, new Vector3(0,0,0), 10 * Time.deltaTime);
    }
    public void FlaskCrash()
    {
        isFlaskThrow = false;
        AudioManager.Instance.PlaySound(crash);
        flasks[flaskIndex].transform.SetParent(handMockDoc.transform);
        flasks[flaskIndex].transform.localPosition = new Vector3(-0.007f,-0.003f,0.005f);
        flasks[flaskIndex].transform.localRotation = Quaternion.Euler(320,260,104);
        flasks[flaskIndex].SetActive(false);
    }
    public int GetIndex()
    {
        return flaskIndex;
    }
}
