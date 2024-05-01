using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasardGeneration : GenerationArbreStrategie
{
    /// <summary>
    /// Methode pour generer les arbres de facon aleatoire.
    /// </summary>
    /// <param name="arbre"></param>
    /// <param name="tailleForet"></param>
    /// <param name="exclusion"></param>
    /// <param name="parentForet"></param>
    /// <param name="nombreArbres"></param>
    public override void GenerationArbre(GameObject arbre, Vector2Int tailleForet, Collider[] exclusion, GameObject parentForet, int nombreArbres)
    {
        List<Vector3> positionArbre = new List<Vector3>();

        for (int i = 0; i < nombreArbres; i++)
        {
            bool estPositionCorrect = false;
            Vector3 position = Vector3.zero;

            while (!estPositionCorrect)
            {
                position = new Vector3(Random.Range(tailleForet.x, tailleForet.y), 0f, Random.Range(tailleForet.x, tailleForet.y));

                bool zoneBarre = EstDansLaZoneInterdit(position, exclusion);//Verification des zones interdites (maison, route, magasin)
                if (!zoneBarre)
                {
                    bool tropProche = EstProcheDArbre(position, positionArbre, 20);//Verification si l'arbre est trop proche d'un autre arbre
                    if (!tropProche)
                    {
                        estPositionCorrect = true;
                    }
                }
            }

            //Placement des arbres

            GameObject arbreInstance = GameObject.Instantiate(arbre);
            arbreInstance.transform.position = position;
        }
    }
}
