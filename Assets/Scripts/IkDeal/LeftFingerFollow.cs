using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeftFingerFollow : MonoBehaviour
{
    public Transform OculusHand;
    private OVRSkeleton.IOVRSkeletonDataProvider _dataProvider;
    private Transform _oculusHandStart;

    private OVRSkeleton.SkeletonPoseData _data;

    // Start is called before the first frame update
    void Start()
    {
        _dataProvider = OculusHand.GetComponent<OVRSkeleton.IOVRSkeletonDataProvider>();
    }

    // Update is called once per frame

    private void UpdateFingerPos()
    {
        _data = _dataProvider.GetSkeletonPoseData();
        if (_data.IsDataValid)
        {
            Transform modelNode_1;
            Transform questNode_1;
            Transform modelNode_2;
            Transform questNode_2;
            Transform modelNode_3;
            Transform questNode_3;

            if (_oculusHandStart == null)
            {
                _oculusHandStart = OculusHand.Find("Bones").Find("Hand_Start");
            }

            for (int i = 0; i < 5; i++)
            {
                questNode_1 = _oculusHandStart.GetChild(i + 1);
                modelNode_1 = transform.GetChild(i);
                modelNode_2 = modelNode_1.GetChild(0);
                questNode_2 = questNode_1.GetChild(0);
                modelNode_3 = modelNode_2.GetChild(0);
                questNode_3 = questNode_2.GetChild(0);

                modelNode_1.localRotation = questNode_1.localRotation;
                modelNode_2.localRotation = questNode_2.localRotation;
                modelNode_3.localRotation = questNode_3.localRotation;
                if (i == 0 || i == 4)
                {
                    modelNode_3.GetChild(0).localRotation = questNode_3.GetChild(0).localRotation;
                }
            }
        }
    }

    void Update()
    {
        UpdateFingerPos();
    }
}