using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json;

public class MazeMapGet : MonoBehaviour {
    
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
    public GeometryData geometryData;

    public IEnumerator GetData() {
        UnityWebRequest webData = UnityWebRequest.Get(sourceUrl);

        yield return webData.SendWebRequest();

        if (webData.isNetworkError || webData.isHttpError) {
            Debug.LogError(webData.error);
            Debug.LogError("Failed to get data");
        }else
        {
            var data = JsonConvert.DeserializeObject<MazeMapData>(webData.downloadHandler.text);

            geometryData = data.geometry;
        }
    }
}
