using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    public GameObject winPanel;

    public bool isStar_0;
    public bool isStar_1;
    public bool isStar_2;

    public Sprite blackbox_0;
    public Sprite blackbox_1;
    public Sprite blackbox_2;

    public Sprite lightbox_0;
    public Sprite lightbox_1;
    public Sprite lightbox_2;

    public Sprite openBox_0;
    public Sprite openBox_1;
    public Sprite openBox_2;

    public Sprite blackStar;
    public Sprite lightStar;
    
    public Image box_0;
    public Image box_1;
    public Image box_2;

    public Image star_0;
    public Image star_1;
    public Image star_2;


    public void InitWin()
    {
        winPanel.SetActive(true);
        if (isStar_0)
        {
            if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "|" + 1, 0) == 0)
            {
                box_0.sprite = lightbox_0;
                star_0.sprite = lightStar;
            }

            else
            {
                box_0.sprite = openBox_0; 
                star_0.sprite = lightStar;

            }
        }
        else
        {
            box_0.sprite = blackbox_0;
            star_0.sprite = blackStar;

        }
        if (isStar_1)
        {
            if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "|" +2, 0) == 0)
            {
                box_1.sprite = lightbox_1;
                star_1.sprite = lightStar;
            }

            else
            {
                box_1.sprite = openBox_1; 
                star_1.sprite = lightStar;

            }
        }
        else
        {
            box_1.sprite = blackbox_1;
            star_1.sprite = blackStar;

        }
        if (isStar_2)
        {
            if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "|" +3, 0) == 0)
            {
                box_2.sprite = lightbox_2;
                star_2.sprite = lightStar;

            }

            else
            {
                box_2.sprite = openBox_2; 
                star_2.sprite = lightStar;

            }
        }
        else
        {
            box_2.sprite = blackbox_2;
            star_2.sprite = blackStar;

        }
    }


    public void OnGUI()
    {
        if (GUILayout.Button("胜利"))
        {
            InitWin();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        winPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
