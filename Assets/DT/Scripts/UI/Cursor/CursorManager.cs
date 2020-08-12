using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;

public class CursorManager : MonoSingleton<CursorManager>
{

    public void SetCursorType(CursorType type)
    {
        switch (type)
        {
            case CursorType.Role:
                break;
            case CursorType.NPC:
                break;
            case CursorType.Equip:
                break;
            case CursorType.Consumer:
                break;
            default:
                Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
                break;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
