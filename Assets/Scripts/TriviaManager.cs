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
    Rigidbody rb;

    //[SerializeField] GameObject finalScreen;
    //[SerializeField] TextMeshProUGUI finalText;

    private void Start() 
    {
        rb = transform.GetComponent<Rigidbody>();
        myQuestionList = JsonUtility.FromJson<QuestionList>(jsonTXT.text);
        GetNewQuestion();
        playerManager = player.GetComponent<PlayerManager>();
        bossManager = boss.GetComponent<BossManager>();
        //finalScreen.SetActive(false);
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
            bossManager.TakeDamage(playerManager.GetDamage());
            if(bossManager.GetCurrentHealth() <= 0)
            {
                //finalScreen.SetActive(true);
                //finalText.text = "WIN";
            }
            StartCoroutine(DelayedGetNewQuestion(Color.green, indexResponse));
        }
        else 
        {
            Debug.Log("wrong");
            playerManager.TakeDamage(bossManager.GetDamage());
            if(playerManager.GetCurrentHealth() <= 0)
            {
                rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
                rb.AddForce(Vector3.forward * 10f, ForceMode.Impulse);
                //finalScreen.SetActive(true);
                //finalText.text = "LOSE";
            }
            StartCoroutine(DelayedGetNewQuestion(Color.red, indexResponse));
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
}
