using System;
using UnityEngine;

public class Death : MonoBehaviour
{
    private FirstPersonController _player;
    private bool isDead;
    [SerializeField] private Animator deathAnimation;

    private Transform _respawnPos;

    private void Awake()
    {
        _respawnPos = transform;
    }

    public void SetPlayer(FirstPersonController player)
    {
        _player = player;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Death")) return;
        isDead = true;
        _player.cameraCanMove = false;
        _player.playerCanMove = false;
        deathAnimation.SetBool("Death", isDead);
    }

    private void SetNewRespawnPos(Transform pos)
    {
        _respawnPos = pos;
    }

    public void RespawnPlayer()
    {
        _player.transform.position = _respawnPos.position;
    }

    public void ResetPlayerInput()
    {
        isDead = false;
        _player.cameraCanMove = true;
        _player.playerCanMove = true;
        deathAnimation.SetBool("Death", isDead);
    }
}
