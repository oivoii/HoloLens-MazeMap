using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class buildMesh2 : MonoBehaviour {

    //public Vector3 vertLeftTopFront = new Vector3(-1,1,1);
    //public Vector3 vertRightTopFront = new Vector3(1,1,1);
    //public Vector3 vertRightTopBack = new Vector3(1,1,-1);
    //public Vector3 vertLeftTopBack = new Vector3(-1,1,-1);

	//private float waitN = 3f;
	//private float waitD = 3f;
	//public int shapeN = 0;

	// Use this for initialization
	void Start () {

		MeshFilter mf = GetComponent<MeshFilter>();
		Mesh mesh = mf.mesh;

		//Vertices//
		var vertices = new Vector3[]
		{
            
            new Vector3(1.67623390098f, 1.55086271355f, 0), //0
            new Vector3(1.57355469496f, 1.52991681666f, 0),
            new Vector3(1.50473501581f, 1.59663141296f, 0),
            new Vector3(1.50483480463f, 1.59675206056f, 0), //3
            new Vector3(1.6073222578f, 1.6177022581f, 0),
            new Vector3(1.58511539742f, 1.6393958556f, 0),
            new Vector3(2.04530292168f, 1.7334756547f, 0), //6
            new Vector3(2.07210458723f, 1.70767168554f, 0),
            new Vector3(2.14434445953f, 1.72240386966f, 0),
            new Vector3(2.21749338057f, 1.65149325844f, 0), //9
            new Vector3(2.17002899195f, 1.6417189099f, 0),
            new Vector3(2.25578825927f, 1.5585383359f, 0),
            new Vector3(2.30339571733f, 1.5681822882f, 0), //12
            new Vector3(2.48557013509f, 1.3914069552f, 0),
            new Vector3(2.31643991264f, 1.3568088866f, 0),
            new Vector3(2.3403335188f, 1.33343752274f, 0), //15
            new Vector3(2.12265379967f, 1.28893680745f, 0),
            new Vector3(2.09742480941f, 1.3120356668f, 0),
            new Vector3(1.95293185723f, 1.2825327746f, 0),
            //new Vector3(1.67623390098f, 1.55086271355f, 0), //19

            new Vector3(1.67623390098f, 1.55086271355f, 1), //20
            new Vector3(1.57355469496f, 1.52991681666f, 1),
            new Vector3(1.50473501581f, 1.59663141296f, 1),
            new Vector3(1.50483480463f, 1.59675206056f, 1),
            new Vector3(1.6073222578f, 1.6177022581f, 1),
            new Vector3(1.58511539742f, 1.6393958556f, 1),
            new Vector3(2.04530292168f, 1.7334756547f, 1),
            new Vector3(2.07210458723f, 1.70767168554f, 1),
            new Vector3(2.14434445953f, 1.72240386966f, 1),
            new Vector3(2.21749338057f, 1.65149325844f, 1),
            new Vector3(2.17002899195f, 1.6417189099f, 1),
            new Vector3(2.25578825927f, 1.5585383359f, 1),
            new Vector3(2.30339571733f, 1.5681822882f, 1),
            new Vector3(2.48557013509f, 1.3914069552f, 1),
            new Vector3(2.31643991264f, 1.3568088866f, 1),
            new Vector3(2.3403335188f, 1.33343752274f, 1),
            new Vector3(2.12265379967f, 1.28893680745f, 1),
            new Vector3(2.09742480941f, 1.3120356668f, 1),
            new Vector3(1.95293185723f, 1.2825327746f, 1)
            //new Vector3(1.67623390098f, 1.55086271355f, 1) //39




   //         //front face//
   //         vertLeftTopFront,//left top front, 0
			//vertRightTopFront,//right top front, 1
			//new Vector3(-1,-1,1),//left bottom front, 2
			//new Vector3(1,-1,1),//right bottom front, 3

			////back face//
			//vertRightTopBack,//right top back, 4
			//vertLeftTopBack,//left top back, 5
			//new Vector3(1,-1,-1),//right bottom back, 6
			//new Vector3(-1,-1,-1),//left bottom back, 7

			////left face//
			//vertLeftTopBack,//left top back, 8
			//vertLeftTopFront,//left top front, 9
			//new Vector3(-1,-1,-1),//left bottom back, 10
			//new Vector3(-1,-1,1),//left bottom front, 11

			////right face//
			//vertRightTopFront,//right top front, 12
			//vertRightTopBack,//right top back, 13
			//new Vector3(1,-1,1),//right bottom front, 14
			//new Vector3(1,-1,-1),//right bottom back, 15

			////top face//
			//vertLeftTopBack,//left top back, 16
			//vertRightTopBack,//right top back, 17
			//vertLeftTopFront,//left top front, 18
			//vertRightTopFront,//right top front, 19

			////bottom face//
			//new Vector3(-1,-1,1),//left bottom front, 20
			//new Vector3(1,-1,1),//right bottom front, 21
			//new Vector3(-1,-1,-1),//left bottom back, 22
			//new Vector3(1,-1,-1)//right bottom back, 23

		};

        int vectorLength = (vertices.Length/2);
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
            verts2dbot2[i][0] = vertices[i+vectorLength][0];
            verts2dbot2[i][1] = vertices[i+vectorLength][1];
            //print(verts2dbot[i][0]);
        };

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
            tmp = indicestop[i+1];
            indicestop[i + 1] = indicestop[i + 2];
            indicestop[i + 2] = tmp;
        };

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

            //0,20,1,
            //1,20,21,

            //1,21,2,
            //2,21,22,

            //2,22,3,
            //3,22,23,

            //3,23,4,
            //4,23,24,

            //4,24,5,
            //5,24,25,

            //5,25,6,
            //6,25,26,

            //6,26,7,
            //7,26,27,

            //7,27,8,
            //8,27,28,

            //8,28,9,
            //9,28,29,

            //9,29,10,
            //10,29,30,

            //10,30,11,
            //11,30,31,

            //11,31,12,
            //12,31,32,

            //12,32,13,
            //13,32,33,

            //13,33,14,
            //14,33,34,

            //14,34,15,
            //15,34,35,

            //15,35,16,
            //16,35,36,

            //16,36,17,
            //17,36,37,
            
            //17,37,18,
            //18,37,38,
            
            //18,38,19,
            //19,38,39,
            
            //19,39,0,
            //0,39,20




			//front face//
			//0,2,3,//first triangle
			//3,1,0,//second triangle

			////back face//
			//4,6,7,//first triangle
			//7,5,4,//second triangle

			////left face//
			//8,10,11,//first triangle
			//11,9,8,//second triangle

			////right face//
			//12,14,15,//first triangle
			//15,13,12,//second triangle

			////top face//
			//16,18,19,//first triangle
			//19,17,16,//second triangle

			////bottom face//
			//20,22,23,//first triangle
			//23,21,20//second triangle
		//};

		//UVs//
		//Vector2[] uvs = new Vector2[]
		//{
		//	//front face// 0,0 is bottom left, 1,1 is top right//
		//	new Vector2(0,1),
		//	new Vector2(0,0),
		//	new Vector2(1,1),
		//	new Vector2(1,0),

		//	new Vector2(0,1),
		//	new Vector2(0,0),
		//	new Vector2(1,1),
		//	new Vector2(1,0),

		//	new Vector2(0,1),
		//	new Vector2(0,0),
		//	new Vector2(1,1),
		//	new Vector2(1,0),

		//	new Vector2(0,1),
		//	new Vector2(0,0),
		//	new Vector2(1,1),
		//	new Vector2(1,0),

		//	new Vector2(0,1),
		//	new Vector2(0,0),
		//	new Vector2(1,1),
		//	new Vector2(1,0),

		//	new Vector2(0,1),
		//	new Vector2(0,0),
		//	new Vector2(1,1),
		//	new Vector2(1,0)
		//};

		mesh.Clear ();
		mesh.vertices = vertices;
		mesh.triangles = trianglesTot;
		//mesh.uv = uvs;
		
		mesh.RecalculateNormals();
	
	}
	
	// Update is called once per frame
	//void Update () {
	//	if(waitN > 0f)
	//	{
	//		waitN -= Time.deltaTime;
	//	}
	//	else
	//	{
	//		waitN = waitD;
	//		shapeN ++;
	//		if(shapeN > 3)
	//		{
	//			shapeN = 0;
	//		}

	//	}

	//	//morph to cube//
	//	if(shapeN == 0)
	//	{
	//		vertLeftTopFront = Vector3.Lerp(vertLeftTopFront, new Vector3(-1,1,1),Time.deltaTime);
	//		vertRightTopFront = Vector3.Lerp(vertRightTopFront, new Vector3(1,1,1),Time.deltaTime);
	//		vertRightTopBack = Vector3.Lerp(vertRightTopBack, new Vector3(1,1,-1),Time.deltaTime);
	//		vertLeftTopBack = Vector3.Lerp(vertLeftTopBack, new Vector3(-1,1,-1),Time.deltaTime);
	//	}

	//	//morph to pyramid//
	//	if(shapeN == 1)
	//	{
	//		vertLeftTopFront = Vector3.Lerp(vertLeftTopFront, new Vector3(0,1,0),Time.deltaTime);
	//		vertRightTopFront = Vector3.Lerp(vertRightTopFront, new Vector3(0,1,0),Time.deltaTime);
	//		vertRightTopBack = Vector3.Lerp(vertRightTopBack, new Vector3(0,1,0),Time.deltaTime);
	//		vertLeftTopBack = Vector3.Lerp(vertLeftTopBack, new Vector3(0,1,0),Time.deltaTime);
	//	}

	//	//morph to ramp//
	//	if(shapeN == 2)
	//	{
	//		vertLeftTopFront = Vector3.Lerp(vertLeftTopFront, new Vector3(-1,-1,2),Time.deltaTime);
	//		vertRightTopFront = Vector3.Lerp(vertRightTopFront, new Vector3(1,-1,2),Time.deltaTime);
	//		vertRightTopBack = Vector3.Lerp(vertRightTopBack, new Vector3(1,0.5f,-1),Time.deltaTime);
	//		vertLeftTopBack = Vector3.Lerp(vertLeftTopBack, new Vector3(-1,0.5f,-1),Time.deltaTime);
	//	}

	//	//morph to roof//
	//	if(shapeN == 3)
	//	{
	//		vertLeftTopFront = Vector3.Lerp(vertLeftTopFront, new Vector3(-1,0.2f,0),Time.deltaTime);
	//		vertRightTopFront = Vector3.Lerp(vertRightTopFront, new Vector3(1,0.2f,0),Time.deltaTime);
	//		vertRightTopBack = Vector3.Lerp(vertRightTopBack, new Vector3(1,0.2f,0),Time.deltaTime);
	//		vertLeftTopBack = Vector3.Lerp(vertLeftTopBack, new Vector3(-1,0.2f,0),Time.deltaTime);
	//	}

	//	Start();
	//}
}















