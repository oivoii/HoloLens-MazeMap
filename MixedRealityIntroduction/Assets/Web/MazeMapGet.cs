using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json;

public class MazeMapGet : MonoBehaviour {
    
    [System.Serializable]
    public struct GeometryPolygon
    {
        public string type;
        public List<List<List<double>>> coordinates;
    }

    [System.Serializable]
    public struct GeometryPoint
    {
        public string type;
        public List<double> coordinates;
    }

    [System.Serializable]
    public struct GeometryGeneric
    {
        public string type;
    }

    [System.Serializable]
    public struct MazeMapData<T>
    {
        public T geometry;
    }

    public string sourceUrl;
    public GeometryPolygon geometryData;
    public GeometryPoint pointData;

    public IEnumerator GetData() {
        UnityWebRequest webData = UnityWebRequest.Get(sourceUrl);

        yield return webData.SendWebRequest();

        if (webData.isNetworkError || webData.isHttpError) {
            Debug.LogError(webData.error);
            Debug.LogError("Failed to get data");
        }else
        {
            var requestData = webData.downloadHandler.text;
            var typeInfo = JsonConvert.DeserializeObject<MazeMapData<GeometryGeneric>>(requestData);

            if(typeInfo.geometry.type == "Polygon") {
                var data = JsonConvert.DeserializeObject<MazeMapData<GeometryPolygon>>(requestData);
                geometryData = data.geometry;
            }
            else if(typeInfo.geometry.type == "Point") {
                var data = JsonConvert.DeserializeObject<MazeMapData<GeometryPoint>>(requestData);
                pointData = data.geometry;
            }
        }
    }
}
