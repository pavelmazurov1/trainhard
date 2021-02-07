using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public UnitState unitState;

    public AudioClip walkSound;
    public AudioClip runSound;
    public AudioClip fallSound;
    public AudioClip jumpStartSound;

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
                        audioSource.loop = true;
                        audioSource.Play();
                        break;
                    case UnitState.MoveStateEnum.Run:
                        audioSource.clip = runSound;
                        audioSource.loop = true;
                        audioSource.Play();
                        break;
                    case UnitState.MoveStateEnum.Fall:
                        audioSource.clip = fallSound;
                        audioSource.loop = false;
                        audioSource.Play();
                        break;
                    case UnitState.MoveStateEnum.Jump:
                        audioSource.clip = jumpStartSound;
                        audioSource.loop = false;
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
