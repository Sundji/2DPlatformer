using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestructor : MonoBehaviour
{

    public float Lifespan;

    private float timer;

    private void Update()
    {

        timer = timer + Time.deltaTime;

        if (timer >= Lifespan)
            Destroy(gameObject);

    }

}
