using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genetic_Algorithm : MonoBehaviour
{
    const int n_population = 4;
    const int cromosome_size = 5;
    int iteration = 5;
    float cross_prob = 0.9f;
    int cros_point = 3;
    float mutation_prob = 0.05f;

    int[,] matrix_population = new int[n_population, cromosome_size + 1];

    int fitness_function(int pos)
    {
        int summa_data=0;
        for (int i = 0; i < cromosome_size; i++)
        {
            summa_data+=matrix_population[pos,i];
        }
        return summa_data;
    }

    void generate_population()
    {
        for (int i = 0; i < n_population; i++)
        {
            for (int j = 0; j < cromosome_size; j++)
            {
                matrix_population[i, j] = Random.Range(0, 2);
                // print(matrix_population[i, j]);
            }
        }

    }
    void eval_population()
    {

        for (int i = 0; i < n_population; i++)
        {
            int temporal = fitness_function(i);
            print(temporal);
        }

    }

    private void Start()
    {
        generate_population();
        eval_population();
    }

}
