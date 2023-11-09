using TMPro;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;

public class TriviaManager : MonoBehaviour
{
    [SerializeField] TextAsset jsonTXT;
    [SerializeField] GameObject player;
    PlayerManager playerManager;
    [SerializeField] GameObject boss;
    BossManager bossManager;
    [System.Serializable]
    public class QuestionList
    {
        public TriviaQuestion[] questions;
    }
    public QuestionList myQuestionList = new QuestionList();
    [SerializeField] TextMeshProUGUI questionUI;
    [SerializeField] GameObject[] buttonsUI;
    [SerializeField] TextMeshProUGUI[] answersUI;
    private int index;
    [SerializeField] float delay = 2.0f;
    [SerializeField] GameObject ZTV;
    Rigidbody rb;
    bool playerLost;
    [SerializeField] GameObject happyFace;
    [SerializeField] GameObject sadFace;
    [SerializeField] GameObject triviaScreen;
    bool oscilation;
    [SerializeField] Animator bossAnimator;
    public static class BossActions
    {
        public const string 
        IsAttack = "IsAttack",
        IsDefeated = "IsDefeated",
        IsDamaged = "IsDamaged";
        
    }
    //[SerializeField] TextMeshProUGUI finalText;

    private void Start() 
    {
        happyFace.SetActive(false);
        sadFace.SetActive(false);
        rb = ZTV.GetComponent<Rigidbody>();
        rb.useGravity = false;
        playerLost = false;
        oscilation = false;
        myQuestionList = JsonUtility.FromJson<QuestionList>(jsonTXT.text);
        GetNewQuestion();
        playerManager = player.GetComponent<PlayerManager>();
        bossManager = boss.GetComponent<BossManager>();
        //finalScreen.SetActive(false);
    }

    private void Update() {
        Lose();
        ZTVidleanimation();
    }
    private void ZTVidleanimation()
    {
        if(!playerLost)
        {
            if(oscilation)
            {
                ZTV.transform.localPosition = 
                    Vector3.Lerp(ZTV.transform.localPosition, new Vector3(0,0.05f,0), 1f * Time.deltaTime);
                if(ZTV.transform.localPosition.y >= 0.04f) oscilation = !oscilation;
            }
            else
            {
                ZTV.transform.localPosition = 
                    Vector3.Lerp(ZTV.transform.localPosition, new Vector3(0,-0.05f,0), 1f * Time.deltaTime);
                if(ZTV.transform.localPosition.y <= -0.04f) oscilation = !oscilation;
            }
        }
    }
    private void GetNewQuestion()
    {
        
        System.Random random = new System.Random();
        index = random.Next(0, myQuestionList.questions.Length);
        questionUI.text = myQuestionList.questions[index].question;
        answersUI[0].text = myQuestionList.questions[index].options[0];
        answersUI[1].text = myQuestionList.questions[index].options[1];
        answersUI[2].text = myQuestionList.questions[index].options[2];
        answersUI[3].text = myQuestionList.questions[index].options[3];
    }

    public void GetResponse(int indexResponse)
    {
        if(myQuestionList.questions[index].correctOptionIndex == indexResponse)
        {
            Debug.Log("correct");
            bossAnimator.SetBool(BossActions.IsDamaged, true);
            bossManager.TakeDamage(playerManager.GetDamage());
            if(bossManager.GetCurrentHealth() <= 0) DefeatBoss();
            else StartCoroutine(DelayedGetNewQuestion(Color.green, indexResponse));
        }
        else
        {
            Debug.Log("wrong");
            bossAnimator.SetBool(BossActions.IsAttack, true);
            playerManager.TakeDamage(bossManager.GetDamage());
            if(playerManager.GetCurrentHealth() <= 0) ThrowZTV();
            else StartCoroutine(DelayedGetNewQuestion(Color.red, indexResponse));
        }
    }
    private IEnumerator DelayedGetNewQuestion(Color color, int indexResponse)
    {
        //Desactivar opciones
        foreach (var button in buttonsUI)
        {
            button.GetComponent<Button>().enabled = false;
        }

        buttonsUI[indexResponse].GetComponent<Image>().color = color;
        yield return new WaitForSeconds(delay);

        //Reactivar opciones
        foreach (var button in buttonsUI)
        {
            button.GetComponent<Button>().enabled = true;
        }

        buttonsUI[indexResponse].GetComponent<Image>().color = Color.white;
        GetNewQuestion();
    }

    private void ThrowZTV(){
        playerLost = true;
        rb.useGravity = true;
        rb.AddForce(new Vector3(0,5,1), ForceMode.Impulse);
        sadFace.SetActive(true);
        triviaScreen.SetActive(false);
    }

    private void DefeatBoss(){
        bossAnimator.SetBool(BossActions.IsDefeated, true);
        happyFace.SetActive(true);
        triviaScreen.SetActive(false);
    }

    private void Lose()
    {
        if(playerLost)
        {
            ZTV.transform.localRotation = 
                Quaternion.Lerp(
                    ZTV.transform.localRotation, 
                    Quaternion.Euler(90, 0, 0), 
                    3 * Time.deltaTime);
            if(ZTV.transform.localRotation.x >= 80)
            {
                playerLost = false;
            }
        }
    }
}
