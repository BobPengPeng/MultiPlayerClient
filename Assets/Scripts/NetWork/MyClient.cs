using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Grpc.Core;
using DataAndNetwork;

public class MyClient
{
    private readonly NetWork.NetWorkClient _netWorkClient;
    private bool _isSendTransData = false;

    public delegate void BroadDelegate(BroadCastMsg broadCastMsg);
    public delegate void SetPlayerDelegate(BodyTransList bodyTransList);
    public delegate BodyTrans GetBodyTrans();

    public MyClient(NetWork.NetWorkClient netWorkClient)
    {
        _netWorkClient = netWorkClient;
    }

    public void SetSendState(bool b)
    {
        _isSendTransData = b;
    }

    //Client send action to server and server reply a action
    public GrpcFeedMsg GetAction(GrpcAction grpcAction)
    {
        GrpcFeedMsg feedMsg = _netWorkClient.TransAction(grpcAction);
        return feedMsg;
    }

    //client sent body transform
    public async Task SendBodyTrans(GetBodyTrans getBodyTrans)
    {
        using (var call = _netWorkClient.ServerGetBodyTrans())
        {
            while (_isSendTransData)
            {
                await call.RequestStream.WriteAsync(getBodyTrans());
                await Task.Delay((int) (Time.deltaTime * 1000f));
            }

            await call.RequestStream.CompleteAsync();
            // GrpcFeedMsg feedMsg = await call.ResponseAsync;
        }
    }
    
    public async Task GetBroadCast(BroadDelegate BroadFunc)
    {
        using (var call = _netWorkClient.GetBoradCast(new GrpcAction {ActionName = "Start BroadCast"}))
        {
            var responseStream = call.ResponseStream;
            while (await responseStream.MoveNext())
            {
                BroadFunc(responseStream.Current);
            }
        }
    }

    public async Task GetBodyTransList(SetPlayerDelegate setPlayerDelegate)
    {
        using (var call = _netWorkClient.ClientGetTransList(new GrpcAction {ActionName = "Ask Data"}))
        {
            var responseStream = call.ResponseStream;
            while (await responseStream.MoveNext())
            {
                setPlayerDelegate(responseStream.Current);
            }
        }
    }
}