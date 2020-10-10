using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePanel : MonoBehaviour
{
    public Button reviewButton;
    public Button retryButton;
    public Button returnButton;
    // Start is called before the first frame update
    void Start()
    {
        reviewButton.onClick.AddListener(()=> {
            NewCanvasUI.My.Panel_Review.SetActive(true);
            ReviewPanel.My.Init(StageGoal.My.playerOperations);
        });

        retryButton.onClick.AddListener(()=> {
            PlayerData.My.Reset();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });

        returnButton.onClick.AddListener(()=> {
            PlayerData.My.Reset();
            SceneManager.LoadScene("Map");
        });
    }
}
