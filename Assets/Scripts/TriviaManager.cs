using TMPro;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class TriviaManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionUI;
    [SerializeField] GameObject[] buttonsUI;
    [SerializeField] TextMeshProUGUI[] answersUI;
    private int index;
    [SerializeField] float delay = 3.0f;
    List<TriviaQuestion> questions = new List<TriviaQuestion>();

    private void Start() 
    {
        questions = FileHandler.ReadFromJSON<TriviaQuestion>("QuestionsDataFile.json");
        GetNewQuestion();
    }

    private void GetNewQuestion()
    {
        System.Random random = new System.Random();
        index = random.Next(0, questions.Count);
        questionUI.text = questions[index].question;
        answersUI[0].text = questions[index].options[0];
        answersUI[1].text = questions[index].options[1];
        answersUI[2].text = questions[index].options[2];
        answersUI[3].text = questions[index].options[3];
    }

    public void GetResponse(int indexResponse)
    {
        if(questions[index].correctOptionIndex == indexResponse)
        {
            Debug.Log("correct");
            StartCoroutine(DelayedGetNewQuestion(Color.green, indexResponse));
        }
        else 
        {
            Debug.Log("wrong");
            StartCoroutine(DelayedGetNewQuestion(Color.red, indexResponse));
        }
    }
    private IEnumerator DelayedGetNewQuestion(Color color, int indexResponse)
    {
        foreach (var button in buttonsUI)
        {
            button.GetComponent<Button>().enabled = false;
        }
        buttonsUI[indexResponse].GetComponent<Image>().color = color;
        yield return new WaitForSeconds(delay);
        foreach (var button in buttonsUI)
        {
            button.GetComponent<Button>().enabled = true;
        }
        buttonsUI[indexResponse].GetComponent<Image>().color = Color.white;
        GetNewQuestion();
    }
}
