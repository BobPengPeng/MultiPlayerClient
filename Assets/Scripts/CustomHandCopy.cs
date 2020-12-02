using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomHandCopy : MonoBehaviour
{
    public Transform[] ImitativeFingers;
    private Transform[] _finalHands;
    private List<Transform> _moveingFingers = new List<Transform>();
    private List<Transform> _imFingers = new List<Transform>();
    private Transform _pinkyFirstNode;

    // Start is called before the first frame update
    void Start()
    {
        _finalHands = transform.GetComponentsInChildren<Transform>();
        for (int i = 1; i < _finalHands.Length; i++)
        {
            _imFingers.Add(_finalHands[i]);
        }

        _pinkyFirstNode = ImitativeFingers[4];
        for (int i = 0; i < ImitativeFingers.Length; i++)
        {
            //pinky finger have four node
            if (i < 4)
            {
                _moveingFingers.Add(ImitativeFingers[i]);
                _moveingFingers.Add(ImitativeFingers[i].GetChild(0));
                _moveingFingers.Add(ImitativeFingers[i].GetChild(0).GetChild(0));
            }
            else
            {
                _moveingFingers.Add(ImitativeFingers[i].GetChild(0));
                _moveingFingers.Add(ImitativeFingers[i].GetChild(0).GetChild(0));
                _moveingFingers.Add(ImitativeFingers[i].GetChild(0).GetChild(0).GetChild(0));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _imFingers.Count; i++)
        {
            if (i % 3 == 0)
            {
                Quaternion temp;
                if (i == 0)
                {
                    temp = _moveingFingers[i].localRotation *
                           Quaternion.Inverse(Quaternion.Euler(new Vector3(-90.0f, 180.0f, 0.0f)));
                    Vector3 v = temp.eulerAngles;
                    _imFingers[i].localRotation = Quaternion.Euler(new Vector3(v.x, v.y, v.z)) * Quaternion.Euler(new Vector3(0.0f, 0.0f, 30.0f));
                }
                else if (i == 4)
                {
                    temp = (_moveingFingers[i].localRotation *
                            Quaternion.Inverse(Quaternion.Euler(new Vector3(-90.0f, 90.0f, 90.0f)))) *
                           _pinkyFirstNode.localRotation;
                }
                else
                {
                    temp = _moveingFingers[i].localRotation *
                           Quaternion.Inverse(Quaternion.Euler(new Vector3(-90.0f, 90.0f, 90.0f)));
                    Vector3 v = temp.eulerAngles;
                    _imFingers[i].localRotation = Quaternion.Euler(new Vector3(v.x, v.z, v.y));
                }


            }
            else
            {
                _imFingers[i].localRotation = _moveingFingers[i].localRotation;
            }
        }
    }
}