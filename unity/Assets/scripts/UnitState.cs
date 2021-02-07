using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState : MonoBehaviour
{
    // Start is called before the first frame update
    public enum MoveStateEnum 
    {
        Idle = 0,
        Walk,
        Run,
        Sit,
        Crowl,
        Jump,
        Fall,
        Climb,
    }

    public int _moveState = (int)MoveStateEnum.Idle;
    public int moveState {
        get { 
            return _moveState;
        }
        set
        {
            if(_moveState != value)
            {
                _moveState = value;
                if (moveStateChanged != null)
                {
                    moveStateChanged(this, EventArgs.Empty);
                }
            }
        }
    }
    public event EventHandler moveStateChanged;

}
