using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
[DisallowMultipleComponent]
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasRenderer))]
[AddComponentMenu("UI/UISkewImage (UI)", 99)]
[ExecuteInEditMode]
public class UISkewImage : Image {
    [SerializeField]
    private Vector3 offsetLeftButtom = Vector3.zero;
    public Vector3 OffsetLeftButtom{
        get{return offsetLeftButtom;}
        set{
            offsetLeftButtom = value;
            SetAllDirty();
        }
    }
    [SerializeField]
    private Vector3 offsetRightButtom = Vector3.zero;
    public Vector3 OffsetRightButtom{
        get{return offsetRightButtom;}
        set{
            offsetRightButtom = value;
            SetAllDirty();
        }
    }
    [SerializeField]
    private Vector3 offsetLeftTop = Vector3.zero;
    public Vector3 OffsetLeftTop{
        get{return offsetLeftTop;}
        set{
            offsetLeftTop = value;
            SetAllDirty();
        }
    }
    [SerializeField]
    private Vector3 offsetRightTop = Vector3.zero;
    public Vector3 OffsetRightTop{
        get{return offsetRightTop;}
        set{
            offsetRightTop = value;
            SetAllDirty();
        }
    }
    Vector3 GetOffsetVector(int i){
        if (i == 0){
            return offsetLeftButtom;
        }else if (i == 1){
            return offsetLeftTop;
        }else if (i == 2){
            return offsetRightTop;
        }else {
            return offsetRightButtom;
        }
    }
    protected override void OnPopulateMesh(VertexHelper toFill){
        base.OnPopulateMesh(toFill);
        int count = toFill.currentVertCount;
        for(int i=0;i<count;i++){
            UIVertex vertex = new UIVertex();
            toFill.PopulateUIVertex(ref vertex, i);
            // Debug.Log(i.ToString() + ": " + oldPosition[i].ToString());
            vertex.position += GetOffsetVector(i);
            toFill.SetUIVertex(vertex, i);
        }
    }
}