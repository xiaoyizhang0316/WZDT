using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncreaseConsumer : BasePassivitySkill
{

    /// <summary>
    /// 加速过
    /// </summary>
    public bool isAdd;

    public int buffId;

    // Start is called before the first frame update
    public override void ReleaseSkills(BaseMapRole baseMapRole, Action onComplete = null)
    {
        
    }

    public override void SwitchButton(BaseMapRole baseMapRole,Button skillButton)
    {
      skillButton.onClick.AddListener(() =>
        {
            if (!isOpen)
            {
                isOpen = true;
                ShowImage(baseMapRole);
            }
            else
            {
                isOpen = false;
                ShowImage(baseMapRole);
            }
        });
    }

    public override void ShowImage(BaseMapRole baseMapRole)
    {
        if (skillImage != null)
        {
            if (isOpen)
            {
                skillImage.fillAmount = 1;
            }
            else
            {
                skillImage.fillAmount = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (isOpen)
        //{
        //    if (other.tag.Equals("Consumer"))
        //    {
        //        if (other.transform.GetComponent<ConsumeSign>().maxIndex != -1)
        //        {
        //            print("消费者加速");
        //            other.GetComponent<ConsumeSign>().Stop();
        //            other.GetComponent<ConsumeSign>().currentSpeed *= 5.2f;
        //            other.GetComponent<ConsumeSign>().MoveToShop();
        //            isAdd = true;
        //        }
        //    }
        //}
    }

    /// <summary>
    /// 消费者离开范围时
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerExit(Collider other)
    {
        if (GetComponentInParent<RolePosSign>().isRelease)
        {
            if (other.tag.Equals("Consumer"))
            {
                //print("消费者减速");
                other.transform.GetComponent<ConsumeSign>().RemoveBuff(buffId);
            }
        }
        
    }

    /// <summary>
    /// 消费者进入范围时
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerStay(Collider other)
    {
        if (GetComponentInParent<RolePosSign>().isRelease && isOpen)
        {
            if (other.tag.Equals("Consumer"))
            {
                //print("消费者加速");
                //other.transform.GetComponent<ConsumeSign>().AddBuff(buffId, 1.4f * GetComponent<BaseMapRole>().baseRoleData.capacity / 100f);
                other.transform.GetComponent<ConsumeSign>().AddBuff(buffId, 1.4f);
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen && GetComponentInParent<RolePosSign>().isRelease)
        {
            float scaleNum = GetComponentInParent<BaseMapRole>().baseRoleData.capacity * 0.7f;
            transform.parent.localScale = new Vector3(scaleNum, 1f, scaleNum);
        }
    }
}
