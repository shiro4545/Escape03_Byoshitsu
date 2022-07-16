using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //所有アイテム
    public string getItems = "";

    //アイテム取得有無
    public bool isGetMedicine1 = false;
    public bool isGetMedicine2 = false;
    public bool isGetMedicine3 = false;
    public bool isGetMedicinePack = false;
    public bool isGetMoney = false;
    public bool isGetTVCard = false;
    public bool isGetBalance = false;
    public bool isGetSciccers = false;
    public bool isGetGlass = false;
    public bool isGetWaterTunk = false;
    public bool isGetKey1 = false;
    public bool isGetTaionkei = false;
    public bool isGetBlock1 = false;
    public bool isGetBlock2 = false;
    public bool isGetBlock3 = false;
    public bool isGetBlock4 = false;
    public bool isGetKey2 = false;

    //謎クリア有無
    public bool isClearMoney = false;
    public bool isClearTVCard = false;
    public bool isClearBalance = false;
    public bool isClearDoll = false;
    public bool isClearSciccers = false;
    public bool isClearPC = false;
    public bool isClearDial = false;

    public bool isClearPuzzle31 = false;
    public bool isClearCurtain31 = false;


    //オブジェクト状態
    public int TenbinStatus = -1; //(-1:皿セット前,0:並行,1:左傾き,2:右傾き)
    public string MedicineStatus = "00";
    public string DollStatus = "0000";
    public string CurtainStatus = "0000002000"; //(0:close,1:open,2:紐縛り)

    //各ヒントの数と動画視聴有無(0:未視聴,1:視聴済み)
    public string[] HintFlgArray = new string[] {
        "0",     //ダミー
        "0000",  //1
        "0000",
        "0000",
        "0000",
        "0000",  //5
        "0000",
        "0000",
        "0000",
        "0000",
        "0000",  //10
        "0000",
        "0000",
        "0000",
        "0000",
        "0000",  //15
        "0000",
        "0000", 
        "0000",
        "0000",
        "0000",  //20
        "0000",
        "0000",
        "0000",
        "0000",
        "0000",  //25
        "0000",
        "0000",
        "0000",
        "0000",
        "0000",  //30
        "0000",
    };
}
