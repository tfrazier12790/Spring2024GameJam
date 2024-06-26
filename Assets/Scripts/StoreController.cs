using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject storefront;
    [SerializeField] GameObject tutorialScreen;
    [SerializeField] GameObject creditsScreen;
    [SerializeField] GameObject combosScreen;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        Debug.Log("Load Scene");
        SceneManager.LoadScene(1);
    }
    public void OpenTutorial()
    {
        tutorialScreen.SetActive(true);
        storefront.SetActive(false);
        combosScreen.SetActive(false);
        creditsScreen.SetActive(false);
    }
    
    public void ReturnToStore()
    {
        creditsScreen.SetActive(false);
        tutorialScreen.SetActive(false);
        storefront.SetActive(true);
        combosScreen.SetActive(false);
    }

    public void OpenCredits()
    {
        creditsScreen.SetActive(true);
        storefront.SetActive(false);
        tutorialScreen.SetActive(false);
        combosScreen.SetActive(false);
    }
    public void OpenCombosScreen()
    {
        creditsScreen.SetActive(false);
        storefront.SetActive(false);
        tutorialScreen.SetActive(false);
        combosScreen.SetActive(true);
    }
}
