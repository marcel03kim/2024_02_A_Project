using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleManager : MonoBehaviour
{
    public Vehicle[] vehicles;

    public Car car;
    public Bicycle bicycle;

    float Timer;
    

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < vehicles.Length; i++)
        {
            vehicles[i].Move();
        } 
        

        Timer -= Time.deltaTime;

        if(Timer < 0)
        {
            for(int i = 0;i < vehicles.Length;i++)
            {
                vehicles[i].Horn();
            }
            Timer = 1;
        }
    }
}
