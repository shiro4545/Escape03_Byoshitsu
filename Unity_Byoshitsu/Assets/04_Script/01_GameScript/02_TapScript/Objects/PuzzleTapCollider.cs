using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleTapCollider : MonoBehaviour
{
    public string EnableCameraPositionName;
    public int EnableBlockNo;

    // Start is called before the first frame update
    void Start()
    {
        var CurrentTrigger = gameObject.AddComponent<EventTrigger>();
        var EntryClick = new EventTrigger.Entry();
        EntryClick.eventID = EventTriggerType.PointerClick;
        EntryClick.callback.AddListener((x) => OnTap());
        CurrentTrigger.triggers.Add(EntryClick);
    }

    // Update is called once per frame
    void Update()
    {
        //????????????????????????
        int SelectNo = 0;
        if (ItemManager.Instance.SelectItem == "Block1")
            SelectNo = 1;
        else if (ItemManager.Instance.SelectItem == "Block2")
            SelectNo = 2;
        else if (ItemManager.Instance.SelectItem == "Block3")
            SelectNo = 3;
        else if (ItemManager.Instance.SelectItem == "Block4")
            SelectNo = 4;

        if (EnableCameraPositionName == CameraManager.Instance.CurrentPositionName &&
            EnableBlockNo == SelectNo)
            GetComponent<BoxCollider>().enabled = true;
        else
            GetComponent<BoxCollider>().enabled = false;
    }


    protected virtual void OnTap()
    {

    }
}
