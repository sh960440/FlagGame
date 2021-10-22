using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState
{
    READY,
    ENTERING,
    MESSAGE,
    LEAVING
}

public class Character : MonoBehaviour
{
    public delegate void SwitchAction(CharacterState state);
    public static event SwitchAction OnStateSwitched;

    const float X_IN = 300;
    const float X_OUT = 1000;
    const float Y_IN = 0;
    const float Y_OUT = 150;
    
    private RectTransform rectTransform;
    private CharacterState state = CharacterState.READY;
    [SerializeField] private float direction;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Update()
    {
        switch (state)
        {
            case CharacterState.ENTERING:
                rectTransform.localPosition = new Vector3(Mathf.Lerp(rectTransform.localPosition.x, X_IN * direction, 0.1f), Mathf.Lerp(rectTransform.localPosition.y, Y_IN, 0.1f));
                break;

            case CharacterState.LEAVING:
                rectTransform.localPosition = new Vector3(Mathf.Lerp(rectTransform.localPosition.x, X_OUT * direction, 0.1f), Mathf.Lerp(rectTransform.localPosition.y, Y_OUT, 0.1f));
                if (Y_OUT - rectTransform.localPosition.y <= 3)
                {
                    rectTransform.localPosition = new Vector3(X_OUT * direction, -Y_OUT);
                    SwitchState();
                }
                break;
        }
    }

    public IEnumerator Move(bool isMovingIn, float duration)
    {
        float counter = 0.0f;

        if (isMovingIn)
        {
            rectTransform.localPosition = new Vector3(X_OUT * direction, -Y_OUT);
        }

        while (counter < duration)
        {
            counter += Time.deltaTime;
            
            if (isMovingIn) rectTransform.localPosition = new Vector3(Mathf.Lerp(rectTransform.localPosition.x, X_IN * direction, counter / duration), Mathf.Lerp(rectTransform.localPosition.y, Y_IN, counter / duration));
            else rectTransform.localPosition = new Vector3(Mathf.Lerp(rectTransform.localPosition.x, X_OUT * direction, counter / duration), Mathf.Lerp(rectTransform.localPosition.y, Y_OUT, counter / duration));

            yield return null;
        }

        
    }

    public void SwitchState()
    {
        if (state == CharacterState.LEAVING) state = CharacterState.READY;
        else state++;
        
        switch (state)
        {
            case CharacterState.ENTERING:
                //UIController.instance.ShowMessage(MessageStrings.flagQuestion[Random.Range(0, MessageStrings.flagQuestion.Length -1)]);
                break;
            case CharacterState.MESSAGE:
                //UIController.instance.SkipMessageAnimation();
                break;
            case CharacterState.LEAVING:
                //UIController.instance.HideMessage();
                break;
        }

        if (OnStateSwitched != null) OnStateSwitched(state);
    }
}