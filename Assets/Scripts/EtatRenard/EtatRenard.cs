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
    private float distanceVisibiliteRenard = 100.0f;

    public EtatRenard(MouvementRenard renard, GameObject poule)
    {
        Renard = renard;
        Poule = poule;
        AgentMouvement = renard.GetComponent<NavMeshAgent>();
        Animateur = renard.GetComponent<Animator>();
    }

    /// <summary>
    /// Methode pour verifier si la poule est visible par le renard
    /// </summary>
    /// <returns></returns>
    protected bool PouleVisible()
    {

        bool visible = false;
        RaycastHit hit;
        if (Poule != null)
        {
            //Calculation de la direction entre renard et poule
            Vector3 positionPoule = new Vector3(Poule.transform.position.x, 0.5f, Poule.transform.position.z);
            Vector3 positionRenard = new Vector3(Renard.transform.position.x, 0.5f, Renard.transform.position.z);
            Vector3 directionPoule = positionPoule - positionRenard;

            if (directionPoule.magnitude <= distanceVisibiliteRenard) //Distance au quelle re nard peut detecter le poule
            {
                // Detection d'obstacle entre renard et poules
                if (Physics.Raycast(positionRenard, directionPoule, out hit))
                {
                    return hit.transform == Poule.transform;
                }
            }
        }
        return visible;
    }
    public abstract void Enter();
    public abstract void Handle();
    public abstract void Leave();
}
