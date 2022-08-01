using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentesAG : MonoBehaviour
{
    public Transform Meta;
    // const int cromosome_size = 20;

    public List<float> CromosomaCamino = new List<float>();
    float distanceFitnnes = 0;

    int position = 0;

    void Start()
    {
        Meta = GameObject.Find("Meta").transform;
        StartCoroutine(MoveTransform());
    }

    float fitness_function(Vector3 a, Vector3 b)
    {
        return Mathf.Abs(a.x-b.x);
    }

    IEnumerator MoveTransform()
    {
        while (CromosomaCamino.Count != position)
        {
            if (CromosomaCamino[position] == 0)
                transform.position = transform.position + new Vector3(0, 0, 0);
            else
                transform.position = transform.position + new Vector3(1, 0, 0);
            yield return new WaitForSeconds(Random.Range(0.15f,0.25f));
            position++;
        }

        //Evaluacion de la poblacion
        distanceFitnnes =fitness_function(this.transform.position, Meta.position);

        CromosomaCamino[CromosomaCamino.Count-1]=(distanceFitnnes);// adding fitness value
        Genetic_Algorithm.matrix_population.Add(CromosomaCamino);
        Destroy(this.gameObject);
    }

}
