using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class bdScript : MonoBehaviour
{
    [SerializeField] private Text texto;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void consultaNombre(int id){
        StartCoroutine(co_consultaNombre(id));
    }


    private IEnumerator co_consultaNombre(int id){
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        WWW w = new WWW("http://drvrtest.000webhostapp.com/loadPerson.php", form);
        yield return w;
        Debug.Log(w.text);
        Persona persona = JsonUtility.FromJson<Persona>(w.text);
        Debug.Log(persona.email);
        texto.text = persona.nombre;
    }
}


[Serializable]
public class Persona{
    public int id = 0;
    public string nombre = "";
    public string email = "";
    public int edad = 0;
}
