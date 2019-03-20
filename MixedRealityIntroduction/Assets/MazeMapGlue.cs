using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class MazeMapGlue : MonoBehaviour {
    public InputField sourceUrlField;
    public GameObject cursor;

    private const int mapSRID = 4326;
    private const string mapSearchUrlTemplate = 
        "https://api.mazemap.com/search/equery/?q={0}&rows=1&start=0&withpois=true";
    private const string mapDataUrlTemplate =
        "https://api.mazemap.com/api/pois/closestpoi/?lat={0}&lng={1}&z={2}&srid={3}";
    private string mapDataUrl;
    
    [System.Serializable]
    public struct PointData
    {
        public string type;
        public List<double> coordinates;

        public double longitude
        {
            get
            {
                return coordinates[0];
            }
        }
        public double latitude
        {
            get
            {
                return coordinates[1];
            }
        }
    }

    [System.Serializable]
    public struct MazeMapResult
    {
        public PointData geometry;
        public double zValue;
    }

    [System.Serializable]
    public struct MazeMapSearch
    {
        public List<MazeMapResult> result;
    }

    IEnumerator GetMapDataUrl() {
        string mapSearchUrl = string.Format(mapSearchUrlTemplate, sourceUrlField.text);

        UnityWebRequest webData = UnityWebRequest.Get(mapSearchUrl);

        yield return webData.SendWebRequest();

        if(webData.isNetworkError || webData.isHttpError) {
            Debug.LogError(webData.error);
            Debug.LogError("Failed to search MazeMap");
        } else
        {
            Debug.Log(webData.downloadHandler.text);
            var data = JsonConvert.DeserializeObject<MazeMapSearch>(webData.downloadHandler.text);
            if(data.result.Count == 0) {
                Debug.LogError("No results for search");
            }else
            {
                MazeMapResult result = data.result[0];

                if (result.geometry.type != "Point")
                    throw new System.ApplicationException(
                        string.Format("Unexpected geometry type: {0}", result.geometry.type));

                mapDataUrl = string.Format(
                    mapDataUrlTemplate,
                    result.geometry.latitude,
                    result.geometry.longitude,
                    Mathf.FloorToInt((float)result.zValue),
                    mapSRID).Replace(",", ".");
            }
        }
    }

    IEnumerator PerformSearchInternal() {
        sourceUrlField.text = "R1";

        /* Perform search in MazeMap */
        yield return GetMapDataUrl();

        /* Put MazeMap POI url into MazeMapGet to get geometry of room */
        MazeMapGet mapGet = GetComponent<MazeMapGet>();
        mapGet.sourceUrl = mapDataUrl;

        /* We can then await MazeMapGet for the geometry data */
        yield return mapGet.GetData();

        Debug.Log("derp");

        yield break;

        /* Once data has arrived, create the mesh to place out */
        buildMesh2 meshBuilder = GetComponent<buildMesh2>();
        StartCoroutine(meshBuilder.CreateMesh(null));
    }

    public void PerformSearch() {
        StartCoroutine(PerformSearchInternal());
    }

    private void Start() {
        PerformSearch();
    }

    private void Update() {
        GPSPosition holoPosition = GetComponent<GPSPosition>();
        buildMesh2 meshData = GetComponent<buildMesh2>();
        Transform currentPos = gameObject.transform;

        /* TODO: Displace currentPos relative to holoPosition, based on mesh */
        const double equatorDegrees = 110.25;

        double xDifference = meshData.latitude - holoPosition.latitude;
        double yDifference = 
            (meshData.longitude - holoPosition.longitude) * Math.Cos(holoPosition.latitude);

        float distance = 
            (float)(equatorDegrees * Math.Sqrt(Math.Pow(xDifference, 2) + Math.Pow(yDifference, 2)));

        float xDiff = (float)xDifference;
        float yDiff = (float)yDifference;

        Vector3 direction = new Vector3((float)xDifference, (float)yDifference).normalized;
        
        /* The end result */
        currentPos.position = new Vector3(direction.x * distance, direction.y * distance);
    }
}
