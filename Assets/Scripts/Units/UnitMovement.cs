using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : NetworkBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;
    
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
    

    #endregion
    
}