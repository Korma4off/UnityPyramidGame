using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private GameObject _sound;

    private bool _player = true;
    private bool _showAd = false;
    private bool _isPlaying = false;

    public bool player => _player;


    private void Awake()
    {
        _sound = transform.GetChild(0).gameObject;
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

    private void Update()
    {
        if (_sound != null)
        {
            bool sound = _isPlaying && _player && !_showAd;
            print("1");
            print(_isPlaying);
            print(_player);
            print(!_showAd);
            if (_sound != null && _sound.activeSelf != sound)
            {
                _sound.SetActive(sound);
            }
        } 
        else
        {
            _sound = GameObject.Find("Sounds");
        }
    }
    public void Ad(bool val)
    {
        _showAd = val;
    }
    public bool PlayerSound()
    {
        _player = !_player;
        return _player;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        _isPlaying = hasFocus;
    }
}
