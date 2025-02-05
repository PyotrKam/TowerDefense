using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace TowerDefence
{
    public class TowerBuyControl : MonoBehaviour
    {
        [System.Serializable]

        public class TowerAsset
        {
            public int goldCost = 15;
            public Sprite towerGUI;
        }

        [SerializeField] private TowerAsset m_ta;
        [SerializeField] private Text m_text;
        [SerializeField] private Button m_button;

        private void Awake()
        {
            TDPlayer.OnGoldUpdate += GoldStatusChek;
        }
         
        private void Start()
        {
            m_text.text = m_ta.goldCost.ToString();
            m_button.GetComponent<Image>().sprite = m_ta.towerGUI;
        }

        private void GoldStatusChek(int gold)
        {
            if (gold >= m_ta.goldCost != m_button.interactable)
            {
                m_button.interactable = !m_button.interactable;
                m_text.color = m_button.interactable ? Color.white : Color.red;
            }
        }

    }



}

