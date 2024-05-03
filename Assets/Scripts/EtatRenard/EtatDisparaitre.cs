using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtatDisparaitre : EtatRenard
{
    public EtatDisparaitre(MouvementRenard renard, GameObject poule) : base(renard, poule)
    {
        AgentMouvement.enabled = false;
    }

    public override void Enter()
    {
        GameObject.Destroy(Renard.GetComponent<Collider>());
    }

    public override void Handle()
    { 
       GameObject.Destroy(Renard.gameObject);
    }

    public override void Leave()
    {
    }
}
