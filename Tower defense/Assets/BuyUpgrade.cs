using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class BuyUpgrade : MonoBehaviour
    {
        [SerializeField] private UpgradeAsset asset;
        [SerializeField] private Image upgradeIcon;
        [SerializeField] private Text level, cost;
        [SerializeField] private Button buyBotton;

        public void Initialize()
        {
              
            upgradeIcon.sprite = asset.sprite;
            var savedLevel = Upgrades.GetUpgradeLevel(asset);
            
            if (savedLevel >= asset.costByLevel.Length)
            {
                level.text += "(Max)";
                buyBotton.interactable = false;
                buyBotton.transform.Find("Image (1)").gameObject.SetActive(false);
                buyBotton.transform.Find("Text").gameObject.SetActive(false);

                cost.text = "X";
            }
            else
            {
                level.text = $"{savedLevel + 1}";
                cost.text = asset.costByLevel[savedLevel].ToString();
            }
            
        }

        internal void CheckCost(int money)
        {
            throw new System.NotImplementedException();
        }

        public void Buy()
        {
            Upgrades.BuyUpgrade(asset);
            Initialize();
        }
    }
}

