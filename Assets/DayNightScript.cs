using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering; // used to access the volume component

public class DayNightScript : MonoBehaviour
{
    public Volume ppv; // this is the post processing volume

    public float tick; // Increasing the tick, increases second rate
    public float seconds;
    public int mins;
    public int hours;
    public int days;
    public bool activateLights; // checks if lights are on
    public GameObject[] lights; // all the lights we want on when its dark
    void Start()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].SetActive(false); // turn them all off
        }
        activateLights = false;
        ppv = gameObject.GetComponent<Volume>();

    }

    // Update is called once per frame
    void FixedUpdate() // we used fixed update, since update is frame dependant. 
    {
        CalcTime();


    }

    public void CalcTime() // Used to calculate sec, min and hours
    {
        seconds += Time.fixedDeltaTime * tick; // multiply time between fixed update by tick

        if (seconds >= 60) // 60 sec = 1 min
        {
            seconds = 0;
            mins += 1;
        }

        if (mins >= 60) //60 min = 1 hr
        {
            mins = 0;
            hours += 1;
        }

        if (hours >= 24) //24 hr = 1 day
        {
            hours = 0;
            days += 1;
        }
        ControlPPV(); // changes post processing volume after calculation
    }

    public void ControlPPV() // used to adjust the post processing slider.
    {
        if (hours >= 20 && hours < 21) // 20 pm dusk
        {
            ppv.weight =1- (float)mins / 60; // since dusk is 1 hr, we just divide the mins by 60 which will slowly increase from 0 - 1 

            if (activateLights == true) // if lights havent been turned on
            {
                if (mins > 25) // wait until pretty dark
                {
                    for (int i = 0; i < lights.Length; i++)
                    {
                        lights[i].SetActive(false); // turn them all on
                    }
                    activateLights = false;
                }
            }
        }


        if (hours >= 5 && hours < 6) // 5:00-6:00 am
        {
            ppv.weight =  (float)mins / 60; // go from 1 - 0

            if (activateLights == false) // if lights are on
            {
                if (mins > 45) 
                {
                    for (int i = 0; i < lights.Length; i++)
                    {
                        lights[i].SetActive(true); // shut them off
                    }
                    activateLights = true;
                }
            }
        }
    }
}
