using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimuleGeneration : GenerationArbreStrategie
{
    /// <summary>
    /// Methode pour generer les arbres de facon simulee.
    /// </summary>
    /// <param name="arbre"></param>
    /// <param name="tailleForet"></param>
    /// <param name="exclusion"></param>
    /// <param name="parentForet"></param>
    /// <param name="nombreArbres"></param>
    public override void GenerationArbre(GameObject arbre, Vector2Int tailleForet, Collider[] exclusion, GameObject parentForet, int nombreArbres)
    {

        int tailleX = 20; 
        int tailleY = 50; 

        // grille de 2d pour stocker l'état si il y a un arbre ou non
        bool[,] grilleDeForet = new bool[tailleX, tailleY];

        //Generer les cases
        for (int x = 0; x < tailleX; x++)
        {
            for (int y = 0; y < tailleY; y++)
            {
                grilleDeForet[x, y] = Random.value < 0.7f; // 70% chance de etre vrai
            }
        }

        // Generations d'arbre
        for (int generation = 0; generation < 10; generation++)
        {
            // Creation d'une nouvelle grille pour la prochaine generation
            bool[,] prchGeneration = new bool[tailleX, tailleY];

            // Appliquer le regle de la simulation
            for (int x = 0; x < tailleX; x++)
            {
                for (int y = 0; y < tailleY; y++)
                {
                    int neighbors = CompteArbreCote(grilleDeForet, x, y);

                    if (grilleDeForet[x, y])
                    {
                        // l'arbre reste en vie pour le prochain
                        prchGeneration[x, y] = neighbors == 3 || neighbors == 4 || neighbors == 6 || neighbors == 7 || neighbors == 8;
                    }
                    else
                    {
                        // un arbre pousse
                        prchGeneration[x, y] = neighbors == 3 || neighbors == 6 || neighbors == 7 || neighbors == 8;
                    }
                }
            }

            // Genration courrante devient la prochaine generation
            grilleDeForet = prchGeneration;
        }

        //limites de la foret
        int minX = -60;
        int maxX = 64;
        int minY = -60;
        int maxY = 64;
        // Placement des arbres
        for (int x = 0; x < tailleX; x++)
        {
            for (int y = 0; y < tailleY; y++)
            {
                if (grilleDeForet[x, y])
                {
                    //Calculation de position
                    float posX = Mathf.Lerp(minX, maxX, (float)x / tailleX);
                    float posY = Mathf.Lerp(minY, maxY, (float)y / tailleY);

                    // Creation de coordonnees aleatoires
                    float randomOffsetX = Random.Range(-1.25f, 1.25f);
                    float randomOffsetY = Random.Range(-1.25f, 1.25f);

                    //distination de l'arbre
                    Vector3 position = new Vector3(posX + randomOffsetX, 0f, posY + randomOffsetY);
                   
                    bool zoneBarre = EstDansLaZoneInterdit(position, exclusion);
                    if (!zoneBarre)
                    {
                        GameObject arbreInstance = GameObject.Instantiate(arbre);
                        arbreInstance.transform.position = position;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Methode pour compter le nombre d'arbre autour d'une case.
    /// </summary>
    /// <param name="grille"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private int CompteArbreCote(bool[,] grille, int x, int y)
    {
        int nb = 0;
        (int, int)[] offsets = { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };

        foreach (var offset in offsets)
        {
            int coteX = x + offset.Item1;
            int coteY = y + offset.Item2;

            if (coteX >= 0 && coteX < grille.GetLength(0) && coteY >= 0 && coteY < grille.GetLength(1))
            {
                if (grille[coteX, coteY])
                {
                    nb++;
                }
            }
        }
        return nb;
    }
}
