using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonRenderer : MonoBehaviour
{
    [SerializeField]
    private float _width = 2; // width of the square in which we draw the hexagon
    [SerializeField]
    private Material _material;


    // Start is called before the first frame update
    void Start()
    {
        DrawHexagon();
    }

    // Update is called once per frame
    void Update()
    {
        
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

        /**
        vertices[0] = new Vector3(-_width / 2, _height);
        vertices[1] = new Vector3(_width / 2, _height);
        vertices[2] = new Vector3(_width, 0);
        vertices[3] = new Vector3(_width / 2, -_height);
        vertices[4] = new Vector3(-_width / 2, -_height);
        vertices[5] = new Vector3(-_width, 0);
        */

        mesh.vertices = vertices;

        mesh.triangles = new int[] { 0, 4, 5, 0, 3, 4, 0, 1, 3, 1, 2, 3 };
        GetComponent<MeshRenderer>().material = _material;

        GetComponent<MeshFilter>().mesh = mesh;
    }
}
