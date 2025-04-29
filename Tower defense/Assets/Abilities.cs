using UnityEngine;
using System;
using SpaceShooter;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TowerDefence
{
    public class Abilities : MonoSingleton<Abilities>
    {
        

        [Serializable]
        public class FireAbility 
        {
            [SerializeField] private int m_Cost = 5;
            [SerializeField] private int m_Damage = 2;
            [SerializeField] private Color m_TargetingColor;
            [SerializeField] private Text _costText; 
            //Trying 25.6
            [SerializeField] private UpgradeAsset _fireAbility;
            [SerializeField] private Button _fireAbilityButton;

            private bool _isActive;
            public int Cost => m_Cost;

            //Trying 25.6
            public void UpdateButtonState(int currentCrystals)
            {
                if (_fireAbilityButton == null) return;

                bool canBuy = currentCrystals >= m_Cost;

                _fireAbilityButton.interactable = _isActive && canBuy;
                if (_costText != null)
                {
                    if (canBuy)
                    {
                        _costText.color = Color.white; 
                    }
                    else
                    {
                        _costText.color = Color.red; 
                    }
                }
                var buttonColors = _fireAbilityButton.colors;
                buttonColors.disabledColor = new Color(1f, 1f, 1f, 0.5f);
                _fireAbilityButton.colors = buttonColors;
            }

            public void UpdateCostText(int value)
            {
                if (_costText != null)
                {
                    _costText.text = value.ToString();
                }
                else
                {
                    Debug.LogError("FireAbility: _costText is not in inspector!");
                }
            }            

            public void Use()
            {
                print($"{_isActive}");
                print($"CHECK {TDPlayer.Instance.m_crystal}");
                if (_isActive == false || TDPlayer.Instance.m_crystal < m_Cost)
                {
                    
                    return;
                }

                print("GOOOO to USE");
                TDPlayer.Instance.ChangeCrystals(-m_Cost);

                ClickProtection.Instance.Activate((Vector2 v) =>
                {
                    Vector3 position = v;
                    position.z = -Camera.main.transform.position.z;
                    position = Camera.main.ScreenToWorldPoint(position);
                    foreach (var collider in Physics2D.OverlapCircleAll(position, 5))
                    {
                        if (collider.transform.parent.TryGetComponent<Enemy>(out var enemy))
                        {
                            enemy.TakeDamage(m_Damage, TDProjectile.DamageType.Magic);
                        }
                    }

                });
            }    

            public void ApplyFireAbilityUpgrade()
            {

                if (_fireAbility == null)
                {
                    //Debug.LogError("FireAbility: _fireAbility is not set in inspector!");
                    return;
                }

                int upgradeFireAbilityLevel = Upgrades.GetUpgradeLevel(_fireAbility);
                //Debug.Log($"FireAbility upgrade level: {upgradeFireAbilityLevel}");

                if (upgradeFireAbilityLevel > 0)
                {
                    SetActivate();
                    //Debug.Log("FireAbility activated");
                }
                else
                {
                    SetDeactivate();
                    //Debug.Log("FireAbility deactivated (no upgrade)");
                }

            }

            public void SetActivate()
            {
                _isActive = true;
                print($"And now it is:............. {_isActive}");
                UpdateButtonState(TDPlayer.Instance.m_crystal);
            }

            private void SetDeactivate()
            {
                _isActive = false;
                UpdateButtonState(TDPlayer.Instance.m_crystal);
            }

        }

        //-------------------------------------------------------------------------------------------------------------

        [Serializable]
        public class TimeAbility 
        {
            [SerializeField] private int m_Cost = 10;
            [SerializeField] private float m_Cooldown = 15f;
            [SerializeField] private float m_Duration = 5;
            [SerializeField] private Text _costText;
            //Trying 25.6
            [SerializeField] private UpgradeAsset _timeAbility;
            [SerializeField] private Button _timeAbilityButton;

            private bool _isActive;
            public int Cost => m_Cost;

            //Trying 25.6
            public void UpdateButtonState()
            {
                if (_timeAbilityButton == null) return;

                _timeAbilityButton.interactable = _isActive;
                var buttonColors = _timeAbilityButton.colors;
                buttonColors.disabledColor = new Color(1f, 1f, 1f, 0.5f);
                _timeAbilityButton.colors = buttonColors;
            }

            public void UpdateCostText(int value)
            {
                if (_costText != null)
                {
                    _costText.text = value.ToString();
                }
                else
                {
                    Debug.LogError("FireAbility: _costText is not in inspector!");
                }
            }


            public void Use() 
            {
                print($"{_isActive}");

                if (_isActive == false)
                {
                    return;
                }

                void Slow(Enemy ship)
                {
                    
                    ship.GetComponent<SpaceShip>().HalfMaxLinearVelocity();
                }
                
                foreach (var ship in FindObjectsOfType<SpaceShip>())
                    ship.HalfMaxLinearVelocity();

                EnemyWaveManager.OnEnemySpawn += Slow;

                IEnumerator Restore()
                {
                    
                    yield return new WaitForSeconds(m_Duration);
                    print("All restored");
                    foreach (var ship in FindObjectsOfType<SpaceShip>())
                        ship.RestoreMaxLinearVelocity();
                    EnemyWaveManager.OnEnemySpawn -= Slow;
                }

                Instance.StartCoroutine(Restore());

                IEnumerator TimeAbilityButton()
                {
                    Instance.TimeButton.interactable = false;
                    yield return new WaitForSeconds(m_Cooldown);
                    Instance.TimeButton.interactable = true;
                }
                Instance.StartCoroutine(TimeAbilityButton());

            }

            public void ApplyTimeAbilityUpgrade()
            {

                if (_timeAbility == null)
                {
                    //Debug.LogError("TimeAbility: _timeAbility is not set in inspector!");
                    return;
                }

                int upgradeTimeAbilityLevel = Upgrades.GetUpgradeLevel(_timeAbility);
                //Debug.Log($"TimeAbility upgrade level: {upgradeTimeAbilityLevel}");

                if (upgradeTimeAbilityLevel > 0)
                {
                    SetActivate();
                    //Debug.Log("TimeAbility activated");
                }
                else
                {
                    SetDeactivate();
                    //Debug.Log("TimeAbility deactivated (no upgrade)");
                }

            }

            public void SetActivate()
            {
                _isActive = true;
                print($"And now it is:............. {_isActive}");
                UpdateButtonState();
            }

            private void SetDeactivate()
            {
                _isActive = false;
                UpdateButtonState();
            }

        }
        private void OnEnable()
        {                       
         TDPlayer.CrystalUpdateSubscribe(OnCrystalsChanged);
            Debug.LogWarning("Using OnEnable");
        }                

        private void OnDisable()
        {
            TDPlayer.CrystalUpdateUnsubscribe(OnCrystalsChanged);
        }

        private void OnCrystalsChanged(int newCrystals)
        {
            m_FireAbility.UpdateButtonState(newCrystals);
            //m_TimeAbility.UpdateButtonState(newCrystals);
            Debug.LogWarning($"Crystals changed: {newCrystals}");
        }
        private void Start()
        {
            
            m_FireAbility.UpdateCostText(m_FireAbility.Cost);
            m_TimeAbility.UpdateCostText(m_TimeAbility.Cost);

            m_FireAbility.ApplyFireAbilityUpgrade();
            m_FireAbility.UpdateButtonState(TDPlayer.Instance.m_crystal);

            m_TimeAbility.ApplyTimeAbilityUpgrade();
            m_TimeAbility.UpdateButtonState();

           OnCrystalsChanged(TDPlayer.Instance.m_crystal);
        }

        [SerializeField] private Image m_TargetingCircle;
        [SerializeField] private Button TimeButton;

        [SerializeField] private FireAbility m_FireAbility;
        public void UseFireAbility() => m_FireAbility.Use();
        [SerializeField] private TimeAbility m_TimeAbility;
        public void UseTimeAbility() => m_TimeAbility.Use(); 
        
        
    }
}

