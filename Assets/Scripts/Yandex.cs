using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NUnit.Framework;
using UnityEngine;

public class Data
{
    public int lvl;
}
public class Yandex : MonoBehaviour
{
    public static Yandex Instance;

    private CanvasController _canvas;

    [DllImport("__Internal")]
    private static extern void SaveExtern(string data);

    [DllImport("__Internal")]
    private static extern void LoadExtern();
    [DllImport("__Internal")]
    private static extern void Ready();

    [DllImport("__Internal")]
    private static extern void RateGame();

    [DllImport("__Internal")]
    private static extern void WatchAd();

    [DllImport("__Internal")]
    private static extern void WatchReward();

    [DllImport("__Internal")]
    private static extern void GetLang();
    public string lang = null;

    private void Awake()
    {
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Ready();
        LoadExtern(); 
        GetLang();
    }
    public void Save(int lvl)
    {
        Data obj = new Data();
        obj.lvl = lvl;
        SaveExtern(JsonUtility.ToJson(obj));
    }

    public void LoadData(string data)
    {
        Data obj = JsonUtility.FromJson<Data>(data);
        Progress.Instance.LoadData(obj.lvl);
    }

    public void Rate()
    {
        RateGame();
    }
    public void Ad(CanvasController canvas)
    {
        _canvas = canvas;
        WatchAd();
    }

    public void EndOfAd()
    {
        _canvas.EndAd();
    }

    public void RewardAd(CanvasController canvas)
    {
        _canvas = canvas;
        WatchReward();
    }

    public void Reward()
    {
        _canvas.EndRewAd(true);
    }

    public void NotReward()
    {
        _canvas.EndRewAd(false);
    }

    public void SetLang(string _lang)
    {
        lang = _lang;
    }
}
