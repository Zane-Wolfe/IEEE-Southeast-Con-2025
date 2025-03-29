using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nighty : MonoBehaviour
{
    [SerializeField] private float day_night_cycle_speed = 1;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(day_night_cycle_speed * Time.deltaTime, 0, 0);
    }
}
