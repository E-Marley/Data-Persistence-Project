using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public Text nameInput;
    public string playerName;
   // public string playerName;
    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        //If on Menu screen, grab current player name from the text input box
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            nameInput = GameObject.Find("PlayerNameText").GetComponent<Text>();
            playerName = nameInput.text;
        }

    }
    public void SaveName()
    {
        //Take current player name to next scene within the Data Manager upon clicking 'Start'
        playerName = nameInput.text;
    }
}
