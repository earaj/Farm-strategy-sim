using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GrilleGeneration : GenerationArbreStrategie
{
    /// <summary>
    /// Methode pour generer les arbres en grille.
    /// </summary>
    /// <param name="arbre"></param>
    /// <param name="tailleForet"></param>
    /// <param name="exclusion"></param>
    /// <param name="parentForet"></param>
    /// <param name="nombreArbres"></param>
    public override void GenerationArbre(GameObject arbre, Vector2Int tailleForet, Collider[] exclusion, GameObject parentForet, int nombreArbres)
    {
        Vector3 centerOffset = new Vector3(-25 * 5 / 2f, 0f, -25 * 5 / 2f);

        for (int row = 0; row < 25; row++)
        {
            for (int col = 0; col < 25; col++)
            {
                Vector3 positionArbre = new Vector3(row * 5, 0f, col * 5) + centerOffset;

                bool zoneBarre = EstDansLaZoneInterdit(positionArbre, exclusion);//Verification des zones interdites (maison, route, magasin)
                if (!zoneBarre)
                {
                     GameObject arbreInstance = GameObject.Instantiate(arbre);
                    arbreInstance.transform.position = positionArbre;
                }
            }
        }
    }
}
