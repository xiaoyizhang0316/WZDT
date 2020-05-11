using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTradeLine : MonoBehaviour
{

    /// <summary>
    /// 发起者位置
    /// </summary>
    public Transform startTarget;

    /// <summary>
    /// 承受者位置
    /// </summary>
    public Vector3 Target;

    public GameObject lineGo;

    public Material material;


    public List<Vector3> pointList = new List<Vector3>();


    public void InitPos(Transform startTarget)
    {
        this.startTarget = startTarget;
        //lineGo = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        Vector3 rightPosition = (startTarget.gameObject.transform.position + Target) / 2;
        Vector3 rightRotation = Target - startTarget.transform.position;
        float HalfLength = Vector3.Distance(startTarget.transform.position, Target) / 2;
        float LThickness = 0.1f;//线的粗细

        //创建圆柱体
        lineGo.SetActive(true);
        lineGo.gameObject.transform.parent = transform;
        lineGo.transform.position = rightPosition;
        lineGo.transform.rotation = Quaternion.FromToRotation(Vector3.up, rightRotation);
        lineGo.transform.localScale = new Vector3(LThickness, HalfLength, LThickness);
        lineGo.GetComponent<MeshRenderer>().material = material;
    }



    // Start is called before the first frame update
    void Start()
    {

        //lineGo.SetActive(false);
        //    GetComponent<LineRenderer>().

    }

    // Update is called once per frame
    void Update()
    {
        //points.Add(Target);

        if (UIManager.My.isSetTrade)
        {
            List<Vector3> points = new List<Vector3>();
            points.Add(startTarget.localPosition);
            //int vertexCount = 30;//采样点数量
            pointList.Clear();
            if (startTarget != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hit = Physics.RaycastAll(ray);
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].transform.tag.Equals("MapLand"))
                    {
                        Target = hit[i].transform.localPosition + new Vector3(0f,1f,0f);
                        //float x = startTarget.localPosition.x * 0.5f + hit[i].transform.localPosition.x * (0.5f);
                        //float y = 5f;
                        ////float y = per * 5f;
                        //float z = startTarget.localPosition.z * 0.5f + hit[i].transform.localPosition.z * (0.5f);
                        //Vector3 point3 = new Vector3(x, y, z);
                        //for (float ratio = 0; ratio <= 1; ratio += 1.0f / vertexCount)
                        //{
                        //    Vector3 tangentLineVertex1 = Vector3.Lerp(startTarget.localPosition, point3, ratio);
                        //    Vector3 tangentLineVectex2 = Vector3.Lerp(point3, hit[i].transform.localPosition, ratio);
                        //    Vector3 bezierPoint = Vector3.Lerp(tangentLineVertex1, tangentLineVectex2, ratio);
                        //    pointList.Add(bezierPoint);
                        //}
                        //GetComponent<LineRenderer>().positionCount = pointList.Count;
                        //GetComponent<LineRenderer>().SetPositions(pointList.ToArray());
                        break;
                    }
                }
                //float x = start.x * 0.5f + Target.x * (0.5f);
                //float y = start.y * 0.5f + Target.y * (0.5f);
                ////float y = per * 5f;
                //float z = start.z * 0.5f + Target.z * (0.5f);
                Vector3 rightPosition = (startTarget.gameObject.transform.position + Target) / 2;
                Vector3 rightRotation = Target - startTarget.transform.position;
                float HalfLength = Vector3.Distance(startTarget.transform.position, Target) / 2;
                float LThickness = 0.1f;//线的粗细

                //创建圆柱体
                //lineGo = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                lineGo.gameObject.transform.parent = transform;
                lineGo.transform.position = rightPosition;
                lineGo.transform.rotation = Quaternion.FromToRotation(Vector3.up, rightRotation);
                lineGo.transform.localScale = new Vector3(LThickness, HalfLength, LThickness);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            //Destroy(lineGo.gameObject);
            lineGo.SetActive(false);
            gameObject.SetActive(false);
            UIManager.My.isSetTrade = false;
        }

    }
}
