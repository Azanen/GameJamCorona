using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Scenes Loading
using UnityEngine.UI; // For using UI

public class Scr_PlayAgain : MonoBehaviour
{

    public Text finalScore;
    public Scr_NPC scoreNPC;

    void Update()
    {

        //countdownText.text = "Time until the day ends : " + currentTime.ToString("0"); //transfert de float a String.
        finalScore.text = "Your Final Score: "+ scoreNPC.satisfactionPoint + " points";
        

    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
