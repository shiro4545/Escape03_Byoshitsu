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


    //オブジェクト状態
    public string HangerStatus = "0000";
    public bool isChangePage = false;
    public string CopStatus = "03";
    public int DeskUpperStatus = 0;
    public int DeskUnderStatus = 0;
    public string TambarinStatus = "001";
    public bool isOpenShelf = false;
    public int DenmokuStatus = 1; //0:電源off,1:ロック状態,2:ロックなし 
    public int DoorStatus = 0; //0:全閉,1:全開,2:ちょい開け
    public string PentagonStatus = "00000"; //0:ピースなし,1~5:それぞれのピースが置かれている

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
