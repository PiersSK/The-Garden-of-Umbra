using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesUI : MonoBehaviour
{
    public Button aspectBtn;
    public Button captureBtn;
    public Button combineBtn;

    public Transform aspect;
    public Transform capture;
    public Transform combine;

    public Transform front;
    public Transform behind;

    private void Start()
    {
        aspectBtn.onClick.AddListener(SetAspectFront);
        captureBtn.onClick.AddListener(SetCaptureFront);
        combineBtn.onClick.AddListener(SetCombineFront);
    }

    public void SetAspectFront()
    {
        aspect.parent = front;
        capture.parent = behind;
        combine.parent = behind;
    }

    public void SetCaptureFront()
    {
        aspect.parent = behind;
        capture.parent = front;
        combine.parent = behind;
    }

    public void SetCombineFront()
    {
        aspect.parent = behind;
        capture.parent = behind;
        combine.parent = front;
    }
}
