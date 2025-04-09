using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace TowerDefence
{
    public class BuildSite : MonoBehaviour, IPointerDownHandler
    {
        public TowerAsset[] buildableTowers;
        public void SetBuildableTowers(TowerAsset[] towers) 
        {
            if (towers == null || towers.Length == 0)
            {
                Destroy(transform.parent.gameObject);
                buildableTowers = towers;
            }
           
        }
        public static event Action<BuildSite> OnClickEvent;

        public static void HideControls()
        {
            OnClickEvent(null);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnClickEvent(this);
        }
    }

}
