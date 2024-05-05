using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouvementRenard : MonoBehaviour
{
    [SerializeField] private Transform[] _pointsPatrouille;

    private Animator _animator;
    private NavMeshAgent _agent;
    private int _indexPatrouille;



    private EtatRenard _etat;

    public EtatPatrouille Patrouille
    {
        private set;
        get;
    }

    public EtatPoursuit Poursuite
    {
        private set;
        get;
    }

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _indexPatrouille = 0;
        _agent.destination = _pointsPatrouille[_indexPatrouille].position;
        _animator = GetComponent<Animator>();
        GameObject poule = GameObject.FindWithTag("Poule");
        Patrouille = new EtatPatrouille(this, poule, _pointsPatrouille);
        Poursuite = new EtatPoursuit(this, poule);

        _etat = Patrouille;
        _etat.Enter();
    }

    // Update is called once per frame
    void Update()
    {
        _etat.Handle();
    }

    /// <summary>
    /// Methode pour changer l'etat du renard
    /// </summary>
    /// <param name="nouvelEtat"></param>
    public void ChangerEtat(EtatRenard nouvelEtat)
    {
        _etat.Leave();
        _etat = nouvelEtat;
        _etat.Enter();
    }

    public void Disparaitre()
    {
        ChangerEtat(new EtatDisparaitre(this, null));
    }
    public Transform[] GetPointsPatrouille()
    {
        return _pointsPatrouille;
    }
}
