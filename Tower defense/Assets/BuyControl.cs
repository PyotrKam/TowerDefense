using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class BuyControl : MonoBehaviour
    {
        private RectTransform t;

        private void Awake()
        {
            t = GetComponent<RectTransform>();
            BuildSite.OnClickEvent += MoveToBuildSite;
            gameObject.SetActive(false);

        }
        private void MoveToBuildSite(Transform buildSite)
        {
            if (buildSite != null && Camera.main != null && t != null)
            {
                var position = Camera.main.WorldToScreenPoint(buildSite.position);
                t.anchoredPosition = position;
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }

            foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
            {
                tbc.SetBuildSite(buildSite);
            }
        }

        private void OnDestroy()
        {            
            BuildSite.OnClickEvent -= MoveToBuildSite;
        }
    }
}

