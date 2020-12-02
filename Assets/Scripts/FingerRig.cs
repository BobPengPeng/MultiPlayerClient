using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerRig : MonoBehaviour
{
    public Transform HandPrefab;
    private OVRSkeleton _skeleton;

    private Transform _finger1;
    private Transform[] _fingerRigs;
    private Transform[] _localFingers;
    private bool StartAsync = false;
    // Start is called before the first frame update
    void Start()
    {
        _skeleton = HandPrefab.GetComponent<OVRSkeleton>();
        _localFingers = transform.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartAsync = true;
            _finger1 = HandPrefab.Find("Bones").Find("Hand_Start").Find("Hand_Index1");
            _fingerRigs = _finger1.GetComponentsInChildren<Transform>();
            Debug.Log(_fingerRigs.Length);
        }

        if (StartAsync)
        {
            if (_fingerRigs.Length != _localFingers.Length)
            {
                for (int i = 0; i < _localFingers.Length; i++)
                {
                    _localFingers[i].localRotation = _fingerRigs[i].localRotation;
                }
            }
        }
        
        
    }
}
