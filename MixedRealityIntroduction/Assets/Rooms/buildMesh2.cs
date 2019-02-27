using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class buildMesh2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(GetMeshData());
	}

    List<Vector3> ConvertMeshData(MazeMapGet.GeometryData geomData) {
        List<Vector3> outData = new List<Vector3>();

        return outData;
    }

    IEnumerator GetMeshData() {
        MazeMapGet w = GetComponent<MazeMapGet>();

        w.sourceUrl = "https://api.mazemap.com/api/pois/2037/?srid=4326";

        yield return StartCoroutine(w.GetData());

        List<Vector3> vertices = ConvertMeshData(w.geometryData);

        StartCoroutine(CreateMesh(vertices.ToArray()));
    }

    IEnumerator CreateMesh(Vector3[] vertices) {
        int vectorLength = (vertices.Length / 2);
        //print(vectorLength);
        // Converting floats to int
        float referense1 = vertices[0][0];
        float referense2 = vertices[0][1];

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i][0] -= referense1;
            vertices[i][0] = (int)(10000 * vertices[i][0]);
            vertices[i][1] -= referense2;
            vertices[i][1] = (int)(10000 * vertices[i][1]);
            vertices[i][2] = (int)(10000 * vertices[i][2]);
        }

        // Copying bottom vertices
        var verts2dbot = new Vector2[vectorLength];
        var verts2dbot2 = new Vector2[vectorLength];

        for (int i = 0; i < vectorLength; i++)
        {
            //verts2dbot[i] = new Vector2(0,0);
            verts2dbot[i][0] = vertices[i][0];
            verts2dbot[i][1] = vertices[i][1];
            verts2dbot2[i][0] = vertices[i + vectorLength][0];
            verts2dbot2[i][1] = vertices[i + vectorLength][1];
            //print(verts2dbot[i][0]);
        };

        yield return null;

        //int[] verts2dtop = new int[vectorLength];

        //for (int i = 0; i < vectorLength; i++)
        //{
        //    verts2dtop[i] = new Vector2(0, 0);
        //    verts2dtop[i][0] = vertices[i][0];
        //    verts2dtop[i][1] = vertices[i][1];
        //};

        var triangulator2d = new Triangulator(verts2dbot);
        var triangulator2d2 = new Triangulator(verts2dbot2);

        var indicesbot = triangulator2d.Triangulate();
        var indicestop = triangulator2d2.Triangulate();

        for (int i = 0; i < indicestop.Length; i++)
        {
            indicestop[i] = indicestop[i] + vectorLength;
            print(indicesbot[i]);
            print(indicestop[i]);
        };

        int tmp;
        for (int i = 0; i < indicestop.Length; i += 3)
        {
            tmp = indicestop[i + 1];
            indicestop[i + 1] = indicestop[i + 2];
            indicestop[i + 2] = tmp;
        };

        yield return null;

        //Triangles// 3 points, clockwise determines which side is visible
        int[] triangles = new int[6 * vectorLength];

        for (int i = 0; i < vectorLength; i++)
        {
            if (i == (vectorLength - 1))
            {
                triangles[6 * i] = i;
                triangles[(6 * i) + 1] = i + vectorLength;
                triangles[(6 * i) + 2] = 0;
                triangles[(6 * i) + 3] = 0;
                triangles[(6 * i) + 4] = i + vectorLength;
                triangles[(6 * i) + 5] = vectorLength;
            }
            else
            {
                triangles[6 * i] = i;
                triangles[(6 * i) + 1] = i + vectorLength;
                triangles[(6 * i) + 2] = i + 1;
                triangles[(6 * i) + 3] = i + 1;
                triangles[(6 * i) + 4] = i + vectorLength;
                triangles[(6 * i) + 5] = i + vectorLength + 1;
            }
        };

        yield return null;

        var trianglesTot = new int[6 * vectorLength + 2 * indicesbot.Length];

        for (int i = 0; i < trianglesTot.Length; i++)
        {
            if (i < triangles.Length)
            {
                trianglesTot[i] = triangles[i];
            }
            else if (i < (triangles.Length + indicesbot.Length))
            {
                trianglesTot[i] = indicesbot[i - triangles.Length];
            }
            else
            {
                trianglesTot[i] = indicestop[i - triangles.Length - indicesbot.Length];
            }
        };

        yield return null;

        Mesh mesh = GetComponent<MeshFilter>().mesh;

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = trianglesTot;
        //mesh.uv = uvs;

        mesh.RecalculateNormals();
    }
}















