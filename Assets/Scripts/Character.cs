using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    const float X_IN = 300;
    const float X_OUT = 1000;
    const float Y_IN = 0;
    const float Y_OUT = 150;
    
    private RectTransform rectTransform;
    private float direction;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        direction = rectTransform.localPosition.x / Mathf.Abs(rectTransform.localPosition.x);
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
}