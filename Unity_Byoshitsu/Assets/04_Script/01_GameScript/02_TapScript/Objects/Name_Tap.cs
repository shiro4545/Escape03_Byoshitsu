using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Name_Tap : TapCollider
{
    public int Index;

    //Judgeクラス
    public Name_Judge StarClass;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        AudioManager.Instance.SoundSE("TapButton");

        //ボタンを後ろに移動
        this.gameObject.transform.Translate(new Vector3(0, 0.03f,0));
        Invoke(nameof(AfterPush), 0.1f);

        //答え合わせ
        StarClass.Judge(Index);
    }

    //ボタンを元に戻す
    private void AfterPush()
    {
        this.gameObject.transform.Translate(new Vector3(0, -0.03f,0));
    }
}