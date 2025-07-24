using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Data;
using Unity.VisualScripting;

/// <summary>
/// Handles potion crafting and casting based on two selected ingredients.
/// Supports 3 elements (fire, water, wind) each with cooldowns and unique
/// effects like explosions, healing, and status effects.
/// Displays active ingredients and manages cooldowns using UI Icons
/// </summary>

// Represents a base ingredient in potion crafting
public class Ingredient
{
    protected UInt16 ingredientID;
    public Sprite ingredientSprite;

    public UInt16 GetPotionPartID() { return ingredientID; }
    public Sprite GetSprite() { return ingredientSprite; }
    public void SetSprite(Sprite newSprite)
    {
        ingredientSprite = newSprite;
    }
}

// Fire Based Ingredient
public class Fire : Ingredient
{
    public Fire()
    {
        ingredientID = 1;
    }
}

// Water Based ingredient
public class Water : Ingredient
{
    public Water()
    {
        ingredientID = 2;
    }
}

// Wind based ingredient
public class Wind : Ingredient
{
    public Wind()
    {
        ingredientID = 3;
    }
}

public class PotionScript : MonoBehaviour
{
    // Static list to store the current potion ingredients
    static List<Ingredient> ingredients;

    [Header("Sprite references")]
    [SerializeField] SpriteRenderer ingredient1SpriteRenderer;
    [SerializeField] SpriteRenderer ingredient2SpriteRenderer;
    [SerializeField] Sprite fireSprite;
    [SerializeField] Sprite waterSprite;
    [SerializeField] Sprite windSprite;

    [Header("UI Element Icons")]
    [SerializeField] Image fireIcon;
    [SerializeField] Image waterIcon;
    [SerializeField] Image windIcon;

    // Variables for cooldown timers
    float fireCoolDown = 0f;
    float waterCoolDown = 0f;
    float windCoolDown = 0f;

    [Header("Audio References")]
    [SerializeField] AudioClip updraftSound;
    [SerializeField] AudioClip cleanseSound;
    [SerializeField] AudioSource audioSource;

    [Header("Potion Effect Prefabs")]
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject tidalWavePrefab;
    [SerializeField] GameObject windShearPrefab;
    [SerializeField] GameObject steamPrefab;
    [SerializeField] GameObject missTextPrefab;
    [SerializeField] float missTextOffset;

