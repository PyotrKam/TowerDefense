using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    
    [RequireComponent(typeof(MapLevel))]
    public class BranchLevel : MonoBehaviour
    {
        [SerializeField] private MapLevel m_RootLevel;
        [SerializeField] private Text m_PointText;
        [SerializeField] private int m_NeedPoints = 3;
               
        public void TryActivate()
        {
            gameObject.SetActive(m_RootLevel.isComplete);
            if (m_NeedPoints > MapCompletion.Instance.TotalScore)
            {
                m_PointText.text = m_NeedPoints.ToString();                
            }
            else
            {
                m_PointText.transform.parent.gameObject.SetActive(false);
                GetComponent<MapLevel>().Initialise();
            }
            
        }
    }
}

