using UnityEngine;
using SpaceShooter;


namespace TowerDefence
{
    public class Tower : MonoBehaviour
    {
        //[SerializeField] private float _defaultRadius = 3.0f;
        [SerializeField] private float m_Radius = 5.0f;
        [SerializeField] private UpgradeAsset radiusUpgrade;

        private Turret[] turrets;
        private Destructible target = null;

        public void Use(TowerAsset asset)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = asset.sprite;
            turrets = GetComponentsInChildren<Turret>();
            foreach (var turret in turrets)
            {
                turret.AssignLoadout(asset.turretProperties);
            }
            GetComponentInChildren<BuildSite>().SetBuildableTowers(asset.m_UpgradesTo);

        }

        private void Start()
        {
            
            ApplyRadiusUpgrade();

            //Debug.Log("Tower Radius: " + m_Radius);
        }

        //Trying
        private void ApplyRadiusUpgrade()
        {
            int upgradeLevel = Upgrades.GetUpgradeLevel(radiusUpgrade);
            m_Radius = 5.0f + upgradeLevel * 1.0f; 
            
        }

        private void Update()
        {
            if (target)
            {
                //Vector2 targetVector = target.transform.position - transform.position;

                if (Vector2.Distance(target.transform.position, transform.position) <= m_Radius)
                {
                    foreach (var turret in turrets)
                    {
                        turret.transform.up = target.transform.position - turret.transform.position;

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

