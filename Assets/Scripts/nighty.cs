using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 class nighty : MonoBehaviour
{
    [SerializeField] private float day_night_cycle_speed = 1;

    public GameData gameData;
    private float elapsed_hours = 12;

    public int getCurrentHourOfDay()
    {
        return (int)elapsed_hours;
    }

    // Update is called once per frame
    void Update()
    {
        float rot = day_night_cycle_speed * Time.deltaTime;
        elapsed_hours += 24f / 360f * rot;
        if (elapsed_hours >= 24) elapsed_hours -= 24f;
        transform.Rotate(rot, 0, 0);

        float minutes = (elapsed_hours - Mathf.Floor(elapsed_hours)) * 60f;
        int hour = (int)(elapsed_hours);
        int min = (int)(minutes);

        gameData.minute = min;
        gameData.hour = hour;
    }
}
