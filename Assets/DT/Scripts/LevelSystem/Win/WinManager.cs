using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinManager : MonoSingleton<WinManager>
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

    public Button box_0Button;
    public Button box_1Button;
    public Button box_2Button;

    public Image star_0;
    public Image star_1;
    public Image star_2;

    public GameObject boxs;

    public List<Sprite> mulist;
    public List<Sprite> tielist;
    public List<Sprite> jinlist;

    public Button returnMap;

    public GameObject equipPrb;
    public GameObject workerPrb;

    public Transform pos_equip;
    public Transform pos_worker;

    public Button confirm;
    public void InitWin()
    {
        boxs.SetActive(false);
        isStar_0 = BaseLevelController.My.starOneStatus;
        isStar_1 = BaseLevelController.My.starTwoStatus;
        isStar_2 = BaseLevelController.My.starThreeStatus;
        winPanel.SetActive(true);
        if (isStar_0)
        {
            if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "|" + 1, 0) == 0)
            {
                box_0.sprite = mulist[0];
                star_0.sprite = lightStar;
                //   boxs.SetActive(true);
                box_0.color = Color.white;
                box_0Button.interactable = true;
            }
            else
            {
                box_0.sprite = mulist[mulist.Count - 1];
                star_0.sprite = lightStar;
                //  boxs.SetActive(true);
                box_0Button.interactable = false;
                box_0.color = Color.gray;
            }
        }
        else
        {
            box_0.sprite = mulist[0];
            star_0.sprite = blackStar;
            box_0Button.interactable = false;
            box_0.color = Color.gray;
        }
        if (isStar_1)
        {
            if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "|" + 2, 0) == 0)
            {
                box_1.sprite = tielist[0];
                star_1.sprite = lightStar;
                box_1Button.interactable = true;
                box_1.color = Color.white;
            }
            else
            {
                box_1.sprite = tielist[tielist.Count - 1];

                star_1.sprite = lightStar;
                box_1Button.interactable = false;
                box_1.color = Color.gray;
            }
        }
        else
        {
            box_1.sprite = tielist[0];
            star_1.sprite = blackStar;
            box_1Button.interactable = false;
            box_1.color = Color.gray;


        }
        if (isStar_2)
        {
            if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "|" + 3, 0) == 0)
            {
                box_2.sprite = jinlist[0];
                star_2.sprite = lightStar;
                box_2Button.interactable = true;
                box_2.color = Color.white;
            }
            else
            {
                box_2.sprite = jinlist[jinlist.Count - 1];
                star_2.sprite = lightStar;
                box_2Button.interactable = false;
                box_2.color = Color.gray;
            }
        }
        else
        {
            box_2.sprite = jinlist[0];
            star_2.sprite = blackStar;
            box_2Button.interactable = false;
            box_2.color = Color.gray;
        }
        CheckNext();
    }

    public IEnumerator playerEffect(Image target, List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            target.sprite = list[i];
            yield return new WaitForSeconds(0.03f);
        }
        boxs.SetActive(true);
    }

    public void OnGUI()
    {
        //if (GUILayout.Button("胜利"))
        //{
        //    InitWin();
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        winPanel.SetActive(false);
        confirm.onClick.AddListener(() =>
        {
            boxs.SetActive(false);
        });
        box_0Button.onClick.AddListener(() =>
        {

            box_0Button.interactable = false;
         List<GearData> gearDatas =    StageGoal.My.GetStarGearData(1);
         List<WorkerData> workDatas =      StageGoal.My.GetStarWorkerData(1);
         for (int i = 0; i < pos_equip.transform.childCount; i++)
         {
             Destroy( pos_equip.transform.GetChild(0).gameObject);
         }
         for (int i = 0; i < gearDatas.Count; i++)
         {
          GameObject equip =   Instantiate(equipPrb, pos_equip);
          equip.GetComponent<Image>().sprite = Resources.Load<Sprite>(gearDatas[i].SpritePath);
          equip.transform.GetChild(0).GetComponent<Text>().text = gearDatas[i].equipName;
         }
         for (int i = 0; i < pos_worker.transform.childCount; i++)
         {
             Destroy( pos_worker.transform.GetChild(0).gameObject);
         }
         for (int i = 0; i < workDatas.Count; i++)
         {
             GameObject worker =   Instantiate(workerPrb, pos_worker);
             worker.GetComponent<Image>().sprite = Resources.Load<Sprite>(workDatas[i].SpritePath);
             worker.transform.GetChild(0).GetComponent<Text>().text = workDatas[i].workerName;
         }
       
            CheckNext();
            StartCoroutine(playerEffect(box_0, mulist));
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "|" + 1, 1);
        });
        box_1Button.onClick.AddListener(() =>
        {
            box_1Button.interactable = false;
            List<GearData> gearDatas =    StageGoal.My.GetStarGearData(2);
            List<WorkerData> workDatas =  StageGoal.My.GetStarWorkerData(2);
            for (int i = 0; i < pos_equip.transform.childCount; i++)
            {
                Destroy( pos_equip.transform.GetChild(0).gameObject);
            }
            for (int i = 0; i < gearDatas.Count; i++)
            {
                GameObject equip =   Instantiate(equipPrb, pos_equip);
                equip.GetComponent<Image>().sprite = Resources.Load<Sprite>(gearDatas[i].SpritePath);
                equip.transform.GetChild(0).GetComponent<Text>().text = gearDatas[i].equipName;
            }
            for (int i = 0; i < pos_worker.transform.childCount; i++)
            {
                Destroy( pos_worker.transform.GetChild(0).gameObject);
            }
            for (int i = 0; i < workDatas.Count; i++)
            {
                GameObject worker =   Instantiate(workerPrb, pos_worker);
                worker.GetComponent<Image>().sprite = Resources.Load<Sprite>(workDatas[i].SpritePath);
                worker.transform.GetChild(0).GetComponent<Text>().text = workDatas[i].workerName;
            }
            boxs.SetActive(true);
            CheckNext();
            StartCoroutine(playerEffect(box_1, tielist));
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "|" + 2, 1);
        });
        box_2Button.onClick.AddListener(() =>
        {
            box_2Button.interactable = false;
            List<GearData> gearDatas =     StageGoal.My.GetStarGearData(3);
            List<WorkerData> workDatas =   StageGoal.My.GetStarWorkerData(3);
            for (int i = 0; i < pos_equip.transform.childCount; i++)
            {
                Destroy( pos_equip.transform.GetChild(0).gameObject);
            }
            for (int i = 0; i < gearDatas.Count; i++)
            {
                GameObject equip =   Instantiate(equipPrb, pos_equip);
                equip.GetComponent<Image>().sprite = Resources.Load<Sprite>(gearDatas[i].SpritePath);
                equip.transform.GetChild(0).GetComponent<Text>().text = gearDatas[i].equipName;
            }
            for (int i = 0; i < pos_worker.transform.childCount; i++)
            {
                Destroy( pos_worker.transform.GetChild(0).gameObject);
            }
            for (int i = 0; i < workDatas.Count; i++)
            {
                GameObject worker =   Instantiate(workerPrb, pos_worker);
                worker.GetComponent<Image>().sprite = Resources.Load<Sprite>(workDatas[i].SpritePath);
                worker.transform.GetChild(0).GetComponent<Text>().text = workDatas[i].workerName;
            }
            boxs.SetActive(true);
            CheckNext();
            StartCoroutine(playerEffect(box_2, jinlist));
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "|" + 3, 1);
        });
        returnMap.onClick.AddListener(() =>
        {
            PlayerData.My.Reset();
            SceneManager.LoadScene("Map");
        });
    }

    public void CheckNext()
    {
        returnMap.interactable = true;

        if (box_0Button.interactable || box_1Button.interactable || box_2Button.interactable)
        {
            returnMap.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}