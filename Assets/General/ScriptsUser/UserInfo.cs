using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo : MonoBehaviour
{

    public static string username;
    public static string email;
    public static string firstname;
    public static int[] examPuntaje = new int[6];
    public static List<Questions>[] questionsArray = new List<Questions>[6];
    public static Columns[] columnsArray = new Columns[6];
    public static List<Video>[] videosArray = new List<Video>[5];
    public static int puntajeTeorica = 0;
    public static int puntajePractica = 0;
    public static int puntajeFinal = 0;
    public static bool primerVideo = false;
    public static int[] leccionPuntaje = new int[6];
    public static List<Falta> faltasArray = new List<Falta>();
    public static List<Criterio> criteriosArray = new List<Criterio>();

    // public static Questions[] questionsArray = new Questions[5];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
