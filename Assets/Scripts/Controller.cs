using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Sprite[] images;
    private int[] cardIds;
    private int[] shuffledCardsIds;

    void Start()
    {
        RegisterCards();
        ShuffleCards();
        PlaceCards(3,4);
    }

    void Update()
    {

    }

    private void RegisterCards()
    {
        int length = images.Length * 2;
        cardIds = new int[length];
        for (int i = 0; i < length; i += 2)
        {
            Debug.Log("añadir: " + i);
            cardIds[i] = i;
            Debug.Log("añadir doble: " + i);
            cardIds[i + 1] = i;
        }
    }

    private void ShuffleCards()
    {
        shuffledCardsIds = Shuffle(cardIds);
    }

    private void PlaceCards(int nRows, int nCols, int marginX = 3, int marginY = 1)
    {
        if (nRows*nCols != cardIds.Length){
            Debug.LogError("producto de nº de filas y columnas debe ser igual al nº de cartas");
            return;
        }
    }

    /*
    Este método consiste en ordenar de forma aleatoria un array de cualquier tipo de objeto (array genérico).
    Para ello primero clona la lista, y luego se mueve cada objeto a la posición que se ha generado aleatoriamente para cada uno.
    */
    T[] Shuffle<T>(T[] a)
    {
        T[] shuffled = a.Clone() as T[];
        int j;
        T aux;
        for (int i = 0; i < shuffled.Length; i++)
        {
            j = Random.Range(i, shuffled.Length);
            aux = shuffled[i];
            shuffled[i] = shuffled[j];
            shuffled[j] = aux;
        }
        return shuffled;
    }
}
