using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Make game manager public static so can access this from other scripts
    public static GameManager gm;

    public bool gameOver = false;
    
    public GameObject playAgainButton;
    public GameObject exitButton;

    public Text gameOverText;
    public Text pumpkinLives;
    
    public string playAgainLevelToLoad;
    public string nextLevelToLoad;

    private int eatenTimes;
    public int pumpkinHealth = 3;

    // Start is called before the first frame update
    void Start()
    {
        // Get a reference to the GameManager component for use by other scripts
        if (gm == null)
            gm = this.gameObject.GetComponent<GameManager>();

        pumpkinLives.text = "3";

        // Inactivate the buttons and texts, if set
        gameOverText.gameObject.SetActive(false);

        if (playAgainButton)
            playAgainButton.SetActive(false);

        if (exitButton)
            exitButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(eatenTimes == 3)
        {
            EndGame();
        }
    }

    public void PumpkinEaten(int timesEaten)
    {
        eatenTimes += timesEaten;
        pumpkinHealth -= timesEaten;
        pumpkinLives.text = pumpkinHealth.ToString();
    }

    void EndGame()
    {
        gameOver = true;

        // Activate gameover text
        gameOverText.gameObject.SetActive(true);

        // Activate the buttons
        if (playAgainButton)
            playAgainButton.SetActive(true);

        if (exitButton)
            exitButton.SetActive(true);
        
        Debug.Log("Game Over!");
    }

    void BeatLevel()
    {
        gameOver = true;

        // repurpose the timer to display a message to the player
        //mainTimerDisplay.text = "LEVEL COMPLETE";
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(playAgainLevelToLoad);
    }

    public void NextLevel()
    {
        //Load next scene
        SceneManager.LoadScene(nextLevelToLoad);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}