using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Judge : MonoBehaviour
{
    //ステータス
    public string Status = "000000";

    //各ボタンの色オブジェクト
    public GameObject[] Left1;
    public GameObject[] Left2;
    public GameObject[] Center1;
    public GameObject[] Center2;
    public GameObject[] Right1;
    public GameObject[] Right2;

    //閉まり扉
    public GameObject CloseDoor;
    //開き扉
    public GameObject OpenDoor;

    //薬１〜３
    public GameObject Medicine1;
    public GameObject Medicine2;
    public GameObject Medicine3;

    //答え合わせ
    public void Judge(int Index)
    {
        //オブジェクトの特定
        GameObject[] Objects = Left1;
        if (Index == 1)
            Objects = Left2;
        else if (Index == 2)
            Objects = Center1;
        else if (Index == 3)
            Objects = Center2;
        else if (Index == 4)
            Objects = Right1;
        else if (Index == 5)
            Objects = Right2;

        //そのボタンのステータス取得
        int OldStatus = int.Parse(Status.Substring(Index, 1));
        int NewStatus = OldStatus + 1;
        if (NewStatus >= Objects.Length)
            NewStatus = 0;

        //オブジェクト切替
        Objects[OldStatus].SetActive(false);
        Objects[NewStatus].SetActive(true);

        //ステータス更新
        if (Index == 0)
            Status = NewStatus + Status.Substring(Index +1);
        else if(Index == 6)
            Status = Status.Substring(0,Index) + NewStatus;
        else
            Status = Status.Substring(0, Index) + NewStatus + Status.Substring(Index + 1);

        //答え合わせ
        if (Status != "012304")
            return;

        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("Clear");

        //演出
        Invoke(nameof(AfterClear1), 1.5f);

        //セーブデータ
        SaveLoadSystem.Instance.gameData.isClearBox = true;
        SaveLoadSystem.Instance.Save();
    }

    //演出
    private void AfterClear1()
    {
        Medicine1.SetActive(true);
        Medicine2.SetActive(true);
        Medicine3.SetActive(true);

        AudioManager.Instance.SoundSE("OpenCover");
        CloseDoor.SetActive(false);
        OpenDoor.SetActive(true);

        BlockPanel.Instance.HideBlock();
    }
}
