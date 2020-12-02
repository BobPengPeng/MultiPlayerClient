using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowFinger : MonoBehaviour
{
    public Transform ThumbFinger;
    public Transform IndexFinger;
    public Transform MiddleFinger;

    public Transform ThumbTip;
    public Transform IndexTip;
    public Transform MiddleTip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ThumbTip.position = ThumbFinger.position;
        IndexTip.position = IndexFinger.position;
        MiddleTip.position = MiddleFinger.position;
    }
}
