using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getnormal : MonoBehaviour
{

    float speed = 100.0f;
    public Vector3[] normalsvertices;
    public Vector3[] vertices;
    public Vector4[] tangents;
    // Start is called before the first frame update
    void Start()
    {
        normalsvertices = GetComponent<MeshFilter>().mesh.normals;
        vertices = GetComponent<MeshFilter>().mesh.vertices;
        tangents = GetComponent<MeshFilter>().mesh.tangents;
    }

    // Update is called once per frame
    void Update()
    {/*
        // obtain the normals from the Mesh
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] normals = mesh.normals;

        // edit the normals in an external array
        Quaternion rotation = Quaternion.AngleAxis(Time.deltaTime * speed, Vector3.up);
        for (int i = 0; i < normals.Length; i++)
            normals[i] = rotation * normals[i];

        // assign the array of normals to the mesh
        mesh.normals = normals;*/
    }
}
