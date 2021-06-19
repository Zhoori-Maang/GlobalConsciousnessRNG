using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Based on research about Psychokinesis by Helmut Schmidt (parapsychology)
// Code by Mohammad Heidari
// Thu, June 17, 2021
public class CircleRNG : MonoBehaviour
{
    public int RenderesCount = 50;
    public float RefreshRate = 0.005f;
    public float CircleRadius = 5.0f;

    private float _randomCore;
    private List<Material> _renderesMaterial = new List<Material>();
    private List<GameObject> _renderesGameobject = new List<GameObject>();
    private int _rendererIndex = 0;

    private LineRenderer Line;

    void Start()
    {
        //Creation of circle objects _renderesGameobject & _renderesMaterial
        for (int i = 0; i < RenderesCount; i++)
        {
            GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            temp.transform.position = new Vector3(Mathf.Sin((Mathf.PI * 2 / RenderesCount) * i) * CircleRadius, Mathf.Cos((Mathf.PI * 2 / RenderesCount) * i) * CircleRadius, 0);
            temp.transform.localScale = Vector3.one * (Mathf.PI * 2 * CircleRadius / RenderesCount); // scale based on radius and counts
            temp.GetComponent<Renderer>().material.color = Color.gray;
            temp.transform.SetParent(this.transform);
            temp.name = i.ToString();
            _renderesMaterial.Add(temp.GetComponent<Renderer>().material);
            _renderesGameobject.Add(temp);
        }
        _rendererIndex = Random.Range(0, _renderesMaterial.Count);
        _renderesMaterial[_rendererIndex].color = Color.magenta;


        // LineRenderer creation
        Line = gameObject.AddComponent<LineRenderer>();
        Line.startWidth = 0; Line.endWidth = (Mathf.PI * 2 * CircleRadius / RenderesCount);
    }


    void ChangeColor(int direction)
    {
        foreach (var item in _renderesMaterial)
        {
            item.color = Color.gray;
        }

        if (direction > 0)
        {
            _rendererIndex++;
            if (_rendererIndex > _renderesMaterial.Count - 1) _rendererIndex = 0;
        }
        else
        {
            _rendererIndex--;
            if (_rendererIndex < 0) _rendererIndex = _renderesMaterial.Count - 1;
        }

        _renderesMaterial[_rendererIndex].color = Color.magenta;

        Line.SetPosition(0, Vector3.zero);
        Line.SetPosition(1, _renderesGameobject[_rendererIndex].transform.position);

        Debug.DrawLine(Vector3.zero, _renderesGameobject[_rendererIndex].transform.position);
        //Debug.Log(_rendererIndex);
    }

    float t = 0;

    // 0.02
    void FixedUpdate()
    {

        t += Time.fixedDeltaTime;
        if (t > RefreshRate)
        {
            _randomCore = Random.value;  // Debug.Log(_randomCore);
            // change color based on normalized to 1 and -1 random input, if bigger than 0 clockwise else anticlockwise
            ChangeColor(_randomCore > 0.5f ? 1 : -1);
            t = 0;
        }

    }
}
