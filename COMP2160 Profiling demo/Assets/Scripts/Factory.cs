using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private Transform prefab;
    [SerializeField] private float period;

    private float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= period) 
        {
            timer -= period;
            Transform t = Instantiate(prefab, transform);
            t.localPosition = Random.insideUnitSphere;
        }
    }
}
