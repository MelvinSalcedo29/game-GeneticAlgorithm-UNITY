using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Genetic_Algorithm : MonoBehaviour
{
    const int n_population = 4;
    public static int cromosome_size = 20;
    int iteration = 50;
    float cross_prob = 0.9f;
    int cros_point = 3;
    float mutation_prob = 0.15f;

    public static List<List<float>> matrix_population = new List<List<float>>();

    public GameObject prefab;
    public Text tex_iteration;
    void generate_population()
    {
        for (int i = 0; i < n_population; i++)
        {
            List<float> M_temporal = new List<float>();
            for (int j = 0; j < cromosome_size; j++)
            {
                M_temporal.Add(Random.Range(0, 2)); // valores del cromosoma
            }
            // M_temporal.Add(0); // fitness del cromosoma

            matrix_population.Add(M_temporal);



        }
        // matrix_population.Clear();

    }

    float fitness_function(List<float> pos)
    {
        float summa_data = 0;
        foreach (var item in pos)
        {
            summa_data += item;
        }
        return summa_data;
    }
    void eval_population()
    {
        foreach (var item in matrix_population)
        {
            float temporal = fitness_function(item);
            item.Add(temporal);
        }

    }

    int get_parent_tournament_selectio(int KA)
    {
        List<List<float>> mP = matrix_population.OrderBy(x => x.LastOrDefault()).ToList();
        int t = Random.Range(0, KA);
        return t;
    }

    int[,] tournament_selection()
    {
        int[,] selected = new int[1, 2];

        int mother_index = 0;
        int father_index = 0;

        while (mother_index == father_index)
        {
            mother_index = get_parent_tournament_selectio(3);
            father_index = get_parent_tournament_selectio(3);

            if (mother_index != father_index)
            {
                selected[0, 0] = mother_index;
                selected[0, 1] = father_index;
                break;
            }
        }
        return selected;
    }


    List<List<float>> one_point_crossover(int mother_index, int father_index)
    {
        List<List<float>> hijos = new List<List<float>>();
        List<float> son1 = new List<float>();
        List<float> son2 = new List<float>();

        for (int i = 0; i < cros_point; i++)
        {
            son1.Add(matrix_population[mother_index][i]);
            son2.Add(matrix_population[father_index][i]);
        }
        for (int i = cros_point; i < cromosome_size; i++)
        {
            son1.Add(matrix_population[father_index][i]);
            son2.Add(matrix_population[mother_index][i]);
        }
        string a = "";
        string b = "";

        for (int i = 0; i < cromosome_size; i++)
        {
            a += son1[i];
            b += son2[i];
        }

        print("hijo a = " + a);
        print("hijo b = " + b);
        hijos.Add(son1);
        hijos.Add(son2);
        return hijos;

    }

    void selecting_next_population()
    {
        int resta = matrix_population.Count - n_population;
        matrix_population = matrix_population.OrderBy(x => x.LastOrDefault()).ToList();

        print("********************************************************************");
        
        matrix_population.RemoveRange(resta, resta);

    }


    void Print_Pulation_value()
    {

        foreach (var item in matrix_population)
        {
            string temporal = "";
            for (int j = 0; j < item.Count(); j++)
            {
                temporal += item[j].ToString() + " ";
            }
            print("cromosoma = " + temporal);
        }

    }

    IEnumerator genetic_algorithm()
    {
        generate_population();
        eval_population();

        for (int i = 0; i < iteration; i++)
        {
            List<List<float>> tempo_list = new List<List<float>>();
            print("______________Iteracion_________" + i);
            
            tex_iteration.text=i.ToString();

            while (true)
            {
                if (matrix_population.Count + tempo_list.Count >= n_population * 2)
                {
                    break;
                }
                // if (Random.Range(0, 100) <= cross_prob * 100) //Crossove  Prob
                // {
                int[,] selected = tournament_selection();

                List<List<float>> offspring = one_point_crossover(selected[0, 0], selected[0, 1]);

                foreach (var son in offspring)
                {
                    if (Random.Range(0, 100) <= mutation_prob * 100)
                    { // Mutation Prob
                        print("Mutation");
                        for (int yy = 0; yy < son.Count; yy++)
                        {
                            if (son[yy] == 0)
                            {
                                son[yy] = 1;
                                break;
                            }
                        }
                    }
                    float summa_data = fitness_function(son);
                    son.Add(summa_data);

                    tempo_list.Add(son);
                }

                // }
            }

            foreach (var item in tempo_list)
            {
                matrix_population.Add(item);
            }


            int t=0;
            foreach (var item in matrix_population)
            {
                GameObject obj = Instantiate(prefab, new Vector3(-10, 0, t), Quaternion.identity);
                obj.GetComponent<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)); ;
                obj.GetComponent<AgentesAG>().CromosomaCamino = item;
                t+=4;
            }

            matrix_population.Clear();

            while (matrix_population.Count != n_population * 2)
            {
                yield return new WaitForSeconds(1);
                // print(matrix_population.Count());
            }


            selecting_next_population();
            print("-----------------------------------------");
            Print_Pulation_value();



        }
    }

    private void Start()
    {
        StartCoroutine(genetic_algorithm());
    }

}
