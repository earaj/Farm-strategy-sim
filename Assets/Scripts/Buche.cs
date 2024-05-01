using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Buche : MonoBehaviour, IActionnable
{

    public float activation = 0.1f; // active le stop

    private bool marche = false;

    private Inventaire inventaire;

    void Start()
    {
    }


    public EtatJoueur EtatAUtiliser(ComportementJoueur sujet)
    {
        if (Permis(sujet))
        {
            Animator animator = sujet.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("Walking", true);
                marche = true;
            }

            GameObject joueur = GameObject.Find("Joueur");
            if (joueur != null)
            {
                NavMeshAgent playerAgent = joueur.GetComponent<NavMeshAgent>();
                if (playerAgent != null)
                {
                    sujet.ActiveNavMeshAgent();
                    playerAgent.SetDestination(transform.position);
                }
            }

            return null;
        }
        else
        {
            return null;
        }
    }

    public bool Permis(ComportementJoueur sujet)
    {
        return true;
    }

    void Update()
    {
        GameObject joueur = GameObject.Find("Joueur");
        Animator animator = joueur.GetComponent<Animator>();
        if (joueur != null && marche)
        {
            Debug.Log("Saaa");
           

            if (Vector3.Distance(transform.position, joueur.transform.position) <= activation)
            {
                Debug.Log("Buche");
                animator.SetBool("Walking", false);
                animator.SetBool("Ramasse", true);
                marche = false;
                GameObject.Find("Joueur").GetComponent<ComportementJoueur>().ActiveCharacterController();
                Ramasser();
            }
        }
    }

    public void Ramasser()
    {
        GameObject joueur = GameObject.Find("Joueur");
        Animator animator = joueur.GetComponent<Animator>();
        if (joueur != null)
        {
            ComportementJoueur comportementJoueur = joueur.GetComponent<ComportementJoueur>();
            if (comportementJoueur != null)
            {
                comportementJoueur.ChangerEtat(comportementJoueur.EtatNormal);
            }
        }
        animator.SetBool("Ramasse", false);

        Debug.Log("CAlled0");
        //inventaire.GetInstanceID
        //SpawnBuche();
        Destroy(gameObject);
    }
}
