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

    public BaseMapRole targetRole;

    public List<Vector3> pointList = new List<Vector3>();

    /// <summary>
    /// 初始化位置
    /// </summary>
    /// <param name="startTarget"></param>
    public void InitPos(Transform startTarget)
    {
        this.startTarget = startTarget;
        lineGo.SetActive(true);
        lineGo.GetComponent<MeshRenderer>().material = material;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.My.isSetTrade)
        {
            if (startTarget != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hit = Physics.RaycastAll(ray);
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].transform.tag.Equals("MapLand"))
                    {
                        Target = hit[i].transform.position + new Vector3(0f,1f,0f);
                        lineGo.GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                    if (hit[i].transform.tag.Equals("Role"))
                    {
                        targetRole = hit[i].transform.GetComponentInParent<BaseMapRole>();
                    }
                }
                Vector3 rightPosition = (startTarget.gameObject.transform.position + Target) / 2;
                Vector3 rightRotation = Target - startTarget.transform.position;
                float HalfLength = Vector3.Distance(startTarget.transform.position, Target) / 2;
                float LThickness = 0.1f;//线的粗细
                lineGo.gameObject.transform.parent = transform;
                lineGo.transform.position = rightPosition;
                lineGo.transform.rotation = Quaternion.FromToRotation(Vector3.up, rightRotation);
                lineGo.transform.localScale = new Vector3(LThickness, HalfLength, LThickness);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            lineGo.SetActive(false);
            gameObject.SetActive(false);
            UIManager.My.isSetTrade = false;
        }
    }
}
