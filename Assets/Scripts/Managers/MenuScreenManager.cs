using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreenManager : MonoBehaviour
{
    [SerializeField]
    GameObject LoginScreen;
    [SerializeField]
    GameObject ModeScreen;
    [SerializeField]
    GameObject JoinRoomScreen;
    [SerializeField]
    GameObject CreateRoomScreen;


    List<GameObject> screens;

    public static MenuScreenManager instance {get; private set;}

    private void Awake(){
        if(instance != null){
            Debug.Log("Found more than one MenuScreenManager in scene");
        }
        instance = this;
    }

    public enum Screens {
        LoginScreen = 0,ModeScreen = 1 ,JoinRoomScreen = 2,CreateRoomScreen =3
    }

    public int currScreen = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        screens = new List<GameObject>(){LoginScreen,ModeScreen,JoinRoomScreen,CreateRoomScreen};
        ChangeScreen(Screens.LoginScreen);
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android){
            if (Input.GetKey(KeyCode.Escape))
            {
                GoBack();
            }
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            GoBack();
        }
    }

    public void ChangeScreenFromIndex(int screenIndex){
        ChangeScreen((Screens)screenIndex);
    }

    public void ChangeScreen(Screens screen){
        int i = 0;
        currScreen = (int)screen;
        screens.ForEach((_screen)=>{
            _screen.SetActive(false);
            if(i == currScreen) _screen.SetActive(true);
            i++;
        });
    }

    void GoBack(){
        if(currScreen != 0){
            currScreen--;
            ChangeScreenFromIndex(currScreen);
        }
    }

    public void StartGame(){
        SceneManager.LoadScene("Game");
    }
}
