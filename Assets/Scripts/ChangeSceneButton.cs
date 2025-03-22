using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour
{
    public void back(){
        SceneManager.LoadScene("Main Menu");
    }
}
