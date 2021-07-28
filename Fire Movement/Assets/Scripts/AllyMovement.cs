using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class AllyMovement : MonoBehaviour
{
    public Transform player;
    public List<GameObject> allies;
    public List<GameObject> enemies;

    public List<GameObject> ally1Movement;
    public List<GameObject> ally2Movement;
    public List<GameObject> ally3Movement;

    public List<GameObject> interimMovements;

    public float speed;
    public float targetDistance;
    public float allowedDistance;
    public RaycastHit hit;

    public TextMeshProUGUI commandCountText;
    public TextMeshProUGUI commandListText;
    public TextMeshProUGUI gradeText;
    public TextMeshProUGUI roomsLeftToClear;

    // Audio sources
    private AudioSource audioSource1, audioSource2;
    public AudioClip scanSound;
    public AudioClip leftClear;
    public AudioClip rightClear;

    public int roomsLeft;
    public static int _roomsLeft;
    public bool clearRoom;

    private Dictionary<string, Action> keywordActions = new Dictionary<string, Action>();
    private KeywordRecognizer keywordRecognizer;

    // To display
    private List<string> commandList; // stores all the words spoken
    private int commandCount = 0;
    public string grade = "F"; // default fail

    private string ally = "";
    private static GameObject currAlly = null;

    private bool updated;

    private void Awake()
    {
        _roomsLeft = roomsLeft;
    }

    private void Start()
    {
        // check which ally called
        keywordActions.Add("first", AllyChange);
        keywordActions.Add("second", AllyChange);
        keywordActions.Add("third", AllyChange);

        // actions to take
        keywordActions.Add("cover", Cover);
        keywordActions.Add("scan", Scan);
        keywordActions.Add("clear room", ClearRoom);

        // end scene
        keywordActions.Add("room clear", RoomClear);

        keywordRecognizer = new KeywordRecognizer(keywordActions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
        keywordRecognizer.Start();
        Debug.Log(keywordRecognizer.IsRunning);

        foreach (var device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
        }

        audioSource1 = allies[0].GetComponent<AudioSource>();
        audioSource2 = allies[1].GetComponent<AudioSource>();
    }

    private void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        ally = args.text;
        //commandList.Add(args.text);
        commandCount++;

        Debug.Log("Keyword: " + args.text);
        keywordActions[args.text].Invoke();
    }

    private void AllyChange()
    {
        if (ally == "first")
            currAlly = allies[0];
        else if (ally == "second")
            currAlly = allies[1];
        else if (ally == "third")
            currAlly = allies[2];
    }

    IEnumerator MoveToPoint(Vector3 pointA, Vector3 pointB, Vector3 pointC, Vector3 interimPoint)
    {
        float waitTime = 0.01f;
        //small number to make it smooth, 0.04 makes it execute 25 times / sec

        float step = speed * waitTime;
        Debug.Log(_roomsLeft);
        Debug.Log(interimPoint);

        // INTERIM 1st ================================================
        // Only do this for 2nd, 3rd and 4th
        if (_roomsLeft == 5 || _roomsLeft == 4 || _roomsLeft == 3)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);

                allies[0].GetComponent<Animator>().Play("Rifle Walk");
                allies[0].transform.position = Vector3.MoveTowards(allies[0].transform.position, interimPoint, step);
                allies[0].transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(allies[0].transform.forward, interimPoint, step, 0.0f));

                if (Vector3.Distance(allies[0].transform.position, interimPoint) < Vector3.kEpsilon)
                    break;
            }
        }

        // BASIC FIRST ALLY
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            allies[0].GetComponent<Animator>().Play("Rifle Walk");
            allies[0].transform.position = Vector3.MoveTowards(allies[0].transform.position, pointA, step);
            allies[0].transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(allies[0].transform.forward, pointA, step, 0.0f));

            if (Vector3.Distance(allies[0].transform.position, pointA) < Vector3.kEpsilon)
            {
                allies[0].GetComponent<Animator>().Play("Firing Rifle");
                Debug.Log("left clear");
                audioSource1.PlayOneShot(leftClear, 0.7f);
                break;
            }
        }

        // INTERIM 2nd ================================================
        // Only do this for the 1st, 3rd, 5th and 6th room
        if (_roomsLeft == 6 || _roomsLeft == 4 || _roomsLeft == 2 || _roomsLeft == 1)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);

                allies[1].GetComponent<Animator>().Play("Rifle Walk");
                allies[1].transform.position = Vector3.MoveTowards(allies[1].transform.position, interimPoint, step);
                allies[1].transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(allies[1].transform.forward, interimPoint, step, 0.0f));

                if (Vector3.Distance(allies[1].transform.position, interimPoint) < Vector3.kEpsilon)
                    break;
            }
        }

        // BASIC 2ND ALLY
        while (true) 
        {
            yield return new WaitForSeconds(waitTime);

            allies[1].GetComponent<Animator>().Play("Rifle Walk");
            allies[1].transform.position = Vector3.MoveTowards(allies[1].transform.position, pointB, step);
            allies[1].transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(allies[1].transform.forward, pointB, step, 0.0f));

            if (Vector3.Distance(allies[1].transform.position, pointB) < Vector3.kEpsilon)
            {
                allies[1].GetComponent<Animator>().Play("Firing Rifle");
                Debug.Log("right clear");
                audioSource2.PlayOneShot(rightClear, 0.7f);
                break;
            }
        }

        // INTERIM 3rd ================================================
        // Only do this for 4th
        if (_roomsLeft == 3)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);

                allies[2].GetComponent<Animator>().Play("Rifle Walk");
                allies[2].transform.position = Vector3.MoveTowards(allies[2].transform.position, interimPoint, step);
                allies[2].transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(allies[2].transform.forward, interimPoint, step, 0.0f));

                if (Vector3.Distance(allies[2].transform.position, interimPoint) < Vector3.kEpsilon)
                    break;
            }
        }

        // BASIC 3RD ALLY
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            allies[2].GetComponent<Animator>().Play("Rifle Walk");
            allies[2].transform.position = Vector3.MoveTowards(allies[2].transform.position, pointC, step);
            allies[2].transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(allies[2].transform.forward, pointC, step, 0.0f));

            if (Vector3.Distance(allies[2].transform.position, pointC) < Vector3.kEpsilon)
            {
                allies[2].GetComponent<Animator>().Play("Firing Rifle");
                break;
            }
        }
    }

    // ================================= COMMANDS =================================
    private void ClearRoom()
    {
        // When this command is activated, the operation starts.
        // Move the players into the room
        // 1st ally move forward with the door position
        // 2nd ally move sideways
        // 3rd ally moves forward as well
        // Commander is supposed to go in as well.

        float step = speed * Time.deltaTime;

        switch (_roomsLeft)
        {
            // Living room, [1]
            case 6:
                Debug.Log("Clearing the living room");
                Vector3 pointA = ally1Movement[1].transform.position;
                Vector3 pointB = ally2Movement[1].transform.position;
                Vector3 pointC = ally3Movement[1].transform.position;

                // Ally 2 to move through interim in this scenario
                Vector3 interim = interimMovements[0].transform.position; // interim

                StartCoroutine(MoveToPoint(pointA, pointB, pointC, interim));
                Debug.Log("Clearing the living room end");
                break;

            // bomb room
            case 5:
                Debug.Log("Clearing the bomb room");
                pointA = ally1Movement[3].transform.position;
                pointB = ally2Movement[3].transform.position;
                pointC = ally3Movement[3].transform.position;

                // Ally 1 and 2
                interim = interimMovements[1].transform.position; // interim

                StartCoroutine(MoveToPoint(pointA, pointB, pointC, interim));
                Debug.Log("Clearing the bomb room end");
                break;

            // bedroom 1
            case 4:
                Debug.Log("Clearing the bedroom 1");
                pointA = ally1Movement[5].transform.position;
                pointB = ally2Movement[5].transform.position;
                pointC = ally3Movement[5].transform.position;

                // Ally 1 and 2
                interim = interimMovements[2].transform.position; // interim

                StartCoroutine(MoveToPoint(pointA, pointB, pointC, interim));
                Debug.Log("Clearing the bedroom 1 end");
                break;

            // outside toilet
            case 3:
                Debug.Log("Clearing the outside toilet");
                pointA = ally1Movement[7].transform.position;
                pointB = ally2Movement[7].transform.position;
                pointC = ally3Movement[7].transform.position;

                // Ally 1 and 2
                interim = interimMovements[3].transform.position; // interim

                StartCoroutine(MoveToPoint(pointA, pointB, pointC, interim));
                Debug.Log("Clearing the outside toilet end");
                break;

            // bedroom 2
            case 2:
                Debug.Log("Clearing the bedroom 2");
                pointA = ally1Movement[9].transform.position;
                pointB = ally2Movement[9].transform.position;
                pointC = ally3Movement[9].transform.position;

                // Ally 2
                interim = interimMovements[4].transform.position; // interim

                StartCoroutine(MoveToPoint(pointA, pointB, pointC, interim));
                Debug.Log("Clearing the bedroom 2 end");
                break;

            // master bedroom
            case 1:
                Debug.Log("Clearing the master bedroom");
                pointA = ally1Movement[11].transform.position;
                pointB = ally2Movement[11].transform.position;
                pointC = ally3Movement[11].transform.position;

                // Ally 2
                interim = interimMovements[5].transform.position; // interim

                StartCoroutine(MoveToPoint(pointA, pointB, pointC, interim));
                Debug.Log("Clearing the master bedroom end");
                break;
        }
    }

    private void RoomClear()
    {
        // When this command is activated, the room is cleared.
        // Mark the room as completed
        _roomsLeft--;
    }

    private void Cover()
    {
        Debug.Log("cover");

        currAlly.GetComponent<Animator>().Play("Firing Rifle");
        currAlly.transform.RotateAround(currAlly.transform.position, currAlly.transform.up, 180f);

        currAlly = null;
    }

    private void Scan()
    {
        Debug.Log("scan");

        currAlly.GetComponent<Animator>().Play("Scan");
        currAlly = null;

        // No IED, no booby trap, door swing inwards.
        audioSource1.PlayOneShot(scanSound, 0.7f);
    }

    // ================================= RESET POSITIONS =================================

    // Respawn allies behind user
    public void moveToStartingPositions()
    {
        // Allies positions are preassigned based on the lists created above:
        // ally1Movement, ally2Movement, ally3Movement

        // When player first steps onto the reticle, move the allies to their starting positions
        allies[0].transform.position = ally1Movement[0].transform.position;
        allies[1].transform.position = ally2Movement[0].transform.position;
        allies[2].transform.position = ally3Movement[0].transform.position;

        allies[2].transform.rotation = Quaternion.Euler(0, 130, 0);
    }

    public void moveToPositions()
    {
        if (Reticle1Trigger.triggered)
        {
            Reticle1Trigger.triggered = false;

            foreach (GameObject ally in allies)
                ally.GetComponent<Animator>().Play("Scan");

            allies[0].transform.position = ally1Movement[2].transform.position;
            allies[1].transform.position = ally2Movement[2].transform.position;
            allies[2].transform.position = ally3Movement[2].transform.position;

            allies[0].transform.rotation = ally1Movement[2].transform.rotation;
            allies[1].transform.rotation = ally2Movement[2].transform.rotation;
            allies[2].transform.rotation = ally3Movement[2].transform.rotation;
            Debug.Log("reticle 1 moved");
        } 
        else if (Reticle2Trigger.triggered)
        {
            Reticle2Trigger.triggered = false;

            foreach (GameObject ally in allies)
                ally.GetComponent<Animator>().Play("Scan");
            
            allies[0].transform.position = ally1Movement[4].transform.position;
            allies[1].transform.position = ally2Movement[4].transform.position;
            allies[2].transform.position = ally3Movement[4].transform.position;

            allies[0].transform.rotation = ally1Movement[4].transform.rotation;
            allies[1].transform.rotation = ally2Movement[4].transform.rotation;
            allies[2].transform.rotation = ally3Movement[4].transform.rotation;
            Debug.Log("reticle 2 moved");
        }
        else if (Reticle3Trigger.triggered)
        {
            Reticle3Trigger.triggered = false;

            foreach (GameObject ally in allies)
                ally.GetComponent<Animator>().Play("Scan");

            allies[0].transform.position = ally1Movement[6].transform.position;
            allies[1].transform.position = ally2Movement[6].transform.position;
            allies[2].transform.position = ally3Movement[6].transform.position;

            allies[0].transform.rotation = ally1Movement[6].transform.rotation;
            allies[1].transform.rotation = ally2Movement[6].transform.rotation;
            allies[2].transform.rotation = ally3Movement[6].transform.rotation;
            Debug.Log("reticle 3 moved");
        }
        else if (Reticle4Trigger.triggered)
        {
            Reticle4Trigger.triggered = false;

            foreach (GameObject ally in allies)
                ally.GetComponent<Animator>().Play("Scan");

            allies[0].transform.position = ally1Movement[8].transform.position;
            allies[1].transform.position = ally2Movement[8].transform.position;
            allies[2].transform.position = ally3Movement[8].transform.position;

            allies[0].transform.rotation = ally1Movement[8].transform.rotation;
            allies[1].transform.rotation = ally2Movement[8].transform.rotation;
            allies[2].transform.rotation = ally3Movement[8].transform.rotation;
            Debug.Log("reticle 4 moved");
        }
        else if (Reticle5Trigger.triggered)
        {
            Reticle5Trigger.triggered = false;

            foreach (GameObject ally in allies)
                ally.GetComponent<Animator>().Play("Scan");

            allies[0].transform.position = ally1Movement[10].transform.position;
            allies[1].transform.position = ally2Movement[10].transform.position;
            allies[2].transform.position = ally3Movement[10].transform.position;

            allies[0].transform.rotation = ally1Movement[10].transform.rotation;
            allies[1].transform.rotation = ally2Movement[10].transform.rotation;
            allies[2].transform.rotation = ally3Movement[10].transform.rotation;
            Debug.Log("reticle 5 moved");
        }
    }

    // ================================= TEXT DISPLAY =================================
    // Grades are assigned based on completeness of commands (TODO)
    void AssignGrades()
    {
        if (commandCount > 20)
            grade = "A";
        else if (commandCount > 18)
            grade = "B";
        else if (commandCount > 10)
            grade = "C";
        else
            grade = "F";
        
        gradeText.text = String.Format("{0}", grade);
    }

    void DisplayText(int count, List<String> text)
    {
        commandCountText.text = string.Format("{0}", count);
        commandListText.text = "";
    }

    void Update()
    {
        // Testing purposes
        // if (clearRoom) ClearRoom();

        // Respawn allies behind user when the first reticle is stepped on
        if (TimerTrigger.triggered && !updated)
        {
            moveToStartingPositions();
            updated = true;
        }

        moveToPositions();

        // Display text
        AssignGrades();
        DisplayText(commandCount, commandList);
        roomsLeftToClear.text = String.Format("{0}", _roomsLeft);

        //foreach (string str in commandList)
        //{
        //    commandListText.text = string.Format("{0}\n", str);
        //}
    }

    private void OnApplicationQuit()
    {
        if (keywordRecognizer != null && keywordRecognizer.IsRunning)
        {
            keywordRecognizer.OnPhraseRecognized -= OnKeywordsRecognized;
            keywordRecognizer.Stop();
        }
    }
}
