using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Buche : MonoBehaviour, IActionnable
{
    //Detection de la buche
    public float activation = 1f;

    private bool marche = false;


    void Start()
    {
        
    
    }

    /// <summary>
    /// Utilisation d'etat pour rammaser le buche
    /// </summary>
    /// <param name="sujet"></param>
    /// <returns></returns>
    public EtatJoueur EtatAUtiliser(ComportementJoueur sujet)
    {
        if (Permis(sujet))
        {
            //Animation de marche
            Animator animator = sujet.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("Walking", true);
                marche = true;
            }

            //Destination du joueur
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
        //Marche vers le buche
        GameObject joueur = GameObject.Find("Joueur");
        Animator animator = joueur.GetComponent<Animator>();
        if (joueur != null && marche)
        {
            if (Vector3.Distance(transform.position, joueur.transform.position) <= activation)
            {
                animator.SetBool("Walking", false);
                animator.SetBool("Ramasse", true);
                marche = false;
                GameObject.Find("Joueur").GetComponent<ComportementJoueur>().ActiveCharacterController();
                Invoke("Ramasser", 3f);
            }
        }
    }

    /// <summary>
    /// Ramassage et la destruction de la buche
    /// </summary>
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
        Inventaire inventaireJoueur = joueur.GetComponent<Inventaire>();
        inventaireJoueur.Bois++;
        Destroy(gameObject);
    }


}