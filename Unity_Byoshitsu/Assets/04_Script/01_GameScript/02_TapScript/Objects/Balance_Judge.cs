using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance_Judge : MonoBehaviour
{
    //薬の置かれた状態 (左皿と右皿) (数字は薬No)
    public string Status = "00";

    //天秤のオブジェクト配列
    public GameObject[] Tenbins;
    //薬のオブジェクト配列
    public GameObject[] Medicine0L;
    public GameObject[] Medicine1L;
    public GameObject[] Medicine2L;

    public GameObject[] Medicine0R;
    public GameObject[] Medicine1R;
    public GameObject[] Medicine2R;

    //左皿の薬オブジェクト配列(1次:天秤,2次:薬)
    public GameObject[][] MedicinesLeft;
    //右皿の薬オブジェクト配列(1次:天秤,2次:薬)
    public GameObject[][] MedicinesRight;

    //セット後の天秤No(0:並行,1:左傾き,2右傾き)
    private int TenbinNo;
    //セット前の天秤No(0:並行,1:左傾き,2右傾き)
    private int PrevTenbinNo;


    //セットする薬No(0:何もなし,1~4:薬No)
    private int MedicineNo;
    //反対の皿の薬No
    private int OtherMedicineNo; 
    //皿の左右
    private string LR;
    

    //薬の重さ
    private Dictionary<string, int> Weight = new Dictionary<string, int>
    {
        {
            //何もなし
            "0",0
        },
        {
            //薬1
            "1",10
        },
        {
            //薬2
            "2",40
        },
        {
            //薬3
            "3",20
        },
        {
            //薬4
            "4",30
        },

    };

    private void Start()
    {
        //２次配列の準備
        MedicinesLeft = new GameObject[][] { Medicine0L, Medicine1L, Medicine2L };
        MedicinesRight = new GameObject[][] { Medicine0R, Medicine1R, Medicine2R };
    }

    //判定クラス
    public void Judge(int _PrevTenbinNo,string _LR)
    {
        LR = _LR;

        //これからセットする薬Noを取得
        MedicineNo = 0;
        if (ItemManager.Instance.SelectItem == "Medicine1")
            MedicineNo = 1;
        if (ItemManager.Instance.SelectItem == "Medicine2")
            MedicineNo = 2;
        if (ItemManager.Instance.SelectItem == "Medicine3")
            MedicineNo = 3;
        if (ItemManager.Instance.SelectItem == "Medicine4")
            MedicineNo = 4;

        //今置いてある薬Noを取得
        int PrevMedicineNo = int.Parse(LR == "L" ? Status.Substring(0, 1) : Status.Substring(1));
        OtherMedicineNo = int.Parse(LR == "R" ? Status.Substring(0, 1) : Status.Substring(1));

        //薬のステータス変更
        if (LR == "L")
            Status = MedicineNo + Status.Substring(1);
        else
            Status = Status.Substring(0, 1) + MedicineNo;


        //セット前の天秤
        PrevTenbinNo = _PrevTenbinNo;

        //セットした後の天秤の傾きを求める
        int WeightLeft = Weight[Status.Substring(0, 1)];
        int WeightRight = Weight[Status.Substring(1)];

        TenbinNo = 0;
        if (WeightLeft > WeightRight)
            TenbinNo = 1;
        if (WeightLeft < WeightRight)
            TenbinNo = 2;

        //既に薬があれば取得する
        if (PrevMedicineNo != 0)
        {
            //薬を非表示に
            if (LR == "L")
                MedicinesLeft[PrevTenbinNo][PrevMedicineNo-1].SetActive(false);
            else
                MedicinesRight[PrevTenbinNo][PrevMedicineNo-1].SetActive(false);

            //薬をアイテム取得
            ItemManager.Instance.GetItem("Medicine" + PrevMedicineNo);
        }

        //薬を持っていればセットする
        if (MedicineNo != 0)
        {
            AudioManager.Instance.SoundSE("PutMedicine");
            ItemManager.Instance.UseItem();

            //薬を表示
            if (LR == "L")
            {
                MedicinesLeft[PrevTenbinNo][MedicineNo-1].SetActive(true);
                MedicinesLeft[TenbinNo][MedicineNo-1].SetActive(true);
            }
            else
            {
                MedicinesRight[PrevTenbinNo][MedicineNo-1].SetActive(true);
                MedicinesRight[TenbinNo][MedicineNo-1].SetActive(true);
            }

        }


        //天秤を切り替える
        if (TenbinNo == PrevTenbinNo)
            BlockPanel.Instance.HideBlock();
        else
            Invoke(nameof(ChangeTenbin), 1.2f);


        //セーブデータ
        SaveLoadSystem.Instance.gameData.TenbinStatus = TenbinNo;
        SaveLoadSystem.Instance.gameData.MedicineStatus = Status;
        SaveLoadSystem.Instance.Save();
    }




    //天秤を切り替える
    private void ChangeTenbin()
    {
        AudioManager.Instance.SoundSE("BalanceMove");
        //前の天秤を消す
        Tenbins[PrevTenbinNo].SetActive(false);
        //次の天秤を表示
        Tenbins[TenbinNo].SetActive(true);


        //反対側の天秤に薬がある場合、切替後の天秤で薬を表示して、前の天秤から消す
        if(OtherMedicineNo != 0)
        {
            if (LR == "R")
            {
                MedicinesLeft[TenbinNo][OtherMedicineNo - 1].SetActive(true);
                MedicinesLeft[PrevTenbinNo][OtherMedicineNo - 1].SetActive(false);
            }
            else
            {
                MedicinesRight[TenbinNo][OtherMedicineNo - 1].SetActive(true);
                MedicinesRight[PrevTenbinNo][OtherMedicineNo - 1].SetActive(false);
            }
        }


        //薬を置いた場合、前の天秤の薬を消す
        if(MedicineNo != 0)
        {
            //前の天秤の薬を消す
            if (LR == "L")
                MedicinesLeft[PrevTenbinNo][MedicineNo - 1].SetActive(false);
            else
                MedicinesRight[PrevTenbinNo][MedicineNo - 1].SetActive(false);
        } 

        BlockPanel.Instance.HideBlock();
    }
}
