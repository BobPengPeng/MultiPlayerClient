using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using RootMotion.FinalIK;
using UnityEngine;

public enum MyHandType
{
    LeftHand,
    RightHand,
}

public class HandPoserContro : MonoBehaviour
{
    // Start is called before the first frame update
    private HandPoser _handPoser;
    public MyHandType _HandType;

    void Start()
    {
        _handPoser = transform.GetComponent<HandPoser>();
        transform.GetComponentsInChildren<Transform>(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) > 0 || OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > 0)
        {
            if (_HandType == MyHandType.LeftHand)
            {
                _handPoser.ChangeRotaWeight(OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger));
            }
            else if (_HandType == MyHandType.RightHand)
            {
                _handPoser.ChangeRotaWeight(OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger));
            }
        }
        else
        {
            _handPoser.ChangeRotaWeight(0);
        }
    }
}