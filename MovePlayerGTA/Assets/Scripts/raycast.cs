using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast
{
    RaycastHit hit;
    Ray ray;

    public bool Ray(Vector3 target, Vector3 direction, float maxDistancia, LayerMask layer)
    {
        ray = new Ray(target, direction);
        RayDraw(target, direction);
        return Physics.Raycast(ray, out hit, maxDistancia, layer);
    }

    public void RayDraw(Vector3 target, Vector3 direction)
    {
        ray = new Ray(target, direction);
        Debug.DrawRay(target, direction , Color.black);
    }
}
