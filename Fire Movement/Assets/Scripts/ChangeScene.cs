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

        //if (sceneName == "Tutorial")
        //{
        //    player.transform.position = new Vector3(0f, 0f, 0f);
        //    player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        //    Destroy(player);
        //}


    }
}
