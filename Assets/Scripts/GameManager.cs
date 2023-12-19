using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [Header("World Time")]
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI daytext;
    [SerializeField]
    private float time;
    [SerializeField] float dayTimer;
    [SerializeField] int dayNumber = 1;
    private float dayMaxTime = 60;

    public delegate void NewDayHandler();
    public event NewDayHandler OnNewDay;
    public Button pauseButton;
    public Button resumeButton;
    public Button fastForwardButton;

    [Header("Company Stats")]
    public string companyName;
    public float money = 0;
    public float income = 1;

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
        time = Time.deltaTime;

        if (dayTimer >= dayMaxTime)
        {
            dayNumber++;
            dayTimer = 0;
            Debug.Log("day: " + dayNumber);

            PollutionController.instance.EndDayAdd();

            OnNewDay?.Invoke();

        }
        else
        {
            dayTimer += Time.fixedDeltaTime;
        }



        int minutes = Mathf.FloorToInt(dayTimer / 60);
        int seconds = Mathf.FloorToInt(dayTimer % 60);
        // timerText.text = ((int)dayTimer).ToString();
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        daytext.text = string.Format("Day " + dayNumber);


        /// <summary>
        /// Constant timer
        /* 
        float secondsInDay = 60 * 60 * 24;
        float secondsInHour = 60 * 60;
        int days = Mathf.FloorToInt(time / secondsInDay);
        int hours = Mathf.FloorToInt(time % secondsInDay / 3600);
        int minutes = Mathf.FloorToInt(time % secondsInHour / 60);
        int seconds = Mathf.FloorToInt(time % 60);
         */
        /// </summary>

    }

    public void Pause()
    {
        Time.timeScale = 0;
        Debug.Log("Time is paused");
    }

    public void Resume()
    {
        Time.timeScale = 1;
        Debug.Log("Time is resumed");
    }

    public void FastForward()
    {
        Time.timeScale = 4;
        Debug.Log("Time is fast forwarded");
    }

    public void OnTimerButtons1(InputValue button)
    {
        Pause();
        Debug.Log("hotkey: pause");
        EventSystem.current.SetSelectedGameObject(null); //Deselects selected game objects
        pauseButton.Select();

    }

    public void OnTimerButtons2(InputValue button)
    {
        Resume();
        Debug.Log("hotkey: resume");
        EventSystem.current.SetSelectedGameObject(null);
        resumeButton.Select();
    }

    public void OnTimerButtons3(InputValue button)
    {
        FastForward();
        Debug.Log("hotkey: fast forward");
        EventSystem.current.SetSelectedGameObject(null);
        fastForwardButton.Select();
    }




    public void ChangeName(string addedText)
    {
        companyName = addedText;
        Debug.Log("Your Company is named: " + addedText);
    }

    public void Buy(float priceAmount)
    {
        money -= priceAmount;
    }

}
