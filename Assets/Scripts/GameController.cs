using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    [SerializeField] int seconds = 0;
    [SerializeField] int minutes = 0;
    [SerializeField] float timer = 0;
    [SerializeField] TMP_Text secondsText;
    [SerializeField] TMP_Text minutesText;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject endGameScreen;
    [SerializeField] GameObject UIElements;
    [SerializeField] GameObject comboScreen;
    [SerializeField] GameObject ingredientIcons;
    [SerializeField] AudioSource audiosource;
    [SerializeField] AudioClip runMusic;
    [SerializeField] AudioClip bossIntroMusic;
    [SerializeField] AudioClip bossMusic;

    [SerializeField] GameObject boss;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        player = GameObject.FindGameObjectWithTag("Player");
        audiosource = GetComponent<AudioSource>();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ReturnToStore()
    {
        SceneManager.LoadScene(0);
    }
    public void SetBoss(GameObject newBoss)
    {
        boss = newBoss;
    }

    public int GetMinutes() {  return minutes; }
    public int GetSeconds() {  return seconds; }

    public void CombosScreenButton()
    {
        pauseScreen.SetActive(false);
        UIElements.SetActive(false);
        comboScreen.SetActive(true);
    }

    public void ReturnToPauseMenu()
    {
        pauseScreen.SetActive(true);
        UIElements.SetActive(true);
        comboScreen.SetActive(false);
    }

    public void ContinueButton()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        ingredientIcons.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (timer < 1)
        {
            timer += Time.deltaTime;
        } else { seconds++; timer = 0; }
        if(seconds >= 60)
        {
            seconds = 0; minutes++;
        }

        if(minutes < 3 && !audiosource.isPlaying)
        {
            audiosource.loop = true;
            audiosource.PlayOneShot(runMusic);
        }
        if (minutes == 3 && seconds == 0)
        {
            audiosource.Stop();
        }
        if (minutes >= 3 && seconds == 1 && !audiosource.isPlaying)
        {
            Debug.Log("BossSongPlaying");
            audiosource.loop = false;
            audiosource.PlayOneShot(bossIntroMusic);
        }
        if (minutes >= 3 && seconds > 0 && !audiosource.isPlaying)
        {
            audiosource.loop = true;
            audiosource.PlayOneShot(bossMusic);
        }


        secondsText.text = string.Format("{0:D2}", seconds);
        minutesText.text = string.Format("{0}", minutes);

        if(Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1 && !gameOverScreen.activeSelf)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
            ingredientIcons.SetActive(false);
        } else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0 && !gameOverScreen.activeSelf)
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
            ingredientIcons.SetActive(true);
        }

        if (player.GetComponent<PlayerMovementScript>().PlayerStatus())
        {
            Time.timeScale = 0;
            gameOverScreen.SetActive(true);
            ingredientIcons.SetActive(false);
        }

        if(boss.GetComponent<Enemy1>().GetHealth() <= 0)
        {
            Time.timeScale = 0;
            endGameScreen.SetActive(true);
            UIElements.SetActive(false);
        }
    }
}
