using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class BuyControl : MonoBehaviour
    {
        [SerializeField] private TowerBuyControl m_TowerBuyPrefab;
           
        private List<TowerBuyControl> m_ActiveControl;
        private RectTransform m_RectTransform;

        private void Awake()
        {
            m_RectTransform = GetComponent<RectTransform>();
            BuildSite.OnClickEvent += MoveToBuildSite;
            gameObject.SetActive(false);
            
        }
        private void MoveToBuildSite(BuildSite buildSite)
        {
            if (buildSite != null && Camera.main != null && m_RectTransform != null)
            {
                var position = Camera.main.WorldToScreenPoint(buildSite.transform.root.position);
                m_RectTransform.anchoredPosition = position;
                gameObject.SetActive(true);
                m_ActiveControl = new List<TowerBuyControl>();

                foreach(var asset in buildSite.buildableTowers)
                {
                    
                    if (asset.IsAvailable())
                    {
                        
                        var newControl = Instantiate(m_TowerBuyPrefab, transform);
                        m_ActiveControl.Add(newControl);                        
                        newControl.SetTowerAsset(asset);
                    }
                    
                }
                var angle = 360 / m_ActiveControl.Count;
                for (int i = 0; i < m_ActiveControl.Count; i++)
                {
                    var offset = Quaternion.AngleAxis(angle * i, Vector3.forward) * (Vector3.up * 165);
                    m_ActiveControl[i].transform.position += offset;
                }
                
            }
            else
            {
                foreach (var control in m_ActiveControl) Destroy(control.gameObject);
                gameObject.SetActive(false);
            }

            foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
            {
                tbc.SetBuildSite(buildSite.transform.root);
            }
        }

        private void OnDestroy()
        {            
            BuildSite.OnClickEvent -= MoveToBuildSite;
        }
    }
}

