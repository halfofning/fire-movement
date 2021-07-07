using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using TMPro;

public class AllyMovement : MonoBehaviour
{
    public Transform player;
    public List<GameObject> allies;
    public List<GameObject> enemies;
    
    public float speed;
    public float targetDistance;
    public float allowedDistance;
    public RaycastHit hit;

    public TextMeshProUGUI commandCountText;
    public TextMeshProUGUI commandListText;

    private Dictionary<string, Action> keywordActions = new Dictionary<string, Action>();
    private KeywordRecognizer keywordRecognizer;

    // To display
    private List<string> spokenWords; // stores all the words spoken
    private int commandCount = 0;

    private string ally = "";
    private GameObject currAlly = null;

    private int layerMask = 1 << 9;

    private void Start()
    {
        // check which ally called
        keywordActions.Add("first man", AllyChange);
        keywordActions.Add("second man", AllyChange);
        keywordActions.Add("third man", AllyChange);

        keywordActions.Add("cover", Cover);
        keywordActions.Add("scan", Scan);

        keywordRecognizer = new KeywordRecognizer(keywordActions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
        keywordRecognizer.Start();
        Debug.Log(keywordRecognizer.IsRunning);

        foreach (var device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
        }
    }

    private void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        ally = args.text;
        spokenWords.Add(args.text);
        Debug.Log("Keyword: " + args.text);
        keywordActions[args.text].Invoke();
    }

    private void AllyChange()
    {
        if (ally == "first")
            currAlly = allies[0];
        else
            currAlly = allies[1];
    }

    private void Cover()
    {
        Debug.Log("cover");

        currAlly.GetComponent<Animator>().Play("Firing Rifle");
        currAlly = null;
    }

    private void Scan()
    {
        Debug.Log("scan");

        currAlly.GetComponent<Animator>().Play("Scan");
        currAlly = null;
    }

    void Update()
    {
        foreach (GameObject ally in allies)
        {
            // Make allies look at player
            //Debug.Log(ally);
            //Vector3 direction = player.transform.position - ally.transform.position;
            //direction.y = 0;

            //ally.transform.rotation = Quaternion.Slerp(ally.transform.rotation,
            //                        Quaternion.LookRotation(direction), 0.1f);

            //if (direction.magnitude > 5)
            //{
            //    ally.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
            //    ally.GetComponent<Animator>().Play("Rifle Walk");
            //} else
            //{
            //    speed = 0;
            //    ally.GetComponent<Animator>().Play("Idle");
            //}

            // Ally look at player
            ally.transform.LookAt(player.transform);

            // Follow player
            if (Physics.Raycast(ally.transform.position, ally.transform.TransformDirection(Vector3.forward), out hit, layerMask))
            {
                Vector3 direction = player.transform.position - ally.transform.position;
                direction.y = 0;

                Debug.DrawRay(ally.transform.position, ally.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                targetDistance = hit.distance;

                if (targetDistance >= allowedDistance)
                {
                    speed = 0.015f;
                    Debug.Log(ally + " run!");
                    ally.GetComponent<Animator>().Play("Rifle Walk");
                    ally.transform.position = Vector3.MoveTowards(ally.transform.position, player.transform.position, speed);
                }
                else
                {
                    Debug.Log(ally + " stop!");
                    speed = 0;
                    ally.GetComponent<Animator>().Play("Idle");
                }
            }
        }

        commandCountText.text = string.Format("%s", commandCount);
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
