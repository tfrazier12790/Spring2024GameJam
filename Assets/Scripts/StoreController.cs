using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls navigation in the main store menu,
/// including starting the game and displaying the tutorial, credits, and combo info.
/// </summary>

public class StoreController : MonoBehaviour
{
    [Header("Screen References")]
    [SerializeField] GameObject storefront;
    [SerializeField] GameObject tutorialScreen;
    [SerializeField] GameObject creditsScreen;
    [SerializeField] GameObject combosScreen;

    // Quits game
    public void QuitGame()
    {
        Application.Quit();
    }

    // Loads the main game scene
    public void StartGame()
    {
        Debug.Log("Load Scene");
        SceneManager.LoadScene(1);
    }

    // Displays the tutorial screen, hiding all other screens
    public void OpenTutorial()
    {
        tutorialScreen.SetActive(true);
        storefront.SetActive(false);
        combosScreen.SetActive(false);
        creditsScreen.SetActive(false);
    }
    
    // Returns to the main store screen, hiding all other screens
    public void ReturnToStore()
    {
        creditsScreen.SetActive(false);
        tutorialScreen.SetActive(false);
        storefront.SetActive(true);
        combosScreen.SetActive(false);
    }

    // Displays the credits screen, hiding all other screens
    public void OpenCredits()
    {
        creditsScreen.SetActive(true);
        storefront.SetActive(false);
        tutorialScreen.SetActive(false);
        combosScreen.SetActive(false);
    }

    // Displays the combinations screen, hiding all other screens
    public void OpenCombosScreen()
    {
        creditsScreen.SetActive(false);
        storefront.SetActive(false);
        tutorialScreen.SetActive(false);
        combosScreen.SetActive(true);
    }
}
