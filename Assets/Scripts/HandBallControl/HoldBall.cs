using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HoldBall : MonoBehaviour
{
    public Pinch PinchFingerLeft;
    public Pinch PinchFingerRight;
    private bool _thumbIn = false;
    private bool _indexIn = false;
    private bool _middleIn = false;

    private bool _isFollow = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetHold()
    {
        if (_thumbIn && _indexIn && _middleIn)
        {
            PinchFingerLeft.SetPinch((() =>
            {
                _isFollow = true;
                StartCoroutine(FollowThumb(PinchFingerLeft));
                transform.GetComponent<BallAction>().StopCoro();
            }));
            
            PinchFingerLeft.SetRelease((() =>
            {
                _isFollow = false;
                transform.GetComponent<BallAction>().BackToOrigin();
            }));
        }
        else
        {
            PinchFingerLeft.SetPinch((() =>
            {
                
            }));
        }
        
        if (_thumbIn && _indexIn && _middleIn)
        {
            PinchFingerRight.SetPinch((() =>
            {
                _isFollow = true;
                StartCoroutine(FollowThumb(PinchFingerRight));
                transform.GetComponent<BallAction>().StopCoro();
            }));
            
            PinchFingerRight.SetRelease((() =>
            {
                _isFollow = false;
                transform.GetComponent<BallAction>().BackToOrigin();
            }));
        }
        else
        {
            PinchFingerRight.SetPinch((() =>
            {
                
            }));
        }
    }

    IEnumerator FollowThumb(Pinch pinchFinger)
    {
        _isFollow = true;
        while (_isFollow)
        {
            transform.position = pinchFinger.transform.position;
            yield return 1;
        }
        yield return 0;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("ThumbTip"))
        {
            Debug.Log("ThumbIn");
            _thumbIn = true;
            SetHold();
        }
        
        if (other.name.Equals("IndexTip"))
        {
            Debug.Log("IndexIn");

            _indexIn = true;
            SetHold();
        }
        
        if (other.name.Equals("MiddleTip"))
        {
            Debug.Log("MiddleIn");

            _middleIn = true;
            SetHold();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Equals("ThumbTip"))
        {
            _thumbIn = false;
            SetHold();
        }

        if (other.name.Equals("IndexTip"))
        {
            _indexIn = false;
            SetHold();
        }
        
        if (other.name.Equals("MiddleTip"))
        {
            _middleIn = false;
            SetHold();
        }
    }
}
