using UnityEngine;

public class Oeuf : MonoBehaviour, IRamassable
{
    private float calculerTemps;
    [SerializeField] private GameObject prefabPoule;

    public void Ramasser(Inventaire inventaireJoueur)
    {
        inventaireJoueur.Oeuf++;
        Destroy(gameObject);
    }

    void Update()
    {
        calculerTemps += Time.deltaTime*10;

        // Verifier si l'oeuf pourrit ou éclôt
        if (calculerTemps >= ConstantesJeu.MINUTES_PAR_JOUR * 3)
        {
            float probabilite = Random.value; 
                
            // 75 pourcent de chance que l'oeuf pourri
            if (probabilite <= 0.75f)
            {

                Destroy(gameObject);
            }
            // 25 pourcent de chance que l'oeuf éclôt
            else
            {
                Instantiate(prefabPoule, transform.position, Quaternion.identity);//Creation de la poule
                Destroy(gameObject);
            }
        }

    }
    public EtatJoueur EtatAUtiliser(ComportementJoueur Sujet)
    {
        return new EtatRamasserObjet(Sujet, this);
    }

    public bool Permis(ComportementJoueur sujet)
    {
        return true;
    }
}