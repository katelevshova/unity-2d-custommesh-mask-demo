using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]   // for working  OnPointerClick
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class HexagonRenderer : MonoBehaviour
{
    [SerializeField]
    private float _width = 2; // width of the square in which we draw the hexagon
    [SerializeField]
    private Material _material;
    private MeshCollider _meshCollider;


    // Start is called before the first frame update
    void Start()
    {
        _meshCollider = GetComponent<MeshCollider>();
        DrawHexagon();
    }

   
    public void DrawHexagon()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[6];
        vertices[0] = new Vector3(-_width / 2, Mathf.Sqrt(_width*1.5f));
        vertices[1] = new Vector3(_width / 2, Mathf.Sqrt(_width * 1.5f));
        vertices[2] = new Vector3(_width, 0);
        vertices[3] = new Vector3(_width / 2, -Mathf.Sqrt(_width * 1.5f) );
        vertices[4] = new Vector3(-_width / 2, -Mathf.Sqrt(_width * 1.5f) );
        vertices[5] = new Vector3(-_width, 0);

        mesh.vertices = vertices;

        mesh.triangles = new int[] { 0, 4, 5, 0, 3, 4, 0, 1, 3, 1, 2, 3 };
        GetComponent<MeshRenderer>().material = _material;

        GetComponent<MeshFilter>().mesh = mesh;
        _meshCollider.sharedMesh = mesh;
    }
}
