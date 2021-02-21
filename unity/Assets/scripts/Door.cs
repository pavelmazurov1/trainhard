using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 openLocation;
    public Quaternion openRotation;

    public Vector3 closeLocation;
    public Quaternion closeRotation;

    public AudioClip openSound;
    public AudioClip closeSound;
    public AudioSource audioSource;

    public float animationTime = 0.5f;
    public bool opened = false;
    public bool locked = false;

    private bool mMoving = false;

    private void Reset()
    {
    }

    private void Start()
    {
        if(audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void Toggle()
    {
        if(locked || mMoving)
        {
            return;
        }
        if (opened)
        {
            Close();
        } else
        {
            Open();
        }
    }

    public void Close()
    {
        if (locked || mMoving || !opened)
        {
            return;
        }
        StartCoroutine(Move(openLocation, openRotation, closeLocation, closeRotation));
        audioSource.PlayOneShot(closeSound);
    }

    public void Open()
    {
        if (locked || mMoving || opened)
        {
            return;
        }
        StartCoroutine(Move(closeLocation, closeRotation, openLocation, openRotation));
        audioSource.PlayOneShot(openSound);
    }

    private IEnumerator Move(Vector3 fromLocation, Quaternion fromRotation, Vector3 toLocation, Quaternion toRotation)
    {
        mMoving = true;
        var actionTime = 0f;
        var startPos = transform.position;
        var startRot = transform.rotation;
        yield return null;
        while (true)
        {
            actionTime += Time.deltaTime;
            var t = actionTime / animationTime;
            if(t >= 1)
            {
                transform.position = toLocation;
                transform.rotation = toRotation;
                break;
            }
            else
            {
                transform.position = Vector3.Lerp(startPos, toLocation, t);
                transform.rotation = Quaternion.Lerp(startRot, toRotation, t);
                yield return null;
            }
        }
        mMoving = false;
        opened = !opened;
    }

}
