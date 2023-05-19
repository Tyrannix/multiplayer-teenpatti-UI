using System;
using System.Collections.Generic;
using UnityEngine;
using Firesplash.UnityAssets.SocketIO;

public class WebSocketManager: MonoBehaviour,IDataPersistence{
    // protected SocketIOUnity _socket;

    public static WebSocketManager instance {get; private set;}

    public SocketIOCommunicator _socket;

    private SIOAuthPayload auth;

    private string token;
    public delegate void ConnectedAction();
    public static event ConnectedAction OnConnected;

    public bool connected = false;

    private void Awake(){
        if(instance != null){
            Debug.Log("Found more than one Socket in scene");
        }
        instance = this;
    }


    void Start(){
        DontDestroyOnLoad(gameObject);
    }
    public void Connect(){
        Debug.Log("Connecting...");
        // _socket = new SocketIOUnity("http://192.168.0.103:5020",new SocketIOOptions{
        //     ExtraHeaders=new Dictionary<string, string>{
        //         {
        //             "authorization","eyJhbGciOiJIUzUxMiJ9.ZXlKaGJHY2lPaUpJVXpNNE5DSXNJblI1Y0NJNklrcFhWQ0o5LmV5SnRiMkpwYkdVaU9pSTRNREF3T0RZMk5EZzVJaXdpWlcxaGFXd2lPaUpyWVd4d1pYTm9RSGx2Y0cxaGFXd3VZMjl0SWl3aVptbHljM1JmYm1GdFpTSTZJbXRoYkhCbGMyZ2lMQ0pzWVhOMFgyNWhiV1VpT2lKd1lYUmxiQ0lzSW5WelpYSkpaQ0k2SW5WSGVVcHNNVXhNWWxJNFltNW1lakkwTUc1R1lTSXNJbkp2YkdVaU9pSlZjMlZ5SWl3aWNtOXNaVWxrSWpvaVJqbFZielJTWW5jME5XcFRTWE5HWVd0MFVEUnNJaXdpYjNCbGNtRjBiM0pmWW5raU9pSmhObGhtYVZod2JXWjRRbkI1ZVVsVk1Hb3RPWElpTENKamRYSnlaVzVqZVY5cFpDSTZJa1pvYkV4WE56bFFNM0J4Ym1wMmRFTXdlR2xyVENJc0ltOXdaWEpoZEc5eVgzVnpaWEpmYVdRaU9pSjBZVzVoZVRjMUlpd2lZM1Z5Y21WdVkzbGZibUZ0WlNJNklsVlRSQ0lzSW1saGRDSTZNVFkzT0RnMk1qQTBPSDAuaVdmd01yczJSaUJld2EtNXVHLXRGNElDSUw1ZnJRLXdoMllBY3hjZ09TRXpkdGdIREpFQmxxdE11Q2lHRVI3dA.Fr_NHNz-3GQxbsXah73lHdXvHU2fvepTn4MXeuA_mcFqRjPae9p_dfZa1pAOpYQSIPGGYUbdyYggkn0GIR2xMA"
        //         }
        //     }
        // });
        // _socket.OnConnected += onSocketConnected;
        // _socket.Connect();
        _socket.Instance.On("connected",onSocketConnected);
        auth = new SIOAuthPayload();
        auth.AddElement("authorization",token);
        Debug.Log(token);
        // _socket.Instance.Connect(auth);
        _socket.Instance.Connect("https://api-ludo-multiplayer.odinflux.com",true,auth);
    }

    public void Disconnect(){
        _socket.Instance.Close();
    }

    void onSocketConnected(string data){
        Debug.Log("connected socket");
        OnConnected();
    }

    public void EmitEvent(string eventName,object data){
        Debug.Log(JsonUtility.ToJson(data));
        _socket.Instance.Emit(eventName,JsonUtility.ToJson(data),false);
    }

    public void ListenEvent(string eventName,Firesplash.UnityAssets.SocketIO.SocketIOInstance.SocketIOEvent callback){
        _socket.Instance.On(eventName,callback);
    }

    void IDataPersistence.LoadData(UserData userData)
    {
        token = userData.accessToken;
    }

    void IDataPersistence.SaveData(UserData userData)
    {
        
    }
}