using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Text
using System.Linq; // Dictionary.ElementAt<KeyValuePair<x, y>>()

public class EasyMode : MonoBehaviour
{
    public Text colorText; // link to the text displaying color name 
    public Text correctCounter; // link to correct points displayed
    public Text levelText; // link to level text

    private Dictionary<string, Color> colors; // dictionary with color name - value pairs - to be assigned in Start
    private string correctColor; // store correct color name from first draw
    private int points; // counter for correct answers
    private int level; // store current level
    private int nextLevel; // store points to reach next level
    private int previousLevel; // store points to reach previous level
    private float speed; // stores current speed

    void Awake()
    {
        // initialize dictionary
        InitializeDictionary();

        // reset variables
        ResetVariables();
    }

    // Start is called before the first frame update
    void Start()
    {
        // wait until touch
        colorText.text = "TOUCH";
    }

    // reset variables
    private void ResetVariables()
    {
        points = 0;
        correctCounter.text = "0";
        levelText.text = "1";
        level = 1;
        nextLevel = 2;
        previousLevel = 1;
        speed = 2.0f;
    }

    // start new round
    private void NewRound()
    {
        StopCoroutine("UpdateColor");
        StartCoroutine("UpdateColor");
    }

    // initialize colors dictionary
    protected virtual void InitializeDictionary()
    {
        colors = new Dictionary<string, Color>()
        {
            {"BLACK", new Color(0f,0f,0f)},
            {"RED", new Color(1f,0f,0f)},
            {"LIME", new Color(0f,1f,0f)},
            {"BLUE", new Color(0f,0f,1f)},
            {"CYAN", new Color(0f,1f,1f)},
            {"MAGENTA", new Color(1f,0f,1f)},
            {"GRAY", new Color(0.5f,0.5f,0.5f)},
            {"MAROON", new Color(0.5f,0f,0f)},
            {"OLIVE", new Color(0.5f,0.5f,0f)},
            {"GREEN", new Color(0f,0.5f,0f)},
            {"PURPLE", new Color(0.5f,0f,0.5f)},
            {"TEAL", new Color(0f,0.5f,0.5f)},
            {"NAVY", new Color(0f,0f,0.5f)}
        };
    }

    // this coroutine represents every turn of the game
    protected virtual IEnumerator UpdateColor()
    {
        // randomize
        int randomNumber = Random.Range(0, colors.Count); // get one random from dictionary range
        correctColor = colors.ElementAt<KeyValuePair<string, Color>>(randomNumber).Key; // get correct color name
        colorText.color = colors.ElementAt<KeyValuePair<string, Color>>(randomNumber).Value; // assign color to color text

        // randomize
        for (int i = 0; i < colors.Count/2; i++)
        {
            randomNumber = Random.Range(0, colors.Count); // get one random from dictionary range
            if (correctColor == colors.ElementAt<KeyValuePair<string, Color>>(randomNumber).Key) break;
            else continue;
        }
        colorText.text = colors.ElementAt<KeyValuePair<string, Color>>(randomNumber).Key; // display random color text

        // wait for small ammount of time
        yield return new WaitForSeconds(speed);

        // start again
        NewRound();
    }

    // button event - check if the player is rightor wrong
    public void CheckAnswer(bool answer)
    {
        switch (answer)
        {
            case true:
                if (correctColor == colorText.text) AddCorrect();
                else DecreseCorrect();
                break;
            case false:
                if (correctColor != colorText.text) AddCorrect();
                else DecreseCorrect();
                break;
        }

        CheckLevelStatus();
        NewRound();
    }

    // increment correct points
    private void AddCorrect()
    {
        points++;
        correctCounter.text = points.ToString();
    }

    // increment correct points
    private void DecreseCorrect()
    {
        if (points > 0)
        {
            points--;
            correctCounter.text = points.ToString();
        }
    }

    // check level status
    private void CheckLevelStatus()
    {
        if (points > nextLevel)
        {
            level++;
            levelText.text = level.ToString();
            if (speed > 0.2f) speed -= 0.2f;
            nextLevel *= 2;
            previousLevel *= 2;
        }

        if (points < previousLevel && level > 1)
        {
            level--;
            levelText.text = level.ToString();
            speed += 0.2f;
            nextLevel /= 2;
            previousLevel = (int)Mathf.Ceil(previousLevel/2.0f);
        }
    }

    // event - exit button clicked
    public void OnExitButtonClicked()
    {
        Application.Quit();
    }
}
