using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    // public Image background; // 改回private
    public Text message; // 改回private
    // public string nextMessage = ""; // 改回private

    //private float transparency = 0;
    //public float fadeSpeed = 0; // 改回private

    private void Start()
    {
        // background = transform.GetComponentInChildren<Image>();
        message = transform.GetComponentInChildren<Text>();
    }

    private void Update()
    {
        // if (fadeSpeed != 0)
        // {
        //     transparency += fadeSpeed * Time.deltaTime;
        //     if (transparency >= 1 || transparency <= 0) fadeSpeed = 0;
        //     background.color = new Color(background.color.r, background.color.g, background.color.b, transparency);
        //     message.color = new Color(message.color.r, message.color.g, message.color.b, transparency);
        // }
    }

    /*public void ShowMessage(string content)
    {
        fadeSpeed = 5f;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(SetNextMessage(content)));
    }*/

    //
    //public void HideMessage() => fadeSpeed = -5f;

    /*public void SkipMessageAnimation()
    {
        StopAllCoroutines();
        messageText.text = nextMessage;
    }*/

    public IEnumerator TypeSentence(string sentence)
    {
        message.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            message.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
    }
}