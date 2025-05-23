using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;


namespace TowerDefence
{
    public class Path : MonoBehaviour
    {
        [SerializeField] private CircleArea startArea;
        [SerializeField] private AIPointPatrol[] points;

        public CircleArea StartArea { get { return startArea; } }
        public int Length { get => points.Length; }

        public AIPointPatrol this[int i] { get => points[i]; }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            foreach (var point in points)
            {
                Gizmos.DrawSphere(point.transform.position, point.Radius);
                
            }


        }

    }

    


}

