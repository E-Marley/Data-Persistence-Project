using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUI : MonoBehaviour
{
    public InputField nameInput;
    public List<Text> scoreSlots;

    void Start()
    {
        UpdateHighScores();
    }

    void UpdateHighScores()
    {
        List<string> playerNames = DataManager.instance.GetHighScoreNames();
        List<int> playerScores = DataManager.instance.GetHighScores();

        for (int i = 0; i < playerNames.Count; i++)
        {
            string scoreText = playerNames[i] + " (" + playerScores[i] + ")";
            scoreSlots[i].text = "High score: " + scoreText;
        }
    }
    public void StartNew()
    {
        //Store player name upon clicking Start button
        string playerName = nameInput.text;
        DataManager.instance.playerName = playerName;
        SceneManager.LoadScene(1);
    }
    private void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); //original code to quit Unity player
#endif
    }
    private void ViewHighScores()
    {
        SceneManager.LoadScene(2);
    }
}
