using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text gameText;

    [SerializeField] private GameObject wongameScreen;
    [SerializeField] private GameObject lostScreen;

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void LostGame()
    {
        lostScreen.SetActive(true);
    }

    public void KeepPlaying(string message)
    {
        gameText.text = message;
        wongameScreen.SetActive(true);
    }

    public void Click_KeepPlaying()
    {
        wongameScreen.SetActive(false);
        lostScreen.SetActive(false);
    }

    public void Click_Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
