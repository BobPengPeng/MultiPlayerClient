using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTriggerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Script Start");
    }

    // Update is called once per frame
    void Update()
    {
        //单按钮触发
        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            Debug.Log("Button A Down");
        }
        
        if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
            Debug.Log("Button B Down");
        }
        
        if (OVRInput.GetDown(OVRInput.RawButton.RThumbstick))
        {
            Debug.Log("Right Stick Down");
        }
        
        //两个手指中的按钮
        if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > 0)
        {
            Debug.Log("Right Index Trigger : " + OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger));
        }
        
        if (OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger) > 0)
        {
            Debug.Log("Right Hand Trigger : " + OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger));
        }

        //摇杆位置（-1,1）
        if (OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).magnitude > 0)
        {
            Debug.Log("Right Stick : " + OVRInput.Get(OVRInput.RawAxis2D.RThumbstick));
        }
        
        //触摸触发
        if (OVRInput.Get(OVRInput.RawTouch.A))
        {
            Debug.Log("Button A Touch");
        }
        
        if (OVRInput.Get(OVRInput.RawTouch.B))
        {
            Debug.Log("Button B Touch");
        }
        
        if (OVRInput.Get(OVRInput.RawTouch.RThumbstick))
        {
            Debug.Log("Right Thumb Stikc Touch");
        }
        
        if (OVRInput.Get(OVRInput.RawTouch.RThumbRest))
        {
            Debug.Log("Right Thumb Rest");
        }
        
        if (OVRInput.Get(OVRInput.RawTouch.RIndexTrigger))
        {
            Debug.Log("Right Index Trigger Touch");
        }
        
        if (OVRInput.Get(OVRInput.RawTouch.RTouchpad))
        {
            Debug.Log("Right Touch Pad");
        }
    }
}
