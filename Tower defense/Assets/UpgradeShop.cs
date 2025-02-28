using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class UpgradeShop : MonoBehaviour
    {
        [SerializeField] private int money;
        [SerializeField] private Text moneyText;

        void Start()
        {
            money = MapCompletion.Instance.TotalScore;
            moneyText.text = money.ToString();
        }        
    }
}

