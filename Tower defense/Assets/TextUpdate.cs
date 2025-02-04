using UnityEngine;
using UnityEngine.UI;


namespace TowerDefence
{
    public class TextUpdate : MonoBehaviour
    {
        public enum UpdateSource { Gold, Life }
        public UpdateSource source = UpdateSource.Gold;
        private Text m_text;

        void Awake()
        {
            m_text = GetComponent<Text>();

            switch (source)
            {
                case UpdateSource.Gold: 
                    TDPlayer.OnGoldUpdate += UpdateText;
                    break;

                case UpdateSource.Life:
                    TDPlayer.OnLifeUpdate += UpdateText;
                    break;                
            }
            
        }

        private void UpdateText(int money)
        {
            m_text.text = money.ToString();
        }
    }
}


