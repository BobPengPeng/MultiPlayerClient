using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataAndNetwork;
using UnityEngine;
using Grpc.Core;
using Google.Protobuf;
using RootMotion.FinalIK;

public class DataToAsync : MonoBehaviour
{
    public List<Transform> ControlModel = new List<Transform>();
    public List<Transform> HandModel = new List<Transform>();
    
    private string _dataKind = "Controller";
    private VRIK _vrik;

    private void Awake()
    {
        _vrik = GetComponentInChildren<VRIK>();
        SetControllerIk();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeToController(BroadCastMsg broadCastMsg)
    {
        _dataKind = "Controller";
        SetControllerIk();
    }

    public void ChangeToHand(BroadCastMsg broadCastMsg)
    {
        _dataKind = "Hand";
        SetHandIk();
    }
    
    private void SetControllerIk()
    {
        _vrik.solver.spine.headTarget = ControlModel[0];
        _vrik.solver.leftArm.target = ControlModel[1];
        _vrik.solver.rightArm.target = ControlModel[2];
    }

    private void SetHandIk()
    {
        _vrik.solver.spine.headTarget = HandModel[0];
        _vrik.solver.leftArm.target = HandModel[1];
        _vrik.solver.rightArm.target = HandModel[2];
    }

    // Update is called once per frame
    void Update()
    {
    }

    public BodyTrans GetBodyTrans()
    {
        if (_dataKind.Equals("Controller"))
        {
            return StaticFuncs.UnityToProto(ControlModel);
        }
        else if (_dataKind.Equals("Hand"))
        {
            return StaticFuncs.UnityToProto(HandModel);
        }
        else
        {
            return null;
        }
    }
}