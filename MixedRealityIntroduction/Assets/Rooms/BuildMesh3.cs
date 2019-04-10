using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BuildMesh3 : MonoBehaviour {

    public static readonly float roomHeight = 3;

    public double longitude;
    public double latitude;
    
    //hardcoded Lists used for testing, remove once the program gets coordinates as an input
    //Realistic Input:
    //List<List<List<double>>> inputV = new List<List<List<double>>> { new List<List<double>> { new List<double> { 10.406319674645273, 63.41589902004175, 0 }, new List<double> { 10.406319453789019, 63.41589950202984, 0 }, new List<double> { 10.406319992690841, 63.41589990951014, 0 }, new List<double> { 10.40631938634157, 63.415900246895845, 0 }, new List<double> { 10.406319486433711, 63.41590056014668, 0 }, new List<double> { 10.406376371497988, 63.41591221801376, 0 }, new List<double> { 10.406475916425519, 63.41581751894445, 0 }, new List<double> { 10.406432257539723, 63.41580857049538, 0 }, new List<double> { 10.40643175627542, 63.4158090270755, 0 }, new List<double> { 10.406431372377392, 63.4158083891613, 0 }, new List<double> { 10.406341380861916, 63.41578995352134, 0 }, new List<double> { 10.406340219327982, 63.41579019599613, 0 }, new List<double> { 10.406340615814855, 63.41578979679394, 0 }, new List<double> { 10.406315832869307, 63.41578471976363, 0 }, new List<double> { 10.40631540394491, 63.41578510745009, 0 }, new List<double> { 10.406315075932673, 63.41578456469771, 0 }, new List<double> { 10.406274208166147, 63.41577619416354, 0 }, new List<double> { 10.406174778685116, 63.4158708820082, 0 }, new List<double> { 10.406210856729558, 63.41587828451439, 0 }, new List<double> { 10.406211529714742, 63.41587806981656, 0 }, new List<double> { 10.406211783037065, 63.41587767155538, 0 }, new List<double> { 10.406212559492221, 63.41587756897874, 0 }, new List<double> { 10.406213018434885, 63.4158772676347, 0 }, new List<double> { 10.40621399764167, 63.4158772683226, 0 }, new List<double> { 10.406214629802294, 63.41587706349683, 0 }, new List<double> { 10.406215448687878, 63.415877189865114, 0 }, new List<double> { 10.406215996560515, 63.415877118767646, 0 }, new List<double> { 10.406216521250357, 63.41587727162236, 0 }, new List<double> { 10.406217849472936, 63.415877273192045, 0 }, new List<double> { 10.406218743650793, 63.41587792507632, 0 }, new List<double> { 10.406219383177874, 63.41587811325112, 0 }, new List<double> { 10.406219569083996, 63.415878527086775, 0 }, new List<double> { 10.406220246323374, 63.41587902129123, 0 }, new List<double> { 10.406219967333131, 63.41587942403493, 0 }, new List<double> { 10.406220333503633, 63.4158802279493, 0 }, new List<double> { 10.406311172464315, 63.41589885529703, 0 }, new List<double> { 10.406311765796422, 63.41589867444323, 0 }, new List<double> { 10.406311908900074, 63.41589833489761, 0 }, new List<double> { 10.406312758235062, 63.415898224851425, 0 }, new List<double> { 10.40631335946237, 63.41589783401069, 0 }, new List<double> { 10.406314808213237, 63.41589796129027, 0 }, new List<double> { 10.406315850021999, 63.41589782755024, 0 }, new List<double> { 10.406316854410777, 63.41589814047724, 0 }, new List<double> { 10.406317752218452, 63.41589822042894, 0 }, new List<double> { 10.40631820698628, 63.415898562372575, 0 }, new List<double> { 10.406319674645273, 63.41589902004175, 0 } }, new List<List<double>> { new List<double> { 10.406360796728087, 63.41585185538391, 0 }, new List<double> { 10.406361199178592, 63.41585159173699, 0 }, new List<double> { 10.406361169190665, 63.415851266704706, 0 }, new List<double> { 10.406361739848702, 63.415851072758144, 0 }, new List<double> { 10.406361942125166, 63.41585076137809, 0 }, new List<double> { 10.406362629938272, 63.41585065437449, 0 }, new List<double> { 10.406363044917429, 63.41585038726438, 0 }, new List<double> { 10.406363782897689, 63.41585037844258, 0 }, new List<double> { 10.406364372001331, 63.415850181464236, 0 }, new List<double> { 10.406365085547648, 63.41585027236782, 0 }, new List<double> { 10.406365792504817, 63.41585016503021, 0 }, new List<double> { 10.406366423238957, 63.41585034842638, 0 }, new List<double> { 10.406367162544765, 63.41585034043003, 0 }, new List<double> { 10.406367637772517, 63.41585059904697, 0 }, new List<double> { 10.406368350619505, 63.41585069072722, 0 }, new List<double> { 10.406368620898965, 63.41585099772975, 0 }, new List<double> { 10.406369237652406, 63.41585117994008, 0 }, new List<double> { 10.40636927781267, 63.41585150528012, 0 }, new List<double> { 10.406369736989005, 63.41585175901995, 0 }, new List<double> { 10.406369544560102, 63.415852071622695, 0 }, new List<double> { 10.40636980263709, 63.41585237169061, 0 }, new List<double> { 10.406369400176951, 63.41585263534513, 0 }, new List<double> { 10.406369430163512, 63.41585296038123, 0 }, new List<double> { 10.40636885951149, 63.41585315432478, 0 }, new List<double> { 10.406368657241895, 63.41585346569673, 0 }, new List<double> { 10.406367969433083, 63.415853572701934, 0 }, new List<double> { 10.406367554437253, 63.41585383982432, 0 }, new List<double> { 10.406366816439268, 63.41585384864467, 0 }, new List<double> { 10.40636622734341, 63.41585404561933, 0 }, new List<double> { 10.406365513820615, 63.41585395471733, 0 }, new List<double> { 10.406364806884206, 63.41585406205399, 0 }, new List<double> { 10.406364176131172, 63.415853878654566, 0 }, new List<double> { 10.406363436811118, 63.41585388664942, 0 }, new List<double> { 10.406362961576459, 63.41585362803051, 0 }, new List<double> { 10.40636224871636, 63.415853536347164, 0 }, new List<double> { 10.406361978449524, 63.41585322935331, 0 }, new List<double> { 10.406361361712394, 63.41585304715011, 0 }, new List<double> { 10.406361321550452, 63.41585272181186, 0 }, new List<double> { 10.406360862360716, 63.41585246806648, 0 }, new List<double> { 10.406361054799403, 63.4158521554513, 0 }, new List<double> { 10.406360796728087, 63.41585185538391, 0 } }, new List<List<double>> { new List<double> { 10.406260439432263, 63.4158313504955, 0 }, new List<double> { 10.406260888408687, 63.41583105727141, 0 }, new List<double> { 10.406260853531156, 63.41583069609571, 0 }, new List<double> { 10.406261492961887, 63.4158304794002, 0 }, new List<double> { 10.406261716974843, 63.41583013377416, 0 }, new List<double> { 10.40626248447498, 63.41583001485482, 0 }, new List<double> { 10.406262945346983, 63.41582971859788, 0 }, new List<double> { 10.40626376588079, 63.41582970917677, 0 }, new List<double> { 10.40626441861894, 63.4158294911986, 0 }, new List<double> { 10.406265211758837, 63.415829592292, 0 }, new List<double> { 10.406265992485329, 63.4158294739537, 0 }, new List<double> { 10.406266691803033, 63.41582967712411, 0 }, new List<double> { 10.406267512754246, 63.415829668535935, 0 }, new List<double> { 10.406268037981455, 63.415829954044085, 0 }, new List<double> { 10.40626883046026, 63.41583005591588, 0 }, new List<double> { 10.406269130236097, 63.41583039578553, 0 }, new List<double> { 10.406269816664043, 63.41583059808254, 0 }, new List<double> { 10.406269861706326, 63.41583095912909, 0 }, new List<double> { 10.4062703748388, 63.415831241938236, 0 }, new List<double> { 10.406270160697654, 63.41583158880896, 0 }, new List<double> { 10.40627045055193, 63.41583192440438, 0 }, new List<double> { 10.406270001564812, 63.415832217635426, 0 }, new List<double> { 10.40627003644234, 63.4158325788111, 0 }, new List<double> { 10.406269397011611, 63.4158327955066, 0 }, new List<double> { 10.406269173003778, 63.4158331411245, 0 }, new List<double> { 10.406268405505337, 63.41583326004738, 0 }, new List<double> { 10.406267944600788, 63.41583355632559, 0 }, new List<double> { 10.406267124056406, 63.41583356574232, 0 }, new List<double> { 10.406266471372483, 63.41583378370203, 0 }, new List<double> { 10.406265678264463, 63.415833682617084, 0 }, new List<double> { 10.406264897462941, 63.41583380097084, 0 }, new List<double> { 10.406264198097007, 63.41583359777928, 0 }, new List<double> { 10.406263377228422, 63.41583360636205, 0 }, new List<double> { 10.406262852034832, 63.415833320879685, 0 }, new List<double> { 10.406262059492834, 63.41583321900418, 0 }, new List<double> { 10.40626175969919, 63.41583287910369, 0 }, new List<double> { 10.406261073307943, 63.41583267681058, 0 }, new List<double> { 10.406261028269231, 63.41583231579798, 0 }, new List<double> { 10.406260515122364, 63.4158320329881, 0 }, new List<double> { 10.406260729279328, 63.41583168609263, 0 }, new List<double> { 10.406260439432263, 63.4158313504955, 0 } } };
    //Simple Input:
    //List<List<List<double>>> inputV = new List<List<List<double>>> { new List<List<double>> { new List<double> { 10.406320, 63.415900, 0 }, new List<double> { 10.406420, 63.415900, 0 }, new List<double> { 10.406420, 63.416000, 0 }, new List<double> { 10.406370, 63.416050, 0 }, new List<double> { 10.406320, 63.416000, 0 }, new List<double> { 10.406320, 63.415900, 0 } } };
    
    public IEnumerator MakeModel(List<List<List<double>>> inputV) {

		MeshFilter mf = GetComponent<MeshFilter>();
		Mesh mesh = mf.mesh;


        //Distance between floor and roof of room
        float scaleFactor = 1;

        //finding index of main room (largest latitude span)
        int roomIndex = 0;
        double maxLat = 0;
        double minLat = 0;
        double biggestLatD = 0;

        for (int i = 0; i < inputV.Count; i++)
        {
            for (int j = 0; j < inputV[i].Count; j++)
            {

                if (inputV[i][j][0] > maxLat) {
                    maxLat = inputV[i][j][1];
                }

                if (inputV[i][j][0] < minLat) {
                    minLat = inputV[i][j][1];
                }
            }

            if (biggestLatD < (maxLat - minLat))
            {
                biggestLatD = (maxLat - minLat);
                roomIndex = i;
            }

        }

        // Setting up public variables
        longitude = inputV[roomIndex][0][0];
        latitude = inputV[roomIndex][0][1];

        //Finding number of points in room. We don't care about the last point in the list, since it is a duplicate of the first.
        int vectorLength = inputV[roomIndex].Count - 1;

        //Translating from geodetic to ECEF coordinates, and putting them in a Vector
        var vertices = new Vector3[2 * vectorLength];

        double rA = 6378137;             //[m] radius of earth at equator
        double rB = 6356752;            //[m] radius of earth at pole
        double c = 1 - ((rB * rB) / (rA * rA)); //square of first numerical eccentricity
        double degToRad = (Math.PI) / 180; // Used to convert from degrees to radians

        double refX = 0; // Used to set the first vertex as the origin of the coordinate system
        double refY = 0;
        double refZ = 0;

        for (int i = 0; i < vectorLength; i++)
        {

            double phi = degToRad * inputV[roomIndex][i][1];                  //latitude[nord - sør]
            double theta = degToRad * inputV[roomIndex][i][0];                //Longitude[øst - vest]
            double sinPhi = Math.Sin(phi);
            double sinTheta = Math.Sin(theta);
            double cosPhi = Math.Cos(phi);
            double cosTheta = Math.Cos(theta);

            double tempo = 1 - (c * (sinPhi * sinPhi));

            double N = (rA / Math.Sqrt(tempo));

            double x = (N * cosPhi * cosTheta);
            double y = (N * cosPhi * sinTheta);
            double z = (((rB * rB) / (rA * rA)) * N * sinPhi);

            if (i == 0)
            {
                refX = x;
                refY = y;
                refZ = z;
            }


            vertices[i][0] = (float)(1 * (x - refX));
            vertices[i][2] = (float)(1 * (y - refY));
            vertices[i][1] = (float)(1 * (z - refZ));

        }


        //Translating all points onto the xy plane.
        for (int i = 1; i < vectorLength; i++)
        {
            double x = vertices[i][0];
            double y = vertices[i][2];
            double z = vertices[i][1];
            double xy_length = Math.Sqrt((x * x) + (y * y));
            double x_unit = x / xy_length;
            double y_unit = y / xy_length;

            double length = Math.Sqrt((x * x) + (y * y) + (z * z));

            vertices[i][0] = (float)(x_unit * length);
            vertices[i][1] = 0;
            vertices[i][2] = (float)(y_unit * length);

            vertices[i + vectorLength][0] = vertices[i][0]; // Making vertices for the roof
            vertices[i + vectorLength][2] = vertices[i][2];
            vertices[i + vectorLength][1] = roomHeight;
        }

        vertices[vectorLength][1] = roomHeight;

        //Finding center of mesh.
        float maxValX = vertices[0].x;
        float minValX = vertices[0].x;
        //float maxValY = vertices[0].y;
        //float minValY = vertices[0].y;
        float maxValZ = vertices[0].z;
        float minValZ = vertices[0].z;

        for (int i = 0; i < vertices.Length; i++)
        {
            if (maxValX < vertices[i].x) { 
                maxValX = vertices[i].x;
            }
            else if (minValX > vertices[i].x) { 
                minValX = vertices[i].x;
            }

            //if (maxValY < vertices[i].y) { maxValY = vertices[i].y; }
            //else if (minValY > vertices[i].y) { minValY = vertices[i].y; }

            if (maxValZ < vertices[i].z) {
                maxValZ = vertices[i].z;
            }
            else if (minValZ > vertices[i].z) {
                minValZ = vertices[i].z;
            }
        }

        float centerX = (maxValX + minValX) / 2;
        //float centerY = (maxValY + minValY) / 2;
        float centerZ = (maxValZ + minValZ) / 2;

        //Converting to int and centering;
        for (int i = 0; i < vertices.Length; i++)
        {
            
            vertices[i].x = (int)(1000000 * (vertices[i].x - centerX));
            
            //vertices[i].y = (int)(1000 * (vertices[i].y - centerY));
            
            vertices[i].z = (int)(1000000 * (vertices[i].z - centerZ));
        }

        // copying vertices for floor and roof into 2d vectors
        var verts2dbot = new Vector2[vectorLength];
        var verts2dbot2 = new Vector2[vectorLength];

        for (int i = 0; i < vectorLength; i++)
        {
            verts2dbot[i][0] = vertices[i][0];
            verts2dbot[i][1] = vertices[i][2];
            verts2dbot2[i][0] = vertices[i+vectorLength][0];
            verts2dbot2[i][1] = vertices[i+vectorLength][2];
        }

        //Generating mesh render triangles for floor and roof
        var triangulator2d = new Triangulator(verts2dbot);
        var triangulator2d2 = new Triangulator(verts2dbot2);

        var indicesRoof = triangulator2d.Triangulate();
        var indicesFloor = triangulator2d2.Triangulate();

        for (int i = 0; i < indicesFloor.Length; i++)
        {
            indicesFloor[i] = indicesFloor[i] + vectorLength;
        }

        // Flips floor
        int tmp;
        for (int i = 0; i < indicesFloor.Length; i += 3)
        {
            tmp = indicesRoof[i + 1];
            indicesRoof[i + 1] = indicesRoof[i + 2];
            indicesRoof[i + 2] = tmp;
        }

        //Generate Triangles used to render walls. 3 points, clockwise determines which side is visible
        int[] triangles = new int[6 * vectorLength];

        for (int i = 0; i < vectorLength; i++)
        {
            if (i == (vectorLength - 1))
            {
                triangles[6 * i] = i;
                triangles[(6 * i) + 1] = 0;
                triangles[(6 * i) + 2] = i + vectorLength;
                triangles[(6 * i) + 3] = 0;
                triangles[(6 * i) + 4] = vectorLength;
                triangles[(6 * i) + 5] = i + vectorLength;
            }
            else
            {
                triangles[6 * i] = i;
                triangles[(6 * i) + 1] = i + 1;
                triangles[(6 * i) + 2] = i + vectorLength;
                triangles[(6 * i) + 3] = i + 1;
                triangles[(6 * i) + 4] = i + vectorLength + 1;
                triangles[(6 * i) + 5] = i + vectorLength;
            }
        }

        // Collecting triangles in one vector
        var trianglesTot = new int[6 * vectorLength + 2 * indicesRoof.Length];

        for (int i = 0; i < trianglesTot.Length; i++)
        {
            if (i < triangles.Length)
            {
                trianglesTot[i] = triangles[i];
            }
            else if (i < (triangles.Length + indicesRoof.Length))
            {
                trianglesTot[i] = indicesRoof[i - triangles.Length];
            }
            else
            {
                trianglesTot[i] = indicesFloor[i - triangles.Length - indicesRoof.Length];
            }
        }

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i][0] = (((vertices[i][0]) / 1000000) * scaleFactor);
            vertices[i][1] = (vertices[i][1] * scaleFactor);
            vertices[i][2] = (((vertices[i][2]) / 1000000) * scaleFactor);
        }

        mesh.Clear ();
		mesh.vertices = vertices;
		mesh.triangles = trianglesTot;

        mesh.RecalculateNormals();

        yield return null;

    }
	
}
