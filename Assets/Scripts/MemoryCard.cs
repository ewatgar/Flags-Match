using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] SpriteRenderer cardBack, cardFlag;
    public Controller controller;
    private bool _bIsCurrentlyFlipped = false;
    private int _id;
    private Vector2 _size;

    public bool IsCurrentlyFlipped
    {
        get { return _bIsCurrentlyFlipped; }
    }

    public int Id
    {
        get { return _id; }
    }

    public Vector2 Size
    {
        get { return _size; }
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void Flip()
    {
        cardBack.gameObject.SetActive(false);
        _bIsCurrentlyFlipped = true;
    }

    public void Unflip()
    {
        cardBack.gameObject.SetActive(true);
        _bIsCurrentlyFlipped = false;
    }

    public void SetCard(int id, Sprite image)
    {
        _id = id;
        cardFlag.sprite = image;
        _size = cardBack.sprite.bounds.size;
    }

    void OnMouseDown()
    {
        if (!IsCurrentlyFlipped && controller.CanFlip)
        {
            Flip();
            controller.NotifyCardFlipped(this);
        }
    }

    public IEnumerator PairAnimation(){
        float animSeconds = 1;
        float totalRotation = 360f;
        float rotationSpeed = totalRotation / animSeconds;
        float currentRotation = 0f;

        while(currentRotation < totalRotation){
            float rotationAmount = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward * rotationAmount);
            currentRotation += rotationAmount;
            yield return null;
        }
    }

    public IEnumerator PairAnimationV2(){
        float speed = 10;
        Vector3 v = Vector3.zero;
        float umbral = 0.15f;
        Vector3 startRot = transform.position;
        Vector3 endRot = startRot + new Vector3(0,0,360);
        while(Vector3.Distance(startRot, endRot)>umbral){
            transform.localEulerAngles = Vector3.SmoothDamp(startRot, endRot, ref v, speed);
            yield return null;
        }
    }

    public IEnumerator PairAnimationV3() {
    float duration = 1.0f; // Duración de la animación en segundos
    float t = 0; // Tiempo transcurrido de la animación
    Quaternion startRot = transform.rotation; // Rotación inicial
    Quaternion endRot = Quaternion.Euler(0, 0, 360); // Rotación final

    while (t < duration) {
        t += Time.deltaTime; // Incrementa el tiempo transcurrido
        float fraction = t / duration; // Calcula la fracción del tiempo total

        // Interpola suavemente entre las rotaciones inicial y final
        transform.rotation = Quaternion.Lerp(startRot, endRot, fraction);

        yield return null; // Espera al siguiente frame
    }

    // Asegúrate de que la rotación final sea exacta
    transform.rotation = endRot;
}

}
