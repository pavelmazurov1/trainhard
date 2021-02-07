using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public UnitState unitState;

    public AudioClip walkSound;
    public AudioClip runSound;

    void Start()
    {
        if(audioSource == null)
            audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;

        if (unitState == null)
            unitState = GetComponent<UnitState>();
        
        if(unitState != null)
        {
            unitState.moveStateChanged += (sender, args) => {
                switch ((UnitState.MoveStateEnum)unitState.moveState)
                {
                    case UnitState.MoveStateEnum.Walk:
                        audioSource.clip = walkSound;
                        audioSource.Play();
                        break;
                    case UnitState.MoveStateEnum.Run:
                        audioSource.clip = runSound;
                        audioSource.Play();
                        break;
                    default:
                        audioSource.Stop();
                        break;
                }
            };
        }
    }

}
