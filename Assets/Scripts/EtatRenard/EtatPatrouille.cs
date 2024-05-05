using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtatPatrouille : EtatRenard
{
    private Transform[] _points;


    public EtatPatrouille(MouvementRenard renard, GameObject poule, Transform[] points) : base(renard, poule)
    {
        _points = points;

    }


    public override void Enter()
    {
        Animateur.SetBool("Walk", true);
    }

    public override void Handle()
    {
        //Si le renard arrive a la destination, il va choisir une autre destination (patrouille)
        if (!AgentMouvement.pathPending && AgentMouvement.remainingDistance < 1f)
        {
                DistinationAleatoire();
        }

        //Si le poule est visible, renard va le poursuit
        if (PouleVisible())
        {
            MouvementRenard mouvement = Renard.GetComponent<MouvementRenard>();
            mouvement.ChangerEtat(mouvement.Poursuite);
        }

    }

    /// <summary>
    /// Methode pour choisir une destination aleatoire
    /// </summary>
    private void DistinationAleatoire()
    {
        int randomIndex = Random.Range(0, _points.Length);
        AgentMouvement.destination = _points[randomIndex].position;
    }

    public override void Leave()
    {
    }
}
