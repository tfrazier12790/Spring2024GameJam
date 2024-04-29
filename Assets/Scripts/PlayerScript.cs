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
    public class Potion
    {
        PotionPart part1;
        PotionPart part2;
        UInt16 finishedPotionID;

        public void SetPotionPart1( PotionPart newPart1)
        {
            part1 = newPart1;
        }
        public PotionPart GetPotionPart1() {  return part1; }
        public void SetPotionPart2( PotionPart newPart2)
        {
            part2 = newPart2;
        }
        public PotionPart GetPotionPart2() { return part2; }
        void SetFinishedPotionID()
        {
            finishedPotionID = (UInt16)((int)part1.GetPotionPartID() * (int)part2.GetPotionPartID());
        }
    }

    public class PotionPart
    {
        protected UInt16 potionPartID;
        protected string potionPartName;

        public UInt16 GetPotionPartID() { return potionPartID; }
        public string GetPotionPartName() {  return potionPartName; }
    }

    public class FirePotion : PotionPart
    {
        public FirePotion()
        {
            potionPartID = 1;
            potionPartName = "Fire Potion";
        }
        public string GetName() {  return potionPartName; }
    }
    public class IcePotion : PotionPart
    {
        public IcePotion()
        {
            potionPartID = 2;
            potionPartName = "Ice Potion";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Potion potion = new Potion();
        List<string> ingredientList = new List<string>();
        potion.SetPotionPart1(new FirePotion());
        potion.SetPotionPart2 (new IcePotion());
        ingredientList.Add(potion.GetPotionPart1().GetPotionPartName());
        ingredientList.Add(potion.GetPotionPart2().GetPotionPartName());

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
