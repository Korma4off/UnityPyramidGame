using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    [SerializeField] private List<string> _texts;
    private bool _selected = false;
    private Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();
    }
    private void Update()
    {
        if (!_selected && Yandex.Instance.lang != null)
        {
            Set(Yandex.Instance.lang);
            _selected = true;
        }
    }

    public void Set(string lang)
    {
        if (lang == "ru")
        {
            _text.text = _texts[0];
        }
        else if (lang == "en")
        {
            _text.text = _texts[1];
        }
        else if (lang == "tr")
        {
            _text.text = _texts[2];
        }
    }
}
