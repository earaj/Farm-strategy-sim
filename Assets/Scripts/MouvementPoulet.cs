using UnityEngine;
using UnityEngine.AI;

public class MouvementPoulet : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;

    private Transform _joueur;

    private float _distanceJoueur = 2f;
    private float vitesseSuivre = 4f;
    private float vitesseDansFerme = 0.5f;

    private GameObject[] _pointsDeDeplacement;
    private GameObject[] _pointsDeDeplacementNuit;
    private bool estDanslaFerme = false;

    private GameManager _gameManager;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _pointsDeDeplacement = GameObject.FindGameObjectsWithTag("PointsPoulet");//Points de deplacement le jour
        _pointsDeDeplacementNuit = GameObject.FindGameObjectsWithTag("PointSpecial");//Points de deplacement la nuit
        _joueur = GameObject.Find("Joueur").transform;
        _animator.SetBool("Walk", true);
        Initialiser();
    }

    void Initialiser()
    {
        // Position initiale a cote du joueur
        Vector3 spawnPosition = _joueur.position + _joueur.forward * 2f;
        transform.position = spawnPosition;

        // Activer le comportement de pondre des oeufs
        gameObject.GetComponent<PondreOeufs>().enabled = true;

        // Suivre le joueur jusqu'à l'entrée de la ferme
        SuivreJoueur();
    }

    /// <summary>
    /// Methode pour choisir une destination aleatoire
    /// </summary>
    void ChoisirDestinationAleatoire()
    {
        _agent.speed = vitesseDansFerme;
        if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
        {
            GameObject point;

            // Si c'est la nuit poule peut sortir de ferme
            if (_gameManager.estNuit)
            {
                point = _pointsDeDeplacementNuit[Random.Range(0, _pointsDeDeplacementNuit.Length)];
            }
            //Sinon poule reste dans la ferme
            else 
            {
                point = _pointsDeDeplacement[Random.Range(0, _pointsDeDeplacement.Length)];
            }
            _agent.SetDestination(point.transform.position);
        }
    }

    /// <summary>
    /// Poule suivre le suivre le joueur
    /// </summary>
    void SuivreJoueur()
    {
        _agent.enabled = true;

        // Calculer la direction vers le joueur
        Vector3 directionVersJoueur = (_joueur.position - transform.position).normalized;
        Vector3 destination = _joueur.position - directionVersJoueur * _distanceJoueur;

        _agent.SetDestination(destination);
        _agent.speed = vitesseSuivre;
    }


    void Update()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // Si le poulet arrive a ferme choisir une nouvelle destination
        if (estDanslaFerme)
        {
            ChoisirDestinationAleatoire();
        }
        else
        {
            // Suivre le joueur jusqu'à l'entrée de la ferme
            SuivreJoueur();
        }
    }

    /// <summary>
    /// Verifier si le poulet est dans la ferme
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("entree"))
        {
            estDanslaFerme = true;
        }
        //Verifier si le renard touche le poule 
        if (other.CompareTag("Renard"))
        {
            Destroy(gameObject);
        }
    }
}