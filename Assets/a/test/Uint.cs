using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Uint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
     
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int SingleNumber(int[] nums) {

        for (int i = 1; i <nums.Length; i++)
        {
            nums[0] ^= nums[i];
        }

        return nums[0];
    }
}
