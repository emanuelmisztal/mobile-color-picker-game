/*
 * Author: Emanuel Misztal
 * 2019
 */

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
    private float speed; // stores current speed of round

    void Awake()
    {
        InitializeDictionary(); // initialize dictionary
        ResetVariables(); // reset variables
    }

    // Start is called before the first frame update
    void Start()
    {
        // wait until touch
        colorText.text = "TOUCH"; // change center text to TOUCH
    }

    // reset variables
    private void ResetVariables()
    {
        points = 0; // reset point counter
        correctCounter.text = "0"; // reset counter text
        levelText.text = "1"; // set current level text to 1
        level = 1; // set current level to 1
        nextLevel = 2; // set next level to 2
        previousLevel = 1; // set previous level to 1
        speed = 2f; // set speed to 2f
    }

    // start new round
    private void NewRound()
    {
        StopCoroutine("UpdateColor"); // stop coroutine if it was played
        StartCoroutine("UpdateColor"); // start coroutine from begining
    }

    // initialize colors dictionary
    protected virtual void InitializeDictionary()
    {
        // create dictionary with color name, color value pairs
        colors = new Dictionary<string, Color>()
        {
            {"BLACK", Color.black},
            {"RED", Color.red},
            {"LIME", new Color(0f,1f,0f)},
            {"BLUE", Color.blue}, 
            {"CYAN", Color.cyan},
            {"MAGENTA", Color.magenta},
            {"GRAY", Color.gray},
            {"MAROON", new Color(0.5f,0f,0f)},
            {"OLIVE", new Color(0.5f,0.5f,0f)},
            {"GREEN", Color.green},
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
            if (correctColor == colors.ElementAt<KeyValuePair<string, Color>>(randomNumber).Key) break; // if random got the same color
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
            case true: // if answer was true
                if (correctColor == colorText.text) AddCorrect(); // increment correct points if color match text
                else DecreseCorrect(); // decrement correct points if color does not match text
                break;

            case false: // if answer was false
                if (correctColor != colorText.text) AddCorrect(); // increment correct points if color does not match text
                else DecreseCorrect(); // decrement correct points if color match text
                break;
        }

        CheckLevelStatus(); // check if level should be incresed or decresed
        NewRound(); // begin new round
    }

    // increment correct points
    private void AddCorrect()
    {
        points++; // increment points
        correctCounter.text = points.ToString(); // update points text
    }

    // increment correct points
    private void DecreseCorrect()
    {
        // check if points are greater than 0
        if (points > 0)
        {
            points--; // decrement points
            correctCounter.text = points.ToString(); // update points text
        }
    }

    // check level status
    private void CheckLevelStatus()
    {
        // check if number of points is greater than number of points to gain new level
        if (points > nextLevel)
        {
            level++; // increase level
            levelText.text = level.ToString(); // update level indicator
            if (speed > 0.2f) speed -= 0.2f; // decrease speed between rounds
            nextLevel *= 2; // increse points to gain level
            previousLevel *= 2; // increse points to previous level
        }

        // check if number of points is smaller than number of points to lose to previous level but level is greater than 1
        if (points < previousLevel && level > 1)
        {
            level--; // decrease level
            levelText.text = level.ToString(); // update level indicator
            speed += 0.2f; // slow down speed
            nextLevel /= 2; // decrease points to next level
            previousLevel = (int)Mathf.Ceil(previousLevel/2.0f); // calculate points to previous level
        }
    }

    // event - exit button clicked
    public void OnExitButtonClicked()
    {
        Application.Quit(); // exit game
    }
}
