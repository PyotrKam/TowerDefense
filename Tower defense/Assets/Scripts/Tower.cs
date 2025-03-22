using UnityEngine;
using SpaceShooter;


namespace TowerDefence
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private float m_Radius = 5.0f;
        [SerializeField] private UpgradeAsset radiusUpgrade;

        private Turret[] turrets;
        private Destructible target = null;

        private void Start()
        {
            turrets = GetComponentsInChildren<Turret>();
            ApplyRadiusUpgrade();

            Debug.Log("Tower Radius: " + m_Radius);
        }

        //Trying
        private void ApplyRadiusUpgrade()
        {
            int upgradeLevel = Upgrades.GetUpgradeLevel(radiusUpgrade);
            m_Radius = 5.0f + upgradeLevel * 1.0f; 
            Debug.Log("New Radius: " + m_Radius);
        }

        private void Update()
        {
            if (target)
            {
                Vector2 targetVector = target.transform.position - transform.position;

                if (targetVector.magnitude <= m_Radius)
                {
                    foreach (var turret in turrets)
                    {
                        turret.transform.up = targetVector;

                        turret.Fire();
                    }
                }
                else
                {
                    target = null;
                }
            }

            else
            {
                var enter = Physics2D.OverlapCircle(transform.position, m_Radius);

                if (enter)
                {
                    target = enter.transform.root.GetComponent<Destructible>();
                }
            }            
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;

            Gizmos.DrawWireSphere(transform.position, m_Radius);


        }
    }
}

