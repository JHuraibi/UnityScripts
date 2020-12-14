using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomTimer : MonoBehaviour
{
    // Use this for initialization
    private float time;
    private Text timerUI;
    private Text userMessageUI;

    private bool validTextUIs = true;
    private bool validMessage = true;
    private bool validLevelName = true;

    public string nameOfTimerUI;
    public string nameOfMessageUI;
    public string message;
    public int secondsToShowMessage;
    public string nextLevelName;
    public int secondsToGoToNextLevel;

    void Start()
    {
        SetupTextUIs();
        SetupMessageUI();
        CheckTimeEntered();
        CheckStringsEntered();

        time = 0.0f;
    }

    private void SetupTextUIs()
    {
        if (nameOfTimerUI == "")
        {
            Debug.Log("<color=red>Error: </color>No Timer TextUI object name given.");
            validTextUIs = false;
        }
        else if (GameObject.Find(nameOfTimerUI).GetComponent<Text>())
        {
            timerUI = GameObject.Find(nameOfTimerUI).GetComponent<Text>();
            timerUI.text = "";
        }
        else
        {
            Debug.Log("<color=red>Error: </color>TextUI \"" + nameOfTimerUI + "\" was not found");
            validTextUIs = false;
        }
    }


    private void SetupMessageUI()
    {
        if (nameOfMessageUI == "")
        {
            Debug.Log("<color=red>Error: </color>No User Message TextUI object name given.");
            validTextUIs = false;
        }
        else if (GameObject.Find(nameOfMessageUI).GetComponent<Text>())
        {
            userMessageUI = GameObject.Find(nameOfMessageUI).GetComponent<Text>();
            userMessageUI.text = "";
        }
        else
        {
            Debug.Log("<color=red>Error: </color>TextUI \"nameOfUserMessageUI\" was not found");
            validTextUIs = false;
        }
    }

    private void CheckTimeEntered()
    {
        if (secondsToShowMessage > secondsToGoToNextLevel)
        {
            Debug.Log("<color=#802400ff>Warning: </color" +
                "Time to show message was longer than time to go to next level.");
        }
        else if (secondsToShowMessage < 0)
        {
            Debug.Log("<color=red>Error: </color" +
                "Time to show message was negative.");
            validTextUIs = false;
        }
    }

    private void CheckStringsEntered()
    {
        if (nextLevelName == "")
        {
            Debug.Log("<color=#802400ff>Warning: </color>No Level Name Given.");
            validLevelName = false;
        }

        if (message == "")
        {
            Debug.Log("<color=#802400ff>Warning: </color>No Message Was Given.");
            validMessage = false;
        }
    }

    void Update()
    {
        time = time + Time.deltaTime;

        if (validTextUIs && validMessage)
        {
            UpdateTimerUI();
            UpdateMessageUI();
        }
    }

    private void UpdateTimerUI()
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        string currentTime = "";

        if (minutes < 10)
        {
            currentTime += "0";
        }

        currentTime += minutes.ToString();

        currentTime += ":";

        if (seconds < 10)
        {
            currentTime += "0";
        }

        currentTime += seconds.ToString();

        // Uncomment line below to print the current time to the console in Unity
        //Debug.Log(minutes + ":" + seconds);

        timerUI.text = currentTime;
    }

    private void UpdateMessageUI()
    {
        if (time > secondsToShowMessage)
        {
            if (validMessage)
            {
                userMessageUI.text = message;
            }
            else
            {
                userMessageUI.text = "Time Almost Up.";
            }
        }

        if (time > secondsToGoToNextLevel)
        {

            if (validLevelName)
            {
                SceneManager.LoadScene(nextLevelName);
            }
            else
            {
                SceneManager.LoadScene("indoor");
            }
        }
    }

}
