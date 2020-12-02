using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataAndNetwork;
using Google.Protobuf.Collections;
using UnityEngine;

public class BodyTransApply : MonoBehaviour
{
    private Transform _bodyDataPrefab;

    private RepeatedField<string> _asyncHost;
    private RepeatedField<bool> _asyncStates;
    private RepeatedField<BodyTrans> _asyncBodyTranses;

    public MyClient.SetPlayerDelegate SetPlayerFunc;

    // Start is called before the first frame update
    void Start()
    {
        _bodyDataPrefab = transform.GetChild(0);
        SetPlayerFunc = SetPlayerDeal;
    }

    public void AddPlayer(BroadCastMsg broadCastMsg)
    {
        if (transform.Find(broadCastMsg.Host))
        {
            return;
        }
        
        Transform newPlayer = Instantiate(_bodyDataPrefab, transform, true);
        newPlayer.name = broadCastMsg.Host;
        newPlayer.gameObject.SetActive(true);
    }

    public void RemovePlayer(BroadCastMsg broadCastMsg)
    {
        Transform removePlayer = transform.Find(broadCastMsg.Host);
        if (removePlayer)
        {
            Destroy(removePlayer.gameObject);
        }
    }

    private void SetPlayerDeal(BodyTransList bodyTransList)
    {
        _asyncHost = bodyTransList.DataSource;
        _asyncBodyTranses = bodyTransList.TransList;
        
        for (int i = 0; i < _asyncHost.Count; i++)
        {
            Transform player = transform.Find(_asyncHost[i]);
            if (player != null)
            {
                StaticFuncs.ProtoToUnity(_asyncBodyTranses[i], player.GetChild(0));
                player.GetChild(1).GetComponent<HandTransApply>().ApplyData(_asyncBodyTranses[i].LeftFingers, _asyncBodyTranses[i].RightFingers);
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {

    }
}