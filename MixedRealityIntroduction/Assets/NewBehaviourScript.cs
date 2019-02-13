using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

  
    // Start is called before the first frame update
    void Start() {

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(1.0f, 0.0f, 2.0f);
        cube.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        StartCoroutine(ProcessData());
    }

    IEnumerator ProcessData()
    {
        WebGet w = GetComponent<WebGet>();

        w.sourceUrl = "https://api.mazemap.com/api/pois/2037/?srid=4326";

        yield return StartCoroutine(w.GetData());

        Debug.Log(w.geometryData.coordinates.Count);
        Debug.Log(w.geometryData.coordinates[0].Count);
        Debug.Log(w.geometryData.coordinates[0][0].Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
