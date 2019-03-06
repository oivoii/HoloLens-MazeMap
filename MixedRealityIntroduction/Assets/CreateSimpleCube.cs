using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSimpleCube: MonoBehaviour {

  
    // Start is called before the first frame update
    void Start() {

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(1.0f, 0.0f, 2.0f);
        cube.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
