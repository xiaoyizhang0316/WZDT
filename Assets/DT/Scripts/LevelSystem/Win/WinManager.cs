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

    public Button retry;

    public Button review;

    public Text star1Con;
    public Text star2Con;
    public Text star3Con;

    public Text UnlockText;
    private int stars = 0;
    private string[] starArr;
    PlayerReplay tempReplay;
    List<PlayerEquip> playerEquips;

    void AddEquipGear(List<GearData> gearDatas)
    {
        foreach (var g in gearDatas)
        {
            playerEquips.Add(new PlayerEquip(NetworkMgr.My.playerID, 0, g.ID, 1));
        }
    }


    void AddEquipWorker(List<WorkerData> workerDatas)
    {
        foreach (var w in workerDatas)
        {
            playerEquips.Add(new PlayerEquip(NetworkMgr.My.playerID, 1, w.ID, 1));
        }
    }

    void UploadGetEquip()
    {
        if (playerEquips.Count > 0)
        //NetworkMgr.My.AddEquipList(playerEquips);
        {
            foreach (var e in playerEquips)
            {
                NetworkMgr.My.AddEquip(e.equipID, e.equipType, e.count);
            }
        }
    }


    public void InitWin()
    {
        star1Con.text = BaseLevelController.My.starOneCondition;
        star2Con.text = BaseLevelController.My.starTwoCondition;
        star3Con.text = BaseLevelController.My.starThreeCondition;
        if (playerEquips == null)
        {
            playerEquips = new List<PlayerEquip>();
        }
        else
        {
            playerEquips.Clear();
        }

        stars = 0;
        retry.onClick.AddListener(() => {
            PlayerData.My.Reset();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
        boxs.SetActive(false);
        isStar_0 = BaseLevelController.My.starOneStatus;
        isStar_1 = BaseLevelController.My.starTwoStatus;
        isStar_2 = BaseLevelController.My.starThreeStatus;
        winPanel.SetActive(true);
        LevelProgress tempProgress = NetworkMgr.My.GetLevelProgressByIndex(int.Parse(SceneManager.GetActiveScene().name.Split('_')[1]));
        if (isStar_0)
        {
            stars += 1;
            starArr[0] = "1";
            //没领过
            if (tempProgress == null || tempProgress.rewardStatus[0] == '0')
            {
                box_0.sprite = mulist[0];
                star_0.sprite = lightStar;
                //   boxs.SetActive(true);
                box_0.color = Color.white;
                box_0Button.interactable = true;
                AddEquipGear(StageGoal.My.GetStarGearData(1));
                AddEquipWorker(StageGoal.My.GetStarWorkerData(1));

            }
            //领过
            else
            {
                box_0.sprite = mulist[mulist.Count - 1];
                star_0.sprite = lightStar;
                //  boxs.SetActive(true);
                box_0Button.interactable = false;
                box_0.color = Color.gray;
            }
            //if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "|" + 1, 0) == 0)
            //{
            //    box_0.sprite = mulist[0];
            //    star_0.sprite = lightStar;
            //    //   boxs.SetActive(true);
            //    box_0.color = Color.white;
            //    box_0Button.interactable = true;
            //}

        }
        else
        {
            starArr[0] = "0";
            //没领过
            if (tempProgress == null || tempProgress.rewardStatus[0] == '0')
            {
                box_0.sprite = mulist[0];
                star_0.sprite = blackStar;
                //   boxs.SetActive(true);
                box_0.color = Color.gray;
                box_0Button.interactable = false;
            }
            //领过
            else
            {
                box_0.sprite = mulist[mulist.Count - 1];
                star_0.sprite = blackStar;
                //  boxs.SetActive(true);
                box_0Button.interactable = false;
                box_0.color = Color.gray;
            }
        }
        if (isStar_1)
        {
            stars += 1;
            starArr[1] = "1";
            //没领过
            if (tempProgress == null || tempProgress.rewardStatus[1] == '0')
            {
                box_1.sprite = tielist[0];
                star_1.sprite = lightStar;
                //   boxs.SetActive(true);
                box_1.color = Color.white;
                box_1Button.interactable = true;
                AddEquipGear(StageGoal.My.GetStarGearData(2));
                AddEquipWorker(StageGoal.My.GetStarWorkerData(2));

            }
            //领过
            else
            {
                box_1.sprite = tielist[mulist.Count - 1];
                star_1.sprite = lightStar;
                //  boxs.SetActive(true);
                box_1Button.interactable = false;
                box_1.color = Color.gray;
            }
            //if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "|" + 2, 0) == 0)
            //{
            //    box_1.sprite = tielist[0];
            //    star_1.sprite = lightStar;
            //    box_1Button.interactable = true;
            //    box_1.color = Color.white;
            //}
            //else
            //{
            //    box_1.sprite = tielist[tielist.Count - 1];

            //    star_1.sprite = lightStar;
            //    box_1Button.interactable = false;
            //    box_1.color = Color.gray;
            //}
        }
        else
        {

            //box_1.sprite = tielist[0];
            //star_1.sprite = blackStar;
            //box_1Button.interactable = false;
            //box_1.color = Color.gray;
            //
            //没领过
            starArr[1] = "0";
            if (tempProgress == null || tempProgress.rewardStatus[1] == '0')
            {
                box_1.sprite = tielist[0];
                star_1.sprite = blackStar;
                //   boxs.SetActive(true);
                box_1.color = Color.gray;
                box_1Button.interactable = false;
            }
            //领过
            else
            {
                box_1.sprite = tielist[mulist.Count - 1];
                star_1.sprite = blackStar;
                //  boxs.SetActive(true);
                box_1Button.interactable = false;
                box_1.color = Color.gray;
            }

        }
        if (isStar_2)
        {
            stars += 1;
            starArr[2] = "1";
            //没领过
            if (tempProgress == null || tempProgress.rewardStatus[2] == '0')
            {
                box_2.sprite = jinlist[0];
                star_2.sprite = lightStar;
                //   boxs.SetActive(true);
                box_2.color = Color.white;
                box_2Button.interactable = true;
                AddEquipGear(StageGoal.My.GetStarGearData(3));
                AddEquipWorker(StageGoal.My.GetStarWorkerData(3));

            }
            //领过
            else
            {
                box_2.sprite = jinlist[mulist.Count - 1];
                star_2.sprite = lightStar;
                //  boxs.SetActive(true);
                box_2Button.interactable = false;
                box_2.color = Color.gray;
            }
            //if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "|" + 3, 0) == 0)
            //{
            //    box_2.sprite = jinlist[0];
            //    star_2.sprite = lightStar;
            //    box_2Button.interactable = true;
            //    box_2.color = Color.white;
            //}
            //else
            //{
            //    box_2.sprite = jinlist[jinlist.Count - 1];
            //    star_2.sprite = lightStar;
            //    box_2Button.interactable = false;
            //    box_2.color = Color.gray;
            //}
        }
        else
        {
            starArr[2] = "0";
            //box_2.sprite = jinlist[0];
            //star_2.sprite = blackStar;
            //box_2Button.interactable = false;
            //box_2.color = Color.gray;
            if (tempProgress == null || tempProgress.rewardStatus[2] == '0')
            {
                box_2.sprite = jinlist[0];
                star_2.sprite = blackStar;
                //   boxs.SetActive(true);
                box_2.color = Color.gray;
                box_2Button.interactable = false;
            }
            //领过
            else
            {
                box_2.sprite = jinlist[mulist.Count - 1];
                star_2.sprite = blackStar;
                //  boxs.SetActive(true);
                box_2Button.interactable = false;
                box_2.color = Color.gray;
            }
        }
        if (NetworkMgr.My.isUsingHttp)
        {
            tempReplay = new PlayerReplay(true);
            //NetworkMgr.My.AddReplayData(tempReplay);
            UploadReplayData();
            UploadGetEquip();
            CommitProgress();

        }
        CheckNext();
    }

    private void UploadReplayData()
    {
        NetworkMgr.My.AddReplayData(tempReplay);
    }

    private bool isPlay = false;

    public IEnumerator playerEffect(Image target, List<Sprite> list)
    {
        isPlay = true;
        for (int i = 0; i < list.Count; i++)
        {
            target.sprite = list[i];
            yield return new WaitForSeconds(0.03f);
            if (i == list.Count - 1)
            {
                isPlay = false;
                boxs.SetActive(true);
                CheckNext();
            }
        }
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
        starArr = new string[] { "0", "0", "0" };
        confirm.onClick.AddListener(() =>
        {
            boxs.SetActive(false);
        });
        box_0Button.onClick.AddListener(() =>
        {
            if (isPlay)
                return;
            box_0Button.interactable = false;
            List<GearData> gearDatas = StageGoal.My.GetStarGearData(1);
            List<WorkerData> workDatas = StageGoal.My.GetStarWorkerData(1);
            for (int i = 0; i < pos_equip.transform.childCount; i++)
            {
                Destroy(pos_equip.transform.GetChild(0).gameObject);
            }
            for (int i = 0; i < gearDatas.Count; i++)
            {
                GameObject equip = Instantiate(equipPrb, pos_equip);
                equip.GetComponent<Image>().sprite = Resources.Load<Sprite>(gearDatas[i].SpritePath);
                equip.transform.GetChild(0).GetComponent<Text>().text = gearDatas[i].equipName;
                if (NetworkMgr.My.isUsingHttp)
                {
                    NetworkMgr.My.AddEquip(gearDatas[i].ID, 0, 1);
                }
            }
            for (int i = 0; i < pos_worker.transform.childCount; i++)
            {
                Destroy(pos_worker.transform.GetChild(0).gameObject);
            }
            for (int i = 0; i < workDatas.Count; i++)
            {
                GameObject worker = Instantiate(workerPrb, pos_worker);
                worker.GetComponent<Image>().sprite = Resources.Load<Sprite>(workDatas[i].SpritePath);
                worker.transform.GetChild(0).GetComponent<Text>().text = workDatas[i].workerName;
                if (NetworkMgr.My.isUsingHttp)
                {
                    NetworkMgr.My.AddEquip(workDatas[i].ID, 1, 1);
                }
            }

            //CheckNext();
            AudioManager.My.PlaySelectType(GameEnum.AudioClipType.BoxOpen);
            StartCoroutine(playerEffect(box_0, mulist));
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "|" + 1, 1);
        });
        box_1Button.onClick.AddListener(() =>
        {
            if (isPlay)
                return;
            box_1Button.interactable = false;
            List<GearData> gearDatas = StageGoal.My.GetStarGearData(2);
            List<WorkerData> workDatas = StageGoal.My.GetStarWorkerData(2);
            for (int i = 0; i < pos_equip.transform.childCount; i++)
            {
                Destroy(pos_equip.transform.GetChild(0).gameObject);
            }
            for (int i = 0; i < gearDatas.Count; i++)
            {
                GameObject equip = Instantiate(equipPrb, pos_equip);
                equip.GetComponent<Image>().sprite = Resources.Load<Sprite>(gearDatas[i].SpritePath);
                equip.transform.GetChild(0).GetComponent<Text>().text = gearDatas[i].equipName;
                if (NetworkMgr.My.isUsingHttp)
                {
                    NetworkMgr.My.AddEquip(gearDatas[i].ID, 0, 1);
                }
            }
            for (int i = 0; i < pos_worker.transform.childCount; i++)
            {
                Destroy(pos_worker.transform.GetChild(0).gameObject);
            }
            for (int i = 0; i < workDatas.Count; i++)
            {
                GameObject worker = Instantiate(workerPrb, pos_worker);
                worker.GetComponent<Image>().sprite = Resources.Load<Sprite>(workDatas[i].SpritePath);
                worker.transform.GetChild(0).GetComponent<Text>().text = workDatas[i].workerName;
                if (NetworkMgr.My.isUsingHttp)
                {
                    NetworkMgr.My.AddEquip(workDatas[i].ID, 1, 1);
                }
            }
            //boxs.SetActive(true);
            //CheckNext();
            AudioManager.My.PlaySelectType(GameEnum.AudioClipType.BoxOpen);
            StartCoroutine(playerEffect(box_1, tielist));
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "|" + 2, 1);
        });
        box_2Button.onClick.AddListener(() =>
        {
            if (isPlay)
                return;
            box_2Button.interactable = false;
            List<GearData> gearDatas = StageGoal.My.GetStarGearData(3);
            List<WorkerData> workDatas = StageGoal.My.GetStarWorkerData(3);
            for (int i = 0; i < pos_equip.transform.childCount; i++)
            {
                Destroy(pos_equip.transform.GetChild(0).gameObject);
            }
            for (int i = 0; i < gearDatas.Count; i++)
            {
                GameObject equip = Instantiate(equipPrb, pos_equip);
                equip.GetComponent<Image>().sprite = Resources.Load<Sprite>(gearDatas[i].SpritePath);
                equip.transform.GetChild(0).GetComponent<Text>().text = gearDatas[i].equipName;
                if (NetworkMgr.My.isUsingHttp)
                {
                    NetworkMgr.My.AddEquip(gearDatas[i].ID, 0, 1);
                }
            }
            for (int i = 0; i < pos_worker.transform.childCount; i++)
            {
                Destroy(pos_worker.transform.GetChild(0).gameObject);
            }
            for (int i = 0; i < workDatas.Count; i++)
            {
                GameObject worker = Instantiate(workerPrb, pos_worker);
                worker.GetComponent<Image>().sprite = Resources.Load<Sprite>(workDatas[i].SpritePath);
                worker.transform.GetChild(0).GetComponent<Text>().text = workDatas[i].workerName;
                if (NetworkMgr.My.isUsingHttp)
                {
                    NetworkMgr.My.AddEquip(workDatas[i].ID, 1, 1);
                }
            }
            //boxs.SetActive(true);

            AudioManager.My.PlaySelectType(GameEnum.AudioClipType.BoxOpen);
            StartCoroutine(playerEffect(box_2, jinlist));
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "|" + 3, 1);
        });
        returnMap.onClick.AddListener(() =>
        {
            //if (NetworkMgr.My.isUsingHttp)
            //{
            //    CommitProgress();
            //}
            //else
            //{
                PlayerData.My.Reset();
                SceneManager.LoadScene("Map");
            //}
        });
        review.onClick.AddListener(() =>
        {
            NewCanvasUI.My.Panel_Review.SetActive(true);
            ReviewPanel.My.Init(StageGoal.My.playerOperations);
        });
    }

    private void CommitProgress()
    {
        NetworkMgr.My.UpdateLevelProgress(NetworkMgr.My.currentLevel, stars, starArr[0] + starArr[1] + starArr[2],
                    starArr[0] + starArr[1] + starArr[2], 0, () =>
                    {
                        //PlayerData.My.Reset();
                        //SceneManager.LoadScene("Map");
                    });
    }

    public void CheckNext()
    {
        returnMap.interactable = true;
        UnlockText .gameObject.SetActive(false);
        if (box_0Button.interactable || box_1Button.interactable || box_2Button.interactable)
        {
            UnlockText .gameObject.SetActive(true);
            returnMap.interactable = false;
        }
        else
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}