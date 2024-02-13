using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    [SerializeField] MemoryCard cardPrefab;
    [SerializeField] Sprite[] images;
    private int[] cardIds;
    private int[] shuffledCardsIds;
    private float x0;
    private float offsetX;
    private float y0;
    private float offsetY;
    //----------------------
    private MemoryCard firstCard = null;
    private MemoryCard secondCard = null;
    private bool _canFlip;
    public int score = 0;

    public bool CanFlip => firstCard == null || secondCard == null;

    void Start()
    {
        StartTimer();
        RegisterCards();
        ShuffleCards();
        PlaceCards(3, 4);
    }

    void Update()
    {

    }

    private void RegisterCards()
    {
        int length = images.Length * 2;
        cardIds = new int[length];
        int j = 0;
        for (int i = 0; i < length; i += 2)
        {
            cardIds[i] = j;
            cardIds[i + 1] = j;
            j++;
        }
    }

    private void ShuffleCards()
    {
        shuffledCardsIds = Shuffle(cardIds);
    }

    private void PlaceCards(int nRows, int nCols, int marginX = 3, int marginY = 1)
    {
        if (nRows * nCols != cardIds.Length)
        {
            Debug.LogError("producto de nº de filas y columnas debe ser igual al nº de cartas");
            return;
        }

        CalcOriginOffset(nCols, nRows, marginX, marginY);

        int currentIndex = 0;
        for (int i = 0; i < nRows; i++)
        {
            for (int j = 0; j < nCols; j++)
            {
                int cardId = shuffledCardsIds[currentIndex];
                float coordsX = x0 + offsetX * j;
                float coordsY = y0 + offsetY * i;
                CreateCard(cardId, coordsX, coordsY);
                currentIndex++;
            }
        }

    }

    private void CreateCard(int id, float coordsX, float coordsY)
    {
        MemoryCard card = Instantiate(cardPrefab);

        card.SetCard(id, images[id]);

        card.controller = this;
        card.transform.position = new Vector3(
            coordsX,
            coordsY,
            -1
        );
    }

    private void CalcOriginOffset(int nCols, int nRows, int marginX, int marginY)
    {
        float anchoCamara = Camera.main.pixelWidth / 100;
        float altoCamara = Camera.main.pixelHeight / 100;

        float anchoCarta = cardPrefab.Size.x;
        float altoCarta = cardPrefab.Size.y;

        float gapX = (anchoCamara - anchoCarta * nCols - marginX * 2) / (nCols - 1);
        if (gapX < 0) gapX = 0;
        float gapY = (altoCamara - altoCarta * nRows - marginY * 2) / (nRows - 1);
        if (gapY < 0) gapY = 0;

        float anchoFila = anchoCarta * nCols + gapX * (nCols - 1);
        float altoColumna = altoCarta * nRows + gapY * (nRows - 1);

        x0 = -(anchoFila - anchoCarta) / 2;
        y0 = -(altoColumna - altoCarta) / 2;

        offsetX = anchoCarta + gapX;
        offsetY = altoCarta + gapY;
    }

    public void NotifyCardFlipped(MemoryCard card)
    {
        if (firstCard == null)
        {
            firstCard = card;
        }
        else
        {
            secondCard = card;
            StartCoroutine(CheckCards());
        }
    }

    IEnumerator CheckCards()
    {
        if (firstCard.Id == secondCard.Id)
        {
            score++;
            StartCoroutine(firstCard.PairAnimation());
            StartCoroutine(secondCard.PairAnimation());

            firstCard.transform.Rotate(Vector3.forward * 0);
            secondCard.transform.Rotate(Vector3.forward * 0);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            firstCard.Unflip();
            secondCard.Unflip();
        }
        firstCard = null;
        secondCard = null;
    }

    public void StartTimer(){
        StartCoroutine(CountTime());
    }

    IEnumerator CountTime(){
        float totalTime = 0;
        while(score < images.Length){
            totalTime += Time.deltaTime;
            yield return null;
        }
        Debug.Log("Has tardado " + totalTime + " segundos, ¡gracias por jugar!");
        SceneManager.LoadScene("SampleScene");
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
