using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;
using IOIntensiveFramework.MonoSingleton;

public class LineManager : MonoSingleton<LineManager>
{

    public int segments = 250;
    public bool loop;
    public bool usePoints;

    public GameObject DrawTradeLine(Vector3 start, Vector3 end,int ID)
    {
        var splinePoints = new List<Vector3>();
        splinePoints.Add(start);
        splinePoints.Add(end);
        if (usePoints)
        {
            var dotLine = new VectorLine(ID.ToString(), new List<Vector3>(segments + 1), 2.0f, LineType.Points);
            dotLine.MakeSpline(splinePoints.ToArray(), segments, loop);
            dotLine.Draw();
        }
        else
        {
            var spline = new VectorLine(ID.ToString(), new List<Vector3>(segments + 1), 2.0f, LineType.Continuous);
            spline.MakeSpline(splinePoints.ToArray(), segments, loop);
            spline.Draw3D();
            spline.rectTransform.SetParent(transform);
        }
        return transform.Find(ID.ToString()).gameObject;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
