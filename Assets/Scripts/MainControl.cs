using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class MainControl : MonoBehaviour
{
    [SerializeField] private int _pyrsCount;
    [SerializeField] private PyramidControl _rewPyr;

    [SerializeField] private Transform _parent;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _activeRing;
    [SerializeField] private List<Color> _colors;

    [SerializeField] private bool _generating;
    private bool _win = false;
    private float _winTimer = 5f;
    // Start is called before the first frame update
    
    private void Start()
    {
        if (_generating)
        {
            Generate();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_win)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, _layerMask))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    PyramidControl pyr = hit.collider.gameObject.GetComponent<PyramidControl>();
                    if (_activeRing == null)
                    {
                        _activeRing = pyr.RingUp();
                    }
                    else if (pyr.Click(_activeRing))
                    {
                        _activeRing = null;
                    }
                }
            }
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<CanvasController>().Play();
        }
    }

    private void Generate()
    {
        List<Transform> rings = new List<Transform>();
        List<Color> colors = _colors;
        foreach (Transform child in _parent)
        {
            if (!child.GetComponent<PyramidControl>().empty)
            {
                Color newColor = _colors[Random.Range(0, _colors.Count)];
                child.GetComponent<PyramidControl>().SetColor(newColor);
                colors.Remove(newColor);
                foreach (Transform ring in child.GetComponent<PyramidControl>().rings)
                {
                    rings.Add(ring.GetChild(0));
                }
            }
        }
        foreach(Transform child in _parent)
        {
            if (!child.GetComponent<PyramidControl>().empty)
            {
                List<Transform> childRings = child.GetComponent<PyramidControl>().rings;
                for (int i = 0; i < 4; i++)
                {
                    Transform newRing = rings[Random.Range(0, rings.Count)];
                    newRing.parent = childRings[i];
                    newRing.position = childRings[i].position;
                    rings.Remove(newRing);
                }
            }
        }
    }

    public void LockPyr()
    {
        _pyrsCount--;
        if (_pyrsCount == 0)
        {
            GameObject.Find("Canvas").GetComponent<CanvasController>().Pause();
            Progress.Instance.LvlUp();
            _win = true;
        }
    }

    public void Reward()
    {
        _rewPyr.Rewarded();
    }
}
