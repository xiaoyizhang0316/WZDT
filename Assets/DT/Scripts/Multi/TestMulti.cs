<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TestMulti : NetworkBehaviour
{
    [ClientRpc]
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TestMulti : NetworkBehaviour
{
    [ClientRpc]
>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
    public void RpcTest()
    {
        Debug.Log("dasds213124eadfadfdsf34321e2");
        GetComponent<Image>().color = Color.red;
        transform.position += Vector3.one;
<<<<<<< HEAD
    }

=======
    }

>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            RpcTest();
        }
<<<<<<< HEAD
    }

}
=======
    }

}
>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
