using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class WebGet : MonoBehaviour {
    
    [System.Serializable]
    public struct GeometryData
    {
        public string type;
        public List<List<List<double>>> coordinates;
    }
    [System.Serializable]
    public struct MazeMapData
    {
        public GeometryData geometry;
    }

    public string sourceUrl;

    // Use this for initialization
    void Start() {
        StartCoroutine(GetData());
    }

    IEnumerator GetData() {
        UnityWebRequest webData = UnityWebRequest.Get(sourceUrl);

        yield return webData.SendWebRequest();

        if (webData.isNetworkError || webData.isHttpError) {
            Debug.LogError(webData.error);
            Debug.LogError("Failed to get data");
        }else
        {
            var data = JsonConvert.DeserializeObject<MazeMapData>(webData.downloadHandler.text);
            var geomHeap = data.geometry.coordinates;
            
            geomHeap.ForEach(delegate(List<List<double>> v)
            {
                v.ForEach(delegate (List<double> v2)
                {
                    Debug.Log(string.Format("{0} - {1}", v2[0], v2[1]));
                });
            });
        }
    }
}
