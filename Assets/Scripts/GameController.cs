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
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ReturnToStore()
    {
        SceneManager.LoadScene(0);
    }

    public int GetMinutes() {  return minutes; }
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

        secondsText.text = string.Format("{0:D2}", seconds);
        minutesText.text = string.Format("{0}", minutes);

        if(Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        } else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
        }
    }
}
