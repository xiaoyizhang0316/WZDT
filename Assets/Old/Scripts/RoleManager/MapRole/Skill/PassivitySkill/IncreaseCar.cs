using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncreaseCar : BasePassivitySkill
{
    /// <summary>
    /// 加速过
    /// </summary>
    public bool isAdd;

    /// <summary>
    /// BUFF ID
    /// </summary>
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

    private void OnTriggerStay(Collider other)
    {
        if (isOpen && GetComponentInParent<RolePosSign>().isRelease)
        {
            if (other.tag.Equals("Car"))
            {
                //print("小车加速");
                other.transform.GetComponent<CarMove>().AddBuff(buffId, 1.4f);
            }
            else if (other.tag.Equals("VirtualCar"))
            {
                other.transform.GetComponent<VirtualCar>().AddBuff(buffId, 1.4f);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (GetComponentInParent<RolePosSign>().isRelease)
        {
            if (other.tag.Equals("Car"))
            {
                other.transform.GetComponent<CarMove>().RemoveBuff(buffId);
            }
            else if (other.tag.Equals("VirtualCar"))
            {
                other.transform.GetComponent<VirtualCar>().RemoveBuff(buffId);
            }
        }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen && GetComponentInParent<RolePosSign>().isRelease)
        {
            float scaleNum = GetComponentInParent<BaseMapRole>().baseRoleData.capacity * 0.8f;
            transform.parent.localScale = new Vector3(scaleNum, 1f, scaleNum);
        }
    }
}
