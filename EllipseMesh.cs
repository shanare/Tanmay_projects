

using System;
using System.CodeDom;
using System.Diagnostics;
using System.Linq;	// This allows the use of the Contains() method
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class EllipseMesh : MonoBehaviour {
    
	public int rSize, angleSize, i_end, j_end;
    public float xLength, yLength;
    
	private Mesh mesh;
	private Vector3[] vertices;
	private int[] edgeIndexes;
    
	private void Awake () {
		Generate();
	}

    private void Generate() {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Oval";


        vertices = new Vector3[rSize * angleSize + 1];
        edgeIndexes = new int[angleSize];
        int[] triangles = new int[600];
        
       

        //Vector2[] uv = new Vector2[vertices.Length];
        //Vector4[] tangents = new Vector4[vertices.Length];
        //Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
        vertices[0] = new Vector3(0, 0);
        float x = 0f, y = 0f, theta = 0f;
        float xRadius = xLength / 2f, yRadius = yLength / 2f, radius = 0f;

        for (int i = 1, j = 0, angle = 0; angle < angleSize; angle++, j++)
        {
            theta = 2f * Mathf.PI * ((float)angle / ((float)angleSize));
            radius = xRadius * yRadius / Mathf.Sqrt(Mathf.Pow(yRadius * Mathf.Cos(theta), 2) + Mathf.Pow(xRadius * Mathf.Sin(theta), 2));

            for (int r = 1; r <= rSize; r++, i++) {
                x = radius * ((float)r / (float)rSize) * Mathf.Cos(theta);
                y = radius * ((float)r / (float)rSize) * Mathf.Sin(theta);
                print(x);
                print(y);
                vertices[i] = new Vector3(x, y);
                //uv[i] = new Vector2((float)x / xSize, (float)y / ySize);
                //tangents[i] = tangent;
            }

            edgeIndexes[j] = i - 1;
        }
        /*     triangles[0]=1;
        triangles[0] = triangles[21] = 0;
          triangles[1] = triangles[3]= triangles[23]= triangles[24] = triangles[27]=5;
          triangles[2] = triangles[5] = triangles[6] = 1;
          triangles[4] = triangles[7] = triangles[10]= triangles[29]= triangles[30] = triangles[33] = 6;
          triangles[8] = triangles[9]= triangles[12]=2;
          triangles[11] = triangles[13]= triangles[16] = triangles[35] = 7;
          triangles[14] = triangles[15] = triangles[18] =3;
          triangles[17] = triangles[19] = 8;
          triangles[20] = 4;
          triangles[22] = triangles[25]=9;
          triangles[26] = triangles[28]= triangles[31] =  10;
         triangles[32] = triangles[34] = 11;


                     triangles[3 * i] = a;
                 triangles[3 * i + 1] = rSize * (i + 1) + 1;
                 triangles[3 * i + 2] = rSize * (i) + 1;


         triangles[3 * a] = rSize * i + j + 1;
                triangles[3 * (a) + 1] = rSize * (i + 1) +j+ 1;
                triangles[3 * (a) + 2] = rSize * (i + 1) +j+ 2;
                a++;

        triangles[0] = 1;
        triangles[1] = 5;
        triangles[2] = 6;
        triangles[3] = 2;
        triangles[4] = 6;
        triangles[5] = 7;
        triangles[6] = 3;
        triangles[7] = 7;
        triangles[8] = 8;
        triangles[9] = 5;
        triangles[10] = 9;
        triangles[11] = 10;
        triangles[12] = 6;
        triangles[13] = 10;
        triangles[14] = 11;
        triangles[15] = 7;
        triangles[16] = 11;
        triangles[17] = 12;
         */







        for (int i = 0, a = angleSize,t=0; i < angleSize-1; i++)
        {
            
            triangles[3 * i] = t;
            triangles[3 * i + 1] = rSize * (i + 1) + 1;
            triangles[3 * i + 2] = rSize * (i) + 1;
            for (int j = 0; j < rSize-1; j++)
            {
                triangles[3 * a] = rSize * i + j + 1;
                triangles[3 * (a) + 1] = rSize * (i + 1) + j + 1;
                triangles[3 * (a) + 2] = rSize * (i + 1) + j + 2;
                triangles[3 * a + 3] = rSize * i + j + 1;
                triangles[3 * (a) + 4] = rSize * (i + 1) +j+ 2;
                triangles[3 * (a) + 5] = rSize * (i) +j +2;
                a = a+2;
             
            }
        }
        i_end = angleSize - 1;
        triangles[3 *i_end] = 0;
        triangles[3 * i_end + 1] = 1; 
        triangles[3 * i_end + 2] = rSize * (i_end) + 1;
        for (int j = 0, a = angleSize*10 ; j < rSize - 1; j++)
        {
            triangles[3 * a] = rSize * i_end + j + 1;
            triangles[3 * (a) + 1] = j + 1;
            triangles[3 * (a) + 2] = j + 2;
            triangles[3 * a + 3] = rSize * i_end + j + 1;
            triangles[3 * (a) + 4] = j + 2;
            triangles[3 * (a) + 5] = rSize * (i_end) + j + 2;
            a = a+2;
        }
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        //UnityEngine.Debug.Log(edgeIndexes[k]);

        //mesh.uv = uv;
        //mesh.tangents = tangents;

        //int[] triangles = new int[(2 * rSize - 1) * angleSize];
        //for (int ti = 0, vi = 0, angle = 0; angle < angleSize; angle++, vi++)
        //{
        //    for (int r = 0; r < rSize; r++, ti += 6, vi++)
        //    {
        //        triangles[ti] = vi;
        //        triangles[ti + 3] = triangles[ti + 2] = vi + 1;
        //        triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
        //        triangles[ti + 5] = vi + xSize + 2;
        //    }
        //}
        //mesh.triangles = triangles;
        //mesh.RecalculateNormals();
    }

	private void OnDrawGizmos()
	{
		if (vertices == null)
		{
			return;
		}
		
		for (int i = 0; i < vertices.Length; i++)
		{
            if (edgeIndexes.Contains(i))
            {
                Gizmos.color = Color.black;
            }
            else
            {
                Gizmos.color = Color.red;
            }
            
			Gizmos.DrawSphere(vertices[i], 0.1f);
		}
	}
}

