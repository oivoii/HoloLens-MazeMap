using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Returns a coordinate that should correspond to the computer lab we are working in
    public static LatLong getGPSPosition() => new LatLong(63.4106993, 10.4095353);


}


public class LatLong
{
    private double latitude;
    private double longitude;


    public LatLong(double latitude, double longitude)
    {
        this.latitude = latitude;
        this.longitude = longitude;

    }
}

