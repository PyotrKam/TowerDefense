using SpaceShooter;
using UnityEngine;
using System;

namespace TowerDefence
{
    public class TDPlayer : Player
    {
        public static new TDPlayer Instance
        { get
            {
                return Player.Instance as TDPlayer;
            }        
        }

        private static event Action<int> OnGoldUpdate;
        private static event Action<int> OnCrystalUpdate;
        public static event Action<int> OnLifeUpdate;

        [SerializeField] private int m_gold = 0;
        [SerializeField] public int m_crystal;
        public int m_defaultCrystals = 55;
        private const string CrystalKey = "PlayerCrystals";

        public static void GoldUpdateSubscribe(Action<int> act)
        {
            OnGoldUpdate += act;
            act(Instance.m_gold);
        }
        //Trying 25.6
        public static void CrystalUpdateSubscribe(Action<int> act)
        {
            if (OnCrystalUpdate == null)
                OnCrystalUpdate = delegate { };

            OnCrystalUpdate += act;

            
            if (Instance != null)
            {
                act(Instance.m_crystal);
            }
        }

        public static void LiveUpdateSubscribe(Action<int> act)
        {
            OnLifeUpdate += act;
            act(Instance.NumLives);
        }

        protected override void Awake()
        {
            base.Awake();

            bool isFirstLaunch = !PlayerPrefs.HasKey(CrystalKey);

            if (isFirstLaunch)
            {
                m_crystal = m_defaultCrystals;
                PlayerPrefs.SetInt(CrystalKey, m_crystal);
                PlayerPrefs.Save();
                Debug.Log($"First launch! Set crystals to {m_crystal}");
            }
            else
            {
                m_crystal = PlayerPrefs.GetInt(CrystalKey);

                Debug.Log($"Loaded crystals: {m_crystal}");
                
                if (m_crystal == 0 && m_defaultCrystals > 0)
                {
                    m_crystal = m_defaultCrystals;
                    PlayerPrefs.SetInt(CrystalKey, m_crystal);
                    PlayerPrefs.Save();
                    Debug.Log($"Auto-fixed zero crystals to {m_crystal}");
                }
            }

        }






        public void ChangeGold(int change)
        {
            m_gold += change;
            OnGoldUpdate?.Invoke(m_gold);
        }
        public void ReduceLife(int change)
        {
            TakeDamage(change);
            OnLifeUpdate(NumLives);
        }
        public void ChangeCrystals(int change)
        {
            if (change == 0) return;

            m_crystal = Mathf.Max(0, m_crystal + change);
            PlayerPrefs.SetInt(CrystalKey, m_crystal);
            PlayerPrefs.Save();
            OnCrystalUpdate?.Invoke(m_crystal);
        }


        [SerializeField] private Tower m_towerPrefab;

        public void TryBuild(TowerAsset towerAsset, Transform buildSite)
        {
            ChangeGold(-towerAsset.goldCost);
            var tower = Instantiate(m_towerPrefab, buildSite.position, Quaternion.identity);
            tower.Use(towerAsset);            
            Destroy(buildSite.gameObject);
        }

        [SerializeField] private UpgradeAsset healtUpgrade;

        private void Start()
        {            
            var level = Upgrades.GetUpgradeLevel(healtUpgrade);
            TakeDamage(-level * 5);
            m_crystal = PlayerPrefs.GetInt(CrystalKey, m_crystal);
            Debug.Log($"In TDPlayer script {m_crystal}");


            Debug.Log($"PlayerPrefs has key {CrystalKey}: {PlayerPrefs.HasKey(CrystalKey)}");
            m_crystal = PlayerPrefs.GetInt(CrystalKey, 0);
            Debug.Log($"Crystals after load: {m_crystal}");
        }
       
        public static void GoldUpdateUnsubscribe(Action<int> act)
        {
            OnGoldUpdate -= act;
        }
        public static void LiveUpdateUnSubscribe(Action<int> act)
        {
            OnLifeUpdate -= act;
        }
        public static void CrystalUpdateUnsubscribe(Action<int> act)
        {
            OnCrystalUpdate -= act;
        }

    }
}

