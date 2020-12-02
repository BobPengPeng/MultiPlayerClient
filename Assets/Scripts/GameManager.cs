using System.Collections;
using System.Collections.Generic;
using System.Net;
using DataAndNetwork;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Client _client;
    private BodyTransApply _bodyTransApply;
    private DataToAsync _dataToAsync;

    private string _localIp;

    // Start is called before the first frame update
    void Start()
    {
        _client = GetComponent<Client>();
        _bodyTransApply = GameObject.Find("OtherPlayers").GetComponent<BodyTransApply>();
        _dataToAsync = GetComponentInChildren<DataToAsync>();
        _localIp = GetLocalIp();
        StartCoroutine(StartTrans());
    }

    public void AddPlayer(BroadCastMsg broadCastMsg)
    {
        if (!broadCastMsg.Host.Contains(_localIp))
        {
            _bodyTransApply.AddPlayer(broadCastMsg);
        }
    }

    public string GetLocalIp()
    {
        ///获取本地的IP地址
        string AddressIP = string.Empty;
        foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
        {
            if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
            {
                AddressIP = _IPAddress.ToString();
            }
        }

        return AddressIP;
    }

    public void RemovePlayer(BroadCastMsg broadCastMsg)
    {
        _bodyTransApply.RemovePlayer(broadCastMsg);
    }

    public void ChangeToController(BroadCastMsg broadCastMsg)
    {
        _dataToAsync.ChangeToController(broadCastMsg);
    }

    public void ChangeToHand(BroadCastMsg broadCastMsg)
    {
        _dataToAsync.ChangeToHand(broadCastMsg);
    }

    // Update is called once per frame
    IEnumerator StartTrans()
    {
        yield return new WaitForSeconds(3);
        _client.BroadCastOn();
        yield return new WaitForSeconds(1);
        _client.AddPlayer();
        yield return new WaitForSeconds(1);
        _client.SendPlayerData();
        yield return new WaitForSeconds(1);
        _client.StartApplyData();
        // _client.ChangeToHand();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _client.BroadCastOn();
            _client.AddPlayer();
            _client.SendPlayerData();
            _client.StartApplyData();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            _client.RemovePlayer();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _client.StopPlayerData();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _client.StopApplyData();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            _client.BroadCastOff();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _client.AddPlayer();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            _client.SendPlayerData();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _client.StartApplyData();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _client.BroadCastOn();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            _client.ChangeToController();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            _client.ChangeToHand();
        }
    }
}