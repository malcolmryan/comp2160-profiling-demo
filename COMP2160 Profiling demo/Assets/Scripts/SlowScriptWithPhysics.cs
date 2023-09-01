using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SlowScriptWithPhysics : MonoBehaviour
{
    [SerializeField] private float repelDistance = 2;
    [SerializeField] private float attractDistance = 4;
    [SerializeField] private float speed = 1;

    private Vector3 move;
    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rigidbody.velocity = move;
    }

    void Update()
    {
        // if the nearest other object is too close, move away from it
        // if it is too far away, move closer to it
        Transform nearest = FindNearest();

        if (nearest != null) 
        {
            Vector3 v = nearest.position - transform.position;

            move = Vector3.zero;
            if (v.magnitude <= repelDistance)
            {
                move = -speed * v.normalized;
            }
            else if (v.magnitude >= attractDistance)
            {
                move = speed * v.normalized;
            }            
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
