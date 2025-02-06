using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NullBuildSite : BuildSite
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        InvokeNullEvent();
    }
}
