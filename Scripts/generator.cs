﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Globalization;
using UnityEditor;

public class generator : MonoBehaviour
{
	Mesh mesh;

	public string srcFile = "map12";
    TextAsset mapFile = null;

    public Vector3[] gen_vertices;
	public int[,] gen_faces;
	public Vector3[] center;
	public int[,] liaison;

    public GameObject parent;
	public Material materialExpr;
	public Material materialPenta;

	public GameObject[] child;

    public GameObject parentModel;

    void Awake()
    {
        mapFile = Resources.Load<TextAsset>(srcFile);

        parent = Instantiate(parentModel) as GameObject;
    	parseTbo();

    	for (int i = 0; i < gen_faces.Length / 6; i++)
    	{
    		var tmp = generate(i);
            tmp.transform.SetParent(parent.transform);
    	}

        LinkCell();
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
		reader = new StringReader(mapFile.text);

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

    GameObject generate(int id)
    {
    	var face = new int[6]; 
    	for (int i = 0; i < 6; i++)
        {
    		face[i] = gen_faces[id, i];
        }
    	var ctr = center[id];
        child[id] = new GameObject("cell_" + id);
    	child[id].AddComponent<MeshRenderer>();
    	child[id].AddComponent<MeshFilter>();
        child[id].AddComponent<Cell>();
        child[id].AddComponent<MeshCollider>();
    	var mesh = child[id].GetComponent<MeshFilter>().mesh;
    	int len;


    	if (face[5] == -1)
        {
            child[id].GetComponent<MeshRenderer>().material = materialPenta;
            child[id].GetComponent<Cell>().isPentagon = true;
            len = 6;
        }
    	else
        {
            child[id].GetComponent<MeshRenderer>().material = materialExpr;
    		len = 7;
        }
 		var vertices_work = new Vector3[len];
        Vector2[] uv = new Vector2[mesh.vertices.Length];
    	for (int i = 0; i < len - 1; i++)
    	{
    		vertices_work[i] = gen_vertices[face[i]];
    	}
    	vertices_work[len - 1] = center[id];
    	var triangles_work = new int[(len - 1) * 3];
    	for (int i = 0; i < len - 1; i++)
    	{
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
    	}
        mesh.vertices = vertices_work;
        mesh.triangles = triangles_work;

        
        if (len == 7)
        {
            mesh.uv = new Vector2[]
        {
            new Vector2(0.5f, 0f),
            new Vector2( 1f, 75f / 300f),
            new Vector2( 1f, 226f / 300f),
            new Vector2(0.5f,1f),
            new Vector2( 0f, 226f / 300f),
            new Vector2( 0f, 75f / 300f),
            new Vector2(0.5f, 0.5f)
        }  ;
        }
        else
        {
            mesh.uv = new Vector2[]
        {
            new Vector2(0f,0.5f),
            new Vector2( 0f, 75f / 300f),
            new Vector2( 0f, 223f / 300f),
            new Vector2(1f,0.5f),
            new Vector2( 0f, 223f / 300f),
            new Vector2(0.5f, 0.5f)
        };
        }

        mesh.RecalculateNormals();
        child[id].GetComponent<MeshCollider>().sharedMesh = mesh;
        child[id].GetComponent<Cell>().center = vertices_work[len - 1];
        return child[id];
    }

    void LinkCell()
    {
        for (int i = 0; i < child.Length; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                int id = liaison[i, j];
                if (id != -1)
                {
                    child[i].GetComponent<Cell>().AddNeighbour(
                        child[id].GetComponent<Cell>()
                    );
                }
            }
        }
    }
}
