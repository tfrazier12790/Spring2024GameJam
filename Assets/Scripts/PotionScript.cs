using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Data;

public class NewBehaviourScript : MonoBehaviour
{
    static List<Ingredient> ingredients;
    [SerializeField] SpriteRenderer ingredient1SpriteRenderer;
    [SerializeField] SpriteRenderer ingredient2SpriteRenderer;
    [SerializeField] Sprite fireSprite;
    [SerializeField] Sprite waterSprite;
    [SerializeField] Sprite windSprite;

    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject tidalWavePrefab;
    [SerializeField] GameObject windShearPrefab;
    [SerializeField] GameObject steamPrefab;

    [SerializeField] GameObject scorekeeper;

    public class Ingredient
    {
        protected UInt16 ingredientID;
        protected string ingredientName;
        public Sprite ingredientSprite;

        public UInt16 GetPotionPartID() { return ingredientID; }
        public string GetPotionPartName() {  return ingredientName; }
        public Sprite GetSprite() { return ingredientSprite; }
        public void SetSprite(Sprite newSprite)
        {
            ingredientSprite = newSprite;
        }
    }

    public class Fireium : Ingredient
    {
        public Fireium()
        {
            ingredientID = 1;
            ingredientName = "Fireium";
        }
        public string GetName() {  return ingredientName; }

        
    }
    public class Waterium : Ingredient
    {
        public Waterium()
        {
            ingredientID = 2;
            ingredientName = "Waterium";
        }
        public string GetName() { return ingredientName; }
    }
    public class Windium : Ingredient
    {
        public Windium()
        {
            ingredientID = 3;
            ingredientName = "Windium";
        }
    }
    public void ThrowPotion()
    {
        int finishedPotionID = -1;
        Debug.Log("First Element: " + ingredients[0].GetPotionPartName());
        Debug.Log("Second Element: " + ingredients[1].GetPotionPartName());
        if (ingredients.Count == 2)
        {
            finishedPotionID = (ingredients[0].GetPotionPartID() - 1) * 3 + (ingredients[1].GetPotionPartID() - 1);
        }
        ingredients.Clear();
        Debug.Log("Potion ID: " + finishedPotionID);
        switch (finishedPotionID) {
            case 0:
                Explosion(); break;
            case 1:
                Steam();  break;
            case 2:
                Updraft(); break;
            case 3:
                Steam(); break;
            case 4:
                TidalWave(); break;
            case 5:
                Cleanse(); break;
            case 6:
                Updraft(); break;
            case 7:
                Cleanse(); break;
            case 8:
                Windshear(); break;
        }
    }

    public void Explosion()
    {
        Instantiate(explosionPrefab, MouseWorldPosition(), transform.rotation);
    }

    public void TidalWave()
    {
        Instantiate(tidalWavePrefab, transform.position, transform.rotation);
    }
    public void Windshear()
    {
        Instantiate(windShearPrefab, transform.position, transform.rotation);
    }
    public void Cleanse()
    {
        gameObject.GetComponent<PlayerMovementScript>().AddHealth(((float)scorekeeper.GetComponent<ScoreKeeper>().GetWaterStat() * 0.6f) + 
                                                                  ((float)scorekeeper.GetComponent<ScoreKeeper>().GetWindStat() * 0.6f));
    }
    public void Steam()
    {
        Instantiate(steamPrefab, MouseWorldPosition(), transform.rotation);
    }
    public void Updraft()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector3.forward);

        //Debug.Log(ray);
        Debug.Log(hit.collider.tag);
        if (hit.collider.tag == "Enemy")
        {
            Debug.Log("HitEnemy");
            hit.collider.gameObject.SendMessage("StartDot");

        }
    }

    public Vector3 MouseWorldPosition()
    {
        return new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (ingredients == null) {
            ingredients = new List<Ingredient>(2);
        }
        scorekeeper = GameObject.FindGameObjectWithTag("Scorekeeper");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ThrowPotion();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (ingredients.Count >= 2)
            { 
                ingredients.RemoveAt(0);
            }
            Ingredient tempIngredient = new Fireium();
            tempIngredient.SetSprite(fireSprite);
            ingredients.Add(tempIngredient);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (ingredients.Count >= 2)
            {
                ingredients.RemoveAt(0);
            }
            Ingredient tempIngredient = new Waterium();
            tempIngredient.SetSprite(waterSprite);
            ingredients.Add(tempIngredient);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (ingredients.Count >= 2)
            {
                ingredients.RemoveAt(0);
            }
            Ingredient tempIngredient = new Windium();
            tempIngredient.SetSprite(windSprite);
            ingredients.Add(tempIngredient);
        }
        if (ingredients.Count >= 1)
        {
            ingredient1SpriteRenderer.sprite = ingredients[0].GetSprite();
        } else { ingredient1SpriteRenderer.sprite = null;  }

        if (ingredients.Count >= 2)
        {
            ingredient2SpriteRenderer.sprite = ingredients[1].GetSprite();
        } else { ingredient2SpriteRenderer.sprite = null; }
    }
}
