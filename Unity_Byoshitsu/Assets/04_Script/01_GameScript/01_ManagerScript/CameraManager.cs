using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    //<summary>現在のカメラの位置名</summary>
    public string CurrentPositionName { get; private set; }

    private bool isStart = false;

    //矢印ボタンオブジェクト
    public GameObject ButtonLeft;
    public GameObject ButtonRight;
    public GameObject ButtonBack;
    //非表示オブジェクトの配列
    public GameObject[] hideObjects;



    //<summary>
    //カメラの位置情報クラス
    //</summary>
    private class CameraPositionInfo
    {
        //<summary>カメラの位置</summary>
        public Vector3 Position { get; set; }
        //<summary>カメラの角度</summary>
        public Vector3 Rotate { get; set; }
        //<summary>ボタンの移動先</summary>
        public MoveNames MoveNames { get; set; }
        //<summary>非表示にするオブジェクト名</summary>
        public string[] hideObjectsName  { get; set; }
    }

    //<summary>
    //ボタンの移動先クラス
    //</summary>
    private class MoveNames
    {
        public string Left { get; set; }
        public string Right { get; set; }
        public string Back { get; set; }
    }

    //<summary>
    //全カメラ位置情報
    //</summary>
    private Dictionary<string, CameraPositionInfo> CameraPositionInfoes = new Dictionary<string, CameraPositionInfo>
    {
        {
            "Title",//タイトル画面
            new CameraPositionInfo
            {
                Position=new Vector3(60.7f,4.7f,6.5f),
                Rotate =new Vector3(7,116,0),
                MoveNames=new MoveNames
                {
                },
            }
                //hideObjectsName = new string[]{"Shelf","KaraokeMachine","Tambarin_Shikaku" }
        },
        {
            "Base1",//スタート位置
            new CameraPositionInfo
            {
                Position=new Vector3(18.3f,13f,8.77f),
                Rotate =new Vector3(8f,-153,0),
                MoveNames=new MoveNames
                {
                    Left="Base2",
                    Right="Base6",
                },
            }
        },
        {
            "Base2",//クマがいるベッド
            new CameraPositionInfo
            {
                Position=new Vector3(8.89f,13f,7.88f),
                Rotate =new Vector3(8,-210.4f,0),
                MoveNames=new MoveNames
                {
                    Left="Base3",
                    Right="Base1",
                }
            }
        },
        {
            "Base3",//　出口とナースカート
            new CameraPositionInfo
            {
                Position=new Vector3(4.95f,13f,0),
                Rotate =new Vector3(10,90,0),
                MoveNames=new MoveNames
                {
                    Left="Base4",
                    Right="Base2",
                }
            }
        },
        {
            "Base4",//天秤があるベッド
            new CameraPositionInfo
            {
                Position=new Vector3(8.11f,13f,-9.95f),
                Rotate =new Vector3(8,30.84f,0),
                MoveNames=new MoveNames
                {
                    Left="Base5",
                    Right="Base3",
                }
            }
        },
        {
            "Base5",//クロスワードがあるベッド
            new CameraPositionInfo
            {
                Position=new Vector3(20.17f,12.85f,-6.85f),
                Rotate =new Vector3(8,-34.7f,0),
                MoveNames=new MoveNames
                {
                    Left="Base6",
                    Right="Base4",
                }
            }
        },
        {
            "Base6",//ウォーターサーバー
            new CameraPositionInfo
            {
                Position=new Vector3(25.11f,13f,-0.5f),
                Rotate =new Vector3(12,-90,0),
                MoveNames=new MoveNames
                {
                    Left="Base1",
                    Right="Base5",
                }
            }
        },

        //***Base1からの派生**********************************************************
        {
            "Desk1",//花とフルーツ
            new CameraPositionInfo
            {
                Position=new Vector3(-1.6f,9.6f,-22f),
                Rotate =new Vector3(17,-129,0),
                MoveNames=new MoveNames
                {
                    Back="Base1",
                }
            }
        },
        {
            "Chair1",//椅子の上の箱
            new CameraPositionInfo
            {
                Position=new Vector3(-6.22f,6.49f,-21.67f),
                Rotate =new Vector3(67,-90,0),
                MoveNames=new MoveNames
                {
                    Back="Base1",
                }
            }
        },
        {
            "TV1",//テレビ
            new CameraPositionInfo
            {
                Position=new Vector3(-4.212f,7.618f,-27.47f),
                Rotate =new Vector3(0,-162.4f,0),
                MoveNames=new MoveNames
                {
                    Back="Base1",
                }
            }
        },
        {
            "TV1CardInsert",//テレビカード挿入口
            new CameraPositionInfo
            {
                Position=new Vector3(-5.6f,7.4f,-31.698f),
                Rotate =new Vector3(34,-148f,0),
                MoveNames=new MoveNames
                {
                    Back="TV1",
                }
            }
        },
        {
            "Shelf1",//上の棚
            new CameraPositionInfo
            {
                Position=new Vector3(-6.246f,12.1f,-29.14f),
                Rotate =new Vector3(4,-176.17f,0),
                MoveNames=new MoveNames
                {
                    Back="Base1",
                }
            }
        },
        {
            "Name1",//名前
            new CameraPositionInfo
            {
                Position=new Vector3(2.794f,9.6f,-34f),
                Rotate =new Vector3(10,180f,0),
                MoveNames=new MoveNames
                {
                    Back="Base1",
                }
            }
        },
        {
            "Bed1",//ベッド上のファイル
            new CameraPositionInfo
            {
                Position=new Vector3(1.83f,8.23f,-28.35f),
                Rotate =new Vector3(76f,180f,0),
                MoveNames=new MoveNames
                {
                    Back="Base1",
                }
            }
        },
        //***Base2からの派生**********************************************************
        {
            "Bed2",//ベッド上のクマ
            new CameraPositionInfo
            {
                Position=new Vector3(22.89f,10.065f,-17.82f),
                Rotate =new Vector3(27f,163f,0),
                MoveNames=new MoveNames
                {
                    Back="Base2",
                }
            }
        },
        {
            "WhiteBoard2",//ベッドにかかっているホワイトボード
            new CameraPositionInfo
            {
                Position=new Vector3(25f,6.8f,-12f),
                Rotate =new Vector3(19f,160f,0),
                MoveNames=new MoveNames
                {
                    Back="Base2",
                }
            }
        },
        {
            "TV2",//テレビ
            new CameraPositionInfo
            {
                Position=new Vector3(34.37f,9.33f,-27.4f),
                Rotate =new Vector3(10,160f,0),
                MoveNames=new MoveNames
                {
                    Back="Base2",
                }
            }
        },
        {
            "Shelf2",//上の棚
            new CameraPositionInfo
            {
                Position=new Vector3(34.62f,12.59f,-30.54f),
                Rotate =new Vector3(12.42f,156f,0),
                MoveNames=new MoveNames
                {
                    Back="Base2",
                }
            }
        },
        {
            "Name2",//名前
            new CameraPositionInfo
            {
                Position=new Vector3(25.83f,9.33f,-35.51f),
                Rotate =new Vector3(10,180f,0),
                MoveNames=new MoveNames
                {
                    Back="Base2",
                }
            }
        },
        
        //***Base3からの派生**********************************************************
        {
            "PC",//パソコン
            new CameraPositionInfo
            {
                Position=new Vector3(28.586f,11.74f,-2.118f),
                Rotate =new Vector3(49.305f,117f,0),
                MoveNames=new MoveNames
                {
                    Back="Base3",
                }
            }
        },
        {
            "NurseCurt",//ナースカート
            new CameraPositionInfo
            {
                Position=new Vector3(25.2f,7.16f,-1.42f),
                Rotate =new Vector3(33,107,0),
                MoveNames=new MoveNames
                {
                    Back="Base3",
                }
            }
        },
        {
            "Door",//出口扉
            new CameraPositionInfo
            {
                Position=new Vector3(34f,10.22f,-0.36f),
                Rotate =new Vector3(4,90,0),
                MoveNames=new MoveNames
                {
                    Left="DoorLeft",
                    Right="DoorRight",
                    Back="Base3",
                }
            }
        },
        {
            "DoorLeft",//テーブルとテレビカード販売機
            new CameraPositionInfo
            {
                Position=new Vector3(43.27f,12.11f,-4.08f),
                Rotate =new Vector3(8,9.72f,0),
                MoveNames=new MoveNames
                {
                    Back="Door",
                }
            }
        },
        {
            "DoorRight",//手洗い
            new CameraPositionInfo
            {
                Position=new Vector3(41.93f,12.11f,-1.12f),
                Rotate =new Vector3(4,166,0),
                MoveNames=new MoveNames
                {
                    Back="Door",
                }
            }
        },
        {
            "TVCardMachine",//テレビカード販売機
            new CameraPositionInfo
            {
                Position=new Vector3(52.48f,10f,19.2f),
                Rotate =new Vector3(25,15,0),
                MoveNames=new MoveNames
                {
                    Back="DoorLeft",
                }
            }
        },
        {
            "Table",//パズルがあるテーブル
            new CameraPositionInfo
            {
                Position=new Vector3(46.67f,16.72f,12.95f),
                Rotate =new Vector3(38.633f,0,0),
                MoveNames=new MoveNames
                {
                    Back="DoorLeft",
                }
            }
        },
        {
            "TableLeft",//パズルがあるテーブル左
            new CameraPositionInfo
            {
                Position=new Vector3(43.91f,10.68f,22.76f),
                Rotate =new Vector3(76,0,0),
                MoveNames=new MoveNames
                {
                    Back="Table",
                }
            }
        },
        {
            "TableCenter",//パズルがあるテーブル中央
            new CameraPositionInfo
            {
                Position=new Vector3(46.94f,10.78f,22.71f),
                Rotate =new Vector3(76,0,0),
                MoveNames=new MoveNames
                {
                    Back="Table",
                }
            }
        },
        {
            "TableRight",//パズルがあるテーブル右
            new CameraPositionInfo
            {
                Position=new Vector3(49.9f,8.86f,23.09f),
                Rotate =new Vector3(76,0,0),
                MoveNames=new MoveNames
                {
                    Back="Table",
                }
            }
        },
        {
            "Box",//手洗い場の箱
            new CameraPositionInfo
            {
                Position=new Vector3(43.56f,9.16f,-23.67f),
                Rotate =new Vector3(70,180,0),
                MoveNames=new MoveNames
                {
                    Back="DoorRight",
                }
            }
        },
        {
            "Toothbrush1",//手洗い場の歯ブラシ青
            new CameraPositionInfo
            {
                Position=new Vector3(51.65f,8.68f,-23.17f),
                Rotate =new Vector3(18,160,0),
                MoveNames=new MoveNames
                {
                    Back="DoorRight",
                }
            }
        },
        {
            "Toothbrush2",//手洗い場の歯ブラシ赤
            new CameraPositionInfo
            {
                Position=new Vector3(42.53f,10,-23.92f),
                Rotate =new Vector3(6,167,0),
                MoveNames=new MoveNames
                {
                    Back="DoorRight",
                }
            }
        },
        {
            "Nagashi",//手洗い場の流し
            new CameraPositionInfo
            {
                Position=new Vector3(46.82f,9,-20.55f),
                Rotate =new Vector3(30,165,0),
                MoveNames=new MoveNames
                {
                    Back="DoorRight",
                }
            }
        },
        
        //***Base4からの派生**********************************************************
        {
            "Desk4",//机の上の天秤
            new CameraPositionInfo
            {
                Position=new Vector3(32f,8.687f,23.069f),
                Rotate =new Vector3(20,90,0),
                MoveNames=new MoveNames
                {
                    Back="Base4",
                }
            }
        },
        {
            "TV4",//人形4体
            new CameraPositionInfo
            {
                Position=new Vector3(31.97f,8.21f,26.34f),
                Rotate =new Vector3(9.592f,40.292f,0),
                MoveNames=new MoveNames
                {
                    Back="Base4",
                }
            }
        },
        {
            "Shelf4",//冷蔵庫
            new CameraPositionInfo
            {
                Position=new Vector3(31.68f,6.51f,24.87f),
                Rotate =new Vector3(20,43f,0),
                MoveNames=new MoveNames
                {
                    Back="Base4",
                }
            }
        },
        {
            "Name4",//名前
            new CameraPositionInfo
            {
                Position=new Vector3(26.038f,9.897f,31.937f),
                Rotate =new Vector3(20,0,0),
                MoveNames=new MoveNames
                {
                    Back="Base4",
                }
            }
        },
        //***Base5からの派生**********************************************************
        {
            "Bed5",//クロスワード
            new CameraPositionInfo
            {
                Position=new Vector3(3.11f,9.71f,23.73f),
                Rotate =new Vector3(80,180,0),
                MoveNames=new MoveNames
                {
                    Back="Base5",
                }
            }
        },
        {
            "Shelf5_top",//時計と歯ブラシ
            new CameraPositionInfo
            {
                Position=new Vector3(-3.14f,12.47f,25.39f),
                Rotate =new Vector3(3,-34,0),
                MoveNames=new MoveNames
                {
                    Back="Base5",
                }
            }
        },
        {
            "Shelf5_btm",//引き出し
            new CameraPositionInfo
            {
                Position=new Vector3(-3.785f,8f,26.579f),
                Rotate =new Vector3(32,323,0),
                MoveNames=new MoveNames
                {
                    Back="Base5",
                }
            }
        },
        {
            "Name5",//名前
            new CameraPositionInfo
            {
                Position=new Vector3(2.94f,9.897f,31.937f),
                Rotate =new Vector3(20,0,0),
                MoveNames=new MoveNames
                {
                    Back="Base5",
                }
            }
        },
        {
            "Memo5",//引き出しの中のメモ
            new CameraPositionInfo
            {
                Position=new Vector3(-7.09f,8.51f,28.36f),
                Rotate =new Vector3(59.511f,0,0),
                MoveNames=new MoveNames
                {
                    Back="Shelf5_btm",
                }
            }
        },
        {
            "Shelf5Door",//下の棚のダイヤル錠
            new CameraPositionInfo
            {
                Position=new Vector3(-6.24f,3.14f,28.62f),
                Rotate =new Vector3(0,-8.204f,0),
                MoveNames=new MoveNames
                {
                    Back="Shelf5_btm",
                }
            }
        },
        //***Base6からの派生**********************************************************
        {
            "WaterServer",//ウォーターサーバー
            new CameraPositionInfo
            {
                Position=new Vector3(-0.33f,8.45f,5.82f),
                Rotate =new Vector3(18,248f,0),
                MoveNames=new MoveNames
                {
                    Back="Base6",
                }
            }
        },
    };

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        //ChangeCameraPosition("Title");
        ChangeCameraPosition("Base4");

        //左矢印ボタン押下時
        ButtonLeft.GetComponent<Button>().onClick.AddListener(() =>
        {
            AudioManager.Instance.SoundSE("TapUIBtn");
            ChangeCameraPosition(CameraPositionInfoes[CurrentPositionName].MoveNames.Left);
        });

        //右矢印ボタン押下時
        ButtonRight.GetComponent<Button>().onClick.AddListener(() =>
        {
            AudioManager.Instance.SoundSE("TapUIBtn");
            ChangeCameraPosition(CameraPositionInfoes[CurrentPositionName].MoveNames.Right);
        });

        //下矢印ボタン押下時
        ButtonBack.GetComponent<Button>().onClick.AddListener(() =>
        {
            AudioManager.Instance.SoundSE("TapUIBtn");
            ChangeCameraPosition(CameraPositionInfoes[CurrentPositionName].MoveNames.Back);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }

    //<summary>
    //カメラ移動
    //</summary>
    //<param>位置名</param>
    public void ChangeCameraPosition(string positionName)
    {
        if(isStart)
        {
          //アイテム拡大画面表示時
          if(ItemManager.Instance.ItemPanel.activeSelf)
          {
            ItemManager.Instance.ItemPanel.SetActive(false);
            positionName = CurrentPositionName;
          }
        }
        isStart = true;

        if (positionName == null) return;

        CurrentPositionName = positionName;

        GetComponent<Camera>().transform.position = CameraPositionInfoes[CurrentPositionName].Position;
        GetComponent<Camera>().transform.rotation = Quaternion.Euler(CameraPositionInfoes[CurrentPositionName].Rotate);

        //iPad対策
        //if(positionName == "RoomRight" && Screen.width > 1300)
        //{
        //  GetComponent<Camera>().transform.position = new Vector3(-4.17f,6.47f,-4);
        //  GetComponent<Camera>().transform.rotation = Quaternion.Euler(new Vector3(18,90,0));
        //}


        //ボタン表示・非表示
        UpdateButtonActive();
        //特定オブジェクトを非表示
        UpdateObjectActive();
    }

    //<summary>
    //ボタン表示非表示の切替
    //</summary>
    private void UpdateButtonActive()
    {
        //左ボタンの表示非表示を切替
        if (CameraPositionInfoes[CurrentPositionName].MoveNames.Left == null)
            ButtonLeft.SetActive(false);
        else ButtonLeft.SetActive(true);
        //右ボタンの表示非表示を切替
        if (CameraPositionInfoes[CurrentPositionName].MoveNames.Right == null)
            ButtonRight.SetActive(false);
        else ButtonRight.SetActive(true);
        //バックボタンの表示非表示を切替
        if (CameraPositionInfoes[CurrentPositionName].MoveNames.Back == null)
            ButtonBack.SetActive(false);
        else ButtonBack.SetActive(true);
    }

    //<summary>
    //特定オブジェクトを非表示
    //</summary>
    private void UpdateObjectActive()
    {
      //既に非表示のオブジェクトを表示する
      foreach (GameObject obj in hideObjects )
      {
        obj.SetActive(true);
      }
      hideObjects = new GameObject[0];

      if (CameraPositionInfoes[CurrentPositionName].hideObjectsName == null)
        return;
      //新たな方向でのオブジェクトを非表示にする
      foreach(string objName in CameraPositionInfoes[CurrentPositionName].hideObjectsName)
      {
        Array.Resize(ref hideObjects, hideObjects.Length + 1);
        hideObjects[hideObjects.Length  - 1] = GameObject.Find(objName);
        GameObject.Find(objName).SetActive(false);
      }
    }
}
