using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FindGPSDistance : MonoBehaviour {

    public struct dd {
        public Vector2 distance;
    }

    // Use this for initialization
    public static dd GPSDistance(double lon1, double lat1, double lon2, double lat2) {

        dd result;
        result.distance = new Vector2();

        double degToRad = (Math.PI) / 180;

        double phi1 = lat1 * degToRad;
        double phi2 = lat2 * degToRad;
        double lambda = (lon2 - lon1) * degToRad; 
        double R = 6371e3;

        double d = Math.Acos(Math.Sin(phi1) * Math.Sin(phi2) + Math.Cos(phi1) * Math.Cos(phi2) * Math.Cos(lambda)) * R;

        double y = Math.Sin(lambda) * Math.Cos(phi2);
        double x = Math.Cos(phi1) * Math.Sin(phi2) - Math.Sin(phi1) * Math.Cos(phi2) * Math.Cos(lambda);

        double factor = Math.Sqrt((y * y) + (x * x));

        result.distance[0] = (float)(d * x / factor);
        result.distance[1] = (float)(d * y / factor);

        return result;
    }

}
