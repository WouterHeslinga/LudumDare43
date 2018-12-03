using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ErrorMessages : MonoBehaviour
{
    public static ErrorMessages Instance { get; private set; }
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    public Text messageText;

    public void ShowMessage(string message, float displayTime = 2)
    {
        StopAllCoroutines();
        StartCoroutine(DisplayMessage(message, displayTime));
    }

    private IEnumerator DisplayMessage(string message, float displayTime = 2)
    {
        messageText.gameObject.SetActive(true);
        messageText.text = message;
        yield return new WaitForSeconds(2);
        messageText.text = "";
        messageText.gameObject.SetActive(false);
    }
}
