using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOneReticle : MonoBehaviour
{
    public List<GameObject> reticles;
    //public Material roomClearMaterial;

    private int currReticleNo = 0;

    private void Start()
    {
        foreach (GameObject g in reticles)
        {
            // Set all GO as inactive
            g.SetActive(false);
        }
    }

    void Update()
    {
        // Set current reticle as active;
        reticles[currReticleNo].SetActive(true);

        // currReticleNo shows the current active reticle.
        // The next reticle will only appear when the current room is cleared.
        // When the current room is cleared, change the reticle material.

        switch (currReticleNo)
        {
            case 0:
                if (AllyMovement._roomsLeft == 4)
                {
                    //reticles[currReticleNo].GetComponent<MeshRenderer>().material = roomClearMaterial;
                    reticles[currReticleNo].SetActive(false);
                    currReticleNo = 1;
                }
                break;
            case 1:
                if (AllyMovement._roomsLeft == 3)
                {
                    //reticles[currReticleNo].GetComponent<MeshRenderer>().material = roomClearMaterial;
                    reticles[currReticleNo].SetActive(false);
                    currReticleNo = 2;
                }
                break;
            case 2:
                if (AllyMovement._roomsLeft == 2)
                {
                    //reticles[currReticleNo].GetComponent<MeshRenderer>().material = roomClearMaterial;
                    reticles[currReticleNo].SetActive(false);
                    currReticleNo = 3;
                }
                break;
            case 3:
                if (AllyMovement._roomsLeft == 1)
                {
                    //reticles[currReticleNo].GetComponent<MeshRenderer>().material = roomClearMaterial;
                    reticles[currReticleNo].SetActive(false);
                    currReticleNo = 4;
                }
                break;
            case 4:
                if (AllyMovement._roomsLeft == 0)
                {
                    //reticles[currReticleNo].GetComponent<MeshRenderer>().material = roomClearMaterial;
                    reticles[currReticleNo].SetActive(false);
                    Grading.success = true;
                }
                break;
        }
    }
}
