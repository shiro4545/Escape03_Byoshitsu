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

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (JudgeClass.isClear)
            return;

        //タップ前の状態(0:何もなし,1~4:薬No)
        int Status = int.Parse(JudgeClass.Status.Substring(DollNo - 1, 1));
        //Debug.Log(Status);

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
}
