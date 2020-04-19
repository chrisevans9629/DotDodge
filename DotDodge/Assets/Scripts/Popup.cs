using System;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public static Popup Instance;
    public Text title;
    public Text message;
    public Button ok;
    public Button cancel;
    public GameObject PopupGameObject;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ok.onClick.AddListener(Ok);
        cancel.onClick.AddListener(Cancel);
    }

    private void Cancel()
    {
        _callBack?.Invoke(false);
        _callBack = null;
        Close();
    }

    private void Ok()
    {
        _callBack?.Invoke(true);
        _callBack = null;
        Close();
    }

    public void Alert(string title, string message)
    {
        this.title.text = title;
        this.message.text = message;
        PopupGameObject.gameObject.SetActive(true);
        cancel.gameObject.SetActive(false);
    }

    private Action<bool> _callBack;
    public void Confirm(string title, string message, Action<bool> callBack)
    {
        _callBack = callBack;
        this.title.text = title;
        this.message.text = message;
        PopupGameObject.gameObject.SetActive(true);
        cancel.gameObject.SetActive(true);
    }

    public void Close()
    {
        PopupGameObject.gameObject.SetActive(false);
    }

}
