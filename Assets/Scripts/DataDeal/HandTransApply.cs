using System.Collections;
using System.Collections.Generic;
using DataAndNetwork;
using Google.Protobuf.Collections;
using UnityEngine;

public class HandTransApply : MonoBehaviour
{
    public Transform LeftHand;
    public Transform RightHand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ApplyData(RepeatedField<UnityTrans> leftHandProto, RepeatedField<UnityTrans> rightHandProto)
    {
        StaticFuncs.ProtoToUnity(leftHandProto, LeftHand);
        StaticFuncs.ProtoToUnity(rightHandProto, RightHand);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
