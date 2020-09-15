using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class addTest : MonoBehaviour
{

    public List<int> nums;

    public int extraCandies;
    // Start is called before the first frame update
    void Start()
    {
        candies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNums()
    {
        for (int i = 1; i <nums.Count; i++)
        {
            nums[i] = nums[i] + nums[i - 1];

        }
    }

    public void candies ()
    {
        List<bool> can = new List<bool>();
        
        for (int i = 0; i < nums.Count; i++)
        {
            if (nums.Max() == nums[i])
            {
                can.Add(true);
                Debug.Log(can[i]+"i: "+i);
                
                continue;
            }

            if (nums[i] + extraCandies > nums.Max())
            {
                can.Add(true);
            }

            else
            {
                can.Add(false);
            }
            Debug.Log(can[i]+"i: "+i);
        }
    }
}
