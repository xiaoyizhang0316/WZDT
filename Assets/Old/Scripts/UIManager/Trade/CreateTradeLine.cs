using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;

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

    public void CheckBank(BaseMapRole target)
    {
        if (NewCanvasUI.My.startRole.baseRoleData.baseRoleData.roleType == RoleType.Bank)
        {
            string str = "利率: " + NewCanvasUI.My.startRole.GetComponent<BankLoan>().CalculateInterest(target) * 100f + "%";
            FloatWindow.My.Init(str, NewCanvasUI.My.startRole.gameObject.transform);
        }
        else if (target.baseRoleData.baseRoleData.roleType == RoleType.Bank)
        {
            string str = "利率: " + target.GetComponent<BankLoan>().CalculateInterest(NewCanvasUI.My.startRole) * 100f + "%";
            FloatWindow.My.Init(str,target.gameObject.transform);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (NewCanvasUI.My.isSetTrade)
        {
            if (startTarget != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out RaycastHit hit);
                if(hit.transform != null)
                {
                    FloatWindow.My.Hide();
                    lineGo.GetComponent<MeshRenderer>().material.color = Color.white;
                    Target = hit.point;
                    if (hit.transform.tag.Equals("MapRole"))
                    {
                        targetRole = hit.transform.GetComponentInParent<BaseMapRole>();
                        Target = hit.transform.GetComponentInParent<BaseMapRole>().tradePoint.position;
                        if (hit.transform.GetComponentInParent<BaseMapRole>().baseRoleData.ID != NewCanvasUI.My.startRole.baseRoleData.ID)
                        {
                            if (NewCanvasUI.My.startRole.baseRoleData.baseRoleData.roleSkillType == GameEnum.RoleSkillType.Product && targetRole.baseRoleData.baseRoleData.roleSkillType == GameEnum.RoleSkillType.Product)
                            {
                                if (!TradeConstraint.My.CheckTradeConstraint(NewCanvasUI.My.startRole.baseRoleData.baseRoleData.roleType, targetRole.baseRoleData.baseRoleData.roleType))
                                {
                                    lineGo.GetComponent<MeshRenderer>().material.color = Color.red;
                                }
                                else
                                {
                                    lineGo.GetComponent<MeshRenderer>().material.color = Color.white;
                                }
                            }
                            else
                            {
                                lineGo.GetComponent<MeshRenderer>().material.color = Color.white;
                            }
                            CheckBank(targetRole);
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
        }
        if (Input.GetMouseButtonUp(0))
        {
            lineGo.SetActive(false);
            gameObject.SetActive(false);
            NewCanvasUI.My.isSetTrade = false;
            if (NewCanvasUI.My.isChange)
            {
                NewCanvasUI.My.isChange = false;
                NewCanvasUI.My.HideAllTradeButton();
            }
        }
    }
}
