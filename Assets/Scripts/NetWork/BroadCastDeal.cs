using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataAndNetwork;
using Google.Protobuf.Collections;
using UnityEngine;

public class BroadCastDeal : MonoBehaviour
{
    private GameManager _gameManager;
    public MyClient.BroadDelegate BroadFunc;
    private TowerControll _towerControll;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GetComponent<GameManager>();
        BroadFunc = BroadCast;
        _towerControll = GameObject.Find("TowerModel").GetComponent<TowerControll>();
    }

    private void BroadCast(BroadCastMsg broadCastMsg)
    {
        if (broadCastMsg.MsgType.Equals("Add Player"))
        {
            _gameManager.AddPlayer(broadCastMsg);
        }

        if (broadCastMsg.MsgType.StartsWith("MoveX"))
        {
            string str = broadCastMsg.MsgType.Split(':')[1];
            float f = float.Parse(str);
            _towerControll.MoveX(f);
        }
        
        if (broadCastMsg.MsgType.StartsWith("MoveZ"))
        {
            string str = broadCastMsg.MsgType.Split(':')[1];
            float f = float.Parse(str);
            _towerControll.MoveZ(f);
        }
        
        if (broadCastMsg.MsgType.Equals("Remove Player"))
        {
            _gameManager.RemovePlayer(broadCastMsg);
        }

        if (broadCastMsg.MsgType.Equals("Change To Controller"))
        {
            _gameManager.ChangeToController(broadCastMsg);
        }
        
        if(broadCastMsg.MsgType.Equals("Change To Hand"))
        {
            _gameManager.ChangeToHand(broadCastMsg);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
    }
}