using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class MazeMapGlue : MonoBehaviour {
    public InputField sourceUrlField;
    public InputField playerFloor;

    private const int mapSRID = 4326;
    private const string mapSearchUrlTemplate = 
        "https://api.mazemap.com/search/equery/?q={0}&rows=10&start=0&withpois=true";
    private const string mapDataUrlTemplate =
        "https://api.mazemap.com/api/pois/closestpoi/?lat={0}&lng={1}&z={2}&srid={3}";
    private string mapDataUrl;

    private SearchResult searchState = SearchResult.Awaiting;
    private MazeMapResult currentRoomInfo;

    private int[] preferredCampuses =
    {
        1,
        20,
    };
    
    private enum SearchResult
    {
        Awaiting,
        Complete,
        Failed,
    };

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
        public string title;
        public int campusId;

        public double zValue;
        public double z;

        public int floor
        {
            get
            {
                int izValue = (int)zValue;
                int iz = (int)z;

                if (izValue == 0 && iz != 0)
                    return iz;
                else
                    return izValue;
            }
        }
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

        var settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        if (webData.isNetworkError || webData.isHttpError) {
            Debug.LogError(webData.error);
            Debug.LogError("Failed to search MazeMap");
        } else
        {
            Debug.Log(webData.downloadHandler.text);
            var data = JsonConvert.DeserializeObject<MazeMapSearch>(webData.downloadHandler.text, settings);
            if(data.result.Count == 0) {
                Debug.LogError("No results for search");
                searchState = SearchResult.Failed;
            }else
            {
                /* First, try to find a room on the preferred campus */
                MazeMapResult result = data.result.Find(delegate(MazeMapResult res)
                {
                    if (res.geometry.type == null)
                        return false;

                    for (int i = 0; i < preferredCampuses.Length; i++)
                        if (preferredCampuses[i] == res.campusId)
                            return true;

                    return false;
                });

                /* If no room was found, just pick the first one */
                if (result.title == null)
                    result = data.result[0];

                /* If it's not a point geometry type, the rest of it would fail */
                if (result.geometry.type != "Point")
                    throw new System.ApplicationException(
                        string.Format("Unexpected geometry type: {0}", result.geometry.type));

                Debug.Log(string.Format("Room: {0} at campus {1}", result.title, result.campusId));

                currentRoomInfo = result;

                mapDataUrl = string.Format(
                    mapDataUrlTemplate,
                    result.geometry.latitude,
                    result.geometry.longitude,
                    Mathf.FloorToInt((float)result.zValue),
                    mapSRID).Replace(",", ".");

                searchState = SearchResult.Complete;
            }
        }
    }

    private int FloorToIndex(MazeMapResult result) {
        int idx = result.floor;

        if (idx > 0)
            idx--;

        return idx;
    }

    private int FloorToIndex(string text)
    {
        try
        {
            int idx = Convert.ToInt32(text, 10);

            if (idx > 0)
                idx--;

            return idx;
        } catch(ArgumentOutOfRangeException e)
        {
            return 0;
        }
        catch (FormatException e)
        {
            return 0;
        }
    }

    private void ShowError(string error = "Something went wrong") {
        GameObject obj = GameObject.FindGameObjectWithTag("StatusText");
        Text statusField = obj.GetComponent<Text>();
        statusField.text = error;
    }

    private void ClearError() {
        ShowError("");
    }

    IEnumerator PerformSearchInternal() {
        ClearError();
        
        /* Perform search in MazeMap */
        yield return GetMapDataUrl();

        if(searchState == SearchResult.Failed)
        {
            ShowError("No results for search");
            yield break;
        }

        /* Put MazeMap POI url into MazeMapGet to get geometry of room */
        MazeMapGet mapGet = GetComponent<MazeMapGet>();
        mapGet.sourceUrl = mapDataUrl;

        /* We can then await MazeMapGet for the geometry data */
        yield return mapGet.GetData();

        if(mapGet.geometryData.coordinates == null)
        {
            ShowError("Couldn't find shape of room");
            yield break;
        }

        /* Once data has arrived, create the mesh to place out */
        BuildMesh3 meshBuilder = GetComponent<BuildMesh3>();

        if(meshBuilder == null)
        {
            Debug.LogError("Failed to locate BuildMesh3 component");
            ShowError();
            yield break;
        }

        yield return meshBuilder.MakeModel(mapGet.geometryData.coordinates);
    }

    public void PerformSearch() {
        StartCoroutine(PerformSearchInternal());
    }

    private void Update() {
        GPSPosition holoPosition = GetComponent<GPSPosition>();
        BuildMesh3 meshData = GetComponent<BuildMesh3>();
        Transform currentPos = gameObject.transform;

        FindGPSDistance.dd direction = FindGPSDistance.GPSDistance( holoPosition.longitude, holoPosition.latitude, meshData.longitude, meshData.latitude);
        
        /* The end result */
        currentPos.position = new Vector3(direction.distance[1], BuildMesh3.roomHeight * (FloorToIndex(currentRoomInfo) - FloorToIndex(playerFloor.text)), direction.distance[0]);
        currentPos.rotation = Quaternion.Euler(0, 90 + (float)meshData.longitude, 0);
    }
}
