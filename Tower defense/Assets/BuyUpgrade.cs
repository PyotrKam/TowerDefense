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
            level.text = $"{savedLevel + 1}";
            cost.text = asset.costByLevel[savedLevel].ToString();
        }

        public void Buy()
        {
            Upgrades.BuyUpgrade(asset);
        }
    }
}

