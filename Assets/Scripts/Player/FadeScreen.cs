using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] public float fadeDuration = 2;
    [SerializeField] Color fadeColor;
    Renderer rend;
    [SerializeField] bool changeScene = false;
    public static FadeScreen Instance {get; private set;}
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        rend = GetComponent<Renderer>();
        FadeIn();
    }
    private void Update() {
        if(changeScene)
        {
            changeScene = false;
            FadeOut();
        }
    }
    public void FadeIn()
    {
        Fade(1, 0);
    }
    public void FadeOut()
    {
        Fade(0, 1);
    }
    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeAction(alphaIn,alphaOut));
    }
    public IEnumerator FadeAction(float alphaIn, float alphaOut)
    {
        float timer = 0;
        Color newColor = fadeColor; 
        while(timer <= fadeDuration)
        {
            //newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn,alphaOut, timer/fadeDuration);
            rend.material.SetColor("_Color", newColor);
            timer += Time.deltaTime;
            yield return null;
        }
        newColor.a = alphaOut;
        rend.material.SetColor("_Color", newColor);
    }
}
