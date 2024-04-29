using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public TMP_Dropdown potionPart1Dropdown;
    public TMP_Dropdown potionPart2Dropdown;
    static List<Ingredient> ingredients;

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

        public UInt16 GetPotionPartID() { return ingredientID; }
        public string GetPotionPartName() {  return ingredientName; }
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
        //Ingredient ingredient1 = 
        //Potion potion = new Potion(ingredient1, ingredient2);
        //Debug.Log(potion.GetFinishedPotionID().ToString());
        int finishedPotionID = (ingredients[potionPart1Dropdown.value].GetPotionPartID() - 1) * 4 + (ingredients[potionPart2Dropdown.value].GetPotionPartID() - 1);
        Debug.Log(finishedPotionID);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (ingredients == null) {
            ingredients = new List<Ingredient> { new Fireium(), new Waterium(), new Lightningium(), new Windium() };
        }

        List<string> ingredientList = new List<string>();

        foreach (Ingredient ingredient in ingredients) {
            ingredientList.Add(ingredient.GetPotionPartName());
        }

        potionPart1Dropdown = GameObject.FindGameObjectWithTag("PotionPart1Dropdown").GetComponent<TMP_Dropdown>();
        potionPart2Dropdown = GameObject.FindGameObjectWithTag("PotionPart2Dropdown").GetComponent<TMP_Dropdown>();

        potionPart1Dropdown.ClearOptions();
        potionPart2Dropdown.ClearOptions();

        potionPart1Dropdown.AddOptions(ingredientList);
        potionPart2Dropdown.AddOptions(ingredientList);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
