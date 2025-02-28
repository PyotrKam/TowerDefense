using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class BuyUpgrade : MonoBehaviour
    {
        [SerializeField] private Image upgradeIcon;
        [SerializeField] private Text level, cost;
        [SerializeField] private Button buyBotton;

        public void SetUpgrade(UpgradeAsset asset, int level = 1)
        {
            upgradeIcon.sprite = asset.sprite;
            this.level.text = level.ToString();
            cost.text = asset.costByLevel[level].ToString();
        }
    }
}

