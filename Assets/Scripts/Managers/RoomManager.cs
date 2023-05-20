using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance {get; private set;}

    struct RoomDataStruct
    {
        public int stake;
        public int balance;
    }
    struct JRoomDataStruct
    {
        public string roomCode;
        public int balance;
    }

    [SerializeField]
    TMP_InputField roomCodeInputField;

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

    //Socket Listening Functions
    void ListenAllEvents(){
        WebSocketManager.instance.ListenEvent("user-data",OnPlayerData);
        WebSocketManager.instance.ListenEvent("room-created",OnRoomCreated);
        WebSocketManager.instance.ListenEvent("room-joined",OnRoomJoined);
    }

    void OnPlayerData(string data){
        Debug.Log(data);
        SocketResponse<RoomPlayer> parsedData = JsonUtility.FromJson<SocketResponse<RoomPlayer>>(data);
        if(parsedData.success && parsedData.data.userId != null){
            Debug.Log(parsedData.data.userId);
            GameController.player = new Player(parsedData.data.userId,0);
        }
    }

    void OnRoomCreated(string data){
        SocketResponse<Room> parsedData = JsonUtility.FromJson<SocketResponse<Room>>(data);
        if(parsedData.success){
            Debug.Log(parsedData.data.roomCode);
            GameController.room = parsedData.data;

            SceneManager.LoadScene("Game");
        }
        // Debug.Log(GameController.room.roomCode);
    }

    void OnRoomJoined(string data){
        SocketResponse<Room> parsedData = JsonUtility.FromJson<SocketResponse<Room>>(data);
        if(parsedData.success){
            Debug.Log(data);
            GameController.room = parsedData.data;
            
            SceneManager.LoadScene("Game");
        }
    }



    //Socket Emitting Functions
    public void CreateRoom(){
        RoomDataStruct data = new RoomDataStruct{
            stake = 20,
            balance = 10000,
        };
        WebSocketManager.instance.EmitEvent("create-room",data);
    }

    public void JoinRoom(){
        if(roomCodeInputField.text != null && roomCodeInputField.text != ""){
            JRoomDataStruct data = new JRoomDataStruct{
                roomCode = roomCodeInputField.text,
                balance = 10000,
            };
            WebSocketManager.instance.EmitEvent("join-room",data);
        }
    }
}
