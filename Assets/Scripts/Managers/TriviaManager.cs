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
    [SerializeField] GameObject cameraPlayer;
    //[SerializeField] bool startShake = false;
    [SerializeField] AnimationCurve curveShake;
    [SerializeField] float durationShake = 1f;
    [SerializeField] Image timer;
    [SerializeField] float maxTime = 15f;
    bool timerActive;
    float currentTime = 15;
    float currentFillAmount = 0;
    [SerializeField] AudioClip[] bossAttackSounds;
    [SerializeField] AudioClip bossHurtSounds;
    [SerializeField] AudioClip zapZTV;
    [SerializeField] AudioClip correctZTV;
    [SerializeField] GameObject zapParticles;
    [SerializeField] GameObject fireParticles;
    System.Random random;

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
        random = new System.Random();
        timer.fillAmount = 1;
        timerActive = true;
        happyFace.SetActive(false);
        sadFace.SetActive(false);
        zapParticles.SetActive(false);
        rb = ZTV.GetComponent<Rigidbody>();
        rb.useGravity = false;
        playerLost = false;
        oscilation = false;
        myQuestionList = JsonUtility.FromJson<QuestionList>(jsonTXT.text);
        playerManager = player.GetComponent<PlayerManager>();
        bossManager = boss.GetComponent<BossManager>();
        GetNewQuestion();
    }
    
    
    private void Update() {
        Lose();
        IdleAnimationZTV();
        if(timerActive) QuestionTimer();
        /*if(startShake)
        {
            StartCoroutine(Shaking());
            startShake = false;
        }*/
        
    }
    public IEnumerator Shaking()
    {
        Vector3 startPosition = cameraPlayer.transform.localPosition;
        float elapsedTime = 0f;
        while(elapsedTime < durationShake)
        {
            elapsedTime += Time.deltaTime;
            float Strength = curveShake.Evaluate(elapsedTime / durationShake);
            cameraPlayer.transform.localPosition = startPosition + Random.insideUnitSphere * Strength;
            yield return null;
        }
        cameraPlayer.transform.localPosition = startPosition;
    }
    private void IdleAnimationZTV()
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
        
        index = random.Next(0, myQuestionList.questions.Length);
        questionUI.text = myQuestionList.questions[index].question;
        answersUI[0].text = myQuestionList.questions[index].options[0];
        answersUI[1].text = myQuestionList.questions[index].options[1];
        answersUI[2].text = myQuestionList.questions[index].options[2];
        answersUI[3].text = myQuestionList.questions[index].options[3];
        timer.fillAmount = 1;
        currentTime = maxTime;
        timerActive = true;
    }
    private void QuestionTimer()
    {
        currentTime -= 1 * Time.deltaTime; 
        if(currentTime > 0)
        {
            currentFillAmount = currentTime/15;
            timer.fillAmount = currentFillAmount;
        }
        else 
        {
            timerActive = false;
            AudioManager.Instance.PlaySound(zapZTV);
            StartCoroutine(ShowZapParticlesZTV());
            AudioManager.Instance.PlaySound(bossAttackSounds[random.Next(0, bossAttackSounds.Length)]);
            bossAnimator.SetBool(BossActions.IsAttack, true);
            playerManager.TakeDamage(bossManager.GetDamage());
            if(playerManager.GetCurrentHealth() <= 0) ThrowZTV();
            else StartCoroutine(TimeOver(delay));
        }
        
    }
    private IEnumerator TimeOver(float delayQuestion)
    {
        //Desactivar opciones
        foreach (var button in buttonsUI)
        {
            button.GetComponent<Button>().enabled = false;
            button.GetComponent<Image>().color = Color.red;
        }
        yield return new WaitForSeconds(delayQuestion);
        //Reactivar opciones
        foreach (var button in buttonsUI)
        {
            button.GetComponent<Button>().enabled = true;
            button.GetComponent<Image>().color = Color.white;
        }
        GetNewQuestion();
    }
    public void GetResponse(int indexResponse)
    {
        timerActive = false;
        if(myQuestionList.questions[index].correctOptionIndex == indexResponse)
        {
            //Debug.Log("correct");
            AudioManager.Instance.PlaySound(correctZTV);
            AudioManager.Instance.PlaySound(bossHurtSounds);
            bossAnimator.SetBool(BossActions.IsDamaged, true);
            bossManager.TakeDamage(playerManager.GetDamage());
            if(bossManager.GetCurrentHealth() <= 0) DefeatBoss();
            else StartCoroutine(DelayedGetNewQuestion(Color.green, indexResponse, delay/2));
        }
        else
        {
            //Debug.Log("wrong");
            AudioManager.Instance.PlaySound(zapZTV);
            StartCoroutine(ShowZapParticlesZTV());
            AudioManager.Instance.PlaySound(bossAttackSounds[random.Next(0, bossAttackSounds.Length)]);
            bossAnimator.SetBool(BossActions.IsAttack, true);
            playerManager.TakeDamage(bossManager.GetDamage());
            if(playerManager.GetCurrentHealth() <= 0) ThrowZTV();
            else StartCoroutine(DelayedGetNewQuestion(Color.red, indexResponse, delay));
        }
    }
    private IEnumerator DelayedGetNewQuestion(Color color, int indexResponse, float delayQuestion)
    {
        //Desactivar opciones
        ToggleTriviaOptions(color, indexResponse, false);
        yield return new WaitForSeconds(delayQuestion);
        //Reactivar opciones
        ToggleTriviaOptions(Color.white, indexResponse, true);
        GetNewQuestion();
    }
    private void ToggleTriviaOptions(Color color, int indexResponse, bool toggle)
    {
        foreach (var button in buttonsUI)
        {
            button.GetComponent<Button>().enabled = toggle;
        }
        buttonsUI[indexResponse].GetComponent<Image>().color = color;
    }

    private void ThrowZTV(){
        playerLost = true;
        rb.useGravity = true;
        rb.AddForce(new Vector3(0,5,1), ForceMode.Impulse);
        fireParticles.SetActive(false);
        timer.enabled = false;
        sadFace.SetActive(true);
        triviaScreen.SetActive(false);
        StartCoroutine(GoToLobby());
    }

    private void DefeatBoss(){
        bossAnimator.SetBool(BossActions.IsDefeated, true);
        happyFace.SetActive(true);
        triviaScreen.SetActive(false);
        boss.SetActive(false);
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
    private IEnumerator ShowZapParticlesZTV()
    {
        zapParticles.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        zapParticles.SetActive(false);
    }
    private IEnumerator GoToLobby()
    {
        yield return new WaitForSeconds(5);
        ChangeSceneManager.Instance.GoToScene(0);
        AudioManager.Instance.ChangeMusic(ChangeSceneManager.Instance.sountrack[0]);
    }
}
