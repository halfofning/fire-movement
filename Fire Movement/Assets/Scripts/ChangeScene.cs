using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public GameObject player;
    public void LoadScene(string sceneName)
    {
        if (sceneName == "FireMovement")
        {
            Destroy(player);
        }

        SceneManager.LoadScene(sceneName);
    }
}
