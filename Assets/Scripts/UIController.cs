using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Events
    public delegate void FadeAction();
    public static event FadeAction OnFadingFinished; 

    // Singleton variable
    public static UIController instance;

    // Game UI
    public Character leftCharacter;
    public Character rightCharacter;
    private DialogBox dialogBox;
    [SerializeField] private Image flagImage;
    [SerializeField] private GameObject buttons;
    [SerializeField] private Text[] buttonText;

    private void OnEnable()
    {
        if (UIController.instance == null)
        {
            UIController.instance = this;
        }
        else
        {
            if (UIController.instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void Start()
    {
        dialogBox = transform.GetComponentInChildren<DialogBox>();
    }
    
    public void SetOptionTextAndFlagImage(Flag question, int answer)
    {
        // Set options
        buttonText[answer].text = question.flagName;
        for (int i = 0; i < 4; i++)
        {
            if (i != answer)
            {
                buttonText[i].text = GetRandomName();
            }
        }

        // Set flag image
        flagImage.sprite = question.flagSprite;

        // Flag fade in
        StartCoroutine(DoFade(flagImage.gameObject.GetComponent<CanvasGroup>(), 0.0f, 1.0f, 0.5f));
    }

    private string GetRandomName()
    {
        string tempString = GameController.instance.GetRandomNameFromAllFlags();
        while(tempString == buttonText[0].text || tempString == buttonText[1].text || tempString == buttonText[2].text || tempString == buttonText[3].text)
        {
            tempString = GameController.instance.GetRandomNameFromAllFlags();
        }

        return tempString;
    }

    public IEnumerator DoFade(CanvasGroup canvasGroup, float start, float end, float duration)
    {
        float counter = 0.0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / duration);
            yield return null;
        }
        if (OnFadingFinished != null) OnFadingFinished();
    }

    public void ShowQuestion()
    {
        StartCoroutine(leftCharacter.Move(true, 0.8f));
        dialogBox.SetSpeaker("Dr. Flag");
        StartCoroutine(DoFade(dialogBox.gameObject.GetComponent<CanvasGroup>(), 0.0f, 1.0f, 0.5f));
        StartCoroutine(dialogBox.TypeSentence(MessageStrings.flagQuestion[Random.Range(0, MessageStrings.flagQuestion.Length - 1)]));
    }

    public void ShowPlayerAnswer(int clickedButton)
    {
        StartCoroutine(rightCharacter.Move(true, 0.8f));
        dialogBox.SetSpeaker("You");
        StartCoroutine(dialogBox.TypeSentence(MessageStrings.playerAnswer[Random.Range(0, MessageStrings.playerAnswer.Length - 1)] + buttonText[clickedButton].text));
    }

    public void ShowResult(int playerAnswer, int correctAnswer)
    {
        StopAllCoroutines();
        dialogBox.SetSpeaker("Dr. Flag");
        if (playerAnswer == correctAnswer) StartCoroutine(dialogBox.TypeSentence(MessageStrings.answer[0] + buttonText[correctAnswer].text));
        else StartCoroutine(dialogBox.TypeSentence(MessageStrings.answer[1] + buttonText[correctAnswer].text));
    }

    public void ShowButtons()
    {
        if(buttons.activeSelf == false) buttons.SetActive(true);
    }

    public void HideButtons()
    {
        if(buttons.activeSelf == true) buttons.SetActive(false);
    }

    public void CleanScene()
    {
        StopAllCoroutines();
        StartCoroutine(leftCharacter.Move(false, 0.8f));
        StartCoroutine(rightCharacter.Move(false, 0.8f));
        StartCoroutine(DoFade(dialogBox.gameObject.GetComponent<CanvasGroup>(), 1.0f, 0.0f, 0.5f));
        StartCoroutine(DoFade(flagImage.gameObject.GetComponent<CanvasGroup>(), 1.0f, 0.0f, 0.5f));
    }
}