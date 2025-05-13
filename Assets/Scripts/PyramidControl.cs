using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidControl : MonoBehaviour
{
    [SerializeField] private GameObject _head;
    [SerializeField] private GameObject _rewIcon;

    [SerializeField] private Transform _upperRing;
    [SerializeField] private List<Transform> _rings;
    [SerializeField] private List<GameObject> _ringsPrefabs;

    [SerializeField] private bool _empty = false;
    [SerializeField] private bool _rew = false;

    [SerializeField] private List<Color> _colors;

    private AudioSource _win;


    public List<Transform> rings => _rings;
    public bool empty => _empty;
    public bool rew => _rew;

    private void Start()
    {
        _win = GameObject.Find("Win").GetComponent<AudioSource>();
    }

    public void SetColor(Color color)
    {
        foreach (Transform r in _rings)
        {
            if (r.childCount > 0)
            {
                r.GetChild(0).gameObject.GetComponent<RingController>().SetColor(color);
            }

        }
    }

    public Transform RingUp()
    {

        if (_rew == true)
        {
            GameObject.Find("Canvas").GetComponent<CanvasController>().StartRewAd();
        }
        int lastRing = 0;
        for (int i = 0; i < 4; i++)
        {
            if (_rings[i].childCount != 0)
            {
                lastRing = i;
            }
        }
        Transform ring = _rings[lastRing].GetChild(0);
        ring.SetParent(_upperRing);
        ring.position = _upperRing.position;

        return ring;
    }

    public bool Click(Transform ring)
    {
        if (_rew == true)
        {
            GameObject.Find("Canvas").GetComponent<CanvasController>().StartRewAd();
            return false;
        }
        else if (_rings[3].childCount == 0)
        {
            int pos = 0;
            for (int i = 0; i < 4; i++)
            {
                if(_rings[i].childCount != 0)
                {
                    pos += 1;
                }
            }
            ring.SetParent(_rings[pos]);
            ring.position = _rings[pos].position;
            Check();
            return true;
        }
        else
        {
            return false;
        }
    }
    
    private void Check()
    {
        bool val = true;
        if (_rings[0].childCount != 0)
        {
            Color color = _rings[0].GetChild(0).GetComponent<RingController>().color;
        
            for (int i = 0; val && i<_rings.Count;  i++)
            {
                if(_rings[i].childCount == 0 || _rings[i].GetChild(0).GetComponent<RingController>().position != i || _rings[i].GetChild(0).GetComponent<RingController>().color != color) 
                {
                    val = false;
                }
            }
        }
        else
        {
            val = false;
        }
        if (val)
        {
            _win.Play();
            _head.SetActive(true);
            _head.GetComponent<SpriteRenderer>().color = _rings[0].GetChild(0).GetComponent<RingController>().color;
            gameObject.GetComponent<Collider>().enabled = false;
            GameObject.Find("MainController").GetComponent<MainControl>().LockPyr();
        }
    }

    public void Rewarded()
    {
        _rew = false;
        _rewIcon.SetActive(false);
    }
}
