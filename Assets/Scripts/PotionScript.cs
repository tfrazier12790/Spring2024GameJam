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
    [SerializeField] Sprite lightningSprite;
    [SerializeField] Sprite windSprite;

    public class Potion
    {
        Ingredient ingredient1;
        Ingredient ingredient2;
        UInt16 finishedPotionID;

        public Potion(Ingredient newIngredient1, Ingredient newIngredient2)
        {
            this.ingredient1 = newIngredient1;
            this.ingredient2 = newIngredient2;
        }

        public void SetPotionPart1( Ingredient newPart1)
        {
            ingredient1 = newPart1;
        }
        public Ingredient GetPotionPart1() {  return ingredient1; }
        public void SetPotionPart2( Ingredient newPart2)
        {
            ingredient2 = newPart2;
        }
        public Ingredient GetPotionPart2() { return ingredient2; }
        void SetFinishedPotionID()
        {
            finishedPotionID = (UInt16)((int)ingredient1.GetPotionPartID() * (int)ingredient2.GetPotionPartID());
        }
        public UInt16 GetFinishedPotionID() {  return finishedPotionID; }
    }

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
            //SetSprite(fireSprite);
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
    public class Lightningium : Ingredient
    {
        public Lightningium()
        {
            ingredientID = 3;
            ingredientName = "Lightningium";
        }
    }
    public class Windium : Ingredient
    {
        public Windium()
        {
            ingredientID = 4;
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
            finishedPotionID = (ingredients[0].GetPotionPartID() - 1) * 4 + (ingredients[1].GetPotionPartID() - 1);
        }
        ingredients.Clear();
        Debug.Log("Potion ID: " + finishedPotionID);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (ingredients == null) {
            ingredients = new List<Ingredient>(2);
        }
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
            Ingredient tempIngredient = new Lightningium();
            tempIngredient.SetSprite(lightningSprite);
            ingredients.Add(tempIngredient);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
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
