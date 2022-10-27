using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class retro : MonoBehaviour
{
    //-------------Feedback------------

    //Pantallas
    [SerializeField] private GameObject retroalimentacionPantalla;
    [SerializeField] private GameObject cargandoTexto;
    [SerializeField] private GameObject criteriosPantalla;
    [SerializeField] private GameObject faltasPantalla;
    [SerializeField] private GameObject calificacionPantalla;

    //criteriosPantalla
    [SerializeField] private Text tituloCriterio;
    [SerializeField] private Image circle1;
    [SerializeField] private Image circle2;
    [SerializeField] private Image circle3;
    [SerializeField] private Image circle4;
    [SerializeField] private Image circle5;
    [SerializeField] private GameObject puntaje1;
    [SerializeField] private GameObject puntaje2;
    [SerializeField] private GameObject puntaje3;
    [SerializeField] private GameObject puntaje4;
    [SerializeField] private GameObject puntaje5;
    [SerializeField] private Button buttonPrevCriterios;
    [SerializeField] private Button buttonRepCriterios;
    [SerializeField] private GameObject[] criterioPrefab = new GameObject[4];
    [SerializeField] private GameObject[] seccionPrefab = new GameObject[4];
    [SerializeField] private GameObject[] scrolls = new GameObject[4];

    //faltasPantalla
    [SerializeField] private Text tituloFalta;
    [SerializeField] private Text subtituloFalta;
    [SerializeField] private GameObject[] tipoPrefab = new GameObject[4];
    [SerializeField] private GameObject[] faltaPrefab = new GameObject[4];
    [SerializeField] private GameObject[] scrollsFaltas = new GameObject[4];

    //calificacionPantalla
    [SerializeField] private Text tituloCalificacion;
    [SerializeField] private Text textoReprobado;
    [SerializeField] private Text textoAprobado;
    [SerializeField] private Image circleGeneral;
    [SerializeField] private Text puntaje;

    //variables
    private List<Falta> faltas = new List<Falta>();
    private List<Criterio> criterios = new List<Criterio>();
    private int[] calis = new int[5];
    private int[] calisLeccion5 = new int[16];
    private int leccion, numeroSecciones = 0, caliTotal = 0;
    private bool hayFaltas, feedback;
    private int etapa = 0;

    //-------------Feedback------------

    private int s1Cali = 100, s2Cali = 100, s3Cali = 100, s4Cali = 100;
    private int s3SalioCarril, s3FueraRangoVelocidad;
    private float distanciaConCono;
    
    
    void Start()
    {
        scrolls[0].SetActive(true);
        scrolls[1].SetActive(false);
        scrolls[2].SetActive(false);
        scrolls[3].SetActive(false);
        scrollsFaltas[0].SetActive(true);
        scrollsFaltas[1].SetActive(false);
        scrollsFaltas[2].SetActive(false);
        scrollsFaltas[3].SetActive(false);
        buttonPrevCriterios.gameObject.SetActive(false);
        buttonRepCriterios.gameObject.SetActive(true);
        retroalimentacionPantalla.SetActive(true);
        cargandoTexto.SetActive(true);
        criteriosPantalla.SetActive(false);
        faltasPantalla.SetActive(false);
        calificacionPantalla.SetActive(false);
        leccion = ExamNumber.examNumber;
        feedback = ExamNumber.feedbackP;
        ExamNumber.feedbackP = false;
        if(feedback){
            ExamNumber.criteriosAux = new List<Criterio>();
            ExamNumber.faltasAux = new List<Falta>();
            foreach (var item in UserInfo.criteriosArray)
            {
                if(item.leccion == leccion){
                    ExamNumber.criteriosAux.Add(item);
                }
            }
            foreach (var item in UserInfo.faltasArray)
            {
                if(item.leccion == leccion){
                    ExamNumber.faltasAux.Add(item);
                }
            }
        }
        if(leccion == 6){
            puntaje1.SetActive(true);
            puntaje2.SetActive(false);
            puntaje3.SetActive(false);
            puntaje4.SetActive(false);
            puntaje5.SetActive(false);
            calis[0] = 100;
            calis[1] = 0;
            calis[2] = 0;
            calis[3] = 0;
            calis[4] = 0;
            numeroSecciones = 1;
            tituloCriterio.text = "Criterios del Examen";
            tituloFalta.text = "Faltas del Examen";
            subtituloFalta.text = "PUNTOS MENOS (EXAMEN)";
            tituloCalificacion.text = "EXAMEN:";
            hayFaltas = true;
        }else{
            puntaje1.SetActive(true);
            puntaje2.SetActive(true);
            puntaje3.SetActive(true);
            puntaje4.SetActive(true);
            puntaje5.SetActive(false);
            calis[0] = 100;
            calis[1] = 100;
            calis[2] = 100;
            calis[3] = 100;
            calis[4] = 0;
            numeroSecciones = 4;
            tituloCriterio.text = "Criterios de la Lección";
            tituloFalta.text = "Faltas de la Lección";
            subtituloFalta.text = "PUNTOS MENOS (LECCIÓN)";
            tituloCalificacion.text = "LECCIÓN:";
            hayFaltas = true;
        }
        if(leccion == 3){
            calis[4] = 100;
            puntaje5.SetActive(true);
            numeroSecciones = 5;
        }
        if(leccion == 1 || leccion == 2){
            hayFaltas = false;
        }
        int numEtapas = 1;
        if(leccion == 5){
            numEtapas = 4;
        }
        int leccion5Final = 0; 
        for(int j = 0; j < numEtapas * 4; j += 4){
            calis[0] = 100;
            calis[1] = 100;
            calis[2] = 100;
            calis[3] = 100;
            caliTotal = 0;
            for(int i = 1; i <= numeroSecciones; i++){
                foreach (var item in ExamNumber.criteriosAux)
                {
                    if(item.leccion == leccion && item.seccion == i + j){
                        calis[i-1] -= item.veces * item.puntos;
                    }
                }
                if(calis[i-1] < 0){
                    calis[i-1] = 0;
                }
                calisLeccion5[(i-1)+j] = calis[i-1];
                caliTotal += calis[i-1];
            }
            caliTotal = caliTotal / numeroSecciones;
            if(hayFaltas){
                foreach (var item in ExamNumber.faltasAux)
                {
                    if(item.leccion == leccion){
                        if(leccion == 5){
                            if(item.etapa == (j/4) + 1){
                                caliTotal -= item.veces * item.puntos;
                            }
                        }else{
                            caliTotal -= item.veces * item.puntos;
                        }
                    }
                }
            }
            leccion5Final = leccion5Final + caliTotal;
        }
        if(leccion == 5){
            caliTotal = leccion5Final / 4;
        }
        for(int i = 0; i < 16; i++){
            Debug.Log(calisLeccion5[i]);
        }

        //secciones
        
        if(leccion == 5){
            etapa = 1;
            circle1.fillAmount = calisLeccion5[0]/100f;
            circle2.fillAmount = calisLeccion5[1]/100f;
            circle3.fillAmount = calisLeccion5[2]/100f;
            circle4.fillAmount = calisLeccion5[3]/100f;
            tituloCriterio.text = "Criterios de Dia";
            for(int j = 0; j < numEtapas * 4; j += 4){
                for(int i = 0; i < 4; i++){
                    GameObject seccion = Instantiate(seccionPrefab[j/4]) as GameObject;
                    seccion.SetActive(true);
                    seccion.GetComponent<textoFilas>().SetText("SECCIÓN " + (i+1), "SECCIÓN " + (i+1), "SECCIÓN " + (i+1));
                    seccion.transform.SetParent(seccionPrefab[j/4].transform.parent, false);
                    
                    if(calisLeccion5[i+j] < 100){
                        foreach (var item in ExamNumber.criteriosAux)
                        {
                            if(item.seccion == i+1+j){
                                GameObject elemento = Instantiate(criterioPrefab[j/4]) as GameObject;
                                elemento.SetActive(true);
                                elemento.GetComponent<textoFilas>().SetText(item.texto + "", item.veces + "", (item.veces * item.puntos) + "");
                                elemento.transform.SetParent(criterioPrefab[j/4].transform.parent, false);
                            }
                        }
                    }else{
                        GameObject ningun = Instantiate(criterioPrefab[j/4]) as GameObject;
                        ningun.SetActive(true);
                        ningun.GetComponent<textoFilas>().SetText("No se violó ningun criterio", "--", "--");
                        ningun.transform.SetParent(criterioPrefab[j/4].transform.parent, false);
                    }
                }
            }
        }else{
            for (int i = 0; i < numeroSecciones; i++)
            {
                GameObject seccion = Instantiate(seccionPrefab[0]) as GameObject;
                seccion.SetActive(true);
                seccion.GetComponent<textoFilas>().SetText("SECCIÓN " + (i+1), "SECCIÓN " + (i+1), "SECCIÓN " + (i+1));
                seccion.transform.SetParent(seccionPrefab[0].transform.parent, false);
                
                if(calis[i] < 100){
                    foreach (var item in ExamNumber.criteriosAux)
                    {
                        if(item.seccion == i+1){
                            GameObject elemento = Instantiate(criterioPrefab[0]) as GameObject;
                            elemento.SetActive(true);
                            elemento.GetComponent<textoFilas>().SetText(item.texto + "", item.veces + "", (item.veces * item.puntos) + "");
                            elemento.transform.SetParent(criterioPrefab[0].transform.parent, false);
                        }
                    }
                }else{
                    GameObject ningun = Instantiate(criterioPrefab[0]) as GameObject;
                    ningun.SetActive(true);
                    ningun.GetComponent<textoFilas>().SetText("No se violó ningun criterio", "--", "--");
                    ningun.transform.SetParent(criterioPrefab[0].transform.parent, false);
                }
            }
            circle1.fillAmount = calis[0]/100f;
            circle2.fillAmount = calis[1]/100f;
            circle3.fillAmount = calis[2]/100f;
            circle4.fillAmount = calis[3]/100f;
            circle5.fillAmount = calis[4]/100f;
        }
        

        //faltas
        if(hayFaltas){
            string[] tipos = {"leves", "deficientes", "graves"};
            if(leccion == 5){
                for(int j = 0; j < 4; j++){
                    for (int i = 0; i < 3; i++)
                    {
                        GameObject seccion = Instantiate(tipoPrefab[j]) as GameObject;
                        seccion.SetActive(true);
                        seccion.GetComponent<textoFilas>().SetText(tipos[i].ToUpper(), tipos[i].ToUpper(), tipos[i].ToUpper());
                        seccion.transform.SetParent(tipoPrefab[j].transform.parent, false);
                        
                        int contadorFaltas = 0;
                        foreach (var item in ExamNumber.faltasAux)
                        {
                            if(item.tipo == tipos[i] && item.etapa == j+1){
                                contadorFaltas++;
                                GameObject elemento = Instantiate(faltaPrefab[j]) as GameObject;
                                elemento.SetActive(true);
                                elemento.GetComponent<textoFilas>().SetText(item.texto + "", item.veces + "", (item.veces * item.puntos) + "");
                                elemento.transform.SetParent(faltaPrefab[j].transform.parent, false);
                            }
                        }
                        if(contadorFaltas == 0){
                            GameObject ningun = Instantiate(faltaPrefab[j]) as GameObject;
                            ningun.SetActive(true);
                            ningun.GetComponent<textoFilas>().SetText("No se cometío ninguna falta", "--", "--");
                            ningun.transform.SetParent(faltaPrefab[j].transform.parent, false);
                        }
                        
                    }
                }
            }else{
                for (int i = 0; i < 3; i++)
                {
                    GameObject seccion = Instantiate(tipoPrefab[0]) as GameObject;
                    seccion.SetActive(true);
                    seccion.GetComponent<textoFilas>().SetText(tipos[i].ToUpper(), tipos[i].ToUpper(), tipos[i].ToUpper());
                    seccion.transform.SetParent(tipoPrefab[0].transform.parent, false);
                    
                    int contadorFaltas = 0;
                    foreach (var item in ExamNumber.faltasAux)
                    {
                        if(item.tipo == tipos[i]){
                            contadorFaltas++;
                            GameObject elemento = Instantiate(faltaPrefab[0]) as GameObject;
                            elemento.SetActive(true);
                            elemento.GetComponent<textoFilas>().SetText(item.texto + "", item.veces + "", (item.veces * item.puntos) + "");
                            elemento.transform.SetParent(faltaPrefab[0].transform.parent, false);
                        }
                    }
                    if(contadorFaltas == 0){
                        GameObject ningun = Instantiate(faltaPrefab[0]) as GameObject;
                        ningun.SetActive(true);
                        ningun.GetComponent<textoFilas>().SetText("No se cometío ninguna falta", "--", "--");
                        ningun.transform.SetParent(faltaPrefab[0].transform.parent, false);
                    }
                    
                }
            }
        }

        //calificacion
        puntaje.text = caliTotal + "";
        circleGeneral.fillAmount = caliTotal/100f;

        //aprobado
        if(caliTotal>79){
            textoAprobado.gameObject.SetActive(true);
            textoReprobado.gameObject.SetActive(false);
        }else{
            textoAprobado.gameObject.SetActive(false);
            textoReprobado.gameObject.SetActive(true);
        }
        if(caliTotal > UserInfo.leccionPuntaje[leccion-1] &&  !feedback){
            UserInfo.leccionPuntaje[leccion-1] = caliTotal;
            UserInfo.faltasArray = new List<Falta>();
            UserInfo.criteriosArray = new List<Criterio>();
            foreach (var item in ExamNumber.criteriosAux)
            {
                UserInfo.criteriosArray.Add(item);
            }
            foreach (var item in ExamNumber.faltasAux)
            {
                UserInfo.faltasArray.Add(item);
            }
            StartCoroutine(CO_Guardar());
        }else{
            retroalimentacionPantalla.SetActive(true);
            criteriosPantalla.SetActive(true);
            faltasPantalla.SetActive(false);
            calificacionPantalla.SetActive(false);
            cargandoTexto.SetActive(false);
        }
    }

    



    public void repetir (){
        switch (leccion)
        {
            case 1:{
                SceneManager.LoadScene("MenuSeleccion1");
                break;
            }
            case 2:{
                SceneManager.LoadScene("MenuSeleccion2");
                break;
            }
            case 3:{
                SceneManager.LoadScene("MenuSeleccion3");
                break;
            }
            case 4:{
                SceneManager.LoadScene("MenuSeleccion4");
                break;
            }
            case 5:{
                SceneManager.LoadScene("MenuSeleccion5");
                break;
            }
            case 6:{
                SceneManager.LoadScene("MenuSeleccion6");
                break;
            }
            default:{
                break;
            }
        }
        
    }
    public void mover(int option){
        switch (option)
        {
            case 1:{
                if(leccion == 5){
                    switch (etapa)
                    {
                        case 1:{
                            etapa++;
                            tituloCriterio.text = "Criterios de Noche";
                            buttonRepCriterios.gameObject.SetActive(false);
                            buttonPrevCriterios.gameObject.SetActive(true);
                            scrolls[0].SetActive(false);
                            scrolls[1].SetActive(true);
                            scrolls[2].SetActive(false);
                            scrolls[3].SetActive(false);
                            circle1.fillAmount = calisLeccion5[4]/100f;
                            circle2.fillAmount = calisLeccion5[5]/100f;
                            circle3.fillAmount = calisLeccion5[6]/100f;
                            circle4.fillAmount = calisLeccion5[7]/100f;
                            break;
                        }
                        case 2:{
                            etapa++;
                            tituloCriterio.text = "Criterios de Neblina";
                            scrolls[0].SetActive(false);
                            scrolls[1].SetActive(false);
                            scrolls[2].SetActive(true);
                            scrolls[3].SetActive(false);
                            circle1.fillAmount = calisLeccion5[8]/100f;
                            circle2.fillAmount = calisLeccion5[9]/100f;
                            circle3.fillAmount = calisLeccion5[10]/100f;
                            circle4.fillAmount = calisLeccion5[11]/100f;
                            break;
                        }
                        case 3:{
                            etapa++;
                            tituloCriterio.text = "Criterios de Lluvia";
                            scrolls[0].SetActive(false);
                            scrolls[1].SetActive(false);
                            scrolls[2].SetActive(false);
                            scrolls[3].SetActive(true);
                            circle1.fillAmount = calisLeccion5[12]/100f;
                            circle2.fillAmount = calisLeccion5[13]/100f;
                            circle3.fillAmount = calisLeccion5[14]/100f;
                            circle4.fillAmount = calisLeccion5[15]/100f;
                            break;
                        }
                        case 4:{
                            etapa = 1;
                            criteriosPantalla.SetActive(false);
                            faltasPantalla.SetActive(true);
                            tituloFalta.text = "Faltas de Dia";
                            scrollsFaltas[0].SetActive(true);
                            scrollsFaltas[1].SetActive(false);
                            scrollsFaltas[2].SetActive(false);
                            scrollsFaltas[3].SetActive(false);
                            break;
                        }
                        default:{
                            break;
                        }
                    }
                }else{
                    criteriosPantalla.SetActive(false);
                    if(hayFaltas){
                        faltasPantalla.SetActive(true);
                    }else{
                        calificacionPantalla.SetActive(true);
                    }
                }
                break;
            }
            case 2:{
                if(leccion == 5){
                    switch (etapa)
                    {
                        case 1:{
                            etapa = 4;
                            criteriosPantalla.SetActive(true);
                            faltasPantalla.SetActive(false);
                            break;
                        }
                        case 2:{
                            etapa--;
                            tituloFalta.text = "Faltas de Dia";
                            scrollsFaltas[0].SetActive(true);
                            scrollsFaltas[1].SetActive(false);
                            scrollsFaltas[2].SetActive(false);
                            scrollsFaltas[3].SetActive(false);
                            break;
                        }
                        case 3:{
                            etapa--;
                            tituloFalta.text = "Faltas de Noche";
                            scrollsFaltas[0].SetActive(false);
                            scrollsFaltas[1].SetActive(true);
                            scrollsFaltas[2].SetActive(false);
                            scrollsFaltas[3].SetActive(false);
                            break;
                        }
                        case 4:{
                            etapa--;
                            tituloFalta.text = "Faltas de Neblina";
                            scrollsFaltas[0].SetActive(false);
                            scrollsFaltas[1].SetActive(false);
                            scrollsFaltas[2].SetActive(true);
                            scrollsFaltas[3].SetActive(false);
                            break;
                        }
                        default:{
                            break;
                        }
                    }
                }else{
                    criteriosPantalla.SetActive(true);
                    faltasPantalla.SetActive(false);
                }
                break;
            }
            case 3:{
                if(leccion == 5){
                    switch (etapa)
                    {
                        case 1:{
                            etapa++;
                            tituloFalta.text = "Faltas de Noche";
                            scrollsFaltas[0].SetActive(false);
                            scrollsFaltas[1].SetActive(true);
                            scrollsFaltas[2].SetActive(false);
                            scrollsFaltas[3].SetActive(false);
                            break;
                        }
                        case 2:{
                            etapa++;
                            tituloFalta.text = "Faltas de Neblina";
                            scrollsFaltas[0].SetActive(false);
                            scrollsFaltas[1].SetActive(false);
                            scrollsFaltas[2].SetActive(true);
                            scrollsFaltas[3].SetActive(false);
                            break;
                        }
                        case 3:{
                            etapa++;
                            tituloFalta.text = "Faltas de Lluvia";
                            scrollsFaltas[0].SetActive(false);
                            scrollsFaltas[1].SetActive(false);
                            scrollsFaltas[2].SetActive(false);
                            scrollsFaltas[3].SetActive(true);
                            break;
                        }
                        case 4:{
                            calificacionPantalla.SetActive(true);
                            faltasPantalla.SetActive(false);
                            break;
                        }
                        default:{
                            break;
                        }
                    }
                }else{
                    calificacionPantalla.SetActive(true);
                    faltasPantalla.SetActive(false);
                }
                break;
            }
            case 4:{
                calificacionPantalla.SetActive(false);
                if(hayFaltas){
                    faltasPantalla.SetActive(true);
                }else{
                    criteriosPantalla.SetActive(true);
                }
                break;
            }
            case 5:{
                switch (etapa)
                {
                    case 2:{
                        etapa--;
                        tituloCriterio.text = "Criterios de Dia";
                        buttonRepCriterios.gameObject.SetActive(true);
                        buttonPrevCriterios.gameObject.SetActive(false);
                        scrolls[0].SetActive(true);
                        scrolls[1].SetActive(false);
                        scrolls[2].SetActive(false);
                        scrolls[3].SetActive(false);
                        circle1.fillAmount = calisLeccion5[0]/100f;
                        circle2.fillAmount = calisLeccion5[1]/100f;
                        circle3.fillAmount = calisLeccion5[2]/100f;
                        circle4.fillAmount = calisLeccion5[3]/100f;
                        break;
                    }
                    case 3:{
                        etapa--;
                        tituloCriterio.text = "Criterios de Noche";
                        scrolls[0].SetActive(false);
                        scrolls[1].SetActive(true);
                        scrolls[2].SetActive(false);
                        scrolls[3].SetActive(false);
                        circle1.fillAmount = calisLeccion5[4]/100f;
                        circle2.fillAmount = calisLeccion5[5]/100f;
                        circle3.fillAmount = calisLeccion5[6]/100f;
                        circle4.fillAmount = calisLeccion5[7]/100f;
                        break;
                    }
                    case 4:{
                        etapa--;
                        tituloCriterio.text = "Criterios de Neblina";
                        scrolls[0].SetActive(false);
                        scrolls[1].SetActive(false);
                        scrolls[2].SetActive(true);
                        scrolls[3].SetActive(false);
                        circle1.fillAmount = calisLeccion5[8]/100f;
                        circle2.fillAmount = calisLeccion5[9]/100f;
                        circle3.fillAmount = calisLeccion5[10]/100f;
                        circle4.fillAmount = calisLeccion5[11]/100f;
                        break;
                    }
                    default:{
                        break;
                    }
                }
                break;
            }
            default:{
                break;
            }
        }
    }

    public void continuar(){
        SceneManager.LoadScene("MenuPractico");
    }

    private IEnumerator CO_Guardar(){

        WWWForm form = new WWWForm();
        form.AddField("option", 1);
        form.AddField("username", UserInfo.username);
        form.AddField("lesson", leccion);
        WWW w = new WWW("http://izyventa.com/elena/Leccion/registerLeccion.php", form);
        yield return w;
        foreach (var item in ExamNumber.criteriosAux)
        {
            form = new WWWForm();
            form.AddField("option", 3);
            form.AddField("username", UserInfo.username);
            form.AddField("text", item.texto);
            form.AddField("times", item.veces);
            form.AddField("points", item.puntos);
            form.AddField("section", item.seccion);
            form.AddField("lesson", item.leccion);
            w = new WWW("http://izyventa.com/elena/Leccion/registerLeccion.php", form);
            yield return w;
        }
        foreach (var item in ExamNumber.faltasAux)
        {
            form = new WWWForm();
            form.AddField("option", 2);
            form.AddField("username", UserInfo.username);
            form.AddField("text", item.texto);
            form.AddField("times", item.veces);
            form.AddField("points", item.puntos);
            form.AddField("type", item.tipo);
            form.AddField("lesson", item.leccion);
            form.AddField("stage", item.etapa);
            w = new WWW("http://izyventa.com/elena/Leccion/registerLeccion.php", form);
            yield return w;
        }
        form = new WWWForm();
        form.AddField("option", 4);
        form.AddField("score", caliTotal);
        form.AddField("username", UserInfo.username);
        form.AddField("lesson", leccion);
        w = new WWW("http://izyventa.com/elena/Leccion/registerLeccion.php", form);
        yield return w;

        
        retroalimentacionPantalla.SetActive(true);
        criteriosPantalla.SetActive(true);
        faltasPantalla.SetActive(false);
        calificacionPantalla.SetActive(false);
        cargandoTexto.SetActive(false);
    }
}

