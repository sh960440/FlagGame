using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    FLAG_IN,        // Set question + Set flag image + Flag fade in
    ROBOT_IN,       // Robot enters1 + Speaker name update + Dialog box fade in + Show question animation
    BUTTONS_IN,     // Show buttons
    BUTTONS_OUT,    // Hide buttons + Player enters + Speaker name update + Show answer animation
    RESULT,         // Speaker name update + Show result animation
    ROBOT_OUT,      // Robot leaves + Player leaves + Dialog box fade out + Flag fade out
    FLAG_OUT
}

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private GameState state = GameState.FLAG_IN;
    [SerializeField] private Flag[] allFlags;
    private Flag question;
    private int answer;
    private int playerAnswer = -1;

    private void OnEnable()
    {
        if (GameController.instance == null)
        {
            GameController.instance = this;
        }
        else
        {
            if (GameController.instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        UIController.OnFadingFinished += DoNextStepAfterFading;
    }

    private void OnDisable()
    {
        UIController.OnFadingFinished -= DoNextStepAfterFading;
    }

    private void Start()
    {
        SetNextQuestion();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (state == GameState.BUTTONS_IN)
                {
                    UIController.instance.ShowButtons();
                }
                else if (state == GameState.BUTTONS_OUT)
                {
                    SwitchState();
                }
                else if (state == GameState.RESULT)
                {
                    SwitchState();
                }
            }
        }
    }

    public void PickAnswer(int pickedAnswer)
    {
        SwitchState();
        UIController.instance.HideButtons();
        UIController.instance.ShowPlayerAnswer(pickedAnswer);
        playerAnswer = pickedAnswer;
    }

    private void SetNextQuestion()
    {
        question = allFlags[Random.Range(0, allFlags.Length)];
        answer = Random.Range(0, 4);
        UIController.instance.SetOptionTextAndFlagImage(question, answer);
    }

    private void DoNextStepAfterFading()
    {
        SwitchState();
    }

    private void SwitchState()
    {
        if (state == GameState.FLAG_OUT) state = GameState.FLAG_IN;
        else state++;

        switch (state)
        {
            case GameState.FLAG_IN:
                SetNextQuestion();
                break;
            case GameState.ROBOT_IN:
                UIController.instance.ShowQuestion();
                break;
            case GameState.BUTTONS_IN:
                
                break;
            case GameState.BUTTONS_OUT:
                
                break;
            case GameState.RESULT:
                UIController.instance.ShowResult(playerAnswer, answer);
                break;
            case GameState.ROBOT_OUT:
                UIController.instance.CleanScene();
                break;
            case GameState.FLAG_OUT:
                
                break;
        }
    }

    public string GetRandomNameFromAllFlags()
    {
        return allFlags[Random.Range(0, allFlags.Length)].flagName;
    }
}
