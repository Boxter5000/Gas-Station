using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathUI : MonoBehaviour
{
    [SerializeField] private Death death;


    public void RespawnPlayer()
    {
        death.RespawnPlayer();
    }

    public void EnableMovement()
    {
        death.ResetPlayerInput();
    }
}
