using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsManager : MonoBehaviour
{
    public GameObject[] fruits;

    private Vector3[] positions = new Vector3[4];

    private Object currentFruit;

    public void DestroyCurrentFruit()
    {
        if (currentFruit)
        {
            Destroy(currentFruit);
        }
    }
    private void Start()
    {
        positions[0] = new Vector3(-4.5f, -1.5f, -0.5f);
        positions[1] = new Vector3(4.5f, -1.5f, -0.5f);
        positions[2] = new Vector3(0.0f, -4.5f, -0.5f);
        positions[3] = new Vector3(0.0f, 1.5f, -0.5f);
    }

    private void OnEnable()
    {
        DestroyCurrentFruit();

        StopAllCoroutines();

        if (this.gameObject.activeSelf)
        {
            StartCoroutine(GenerateFruit());
        }
    }

    private int WeightFunction(float x)
    {
        float sum = 0.0f;
        int it = 0;

        foreach (GameObject fruit in fruits)
        {
            sum += fruit.GetComponent<Fruit>().weight / 100;

            if (x < sum)
            {
                return it;
            }

            it++;
        }
        return it;
    }

    private IEnumerator GenerateFruit()
    {
        while (true)
        {
            yield return new WaitForSeconds(15f);

            int index = WeightFunction(Random.Range(0f, 1f));

            if (index < fruits.Length)
            {
                fruits[index].SetActive(true);

                Vector3 position = positions[Random.Range(0, positions.Length)];
                currentFruit = Instantiate(fruits[index], position, fruits[index].transform.rotation);

                yield return new WaitForSeconds(8f);

                if (fruits[index].activeSelf)
                {
                    fruits[index].SetActive(false);
                    Destroy(currentFruit);
                }
            }
        }

    }
}
