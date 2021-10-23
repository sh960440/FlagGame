using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    [SerializeField] private Text speakerName;
    [SerializeField] private Text message;

    public IEnumerator TypeSentence(string sentence)
    {
        message.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            message.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
    }

    public void SetSpeaker(string speaker)
    {
        speakerName.text = speaker;
    }
}