using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance {get; private set;}

    private void Awake(){
        if(instance != null){
            Debug.Log("Found more than one RoomManager in scene");
        }
        instance = this;
    }

    public void ConnectToSocket(){
        WebSocketManager.OnConnected += OnConnected;
        if(!WebSocketManager.instance.connected){
            UIController.instance.toggleLoader(true);
            WebSocketManager.instance.Connect();
        }
        ListenAllEvents();
    }

    void OnConnected(){
        Debug.Log("Connected");
        UIController.instance.toggleLoader(false);
    }

    void ListenAllEvents(){

    }
}
