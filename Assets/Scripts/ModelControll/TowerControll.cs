using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControll : MonoBehaviour
{
    public Transform RotaTransform;
    public Transform HolkTransform;
    public Transform Line;
    public Transform Hook;

    private float _rotaTower = 0;
    private float _theNode = 0;
    private float _hook = 0;

    // Start is called before the first frame update
    void Start()
    {
    }
    public void MoveX(float f)
    {
        _rotaTower = f;
    }

    public void MoveZ(float f)
    {
        Debug.Log(f);
        _theNode = f;
    }
    
    // Update is called once per frame
    void Update()
    {
        RotaTransform.localRotation = Quaternion.Euler(RotaTransform.localRotation.eulerAngles + new Vector3(0, _rotaTower * 0.5f, 0));
        HolkTransform.localPosition += new Vector3(_theNode * HolkTransform.localScale.x * 0.00015f, 0, 0);        
    }
}