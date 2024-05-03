using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EtatRenard 
{
    public MouvementRenard Renard
    {
        set; get;
    }
    public GameObject Poule
    {
        set; get;
    }
    public NavMeshAgent AgentMouvement
    {
        set; get;
    }
    public Animator Animateur
    {
        set; get;
    }
    private float distanceVisibiliteRenard = 10.0f;

    public EtatRenard(MouvementRenard renard, GameObject poule)
    {
        Renard = renard;
        Poule = poule;
        AgentMouvement = renard.GetComponent<NavMeshAgent>();
        Animateur = renard.GetComponent<Animator>();
    }

    protected bool PouleVisible()
    {
        if(Poule != null) { 
        bool visible = false;
        RaycastHit hit;

        Vector3 positionPoule = new Vector3(Poule.transform.position.x, 0.5f, Poule.transform.position.z);
        Vector3 positionRenard = new Vector3(Renard.transform.position.x, 0.5f, Renard.transform.position.z);
        Vector3 directionPoule = positionPoule - positionRenard;

        if (directionPoule.magnitude <= distanceVisibiliteRenard)
        {
            // Detection d'obstacke entre renard et poules
            if (Physics.Raycast(positionRenard, directionPoule, out hit))
            {
                if (hit.transform == Poule.transform)
                {
                    // Verification d'angle
                    float angle = Vector3.Angle(Renard.transform.forward, directionPoule);
                    visible = angle <= 40.0f;
                }
            }
        }
        return visible;
        }
        else { return false; }
    }
    public abstract void Enter();
    public abstract void Handle();
    public abstract void Leave();
}
