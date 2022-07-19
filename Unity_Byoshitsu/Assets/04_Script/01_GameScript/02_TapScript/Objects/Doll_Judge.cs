using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll_Judge : MonoBehaviour
{
    //クリア有無
    public bool isClear;

    //薬ステータス(0:何もなし,1~4:薬No)
    public string Status = "0000";

    //冷蔵後のドア(閉)
    public GameObject CloseDoor;
    //冷蔵後のドア(開)
    public GameObject OpenDoor;

    //コップ
    public GameObject Glass;
    //ハサミ
    public GameObject Sciccers;

    //Puzzle31クラス
    public Curtain_Judge Curtain31;


    //答え合わせ
    public void Judge(int DollNo, int NewStatus)
    {
        //ステータス更新
        if (DollNo == 1)
            Status = NewStatus + Status.Substring(1);
        if (DollNo == 2)
            Status = Status.Substring(0, 1) + NewStatus + Status.Substring(2);
        if (DollNo == 3)
            Status = Status.Substring(0, 2) + NewStatus + Status.Substring(3);
        if (DollNo == 4)
            Status = Status.Substring(0, 3) +NewStatus;


        //答え合わせ
        if(Status == "3412")
        {
            BlockPanel.Instance.ShowBlock();
            AudioManager.Instance.SoundSE("Clear");

            //演出
            Invoke(nameof(AfterClear1), 1.5f);

            isClear = true;
            SaveLoadSystem.Instance.gameData.isClearDoll = true;
        }

        //セーブデータ
        SaveLoadSystem.Instance.gameData.DollStatus = Status;
        SaveLoadSystem.Instance.Save();
    }

    //
    private void AfterClear1()
    {
        CameraManager.Instance.ChangeCameraPosition("Shelf4");
        Invoke(nameof(AfterClear2), 1.5f);
    }
    //
    private void AfterClear2()
    {
        AudioManager.Instance.SoundSE("OpenReizoko");
        CloseDoor.SetActive(false);
        OpenDoor.SetActive(true);
        Glass.SetActive(true);
        
        Invoke(nameof(AfterClear3), 1.5f);
    }
    //
    private void AfterClear3()
    {
        CameraManager.Instance.ChangeCameraPosition("TV4");
        BlockPanel.Instance.HideBlock();
    }

}
