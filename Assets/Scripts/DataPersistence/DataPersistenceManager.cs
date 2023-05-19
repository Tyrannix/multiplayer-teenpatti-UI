using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System  .Linq;

public class DataPersistenceManager : MonoBehaviour
{

    [Header("File Storage Config")]
    [SerializeField] 
    private string fileName;

    [SerializeField] 
    private bool useEncryption;

    public static DataPersistenceManager instance {get; private set;}

    private UserData userData;

    private FileDataHandler fileDataHandler;

    private List<IDataPersistence> dataPersistences;

    private void Awake(){
        if(instance != null){
            Debug.Log("Found more than one DataPersistenceManager in scene");
        }
        instance = this;
    }

    private void Start()
    {
        fileDataHandler = new FileDataHandler(Application.persistentDataPath,fileName,useEncryption);
        dataPersistences = FindAllDataPersistences();
        CheckLogin();
    }

    private void CheckLogin(){
        
        userData = fileDataHandler.Load();
        
        if(userData == null){
            Debug.Log("User Not Logged In");
            createUserData();
        }
        else{
            loadData();
            Debug.Log("Checking Token");
            RequestHeader header = new RequestHeader {
                Key = "Authorization",
                Value = $"Bearer {userData.accessToken}"
            };
            List<RequestHeader> headers = new List<RequestHeader> { header };
            StartCoroutine(APIHelper.GetRequest("auth/verifyToken",CheckValidToken,headers));
        }
    }

    private void CheckValidToken(string data){
        // Debug.Log(data);
        var res = JsonUtility.FromJson<SocketResponse>(data);
        // Debug.Log(res.success);
        if(res.success && userData.userId != null){
            Debug.Log("User Already Logged In");
            // MenuManager.instance.ChangeScreen(1);
            RoomManager.instance.ConnectToSocket();
            MenuScreenManager.instance.ChangeScreen(MenuScreenManager.Screens.ModeScreen);
        }
    }

    private void createUserData(){
        userData = new UserData();
    }

    public void saveData(){
        foreach(IDataPersistence dataPersistence in dataPersistences){
                dataPersistence.SaveData(userData);
        }
        fileDataHandler.Save(userData);
        Debug.Log("Data Saved");
    }
    public void loadData(){
        foreach(IDataPersistence dataPersistence in dataPersistences){
                dataPersistence.LoadData(userData);
        }
    }

    private List<IDataPersistence> FindAllDataPersistences()
    {
        IEnumerable<IDataPersistence> dataPersistences = FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistence>();
        Debug.Log(dataPersistences.Count());
        return new List<IDataPersistence>(dataPersistences);
    }

}
