using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OtherAppManager : MonoBehaviour
{
    //カラオケボタン
    public GameObject BtnKaraoke;
    //トイレボタン
    public GameObject BtnToilet;

    //<summary>
    //URLクラス
    //</summary>
    private class URL
    {
        //<summary>iOSのURL</summary>
        public string iOS { get; set; }
        //<summary>AndroidのURL</summary>
        public string Android { get; set; }
    }

    //<summary>
    //アプリURL情報
    //</summary>
    private Dictionary<string, URL> AppInfoes = new Dictionary<string, URL>
    {
        {
            "Karaoke", //カラオケ
            new URL
            {
                iOS = "https://apps.apple.com/jp/app/%E3%82%AB%E3%83%A9%E3%82%AA%E3%82%B1%E3%83%AB%E3%83%BC%E3%83%A0%E3%81%8B%E3%82%89%E3%81%AE%E8%84%B1%E5%87%BA/id1629906364",
                Android = "https://play.google.com/store/apps/details?id=com.Harekore.Karaoke"
,            }
        },
        {
            "Toilet", //トイレからの脱出
            new URL
            {
                iOS = "https://apps.apple.com/jp/app/%E3%83%88%E3%82%A4%E3%83%AC%E3%81%8B%E3%82%89%E3%81%AE%E8%84%B1%E5%87%BA/id1620184427",
                Android = "https://play.google.com/store/apps/details?id=com.Harekore.Escape01_toilet"
,            }
        },
    };
    
    // Start is called before the first frame update
    void Start()
    {
        //カラオケ
        BtnKaraoke.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapApp("Karaoke");
        });
        //トイレ
        BtnToilet.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapApp("Toilet");
        });
    }


    //<summary>
    //各アプリをタップした時
    //</summary>
    private void OnTapApp(string AppName)
    {
        string link = "";

        //各URLをセット
#if UNITY_IOS
        link = AppInfoes[AppName].iOS;
#elif UNITY_ANDROID
        link = AppInfoes[AppName].Android;
#endif

        //URLを開く
        Application.OpenURL(link);
    }

}
