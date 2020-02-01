using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Globalization;
using UnityEditor;

public class generator : MonoBehaviour
{
	Mesh mesh;

	public string srcfile = "map12.tbo";

	public Vector3[] gen_vertices;
	public int[,] gen_faces;
	public Vector3[] center;
	public int[,] liaison;

	public GameObject[] child;

	void Awake()
	{
		srcfile = "Assets/" + srcfile;
	}

    void Start()
    {

    	parseTbo();

        //Debug.Log("Length " + gen_faces.Length);
    	for (int i = 0; i < gen_faces.Length / 6; i++)
    	{
           // Debug.Log("id " + i);
    		generate(i);
    	}

        //gameObject.AddComponent<MeshFilter>();
        //gameObject.AddComponent<MeshRenderer>();

        //mesh = new Mesh();
        //GetComponent<MeshFilter>().mesh = mesh;
    	/*mesh = GetComponent<MeshFilter>().mesh;
        mesh.vertices = gen_vertices;

        int[] triangles = gen_triangle;

        mesh.triangles = triangles;

        Vector2[] uvs = new Vector2[gen_vertices.Length];

        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(gen_vertices[i].x, gen_vertices[i].z);
        }
        mesh.uv = uvs;

        mesh.RecalculateNormals();*/
        //GenerateSecondaryUvSet(GetComponent<MeshFilter>().sharedMesh);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private Vector3 stringToFloat(string str)
    {
    	var culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
		culture.NumberFormat.NumberDecimalSeparator = ".";
       	var parse = Array.ConvertAll(str.Split(' '), s => float.Parse(s, culture));
       	return (new Vector3(parse[0], parse[1], parse[2]));
    }

    private int[] stringToInt(string str)
    {
    	String[] sep = { " " };
    	return (Array.ConvertAll(str.Split(sep, StringSplitOptions.RemoveEmptyEntries), s => int.Parse(s) - 1));
    }

    /*sommet, faces, center, laison*/
    void parseTbo()
    {	
    	TextReader reader;
		reader = new StreamReader(srcfile);

		reader.ReadLine(); //comment line
		int lenVertices = int.Parse(reader.ReadLine());
		gen_vertices = new Vector3[lenVertices];
		for (int i = 0; i < lenVertices; i++)
		{
			gen_vertices[i] = stringToFloat(reader.ReadLine());
		}
		reader.ReadLine(); //comment line
		int lenFaces = int.Parse(reader.ReadLine());
		center = new Vector3[lenFaces];
		liaison = new int[lenFaces, 6];
		gen_faces = new int[lenFaces, 6];
		child = new GameObject[lenFaces];
		for (int i = 0; i < lenFaces; i++)
		{
			var point = stringToInt(reader.ReadLine());
			for (int j = 0; j < 6; j++)
			{
				gen_faces[i, j] = point[j];
			}
		}
		reader.ReadLine(); //comment line
		int lenCenter = int.Parse(reader.ReadLine());
		for (int i = 0; i < lenCenter; i++)
		{
			center[i] = stringToFloat(reader.ReadLine());
		}
		reader.ReadLine(); //comment line
		int lenLiaison = int.Parse(reader.ReadLine());
		for (int i = 0; i < lenCenter; i++)
		{
			var l = stringToInt(reader.ReadLine());
			for (int j = 0; j < 6; j++)
			{
				liaison[i, j] = l[j];
			}
		}
    }

    void generate(int id)
    {
    	var face = new int[6];
       // Debug.Log(id);
    	for (int i = 0; i < 6; i++)
        {
            Debug.Log("vertices id " + gen_faces[id, i]);
    		face[i] = gen_faces[id, i];
        }
    	var ctr = center[id];
        child[id] = new GameObject("cell");
    	child[id].AddComponent<MeshRenderer>();
    	child[id].AddComponent<MeshFilter>();
    	var mesh = child[id].GetComponent<MeshFilter>().mesh;
    	int len;

        Debug.Log(face[5]);

    	if (face[5] == -1)
    		len = 6;
    	else
    		len = 7;
 		var vertices_work = new Vector3[len];
    	for (int i = 0; i < len - 1; i++)
    	{
    		vertices_work[i] = gen_vertices[face[i]];
            Debug.Log(gen_vertices[face[i]]);
            //Debug.Log("id " + id +  " i " + i);
    	}
       // Debug.Log("verticles i " + (len));
    	vertices_work[len - 1] = center[id];
        //Debug.Log(mesh.vertices[len - 1]);
    	var triangles_work = new int[(len - 1) * 3];
    	for (int i = 0; i < len - 1; i++)
    	{
            ///Debug.Log("last cicle " + (len - 2));
            //Debug.Log("i " + i);
            if (i != len - 2)
            {
    		triangles_work[i * 3] = i;
    		triangles_work[(i * 3) + 1] = i + 1;
    		triangles_work[(i * 3) + 2] = len - 1; //6 if hexagone, 5 if pentagone
    												//for the center verticles
            }
            else
            {
            triangles_work[i * 3] = i;
            triangles_work[(i * 3) + 1] = 0;
            triangles_work[(i * 3) + 2] = len - 1;              
            }
            //Debug.Log(mesh.triangles[i * 3] + " " + mesh.triangles[(i * 3) + 1] + " " + mesh.triangles[(i * 3) + 2]);
    	}
        Debug.Log("=======");
        foreach(var a in vertices_work)
        {
            Debug.Log(a);
        }
        Debug.Log("________");
        foreach(var a in triangles_work)
        {
            Debug.Log(a);
        }
       /* mesh.vertices = new Vector3[] {
            new Vector3(0,0,0),
            new Vector3(0,1,0),
            new Vector3(0,0,1)
        };
        mesh.triangles = new int[] {
            0,1,2
        };*/

        mesh.vertices = vertices_work;
        mesh.triangles = triangles_work;

        mesh.RecalculateNormals();
    }
}
