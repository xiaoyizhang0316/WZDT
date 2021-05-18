using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class RemoveScript : MonoBehaviour
{
    // Start is called before the first frame update
    Regex searchTerm = new Regex(@"\((.*\))");
    void Start()
    {
        Remove();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Remove()
    {
        string scriptName = GetComponent<BaseSkill>().ToString();
        Debug.Log(scriptName);
        var value = (searchTerm.Match(scriptName).Groups.Count > 0) ?
            searchTerm.Match(scriptName).Groups[0].Value : string.Empty;
        Debug.Log(value.Substring(1,value.Length-2));
        scriptName = value.Substring(1, value.Length - 2);

        try
        {
            Destroy(transform.GetComponent(scriptName));
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
}
