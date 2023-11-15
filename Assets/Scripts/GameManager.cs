using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [Header("World Time")]
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI daytext;
    [SerializeField] float dayTimer;
    [SerializeField] int dayNumber = 1;
    private float dayMaxTime = 60;

    [Header("Company Stats")]
    public string companyName;
    public float money = 0;
    public float income = 1;
    public float popularity = 0;

    [Header("Upgrades")]
    [SerializeField] private bool donationUpgrade = false;

    private float donationChance;

    [Header("ChangeGameState")]
    bool GameHasEnded = false;

    public GameObject GameOverBackground;
    public void GameOver()
    {
        if (GameHasEnded == false)
        {
            GameHasEnded = true;
            GameOverBackground.SetActive(true);
            Debug.Log("Game Over");
        }
    }
    //Instead of using a button to end game use the following for same effect
    //FindObjectOfType<GameManager>().GameOver();
    public void RestartGame()
    {
        if (GameHasEnded == true) 
        {
            GameHasEnded = false;
            GameOverBackground.SetActive(false);
            Debug.Log("New Game");
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void FixedUpdate()
    {

        if (dayTimer >= dayMaxTime)
        {
            dayNumber++;
            Debug.Log("day: " + dayNumber);
            dayTimer = 0;
        }
        else
        {
            dayTimer += Time.fixedDeltaTime;
        }


        if (donationUpgrade)
        {
            donationChance = Random.Range(0, 100);

            if (donationChance < popularity)
            {
                money += Random.Range(0, donationChance) / 2;
                Debug.Log("You got a dontaion of: " + donationChance / 2 + " Dabloons");
            }
        }

    }

    private void Update()
    {
        int minutes = Mathf.FloorToInt(dayTimer / 60);
        int seconds = Mathf.FloorToInt(dayTimer % 60);
        //timerText.text = ((int)dayTimer).ToString();
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        daytext.text = string.Format("Day " + dayNumber);
    }


    public void Pause()
    {
        Time.timeScale = 0; 
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void FastForward()
    {
        Time.timeScale = 2;
    }



    public void ChangeName(string addedText)
    {
        companyName = addedText;
        Debug.Log("Your Company is named: " + addedText);
    }

}
