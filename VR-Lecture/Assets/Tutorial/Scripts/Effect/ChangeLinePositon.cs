using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLinePositon : MonoBehaviour
{
    public int index;

    private LineRenderer target;

    private void Awake()
    {
        target = GetComponent<LineRenderer>();
    }

    public void Call(Vector3 worldPosition)
    {
        if (target.useWorldSpace)
        {
            target.SetPosition(index, worldPosition);
        }
        else
        {
            var localPosition = transform.InverseTransformPoint(worldPosition);
            target.SetPosition(index, localPosition);
        }
    }
}
