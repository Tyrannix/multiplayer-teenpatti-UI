using System.Collections;
using System.Collections.Generic;
using System;
using System.Net.Mail;
using UnityEngine;
using TMPro;

public class LoginServices : MonoBehaviour,IDataPersistence
{
    [SerializeField]
    private TMP_InputField username;
    
    [SerializeField]
    private TMP_InputField password;

    [Serializable]
    private class LoginData{
        public string email;
        public int mobileNumber;
        public string password;
    }
    [Serializable]
    private class UsersData{
        public string _id;
    }
    
    [Serializable]
    private class TokenData{
        public Token access;
        public Token refresh;
    }

    [Serializable]
    private class Token{
        public string token;
        public int expiryin;
    }

    [Serializable]
    private class LoginResponse{
        public bool success;
        public TokenData token;
        public UsersData data;
    }

    private LoginResponse res;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadData(UserData data){

    }

    public void SaveData(UserData data){
        Debug.Log($"Saving Token {res}");
        data.accessToken = res.token.access.token;
        data.userId = res.data._id;
        // MenuManager.instance.ChangeScreen(1);
        MenuScreenManager.instance.ChangeScreen(MenuScreenManager.Screens.ModeScreen);
    }

    public void Login(){

        LoginData? data = validateData(username.text,password.text);
        if(data != null){
            string body = JsonUtility.ToJson(data);
            Debug.Log(body);
            // setup the request header
            RequestHeader header = new RequestHeader {
                Key = "Content-Type",
                Value = "application/json"
            };
            List<RequestHeader> headers = new List<RequestHeader> { header };
            UIController.instance.toggleLoader(true);
            StartCoroutine(APIHelper.PostRequest("auth/login",body,getLoginData,headers));
        }
    }

    void getLoginData(string? data){
        UIController.instance.toggleLoader(false);
        res = JsonUtility.FromJson<LoginResponse>(data);
        if(res != null && res.success){
            DataPersistenceManager.instance.saveData();
            DataPersistenceManager.instance.loadData();
            // RoomManager.instance.Invoke("ConnectToSocket",0.5f);
            RoomManager.instance.ConnectToSocket();
        }
    }


    private static LoginData? validateData(string username,string password){
        string? email = null;
        int? number = null;
        if(IsValidEmail(username)){
            email = username;
        }
        else {
            var check = Int32.TryParse(username,out int value);
            if(check){
                number = value;
            }
        }

        if((email != null || number != null) && password != ""){
           return new LoginData {email = email,mobileNumber=number==null ? 0 : (int)number,password = password};
        }

        return null;
    }

    private static bool IsValidEmail(string email)
    { 
        var valid = true;
        
        try
        { 
            var emailAddress = new MailAddress(email);
        }
        catch
        {
            valid = false;
        }

        return valid;
    }
}
