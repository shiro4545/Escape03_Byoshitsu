using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Name_Judge : MonoBehaviour
{
    //ステータス
    public string Status = "0000";

    //各ボタンの文字配列
    public GameObject[] Btn1;
    public GameObject[] Btn2;
    public GameObject[] Btn3;
    public GameObject[] Btn4;

    //閉まり扉
    public GameObject CloseDoor;
    //開き扉
    public GameObject OpenDoor;
    //Block4
    public GameObject Block;

    //PCパスワードクラス
    public PC_Judge PCClass;

    //テーブルの左板
    public GameObject TablePlate;
    //Puzzle31
    public GameObject Puzzle31;

    //答え合わせ
    public void Judge(int Index)
    {
        //ボタン識別
        GameObject[] BtnArray = Btn1;
        if (Index == 1)
            BtnArray = Btn2;
        else if (Index == 2)
            BtnArray = Btn3;
        else if (Index == 3)
            BtnArray = Btn4;

        //ステータス更新
        int OldStatus = int.Parse(Status.Substring(Index, 1));
        int NewStatus = OldStatus + 1;
        if (NewStatus >= BtnArray.Length)
            NewStatus = 0;

        if (Index == 0)
            Status = NewStatus + Status.Substring(1);
        else if (Index == 3)
            Status = Status.Substring(0, 3) + NewStatus;
        else
            Status = Status.Substring(0, Index) + NewStatus + Status.Substring(Index + 1);

        //オブジェクト切替
        BtnArray[OldStatus].SetActive(false);
        BtnArray[NewStatus].SetActive(true);

        //答え合わせ
        if (Status != "2464" || !PCClass.isClear)
            return;

        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("Clear");

        //演出
        Invoke(nameof(AfterClear1), 1.5f);

        //セーブデータ
        SaveLoadSystem.Instance.gameData.isClearName = true;
        SaveLoadSystem.Instance.Save();
    }

    //演出
    private void AfterClear1()
    {
        AudioManager.Instance.SoundSE("Slide");

        CloseDoor.SetActive(false);
        OpenDoor.SetActive(true);
        Block.SetActive(true);

        Invoke(nameof(AfterClear2), 1.5f);
    }
    //パズル左を開ける
    private void AfterClear2()
    {
        CameraManager.Instance.ChangeCameraPosition("Table");
        Invoke(nameof(AfterClear3), 1.5f);
    }
    //
    private void AfterClear3()
    {
        AudioManager.Instance.SoundSE("OpenPlate");

        Puzzle31.SetActive(true);
        TablePlate.SetActive(false);
        Invoke(nameof(AfterClear4), 1.5f);
    }
    //
    private void AfterClear4()
    {
        CameraManager.Instance.ChangeCameraPosition("Name2");
        BlockPanel.Instance.HideBlock();
    }

    //視点変えた時にボタンリセット
    public void ResetBtn()
    {
        Status = "0000";
        for (int i = 0; i < Btn1.Length; i++)
        {
            Btn1[i].SetActive(false);
            Btn2[i].SetActive(false);
            Btn3[i].SetActive(false);
            Btn4[i].SetActive(false);
        }
        Btn1[0].SetActive(true);
        Btn2[0].SetActive(true);
        Btn3[0].SetActive(true);
        Btn4[0].SetActive(true);
    }
}