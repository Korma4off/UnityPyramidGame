using System;
using UnityEngine;

public class Progress : MonoBehaviour
{
    public static Progress Instance;

    public bool sound = true;
    private int _lvl = 1;
    public int lvl => _lvl;

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

    public void LvlUp()
    {
        _lvl++;
        if(_lvl == 3)
        {
            Yandex.Instance.Rate();
        }
        Yandex.Instance.Save(_lvl);
    }

    public void ResetData()
    {
        _lvl = 1;

        Yandex.Instance.Save(_lvl);
    }
    
    public void LoadData(int lvl)
    {
        if (lvl == 0)
        {
            lvl = 1;
        }
        _lvl = lvl;
        GameObject.Find("Canvas").GetComponent<CanvasController>().Restart();
    }
}
