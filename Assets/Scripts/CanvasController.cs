using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject _playButton;
    [SerializeField] private GameObject _timeOut;

    [SerializeField] private float _minutes;
    [SerializeField] private Text _minutesText;

    [SerializeField] private float _seconds;
    [SerializeField] private Text _secondsText;

    [SerializeField] private Text _lvlText;

    [SerializeField] private GameObject _sound;

    private bool _pause = false;

    private int _loadedScene;

    private void Start()
    {
        StartAd();
        int lvl = Progress.Instance.lvl;
        _loadedScene = SceneManager.GetActiveScene().buildIndex;
        _lvlText.text = lvl.ToString();
        if ( _loadedScene == 0 && lvl == 1)
        {
            _playButton.SetActive(false);
        }
        if (!SoundManager.Instance.player)
        {
            _sound.SetActive(false);
        }

    }

    private void Update()
    {
        if (_loadedScene != 0 && !_pause)
        {
            if (_seconds < 0)
            {
                if (_minutes > 0)
                {
                    _minutes--;
                    _seconds = 59f;
                }
                else
                {
                    _pause = true;
                    _timeOut.SetActive(true);
                }
            }
            _seconds -= Time.deltaTime;

            _minutesText.text = StringFormating(_minutes);
            _secondsText.text = StringFormating(_seconds);
        }
    }

    public void Pause()
    {
        _pause = true;
    }

    public void StartAd()
    {
        _pause = true;
        SoundManager.Instance.Ad(true);
        Yandex.Instance.Ad(this);
    }
    public void StartRewAd()
    {
        _pause = true;
        SoundManager.Instance.Ad(true);
        Yandex.Instance.RewardAd(this);
    }

    public void EndAd()
    {
        _pause = false;
        SoundManager.Instance.Ad(false);
    }

    public void EndRewAd(bool value)
    {
        
        if (_timeOut.activeSelf)
        {
            if (value)
            {
                _pause = false;
                _seconds = 30f;
                _secondsText.text = StringFormating(_seconds);
                _timeOut.SetActive(false);
            }
        }
        else 
        {
            _pause = false;
            if (value)
            {
                GameObject.Find("MainController").GetComponent<MainControl>().Reward();
            }
        }

        SoundManager.Instance.Ad(false);
    }
    public void SoundButton()
    {
        _sound.SetActive(SoundManager.Instance.PlayerSound());
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Home ()
    {
        SceneManager.LoadScene(0);
    }

    public void NewGame()
    {
        Progress.Instance.ResetData();
        SceneManager.LoadScene(1);
    }

    public void Play()
    {
        int lvl = Progress.Instance.lvl;
        if (lvl > 10) 
        {
            lvl = UnityEngine.Random.Range(6, 11);
        }
        SceneManager.LoadScene(lvl);
    }

    private string StringFormating(float val)
    {
        val = Convert.ToInt32(val);
        string str = val.ToString();
        if (str.Length == 1)
        {
            str = "0" + str;
        }
        return str;
    }
}
