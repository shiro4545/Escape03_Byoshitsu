using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll_Tap : TapCollider
{
    //人形No
    public int DollNo;

    //薬オブジェクト配列
    public GameObject[] Medicines;

    //Judgeクラス
    public Doll_Judge JudgeClass;

    //立ち人形
    public GameObject Stand;
    //倒れた人形
    public GameObject Down;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        //人形に薬をのせる
        if (!JudgeClass.isClear)
        {
            //タップ前の状態(0:何もなし,1~4:薬No)
            int Status = int.Parse(JudgeClass.Status.Substring(DollNo - 1, 1));
            
            //既に薬がある場合
            if(Status != 0)
            {
                Medicines[Status - 1].SetActive(false);
                ItemManager.Instance.GetItem("Medicine" + Status);
            }

            //薬を持っているか
            int Select = 0;
            if (ItemManager.Instance.SelectItem == "Medicine1")
                Select = 1;
            if (ItemManager.Instance.SelectItem == "Medicine2")
                Select = 2;
            if (ItemManager.Instance.SelectItem == "Medicine3")
                Select = 3;
            if (ItemManager.Instance.SelectItem == "Medicine4")
                Select = 4;

            //薬を持っている場合
            if(Select != 0)
            {
                AudioManager.Instance.SoundSE("PutMedicine2");
                ItemManager.Instance.UseItem();

                Medicines[Select - 1].SetActive(true);
            }

            //答え合わせ
            JudgeClass.Judge(DollNo, Select);
        }

        //人形を倒す
        if (!JudgeClass.Curtain31.isClear)
            return;

        AudioManager.Instance.SoundSE("DownDoll");
        Stand.SetActive(false);
        Down.SetActive(true);

        if (DollNo == 1)
            SaveLoadSystem.Instance.gameData.isDown1 = true;
        else if(DollNo == 2)
            SaveLoadSystem.Instance.gameData.isDown2 = true;
        else if (DollNo == 3)
            SaveLoadSystem.Instance.gameData.isDown3 = true;
        else if (DollNo == 4)
            SaveLoadSystem.Instance.gameData.isDown4 = true;

        SaveLoadSystem.Instance.Save();

    }
}
