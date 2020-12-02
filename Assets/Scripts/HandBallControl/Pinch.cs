using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinch : MonoBehaviour
{
    private bool _indexIn = false;
    private bool _middleIn = false;

    private Action _pinchAction;
    private Action _releaseAction;
    // Start is called before the first frame update
    void Start()
    {
        _pinchAction = () =>
        {
            Debug.Log("pinch");
        };

        _releaseAction = () =>
        {
            Debug.Log("release");
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPinch(Action action)
    {
        _pinchAction = action;
    }

    public void SetRelease(Action action)
    {
        _releaseAction = action;
    }
    
    private void PinchIn()
    {
        if (_indexIn && _middleIn)
        {
            _pinchAction();
        }
    }

    private void PinchOut()
    {
        if (_indexIn && _middleIn)
        {
            return;
        }

        _releaseAction();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("IndexTip"))
        {
            _indexIn = true;
            PinchIn();
        }
        
        if (other.name.Equals("MiddleTip"))
        {
            _middleIn = true;
            PinchIn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Equals("IndexTip"))
        {
            _indexIn = false;
            PinchOut();
        }
        
        if (other.name.Equals("MiddleTip"))
        {
            _middleIn = false;
            //TODO:need to update
            // PinchOut();
        }
    }
}
