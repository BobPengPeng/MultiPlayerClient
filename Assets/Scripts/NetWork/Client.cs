using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Grpc.Core;
using UnityEngine;
using DataAndNetwork;

public class Client : MonoBehaviour
{
    // Start is called before the first frame update
    public string IP = "192.168.43.96:30052";
    private static Channel _channel;
    private MyClient _myClient;
    private DataToAsync _asyncData;
    private BroadCastDeal _broadCastDeal;
    private BodyTransApply _bodyTransApply;

    // private MyClient.GetBodyTrans _getBodyTrans;
    void Start()
    {
        _channel = new Channel(IP, ChannelCredentials.Insecure);
        _myClient = new MyClient(new NetWork.NetWorkClient(_channel));
        
        _broadCastDeal = GetComponent<BroadCastDeal>();
        _asyncData = GetComponentInChildren<DataToAsync>();
        _bodyTransApply = GetComponentInChildren<BodyTransApply>();
    }


    public void DoAction(string str)
    {
        _myClient.GetAction(new GrpcAction{ActionName = str});
    }
    
    public void BroadCastOn()
    {
        _myClient.GetBroadCast(_broadCastDeal.BroadFunc);
    }

    public void BroadCastOff()
    {
        _myClient.GetAction(new GrpcAction {ActionName = "Stop BroadCast"});
    }

    public void AddPlayer()
    {
        _myClient.GetAction(new GrpcAction{ActionName = "Add Player"});
    }

    public void RemovePlayer()
    {
        _myClient.GetAction(new GrpcAction{ActionName = "Remove Player"});
    }

    public void ChangeToController()
    {
        _myClient.GetAction(new GrpcAction{ActionName = "Change To Controller"});
    }

    public void ChangeToHand()
    {
        _myClient.GetAction(new GrpcAction{ActionName = "Change To Hand"});
    }
    
    public void SendPlayerData()
    {
        _myClient.SetSendState(true);
        _myClient.SendBodyTrans(_asyncData.GetBodyTrans);
    }

    public void StopPlayerData()
    {
        _myClient.SetSendState(false);
    }

    public void StartApplyData()
    {
        _myClient.GetBodyTransList(_bodyTransApply.SetPlayerFunc);
    }

    public void StopApplyData()
    {
        _myClient.GetAction(new GrpcAction {ActionName = "Stop Apply Data"});
    }
    
    private void OnApplicationQuit()
    {
        if (_channel.State != ChannelState.Shutdown)
        {
            _channel.ShutdownAsync();
        }
    }

    private void OnDisable()
    {
        if (_channel.State != ChannelState.Shutdown)
        {
            _channel.ShutdownAsync();
        }
    }
    
    void Update()
    {
        
    }
}