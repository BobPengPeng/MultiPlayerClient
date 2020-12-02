using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAction : MonoBehaviour
{
    private Vector3 _outBallScale;
    private Vector3 _limitBallScale;
    private Vector3 _insideBallScale;

    private float _minEffectDis;
    private float _maxEffectDis;

    private Client _client;
    // Start is called before the first frame update
    void Start()
    {
        _insideBallScale = transform.localScale;
        _limitBallScale = transform.parent.Find("LimitBall").localScale;
        _outBallScale = transform.parent.Find("OutBall").localScale;
        _minEffectDis = (_limitBallScale.x - _insideBallScale.x) / 2;
        _maxEffectDis = (_outBallScale.x - _insideBallScale.x) / 2;
        _client = GameObject.Find("GameManager").GetComponent<Client>();
    }

    private void AstrictBall()
    {
        float dis = transform.localPosition.magnitude;
        if (dis > _maxEffectDis)
        {
            transform.localPosition = transform.localPosition * _maxEffectDis / dis;
        }
    }

    private Vector3 GetMoveData()
    {
        float dis = transform.localPosition.magnitude;
        if (dis < _minEffectDis)
        {
            return Vector3.zero;
        }
        else
        {
            return transform.localPosition.normalized * ((dis - _minEffectDis) / (_maxEffectDis - _minEffectDis));
        }
    }
    
    public void BackToOrigin()
    {
        StartCoroutine(BackOrigin());
    }

    public void StopCoro()
    {
        StopAllCoroutines();
    }
    
    IEnumerator BackOrigin()
    {
        while (transform.localPosition.magnitude > 0.0f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, 0.5f);
            yield return 1;
        }
        yield return 0;
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        AstrictBall();
        Vector3 vector3 = GetMoveData();
        float x = -vector3.x;
        _client.DoAction("MoveX:" + x);
        float z = -vector3.z;
        _client.DoAction("MoveZ:" + z);
    }
}
