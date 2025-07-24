using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles all of the music and timing of the game, displaying the amount of time that has
/// elapsed in the game, as well as when the game ends by either player death or the death 
/// of the boss enemy, and loading the store scene when the game ends.
/// </summary>

public class TimerScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TMP_Text secondsText;
    [SerializeField] TMP_Text minutesText;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject winGameScreen;
    [SerializeField] GameObject UIElements;
    [SerializeField] GameObject comboScreen;
    [SerializeField] GameObject ingredientIcons;
    [SerializeField] GameObject boss;
    GameObject player;

    [Header("Audio sources and files")]
    [SerializeField] AudioSource audiosource;
    [SerializeField] AudioClip runMusic;
    [SerializeField] AudioClip bossIntroMusic;
    [SerializeField] AudioClip bossMusic;

    // Variables for keeping track of time
    int seconds = 0;
    int minutes = 0;
    float timer = 0;

    void Start()
    {
        // Set time scale to normal and set references to player and audio source
        Time.timeScale = 1;
        player = GameObject.FindGameObjectWithTag("Player");
        audiosource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (timer < 1)
        {
            timer += Time.deltaTime;
        }
        else { seconds++; timer = 0; }
        if (seconds >= 60)
        {
            seconds = 0; minutes++;
        }

        // Switch between normal, boss intro, and moss music based on elapsed time
        if (minutes < 3 && !audiosource.isPlaying)
        {
            // Minute 1 - 3: Normal Music
            audiosource.loop = true;
            audiosource.PlayOneShot(runMusic);
        }
        if (minutes == 3 && seconds == 0)
        {
            // Stop any music playing for boss intro
            audiosource.Stop();
        }
        if (minutes >= 3 && seconds == 1 && !audiosource.isPlaying)
        {
            // Minute 3: Start boss intro music (only plays once)
            audiosource.loop = false;
            audiosource.PlayOneShot(bossIntroMusic);
        }
        if (minutes >= 3 && seconds > 0 && !audiosource.isPlaying)
        {
            // Loop boss music once intro is finished
            audiosource.loop = true;
            audiosource.PlayOneShot(bossMusic);
        }

        // Updates timer text with minutes and seconds
        secondsText.text = string.Format("{0:D2}", seconds);
        minutesText.text = string.Format("{0}", minutes);

        // Toggles pause screen when escape is pressed (disabled at game over)
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1 && !gameOverScreen.activeSelf)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
            ingredientIcons.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0 && !gameOverScreen.activeSelf)
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
            ingredientIcons.SetActive(true);
        }

        // End game is player is dead
        if (player.GetComponent<PlayerScript>().IsPlayerDead())
        {
            Time.timeScale = 0;
            gameOverScreen.SetActive(true);
            ingredientIcons.SetActive(false);
        }

        // If the boss has spawned and died, show win screen
        if (boss != null)
        {
            if (boss.GetComponent<Enemy1Script>().GetHealth() <= 0)
            {
                Time.timeScale = 0;
                winGameScreen.SetActive(true);
                UIElements.SetActive(false);
            }
        }
    }

    // Quit game function
    public void QuitGame()
    {
        Application.Quit();
    }

    // Reset game to store screen
    public void ReturnToStore()
    {
        SceneManager.LoadScene(0);
    }

    // Function to remotely set the boss
    public void SetBoss(GameObject newBoss)
    {
        boss = newBoss;
    }

    // Return minutes and seconds respectively
    public int GetMinutes() {  return minutes; }
    public int GetSeconds() {  return seconds; }

    // Show button combo screen
    public void CombosScreenButton()
    {
        pauseScreen.SetActive(false);
        UIElements.SetActive(false);
        comboScreen.SetActive(true);
    }

    // Return to pause screen from combo screen
    public void ReturnToPauseMenu()
    {
        pauseScreen.SetActive(true);
        UIElements.SetActive(true);
        comboScreen.SetActive(false);
    }

    // Unpause game from pause screen
    public void ContinueButton()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        ingredientIcons.SetActive(true);
    }
}
