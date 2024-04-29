using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationArbre : MonoBehaviour
{
    [SerializeField] private GameObject arbre;
    [SerializeField] private Vector2Int tailleForet;
    [SerializeField] private float espace = 5f;

    [SerializeField] private Collider[] exclusion;
    [SerializeField] private GameObject parentForet;

    [SerializeField] private int nombreArbres = 350;

    void Start()
    {
        if (ParametresParties.Instance.typeForet == 0)
        {
            GenererGrid();
        }
         else if (ParametresParties.Instance.typeForet == 1)
        {
            GenererAleatoir();
        }
        else if (ParametresParties.Instance.typeForet == 2)
        {
            //achanger a formule
            GenererAleatoir();
        }
        //GenererAleatoir();
    }


    /// <summary>
    /// Generer une grille d'arbres
    /// </summary>
    public void GenererGrid()
    {
        Vector3 centerOffset = new Vector3(-tailleForet.x * espace / 2f, 0f, -tailleForet.y * espace / 2f);

        for (int row = 0; row < tailleForet.x; row++)
        {
            for (int col = 0; col < tailleForet.y; col++)
            {
                Debug.Log("row: " + row + " col: " + col);
                Vector3 positionArbre = new Vector3(row * espace, 0f, col * espace) + centerOffset;

                bool zoneBarre = EstDansLaZoneInterdit(positionArbre);
                if (!zoneBarre)
                {
                    Instantiate(arbre, positionArbre, Quaternion.identity);
                }
            }
        }
        Destroy(parentForet);
    }


    /// <summary>
    /// Generer des arbres de maniere aleatoire
    /// </summary>
    public void GenererAleatoir()
    {
        List<Vector3> positionArbre = new List<Vector3>();

        for (int i = 0; i < nombreArbres; i++)
        {
            bool estPositionCorrect = false;
            Vector3 position = Vector3.zero;

            while (!estPositionCorrect)
            {
                position = new Vector3(Random.Range(-60, 64) , 0f, Random.Range(-60, 64));

                bool zoneBarre = EstDansLaZoneInterdit(position);
                if (!zoneBarre)
                {
                    bool tropProche = EstProcheDArbre(position, positionArbre);
                    if (!tropProche)
                    {
                        estPositionCorrect = true;
                    }
                }
            }

            positionArbre.Add(position);
            Instantiate(arbre, position, Quaternion.identity);
        }
        Destroy(parentForet);
    }

    private bool EstDansLaZoneInterdit(Vector3 position)
    {
        foreach (Collider zone in exclusion)
        {
            if (zone.bounds.Contains(position))
            {
                return true;
            }
        }
        return false;
    }

    private bool EstProcheDArbre(Vector3 position, List<Vector3> positionsArbres)
    {
        foreach (Vector3 positionArbre in positionsArbres)
        {
            if (Vector3.Distance(positionArbre, position) < espace)
            {
                return true;
            }
        }
        return false;
    }


}
