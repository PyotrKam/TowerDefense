using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;
using UnityEditor;
using UnityEngine.UIElements;
using System;

namespace TowerDefence
{
    [RequireComponent(typeof(TDPatrolController))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int m_damage = 1;
        [SerializeField] private int m_gold = 1;

        public event Action OnEnd;
        private void OnDestroy()
        {
            OnEnd?.Invoke();
        }

        public void Use(EnemyAsset asset)
        {
            var sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();
            sr.color = asset.color;
            sr.transform.localScale = new Vector3(asset.spriteScale.x, asset.spriteScale.y, 1);

            sr.GetComponent<Animator>().runtimeAnimatorController = asset.animations;

            GetComponent<SpaceShip>().Use(asset);

            GetComponentInChildren<CircleCollider2D>().radius = asset.radius;

            m_damage = asset.damage;
            m_gold = asset.gold;
        }

        public void DamagePlayer()
        {
            TDPlayer.Instance.ReduceLife(m_damage);
        }

        public void GivePlayerGold()
        {
            TDPlayer.Instance.ChangeGold(m_gold);
        }
    }

    [CustomEditor(typeof(Enemy))]
    public class EnemyInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            EnemyAsset a = EditorGUILayout.ObjectField(null, typeof(EnemyAsset), false) as EnemyAsset;
            if (a)
            {
                (target as Enemy).Use(a);
            }
        }
    }
}

