using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOneReticle : MonoBehaviour
{
    public List<GameObject> reticles;
    public Material roomClearMaterial;

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
        Debug.Log(currReticleNo);

        // currReticleNo shows the current active reticle.
        // The next reticle will only appear when the current room is cleared.
        // When the current room is cleared, change the reticle material.

        switch (currReticleNo)
        {
            case 0:
                if (Reticle1Trigger.triggered && AllyMovement.roomsCleared == 1)
                {
                    reticles[currReticleNo].GetComponent<MeshRenderer>().material = roomClearMaterial;
                    currReticleNo = 1;
                }
                break;
            case 1:
                if (Reticle2Trigger.triggered && AllyMovement.roomsCleared == 2)
                {
                    reticles[currReticleNo].GetComponent<MeshRenderer>().material = roomClearMaterial;
                    currReticleNo = 2;
                }
                break;
            case 2:
                if (Reticle3Trigger.triggered && AllyMovement.roomsCleared == 3)
                {
                    reticles[currReticleNo].GetComponent<MeshRenderer>().material = roomClearMaterial;
                    currReticleNo = 3;
                }
                break;
            case 3:
                if (Reticle4Trigger.triggered && AllyMovement.roomsCleared == 4)
                {
                    reticles[currReticleNo].GetComponent<MeshRenderer>().material = roomClearMaterial;
                    currReticleNo = 4;
                }
                break;
            case 4:
                if (Reticle5Trigger.triggered && AllyMovement.roomsCleared == 5)
                {
                    reticles[currReticleNo].GetComponent<MeshRenderer>().material = roomClearMaterial;
                    Grading.success = true;
                }
                break;
        }
    }
}
