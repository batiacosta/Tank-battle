using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;

    private Camera _camera;
    
    #region Server
    [Command]
    public void CmdMove(Vector3 position)
    {
        if (!NavMesh.SamplePosition(position, out NavMeshHit navMeshHit, 1f, NavMesh.AllAreas))
        {
            return;
        }

        agent.SetDestination(navMeshHit.position);
    }
    #endregion

    #region Client

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        _camera = Camera.main;
    }

    [ClientCallback]
    private void Update()
    {
        if(!isOwned) return;

        if (!Mouse.current.rightButton.wasPressedThisFrame) return;

        Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if(!Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity)) return;
        
        CmdMove(raycastHit.point);
    }

    #endregion
}