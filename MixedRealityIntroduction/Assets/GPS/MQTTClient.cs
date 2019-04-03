using UnityEngine;
using System.Collections;
using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Utility;
using uPLibrary.Networking.M2Mqtt.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class MQTTClient : MonoBehaviour
{
    private MqttClient client;
    private GPSPosition gpsPosition;
    // Use this for initialization
    void Start()
    {
        gpsPosition = GetComponent<GPSPosition>();

        // create client instance 
        client = new MqttClient("broker.hivemq.com");


        // register to message received 
        client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

        string clientId = Guid.NewGuid().ToString();
        client.Connect(clientId);
        
        client.Subscribe(new string[] { "EIT/HololensMazemap" },
            new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
    }

    [System.Serializable]
    public struct GPSData
    {
        public double latitude;
        public double longitude;
    }

    void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e) {
        String message = System.Text.Encoding.UTF8.GetString(e.Message);

        GPSData coords = JsonConvert.DeserializeObject<GPSData>(message);

        gpsPosition.latitude = coords.latitude;
        gpsPosition.longitude = coords.longitude;

        Debug.Log("Received: " + System.Text.Encoding.UTF8.GetString(e.Message));
    }
}