    void Start()
    {
        // Initialize ingredient list and object references
        if (ingredients == null) {
            ingredients = new List<Ingredient>(2);
        }
        ingredients.Clear();    // Reset ingredient list at game start

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Casts potion on right click
        if (Input.GetMouseButtonDown(1))
        {
            ThrowPotion();
        }

        // Updates cooldown timers and UI icon states
        if(fireCoolDown > 0)
        {
            fireCoolDown -= Time.deltaTime;
            fireIcon.color = Color.gray;    // Grays out icon if on cooldown
        } else
        {
            fireIcon.color = Color.white;
        }

        if (waterCoolDown > 0)
        {
            waterCoolDown -= Time.deltaTime;
            waterIcon.color = Color.gray;
        } else
        {
            waterIcon.color = Color.white;
        }

        if (windCoolDown > 0)
        {
            windCoolDown -= Time.deltaTime;
            windIcon.color = Color.gray;
        } else
        {
            windIcon.color = Color.white;
        }

        // Add ingredient to potion on keyboard input (1 = Fire, 2 = Water, 3 = Wind)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (fireCoolDown <= 0)
            {
                if (ingredients.Count >= 2)
                {
                    // Removes oldest added ingredient, replacing it with new ingredient
                    ingredients.RemoveAt(0);
                }
                Ingredient tempIngredient = new Fire();
                tempIngredient.SetSprite(fireSprite);
                ingredients.Add(tempIngredient);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (waterCoolDown <= 0)
            {
                if (ingredients.Count >= 2)
                {
                    ingredients.RemoveAt(0);
                }
                Ingredient tempIngredient = new Water();
                tempIngredient.SetSprite(waterSprite);
                ingredients.Add(tempIngredient);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (windCoolDown <= 0)
            {
                if (ingredients.Count >= 2)
                {
                    ingredients.RemoveAt(0);
                }
                Ingredient tempIngredient = new Wind();
                tempIngredient.SetSprite(windSprite);
                ingredients.Add(tempIngredient);
            }
        }

        // Updates UI Sprites to show selected ingredients
        if (ingredients.Count >= 1)
        {
            ingredient1SpriteRenderer.sprite = ingredients[0].GetSprite();
        } else { ingredient1SpriteRenderer.sprite = null;  }

        if (ingredients.Count >= 2)
        {
            ingredient2SpriteRenderer.sprite = ingredients[1].GetSprite();
        } else { ingredient2SpriteRenderer.sprite = null; }
    }

    // Determines effect based on ingredient combination
    public void ThrowPotion()
    {
        int finishedPotionID = -1; // Fallback value for invalid/empty combinations

        // Determines potion ID based on ingredients
        if (ingredients.Count == 2)
        {
            finishedPotionID = (ingredients[0].GetPotionPartID() - 1) * 3 + (ingredients[1].GetPotionPartID() - 1);
        }

        // Performs correct potion effect based on potion ID
        switch (finishedPotionID)
        {
            case 0:
                Explosion(); fireCoolDown = 3f; break;
            case 1:
                Steam(); fireCoolDown = 1f; waterCoolDown = 1f; break;
            case 2:
                Updraft(); fireCoolDown = 1f; windCoolDown = 1f; break;
            case 3:
                Steam(); fireCoolDown = 1f; waterCoolDown = 1f; break;
            case 4:
                TidalWave(); waterCoolDown = 3f; break;
            case 5:
                Cleanse(); waterCoolDown = 2f; windCoolDown = 2f; break;
            case 6:
                Updraft(); fireCoolDown = 1f; windCoolDown = 1f; break;
            case 7:
                Cleanse(); waterCoolDown = 2f; windCoolDown = 2f; break;
            case 8:
                Windshear(); windCoolDown = 3f; break;
        }
        ingredients.Clear(); // Clears ingredients 
    }

    // All Potion Effects

    public void Explosion() 
    {
        Instantiate(explosionPrefab, MouseWorldPosition(), Quaternion.Euler(new Vector3(0, 0, UnityEngine.Random.Range(0.0f, 360.0f))));
    }

    public void TidalWave()
    {
        Instantiate(tidalWavePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, UnityEngine.Random.Range(0.0f, 360.0f))));
    }
    public void Windshear()
    {
        Instantiate(windShearPrefab, transform.position, transform.rotation);
    }
    public void Cleanse()
    {
        // Heal player based on player water and wind stat
        gameObject.GetComponent<PlayerScript>().AddHealth(((float)ScoreKeeper.Instance.GetComponent<ScoreKeeper>().GetWaterStat() * 0.6f) +
                                                                  ((float)ScoreKeeper.Instance.GetComponent<ScoreKeeper>().GetWindStat() * 0.6f));
        audioSource.PlayOneShot(cleanseSound);
    }
    public void Steam()
    {
        Instantiate(steamPrefab, MouseWorldPosition(), Quaternion.Euler(new Vector3(0, 0, UnityEngine.Random.Range(0.0f, 360.0f))));
    }
    public void Updraft()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector3.forward);

        if (hit.collider.tag == "Enemy")
        {
            // Apply damage over time effect to enemy
            Debug.Log("HitEnemy");
            hit.collider.gameObject.SendMessage("StartDot");
            audioSource.PlayOneShot(updraftSound);
        }
        else
        {
            // Show miss feedback if no enemy is hit
            Debug.Log("Dot Missed!");
            Instantiate(missTextPrefab);
        }
    }

    // Converts mouse screen position to world space
    public Vector3 MouseWorldPosition()
    {
        Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(screenPos.x, screenPos.y, 0);
    }

}
