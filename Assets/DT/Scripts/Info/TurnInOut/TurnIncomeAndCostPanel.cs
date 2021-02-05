using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnIncomeAndCostPanel : MonoBehaviour
{
    public Button in_btn;
    public Button out_btn;
    // Start is called before the first frame update
    void Start()
    {
        in_btn.onClick.AddListener(()=>
        {
            StageGoal.My.HideTurnIncomeAndCost();
            in_btn.gameObject.SetActive(false);
            out_btn.gameObject.SetActive(true);
        });
        out_btn.onClick.AddListener(()=>
        {
            
            StageGoal.My.ShowTurnIncomeAndCost();
            out_btn.gameObject.SetActive(false);
            in_btn.gameObject.SetActive(true);
        });
    }
}
