using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationArbre : MonoBehaviour
{
    [SerializeField] private GameObject arbre;
    [SerializeField] private Vector2Int tailleForet;

    [SerializeField] private Collider[] exclusion;
    [SerializeField] private GameObject parentForet;

    [SerializeField] private int nombreArbres = 280;

    private GenerationArbreStrategie generationStrategy;

    void Start()
    {
        //Verifier le type de foret choisi
        if (ParametresParties.Instance.typeForet == 0)
        {
            generationStrategy = new GrilleGeneration();
        }
         else if (ParametresParties.Instance.typeForet == 1)
        {
            generationStrategy = new HasardGeneration();
        }
        else if (ParametresParties.Instance.typeForet == 2)
        {
            generationStrategy = new SimuleGeneration();
        }

        //Relier la strategie de generation d'arbre
        generationStrategy.GenerationArbre(arbre, tailleForet, exclusion, parentForet, nombreArbres);

        //Detruire le parent de la foret existant
        Destroy(parentForet);
    }
}
