﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectStageController : MonoBehaviour
{
    public ChallengeCntController challengeCntController;
    public StagePageController stagePageController;
    public SettingPopup settingPopup;
    [SerializeField] Animator mapAnim;
    [SerializeField] RectTransform l_Btn;
    [SerializeField] RectTransform r_Btn;
    [SerializeField] RectTransform enter_Btn;
    [SerializeField] RectTransform bgPanel;
    [SerializeField] RectTransform backBtn;

    int chapterIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        EventSet();
        SoundManager.Instance.PlayBGM(BGMEnum.StageSelect);
        BtnActiveSet();
        backBtn.gameObject.SetActive(false);

        if (GameManager.Ins.selectStageId != -1)
        {
            ChapterInit();
        }
    }

    void EventSet()
    {
        stagePageController.startEvent += GameStartOn;
    }

    void ChapterInit() 
    {
        chapterIndex = GameManager.Ins.selectStageId / 5;
        BtnActiveSet();
        enter_Btn.gameObject.SetActive(true);
        IdleAnimPlay();
        BGPosSet();
    }

    public void SettingBtnClickOn()
    {
        SoundManager.Instance.PlaySe(SeEnum.Touch);
        settingPopup.gameObject.SetActive(true);
    }

    void GameStartOn()
    {
        GameManager.Ins.StageStartOn(stagePageController.stageData.id);        
    }

    public void GameExitOn()
    {
        SoundManager.Instance.PlaySe(SeEnum.Touch);
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            mapAnim.Play("C1_to_C2_R");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            mapAnim.Play("C2_to_C1_R");
        }
    }

    public void NextBtnClick(bool nextOn)
    {
        chapterIndex = nextOn ? chapterIndex + 1 : chapterIndex - 1;

        AllDeActive();

        if (chapterIndex == 0)
        {
            string animName = nextOn == false ? "C2_to_C1_RR" : "C1_R_idle";

            mapAnim.Play(animName);
            
        }
        else if(chapterIndex == 1)
        {
            string animName = nextOn == false ? "C3_to_C2_RR" : "C1_to_C2_RR";
            mapAnim.Play(animName);
            
        }
        else if (chapterIndex == 2)
        {
            string animName = nextOn == false ? "C4_to_C3_RR" : "C2_to_C3_RR";
            mapAnim.Play(animName);
            
        }

        BGPosSet();

        StartCoroutine(NextBtnClickOn());
        
    }

    void BGPosSet()
    {
        int xValue = chapterIndex * -1080;
        bgPanel.anchoredPosition3D = new Vector2(xValue, 0);
    }


    IEnumerator NextBtnClickOn()
    {
        yield return new WaitForSeconds(2f);
        BtnActiveSet();
        enter_Btn.gameObject.SetActive(true);
        IdleAnimPlay();
    }

    void BtnActiveSet()
    {
        if (chapterIndex == 0)
        {
            l_Btn.gameObject.SetActive(false);
            r_Btn.gameObject.SetActive(true);
        }
        else if (chapterIndex == 2)
        {
            l_Btn.gameObject.SetActive(true);
            r_Btn.gameObject.SetActive(false);
        }
        else
        {
            l_Btn.gameObject.SetActive(true);
            r_Btn.gameObject.SetActive(true);
        }
    }

    void AllDeActive()
    {
        l_Btn.gameObject.SetActive(false);
        r_Btn.gameObject.SetActive(false);
        enter_Btn.gameObject.SetActive(false);
        backBtn.gameObject.SetActive(false);
    }

    public void EnterBtnClick()
    {
        AllDeActive();

        if (chapterIndex == 0)
        {
            mapAnim.Play("C1_toIngame_r");
        }
        else if (chapterIndex == 1)
        {
            mapAnim.Play("C2_toIngame_r");
        }
        else if (chapterIndex == 2)
        {
            mapAnim.Play("C3_toIngame_r");
        }

        StartCoroutine(EntroBtnClickOn());
    }

    IEnumerator EntroBtnClickOn()
    {
        stagePageController.ChapterLoadReadyOn(chapterIndex);

        yield return new WaitForSeconds(1.5f);
        backBtn.gameObject.SetActive(true);

        stagePageController.ChapterSelectOn(chapterIndex);
    }


    public void BackBtnClick()
    {
        stagePageController.selectStageOn = false;

        AllDeActive();

        if (chapterIndex == 0)
        {
            mapAnim.Play("C1_toIngame_r_back");
        }
        else if (chapterIndex == 1)
        {
            mapAnim.Play("C2_toIngame_r_back");
        }
        else if (chapterIndex == 2)
        {
            mapAnim.Play("C3_toIngame_r_back");
        }

        StartCoroutine(BackBtnClickOn());
    }
    IEnumerator BackBtnClickOn()
    {
        yield return new WaitForSeconds(1.5f);

        BtnActiveSet();
        enter_Btn.gameObject.SetActive(true);

        IdleAnimPlay();
    }

    void IdleAnimPlay()
    {
        switch (chapterIndex)
        {
            case 0: mapAnim.Play("C1_R_idle"); break;
            case 1: mapAnim.Play("C2_R_idle"); break;
            case 2: mapAnim.Play("C3_R_idle"); break;
        }
    }


}
