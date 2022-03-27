using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class BankManager : MonoBehaviour
{
    public class Prices
    {
        public static int animalPrice = 50;
        public static int landPrice = 200;
        public static int boxUpradePrice = 1000;
    }

    public static BankManager instance;

    private int currentMoney = 0;

    public TextMeshProUGUI moneyText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateMoneyText();
    }

    public void AddMoney(int ammount)
    {
        currentMoney += ammount;
        UpdateMoneyText();
    }

    public void RemoveMoney(int ammount)
    {
        if(ammount <= currentMoney)
        {
            currentMoney -= ammount;
            UpdateMoneyText();
        }
    }

    private void UpdateMoneyText()
    {
        moneyText.text = "Coins: " + currentMoney;
    }

    public bool CanBuyAnimal()
    {
        return (currentMoney >= Prices.animalPrice);
    }

    public bool CanBuyLand()
    {
        return (currentMoney >= Prices.landPrice);
    }

    public bool CanUpgradeBox()
    {
        return (currentMoney >= Prices.boxUpradePrice);
    }

    public bool CanBuyObject(int price)
    {
        return (currentMoney >= price);
    }
}
