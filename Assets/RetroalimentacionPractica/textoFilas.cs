using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textoFilas : MonoBehaviour
{
    [SerializeField] private Text texto;
    [SerializeField] private Text veces;
    [SerializeField] private Text puntos;

    public void SetText(string textoString, string vecesString, string puntosString){
        texto.text = textoString;
        veces.text = vecesString;
        puntos.text = puntosString;
    }
}