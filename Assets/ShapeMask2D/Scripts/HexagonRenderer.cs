using System;
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

    private Vector3 _screenPoint;
    private Vector3 _offset;

    [HideInInspector]
    Vector3[] vertices = new Vector3[6];


    // Start is called before the first frame update
    void Start()
    {
        _meshCollider = GetComponent<MeshCollider>();
        InputManager.OnDoubleClickHandler += OnDoubleClickHandler;
    }

    private void OnDoubleClickHandler()
    {
        Vector3 relativePoint = transform.InverseTransformPoint(InputManager.Instance.mousePosition3D);
        
        Boolean isInside = ContainsPoint(vertices, relativePoint);
        
        if (isInside)
        {
            ChangeColor();
        }
    }

    private void ChangeColor()
    {
        _material.color = UnityEngine.Random.ColorHSV();
    }

    public Boolean ContainsPoint(Vector3[] polyPoints, Vector3 p ) 
    {
        var j = polyPoints.Length - 1;
        var inside = false;
        for (int i = 0; i < polyPoints.Length; j = i++)
        {
            var pi = polyPoints[i];
            var pj = polyPoints[j];
            if (((pi.y <= p.y && p.y < pj.y) || (pj.y <= p.y && p.y < pi.y)) &&
                (p.x < (pj.x - pi.x) * (p.y - pi.y) / (pj.y - pi.y) + pi.x))
                inside = !inside;
        }
        return inside;
    }

    bool IsInCollider(Collider other, Vector3 point)
    {

        if (other.ClosestPoint(point) == point)
        {
            return true;
        }

        return false;
    }

    public void DrawHexagon()
    {
        Mesh mesh = new Mesh();

       
        vertices[0] = new Vector3(-_width / 2, Mathf.Sqrt(_width * 1.5f));
        vertices[1] = new Vector3(_width / 2, Mathf.Sqrt(_width * 1.5f));
        vertices[2] = new Vector3(_width, 0);
        vertices[3] = new Vector3(_width / 2, -Mathf.Sqrt(_width * 1.5f));
        vertices[4] = new Vector3(-_width / 2, -Mathf.Sqrt(_width * 1.5f));
        vertices[5] = new Vector3(-_width, 0);

        mesh.vertices = vertices;


        Vector2[] uvs = new Vector2[vertices.Length];

        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
        }
        mesh.uv = uvs;

        mesh.triangles = new int[] { 0, 4, 5, 0, 3, 4, 0, 1, 3, 1, 2, 3 };
        GetComponent<MeshRenderer>().material = _material;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.Optimize();

        GetComponent<MeshFilter>().mesh = mesh;
        _meshCollider.sharedMesh = null;
        _meshCollider.sharedMesh = mesh;
    }

    private void OnDisable()
    {
        InputManager.OnDoubleClickHandler -= OnDoubleClickHandler;
    }
}
