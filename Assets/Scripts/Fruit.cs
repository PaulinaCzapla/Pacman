using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public int points;

    public float weight; // in %

    private void Eat()
    {
        FindObjectOfType<GameManager>().FruitEaten(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Pacman>() != null)
        {
            Eat();
        }
    }


}
