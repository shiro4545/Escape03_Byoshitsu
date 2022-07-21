using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear_Judge : MonoBehaviour
{
    //ステータス(白くま7回)
    public string Status1 = "0000000";
    //ステータス(左3右6)
    public string Status2 = "000000000";
    //クリア有無
    public bool isClear = false;

    //クマのオブジェクト (0:座り,1:倒れる)
    public GameObject[] Bear1;
    public GameObject[] Bear2;
    public GameObject[] Bear3;

    //天秤の皿
    public GameObject Tenbin;

    //テーブルの板
    public GameObject Plate2;
    //テーブルのパズル
    public GameObject Puzzle8;

    //答え合わせ
    public void Judge(int No)
    {
        if (!isClear)
            //シロクマ７回の時
        {
            //ステータス更新
            Status1 = Status1.Substring(1, 6) + No;

            //答え合わせ
            if (Status1 == "3333333")
                Clear1();
        }
        else
            //左3右6の時
        {
            if(SaveLoadSystem.Instance.gameData.isDown1 &&
                SaveLoadSystem.Instance.gameData.isDown2 &&
                SaveLoadSystem.Instance.gameData.isDown3 &&
                SaveLoadSystem.Instance.gameData.isDown4)
            {
                //人形を全て倒している場合
                //ステータス更新
                Status2 = Status2.Substring(1, 8) + No;

                //答え合わせ
                if (Status2 == "111222222")
                    Clear2();
            }
        }
       
    }

    //白くま７回のクリア処理
    private void Clear1()
    {
        AudioManager.Instance.SoundSE("Clear");
        BlockPanel.Instance.ShowBlock();

        Invoke(nameof(AfterClear1), 1.5f);

        //セーブデータ
        isClear = true;
        SaveLoadSystem.Instance.gameData.isClearBear = true;
        SaveLoadSystem.Instance.Save();
    }
    //演出
    private void AfterClear1()
    {
        AudioManager.Instance.SoundSE("DownDOll2");
        Bear3[0].SetActive(false);
        Bear3[1].SetActive(true);
        Tenbin.SetActive(true);
        BlockPanel.Instance.HideBlock();
    }


    //左3右6のクリア処理
    private void Clear2()
    {
        AudioManager.Instance.SoundSE("Clear");
        BlockPanel.Instance.ShowBlock();

        Invoke(nameof(AfterClear2), 1.5f);

        //セーブデータ
        SaveLoadSystem.Instance.gameData.isClearBear2 = true;
        SaveLoadSystem.Instance.Save();
    }
    //演出
    private void AfterClear2()
    {
        CameraManager.Instance.ChangeCameraPosition("Table");
        Invoke(nameof(AfterClear3), 1.5f);
    }
    //
    private void AfterClear3()
    {
        AudioManager.Instance.SoundSE("OpenPlate");
        Plate2.SetActive(false);
        Puzzle8.SetActive(true);

        Bear1[0].SetActive(false);
        Bear2[0].SetActive(false);
        Bear1[1].SetActive(true);
        Bear2[1].SetActive(true);

        Invoke(nameof(AfterClear4), 1.5f);
    }
    //
    private void AfterClear4()
    {
        CameraManager.Instance.ChangeCameraPosition("Bed2");
        BlockPanel.Instance.HideBlock();
    }
}
