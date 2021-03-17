using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorLevelDirector : MonoBehaviour
{
    public InGameMenuScript InGameMenu;

    public AudioClip EnterAudioClip;
    public AudioClip EmptyBunkerClip;
    public AudioClip GoCollectorClip;
    public AudioClip GoFindValveClip;

    public ColliderProxy BunkerCollider;
    public InterractableItem DocumentsItem;
    public InterractableItem CollectorDoorItem;
    public InterractableItem ValveItem;
    public Transform ValveTargetTransform;

    private GameData GameData;
    // Start is called before the first frame update
    void Start()
    {
        InGameMenu.MenuAvailable = true;
        GameData = GameData.Instance;
        GameData.Scene = "уровень_элеватор";
        GameData.Save();

        StartCoroutine(Main());
    }

    IEnumerator Main()
    {
        yield return StartCoroutine(InGameMenu.UnFade(5));
        yield return StartCoroutine(PlayRadioMessage(EnterAudioClip));
        yield return StartCoroutine(WaitInBunker());
        yield return StartCoroutine(PlayRadioMessage(EmptyBunkerClip));
        yield return StartCoroutine(WaitDocumentPicked());
        yield return StartCoroutine(PlayRadioMessage(GoCollectorClip));
        yield return StartCoroutine(WaitCollectorDoorReached());
        yield return StartCoroutine(PlayRadioMessage(GoFindValveClip));
        yield return StartCoroutine(WaitValveFound());
        yield return StartCoroutine(WaitCollectorDoorOpened());
        yield return StartCoroutine(InGameMenu.Fade(5));

        SceneManager.LoadScene("главное_меню");
        //load next level
    }

    IEnumerator PlayRadioMessage(AudioClip message)
    {
        var audioSource = GameData.FlatAudioSource;
        audioSource.PlayOneShot(message);
        while (audioSource.isPlaying)
        {
            yield return null;
        }
    }

    private EventHandler<Collider> bunkerColliderHandler;
    IEnumerator WaitInBunker()
    {
        bool reached = false;
        BunkerCollider.DoActivate();
        bunkerColliderHandler = (sender, collider) => {
            if (collider.tag == "Player")
            {
                BunkerCollider.OnTriggerEnterEvent -= bunkerColliderHandler;
                reached = true;
            }
        };
        BunkerCollider.OnTriggerEnterEvent += bunkerColliderHandler;
        while (!reached)
        {
            yield return null;
        }
    }

    private EventHandler documentsInterracted;
    IEnumerator WaitDocumentPicked()
    {
        bool done = false;
        documentsInterracted = (sender, args) =>
        {
            DocumentsItem.OnInterractedEvent -= documentsInterracted;
            done = true;
        };
        DocumentsItem.OnInterractedEvent += documentsInterracted;
        while (!done)
        {
            yield return null;
        }
    }

    IEnumerator WaitCollectorDoorReached()
    {
        bool done = false;
        documentsInterracted = (sender, args) =>
        {
            CollectorDoorItem.OnInterractedEvent -= documentsInterracted;
            done = true;
        };
        CollectorDoorItem.OnInterractedEvent += documentsInterracted;
        while (!done)
        {
            yield return null;
        }
        Debug.Log("WaitCollectorDoorReached");
    }

    IEnumerator WaitValveFound()
    {
        bool done = false;
        documentsInterracted = (sender, args) =>
        {
            ValveItem.OnInterractedEvent -= documentsInterracted;
            done = true;
        };
        ValveItem.OnInterractedEvent += documentsInterracted;
        while (!done)
        {
            yield return null;
        }
        ValveItem.gameObject.SetActive(false);
        Debug.Log("WaitValveFound");
    }
    
    IEnumerator WaitCollectorDoorOpened()
    {
        bool done = false;
        documentsInterracted = (sender, args) =>
        {
            CollectorDoorItem.OnInterractedEvent -= documentsInterracted;
            done = true;
        };
        CollectorDoorItem.OnInterractedEvent += documentsInterracted;
        while (!done)
        {
            yield return null;
        }
        Debug.Log("WaitCollectorDoorOpened");
        yield return StartCoroutine(RotateValve());
    }

    IEnumerator RotateValve()
    {
        ValveItem.gameObject.SetActive(true);
        var valveTransform = ValveItem.transform;
        valveTransform.position = ValveTargetTransform.position;
        valveTransform.rotation = ValveTargetTransform.rotation;

        float time = 0;
        var targetRotation = Quaternion.AngleAxis(180, ValveTargetTransform.forward);
        var startRotation = valveTransform.rotation;
        while (time < 2)
        {
            valveTransform.rotation = Quaternion.Lerp(startRotation, targetRotation, time / 2);
            time += Time.deltaTime;
            yield return null;
        }
        Debug.Log("RotateValve");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
