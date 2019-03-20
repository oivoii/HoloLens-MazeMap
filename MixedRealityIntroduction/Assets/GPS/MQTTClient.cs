using UnityEngine;
using System.Collections;
using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Utility;
using uPLibrary.Networking.M2Mqtt.Exceptions;

using System;

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

        // subscribe to the topic "/home/temperature" with QoS 2 
        client.Subscribe(new string[] { "EIT/HololensMazemap" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

    }
    void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)

        
    {
        String message = System.Text.Encoding.UTF8.GetString(e.Message);
        String[] coordinates = message.Split(',');
        float latitude = (float) Convert.ToDouble(coordinates[0]);
        float longitude = (float)Convert.ToDouble(coordinates[1]);




        gpsPosition.latitude = latitude;
        gpsPosition.longitude = longitude;
       



        Debug.Log("Received: " + System.Text.Encoding.UTF8.GetString(e.Message));
    }

    // Update is called once per frame
    void Update()
    {



    }
}