using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowScript : MonoBehaviour
{
    [SerializeField] private float repelDistance = 1;
    [SerializeField] private float attractDistance = 2;
    [SerializeField] private float speed = 1;

    void Update()
    {
        // if the nearest other object is too close, move away from it
        // if it is too far away, move closer to it
        Transform nearest = FindNearest();

        if (nearest != null) 
        {
            Vector3 v = nearest.position - transform.position;

            Vector3 move = Vector3.zero;
            if (v.magnitude <= repelDistance)
            {
                move = -speed * Time.deltaTime * v.normalized;
            }
            else if (v.magnitude >= attractDistance)
            {
                move = speed * Time.deltaTime * v.normalized;
            }
            
            transform.Translate(move, Space.World);
        }
    }

    private Transform FindNearest()
    {
        // NOTE: This is a very slow way of doing things!

        SlowScript[] allObjects = GameObject.FindObjectsOfType<SlowScript>();

        float minDistance = float.PositiveInfinity;
        Transform nearest = null;

        for (int i = 0; i < allObjects.Length; i++)
        {
            // ignore this object
            if (allObjects[i] == this)
            {
                continue;
            }

            Transform t = allObjects[i].transform;
            float distance = Vector3.Distance(t.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = t;
            }
        }

        return nearest;
    }
}
