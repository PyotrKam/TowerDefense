using System;

using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class UpgradeShop : MonoBehaviour
    {
         
        [SerializeField] private int money;
        [SerializeField] private Text moneyText;
        [SerializeField] private BuyUpgrade[] sales;

        void Start()
        {
            money = MapCompletion.Instance.TotalScore;
            moneyText.text = money.ToString();
            foreach (var slot in sales)
            {
                slot.Initialize();
            }
        }
        
        
    }
}

