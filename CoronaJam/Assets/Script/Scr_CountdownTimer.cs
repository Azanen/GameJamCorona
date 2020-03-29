using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Scenes Loading


public class Scr_CountdownTimer : MonoBehaviour
{
    private float currentTime = 0f;
    private float startingTime = 15f; //duree en seconde du Timer => 60 = 1 min
    public Scr_Manager manager;
    public Text countdownText;
    public Image potionInventory;
    public Image maskInventory;
    public Image totalInventory;
    public Text score;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;

        countdownText.text = "Time until the day ends : " + currentTime.ToString("0"); //transfert de float a String.

        if (currentTime <= 0)
        {
            currentTime = 0; //si timer descend en bas de 0, et va aller a la scene ENDGAME
            EndMenu();
        }
        potionInventory.fillAmount = (float) manager.potionNbr / 5;
        maskInventory.fillAmount = (float)manager.maskNbr / 5;
        totalInventory.fillAmount = (float)manager.total / 5;
      
    }

    //Mes Functions

    void EndMenu()
    {
        //Application.LoadLevel("EndMenu");
        SceneManager.LoadScene("EndMenu");
    }

    
}
