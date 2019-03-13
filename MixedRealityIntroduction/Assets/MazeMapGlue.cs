using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class MazeMapGlue : MonoBehaviour {
    public InputField sourceUrlField;

    private const int mapSRID = 4326;
    private const string mapSearchUrlTemplate = 
        "https://api.mazemap.com/search/equery/?q={0}&rows=1&start=0&withpois=true";
    private const string mapDataUrlTemplate =
        "https://api.mazemap.com/api/pois/closestpoi/?lat={1}&lng={0}&z={2}&srid={3}";
    private string mapDataUrl;
    
    [System.Serializable]
    public struct PointData
    {
        public string type;
        public List<double> coordinates;
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
            var data = JsonConvert.DeserializeObject<MazeMapSearch>(webData.downloadHandler.text);
            if(data.result.Count == 0) {
                Debug.LogError("No results for search");
            }else
            {
                MazeMapResult result = data.result[0];
                mapDataUrl = string.Format(
                    mapDataUrlTemplate,
                    result.geometry.coordinates[0],
                    result.geometry.coordinates[1],
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

        /* Once data has arrived, create the mesh to place out */
        buildMesh2 meshBuilder = GetComponent<buildMesh2>();
        StartCoroutine(meshBuilder.CreateMesh(null));

        /* TODO: Get GPS coordinates of HoloLens */
        /* TODO: Maintain relative positioning to room */
    }

    public void PerformSearch() {
        StartCoroutine(PerformSearchInternal());
    }

    private void Start() {
        PerformSearch();
    }
}
