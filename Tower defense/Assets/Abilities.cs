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
            public void Use()
            {
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
        }



        [Serializable]
        public class TimeAbility 
        {
            [SerializeField] private int m_Cost = 10;
            [SerializeField] private float m_Cooldown = 15f;
            [SerializeField] private float m_Duration = 5;
            public void Use() 
            {
               
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

        }

        [SerializeField] private Image m_TargetingCircle;
        [SerializeField] private Button TimeButton;

        [SerializeField] private FireAbility m_FireAbility;
        public void UseFireAbility() => m_FireAbility.Use();
        [SerializeField] private TimeAbility m_TimeAbility;
        public void UseTimeAbility() => m_TimeAbility.Use();        
    }
}

