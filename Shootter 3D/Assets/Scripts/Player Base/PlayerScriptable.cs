using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScriptable : MonoBehaviour
{
    public float runningMovementSpeed = 6f;
    public float walkindMovementSpeed = 3f;
    public float gravity = -21f;
    public float jumpHight=1.5f;
    public float crouchingMovementSpeed=1.7f;
    public float standingHeightY = 2f;

    public float crouchHeightY=0.5f;
    public float crouchSpeed = 10f;
}
