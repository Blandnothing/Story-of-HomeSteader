using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPositionButtonScript : MonoBehaviour
{
    public void ResetPosition()
    {
        Transform player = GameObject.Find("HeroKnight").transform;
        player.position = Vector3.zero;
    }
}
