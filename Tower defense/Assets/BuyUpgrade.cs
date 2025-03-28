using System;
using UnityEngine;
using UnityEngine.UI;
using SpaceShooter;

namespace TowerDefence
{
    public class BuyUpgrade : MonoBehaviour
    {
        [SerializeField] private UpgradeAsset asset;
        [SerializeField] private Image upgradeIcon;
        [SerializeField] private Text level, costText;
        [SerializeField] private Button buyBotton;
        private int costNumber = 0;

        // Trying
        //public static event Action<UpgradeAsset> OnUpgradePurchased;

        public void Initialize()
        {
              
            upgradeIcon.sprite = asset.sprite;
            var savedLevel = Upgrades.GetUpgradeLevel(asset);
            

            if (savedLevel >= asset.costByLevel.Length)
            {
                level.text = $"Lvl: {savedLevel} (Max)";
                buyBotton.interactable = false;
                buyBotton.transform.Find("Image (1)").gameObject.SetActive(false);
                buyBotton.transform.Find("Text").gameObject.SetActive(false);
                costText.text = "X";
                costNumber = int.MaxValue;
            }
            else
            {
                level.text = $"{savedLevel + 1}";
                costNumber = asset.costByLevel[savedLevel];
                costText.text = costNumber.ToString();
            }
            
        }

        internal void CheckCost(int money)
        {
            buyBotton.interactable = money >= costNumber;
        }

        public void Buy()
        {
            //Debug.Log($"Trying to buy {asset.name}");

            Upgrades.BuyUpgrade(asset);
            Initialize();
           
            //OnUpgradePurchased?.Invoke(asset);


        }
    }
}

