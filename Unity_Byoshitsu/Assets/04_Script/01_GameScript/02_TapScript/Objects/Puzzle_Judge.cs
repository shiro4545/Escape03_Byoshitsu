using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Judge : MonoBehaviour
{
    //パズル31 or パズル8 
    public bool Puzzle31Flg;
    
    //クリア有無
    public bool isClear = false;
    //ステータス(0:なし,1:ブロックあり)
    public string Status = "000000000000000000";

    //ブロックオブジェクト配列
    public GameObject[] Blocks;
    //コライダー配列
    public GameObject[] Colliders;


    //赤ランプ(31用)
    public GameObject Red;
    //カーテンクラス(31用)
    public Curtain_Judge CurtainClass_31;

    //右の板(8用)
    public GameObject Plate8;
    //テンキー(8用)
    public GameObject Tenkey;

    /// <summary>
    /// ブロックの干渉関係 (PositionNo,干渉するPositionNo
    /// </summary>
    private Dictionary<int, int[]> BlockColision = new Dictionary<int, int[]>
    {
        {
            0,
            new int[]{0,1,3,4,}
        },
        {
            1,
            new int[]{0,1,3,4,5,6,7}
        },
        {
            2,
            new int[]{2,4,5,8}
        },
        {
            3,
            new int[]{0,1,3,4,5,6,7,8}
        },
        {
            4,
            new int[]{0,1,2,3,4}
        },
        {
            5,
            new int[]{1,2,3,5,6,7}
        },
        {
            6,
            new int[]{1,3,5,6,7}
        },
        {
            7,
            new int[]{1,3,5,6,7,8}
        },
        {
            8,
            new int[]{2,3,7,8}
        },
        {
            9,
            new int[]{9,10,12,13}
        },
        {
            10,
            new int[]{9,10,12,13,14,15,16}
        },
        {
            11,
            new int[]{11,13,14,17}
        },
        {
            12,
            new int[]{9,10,12,13,14,15,16,17}
        },
        {
            13,
            new int[]{9,10,11,12,13}
        },
        {
            14,
            new int[]{10,11,12,14,15,16}
        },
        {
            15,
            new int[]{10,12,14,15,16}
        },
        {
            16,
            new int[]{10,12,14,15,16,17}
        },
        {
            17,
            new int[]{11,12,16,17}
        },

    };

    /// <summary>
    /// ブロックを置く場合
    /// </summary>
    /// <param name="PositionNo"></param>
    /// <param name="EnableBlockNo"></param>
    public void PutBlock(int PositionNo)
    {
        AudioManager.Instance.SoundSE("PutItem");
        ItemManager.Instance.UseItem();
        //ブロックを置く
        Blocks[PositionNo].SetActive(true);

        //干渉するブロック位置のコライダーを非表示に
        foreach (int i in BlockColision[PositionNo])
            Colliders[i].SetActive(false);

        //ステータス更新
        if (PositionNo == 0)
            Status = "1" + Status.Substring(PositionNo + 1);
        else if (PositionNo == 17)
            Status = Status.Substring(0, PositionNo) + "1";
        else
            Status = Status.Substring(0, PositionNo) + "1" + Status.Substring(PositionNo + 1);

        //答え合わせ
        if (Puzzle31Flg)
            Judge31();
        else
            Judge8();

        //セーブデータ
        if(Puzzle31Flg)
            SaveLoadSystem.Instance.gameData.Puzzle31Status = Status;
        else
            SaveLoadSystem.Instance.gameData.Puzzle8Status = Status;
        SaveLoadSystem.Instance.Save();
    }


    /// <summary>
    /// ブロックをアイテム取得後,Putコライダーを制御
    /// </summary>
    /// <param name="PositionNo"></param>
    public void GetBlock(int PositionNo)
    {
        //ステータス更新
        if (PositionNo == 0)
            Status = "0" + Status.Substring(PositionNo + 1);
        else if (PositionNo == 17)
            Status = Status.Substring(0, PositionNo) + "0";
        else
            Status = Status.Substring(0, PositionNo) + "0" + Status.Substring(PositionNo + 1);

        //ブロックがあった位置と干渉する位置のループ
        foreach (int i in BlockColision[PositionNo])
        {
            bool isColision = false;
            //干渉する位置がさらに干渉する位置のループ
            foreach(int j in BlockColision[i])
            {
                //そこにブロックが置いてあるか
                if(Status.Substring(j,1) == "1")
                {
                    isColision = true;
                    break;
                }
            }
            //干渉先の干渉先にブロックがない場合,その干渉先のコライダーを表示
            if (!isColision)
                Colliders[i].SetActive(true);
        }

        //赤ランプ非表示
        if (!SaveLoadSystem.Instance.gameData.isClearCurtain31)
        {
            isClear = false;
            SaveLoadSystem.Instance.gameData.isClearPuzzle31 = false;
            Red.SetActive(false);
        }

        //セーブデータ
        if (Puzzle31Flg)
            SaveLoadSystem.Instance.gameData.Puzzle31Status = Status;
        else
            SaveLoadSystem.Instance.gameData.Puzzle8Status = Status;
        SaveLoadSystem.Instance.Save();
    }


    /// <summary>
    /// Puzzle31用の答え合わせ
    /// </summary>
    private void Judge31()
    {
        if (Status != "100000010001000000")
        {
            //不正解の場合
            if (!SaveLoadSystem.Instance.gameData.isClearCurtain31)
            {
                isClear = false;
                SaveLoadSystem.Instance.gameData.isClearPuzzle31 = false;
                Red.SetActive(false);
            }
            
        }
        else
        {
            //正解の場合
            isClear = true;
            SaveLoadSystem.Instance.gameData.isClearPuzzle31 = true;
            Red.SetActive(true);
            CurtainClass_31.Judge();
        }

        SaveLoadSystem.Instance.Save();
    }

    /// <summary>
    /// Puzzle8用の答え合わせ
    /// </summary>
    private void Judge8()
    {
        if (Status != "000001000101000010")
            return;

        AudioManager.Instance.SoundSE("Clear");
        BlockPanel.Instance.ShowBlock();

        Invoke(nameof(AfterClear1), 1.5f);

        //セーブデータ
        isClear = true;
        SaveLoadSystem.Instance.gameData.isClearPuzzle8 = true;
        SaveLoadSystem.Instance.Save();

    }
    //演出
    private void AfterClear1()
    {
        CameraManager.Instance.ChangeCameraPosition("Table");
        Invoke(nameof(AfterClear2), 1.5f);
    }
    //
    private void AfterClear2()
    {
        AudioManager.Instance.SoundSE("OpenPlate");
        Plate8.SetActive(false);
        Tenkey.SetActive(true);
        Invoke(nameof(AfterClear3), 1.5f);
    }
    //
    private void AfterClear3()
    {
        CameraManager.Instance.ChangeCameraPosition("TableCenter");
        BlockPanel.Instance.HideBlock();
    }


    /// <summary>
    /// 「続きから」の処理
    /// </summary>
    public void Restrat()
    {
        for(int i = 0; i < Status.Length; i++)
        {
            if(Status.Substring(i,1) == "1")
            {
                //ブロックを置く
                Blocks[i].SetActive(true);

                //干渉するブロック位置のコライダーを非表示に
                foreach (int j in BlockColision[i])
                    Colliders[j].SetActive(false);
            }
        }
    }
}
