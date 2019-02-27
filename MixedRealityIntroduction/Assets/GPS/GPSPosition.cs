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


    public static LatLong getGPSPosition() => new LatLong(63.4305658, 10.3951929);


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

