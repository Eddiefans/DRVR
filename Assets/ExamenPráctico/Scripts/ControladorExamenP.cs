using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LightingManager))]
public class ControladorExamenP : MonoBehaviour
{
    public InputManager im;
    //public InputManagerForKeyboard im;
    public List<WheelCollider> throttleWheels;
    public List<WheelCollider> steeringWheels;
    public float strenghtCoefficient = 10000f;
    public float maxTurn = 20f;
    public Transform cm;
    public Rigidbody rb;
    public LightingManager lm;
    public float brakeStrenght;
    public List<GameObject> tailLights;
    private MeshDestroy start;
    public AudioSource breakingGlass;
    public AudioSource engineSound, turnOnSound, bienHecho, felicidades, rojo;
    private bool bEncender = false;
    private float velocidadMaxima = 80;
    private bool coroutine2,coroutine3, coroutine4;
    private int layerMask = 1 << 6;
    private int layermask2 = 2 << 8;
    private bool choque;

    private IEnumerator coroutineExamen, coroutineExamen2 , felicitacion;
    private bool LeccionFinished , leccionFinished2;
    private bool noGear = true;
    
    public AudioSource cambioVelocidad;

    /*------------Audios y booleans seccion 1-------------*/

    public AudioSource  intro, enciendeAuto, salEstacionamiento, avanza, zonaPeatonal,
                        glorieta,salidaGlorieta , vuelta , subeGasa, integracion, sigueDer, carretera, curvas;
    private bool  bAvanza, bZonaPeatonal, bGlorieta, bSalGlorieta,bVuelta,bSubida,
                        bIntegracion, bSigueDer, bCarretera, bCurvas, bFin;
    public GameObject avanzaObj, zonaPeatonalObj, glorietaObj, salGlorietaObj,vueltaObj,subidaObj,
                        integracionObj, sigueDerObj, carreteraObj, curvasObj, FINobj;

    /*------------Audios y booleans seccion 2-------------*/
    public AudioSource introparte2,enciende2, acelera, vueltaS2,obstaculo, reincorporate, 
                rebasa, giraIzq, giraDer, cuidadito, carreteraAudio;
    private bool introB, enciendeB, aceleraB, vueltaS2B, obstaculoB, reincorporateB, 
                rebasaB, giraIzqB,giraDerB, adelantaB, s2_bIsFirstTimeCar, frenoSecoB, llego60;
    public GameObject teletransportacion,vueltaS2O,obstaculoO,reincorporateO
                ,rebasaO, giraIzqO, giraDerO, carro1Box, obstaculoCheck 
                , adelantacheck, frenoPa, teletransportacion2, finDeFinales;
    public VehicleAiController s1_vehicleAiController , s2_vehicleAiController;
    private int mamoObstaculo = 0, mamoArrebasada = 0, mamoFrenoSeco = 0, mamo60=0;

    public GameObject vueltaIzqInicio, vueltaIzqFin;
    public AudioSource asvueltaIzq;


    /* Puntuaciones */
    private int LeccionCali = 100;
    public GameObject incorporacion, velZonaPeatonal, glorietaCarril, fg5_goSafeZoneChris;
    float time,time2,time3, time4, timeDef6;
    private RaycastHit hit1;

    private  int pasoRojo=0 , choquesito=0, ExcesoVelPeaton=0 , incorporacionCal=0 ,
                carrilesCount=0, glorietaError=0, ExcesoVel=0, seParo=0, vasLento=0;
    public List<GameObject> S1_carriles, pasosPeatonAlto;

    /* Faltas */
    public List<GameObject> fg1_goPrimerTope, fg1_goSegundoTope, fg2_goCuboSemaforo, fg3_goZonaPeatonal, fg8_goLineas, fd5_goCuboAmarillo;
    public GameObject fg5_goSafeZone, fl2_goTope1, fl2_goTope2;
    private float fg1_iVelocidadEntrada,time8;
    private int faltasGraves, fd4_iVelocidadActual, fd4_iVelocidadAnterior, faltasDeficientes, faltasLeves;
    private int grave1, grave2, grave3, grave4, grave5, grave6, grave8, def1,  def2, def3, def4, def5, def6, leve1, leve2, leve3;
    private bool[] fg1_bYaEsteTope1, fg1_bYaEsteTope2, fg2_bYaEsteCubo, fg3_bSeParo, fg8_bYaLinea, fd5_bYaEsteAmarillo, fd4_sancionesVelocidades;
    private bool fl2_bTope1, fl2_bTope2;
    public UnityEvent eventoRestart, eventoReturn;

              /*------------Pantallas y textos de faltas y módulo de Alertas-------------*/
    
    [SerializeField] private GameObject  alerta;
    [SerializeField] private Text  textoMotivo;
    private AudioSource[] audios;
    private bool isOver;
    [SerializeField] private GameObject  faltaLeve;
    [SerializeField] private GameObject  faltaDeficiente;
    [SerializeField] private GameObject  faltaGrave;
    [SerializeField] private Text  textoLeve;
    [SerializeField] private Text  textoDeficiente;
    [SerializeField] private Text  textoGrave;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        StartCoroutine(coroutineExamen);
        StartCoroutine(delayTurnOn());
        StartCoroutine(delayLuces());
        start = GameObject.FindGameObjectWithTag("VentanaARomper").GetComponent<MeshDestroy>();
        im = GetComponent<InputManager>();
        //im = GetComponent<InputManagerForKeyboard>();
        rb = GetComponent<Rigidbody>();

        if(cm){
            rb.centerOfMass = cm.localPosition; //comentario random que no sirve para nada

        }
    }

    private void Init(){

        coroutineExamen = LeccionFinal();
        felicitacion = finalLeccion();
        coroutineExamen2 = LeccionFinal2();

        fg1_bYaEsteTope1 = new bool[fg1_goPrimerTope.Count];
        fg1_bYaEsteTope2 = new bool[fg1_goSegundoTope.Count];
        fg3_bSeParo = new bool[fg3_goZonaPeatonal.Count];
        fd4_sancionesVelocidades = new bool[7];
        fd4_sancionesVelocidades[0] = true;
        

    }

    private void Awake() {

    }

    void Update()
    {

        /* if(im.l){
            lm.ToggleHeadlights();
            Debug.Log("yata");
        } */

        foreach (GameObject tl in tailLights)
        {
            tl.GetComponent<Renderer>().material.SetColor("_EmissionColor", im.brakeNormal ? new Color(0.5f, 0.111f, 0.111f) : Color.black);

        }

        leccion();
        //turnOn();
        faltas();
        checkFaltas();
        checkVelocidad();
        checkButtons();
    }

    private void checkFaltas(){
      if(faltasGraves > 3){
          //detenerLeccion
          alerta.SetActive(true);
          textoMotivo.text = "Acumulación de 4 faltas Graves";
          Debug.Log("Se debe detener lección por faltas graves");
          inmovilizarCarro();
      }
      if(faltasDeficientes > 5){
          //detenerLeccion
          alerta.SetActive(true);
          textoMotivo.text = "Acumulación de 6 faltas Deficientes";
          Debug.Log("Se debe detener lección por faltas deficientes");
          inmovilizarCarro();
      }
    }

    public void eventoAtropellarPeaton(){
        alerta.SetActive(true);
        textoMotivo.text = "Atropellaste a un peatón";
        Debug.Log("Se debe detener lección por atropellamiento");
        inmovilizarCarro();
    }

    private void inmovilizarCarro(){
        isOver = true;
        audios = GameObject.Find("Examen").GetComponentsInChildren<AudioSource>();
        foreach (var item in audios)
        {
            item.Pause();
        }
        rb.MovePosition(transform.position + new Vector3(0,10,0));
        rb.useGravity = false;
    }

    private void checkButtons(){
        if(isOver){
            if(im.restart){
                eventoRestart?.Invoke();
                Debug.Log("ghola chaval");
            }
            
        }
        if(im.returned){
            Debug.Log("ghola chaval");
            eventoReturn?.Invoke();
        }
    }

    public void eventoFaltaGrave2(){
        faltasGraves++;
        grave2++;
        StartCoroutine(delayFalta(3, "No detenerse en semáforo rojo"));
        Debug.Log("Grave 2");
    }

    public void eventoFaltaGrave5(){
        faltasGraves++;
        grave5++;
        StartCoroutine(delayFalta(3, "No dejar pasar al peatón"));
        Debug.Log("Grave 5");
    }

    public void eventoFaltaGrave6(){
        faltasGraves++;
        grave6++;
        StartCoroutine(delayFalta(3, "Al girar a la izquierda no ceder el paso a los que vienen de frente"));
        Debug.Log("Grave 6");
    }

    public void eventoFaltaDeficiente5(){
        faltasDeficientes++;
        def5++;
        StartCoroutine(delayFalta(2, "No detenerse en semáforo amarillo"));
        Debug.Log("Deficiente 5");
    }

    public void eventoFaltaDeficiente6(){
        if(timeDef6+3 < Time.time){
            timeDef6 = Time.time;
            faltasDeficientes++;
            def6++;
            StartCoroutine(delayFalta(2, "No mantener la disancia con respecto a otro vehículos"));
            Debug.Log("Deficiente 6");
        }
    }

    private void checkVelocidad(){
        if(bEncender){
            if(im.clutch){
                
                if(im.v1){
                  cambioVelocidad.Play();
                    noGear = false;
                    fd4_iVelocidadActual = 1;
                    velocidadMaxima = 20;
                    Thread.Sleep(150);
                    Debug.Log("Velocidad 1");
                    sancionVelocidades(1);
                }else if(im.v2){
                  cambioVelocidad.Play();
                    fd4_iVelocidadActual = 2;
                    velocidadMaxima = 40;
                    Thread.Sleep(150);
                    Debug.Log("Velocidad 2");
                    sancionVelocidades(2);
                }else if(im.v3){
                  cambioVelocidad.Play();
                    fd4_iVelocidadActual = 3;
                    velocidadMaxima = 60;
                    Thread.Sleep(150);
                    Debug.Log("Velocidad 3");
                    sancionVelocidades(3);
                }else if(im.v4){
                  cambioVelocidad.Play();
                    fd4_iVelocidadActual = 4;
                    velocidadMaxima = 80;
                    Thread.Sleep(150);
                    Debug.Log("Velocidad 4");
                    sancionVelocidades(4);
                }else if(im.v5){
                  cambioVelocidad.Play();
                    fd4_iVelocidadActual = 5;
                    velocidadMaxima = 120;
                    Thread.Sleep(150);
                    Debug.Log("Velocidad 5");
                    sancionVelocidades(5);
                }else if(im.vR){
                  cambioVelocidad.Play();
                    noGear = false;
                    fd4_iVelocidadActual = 6;
                    velocidadMaxima = 30;
                    Thread.Sleep(150);
                    Debug.Log("Velocidad reversa");
                    sancionVelocidades(6);
                }/* else if(im.CurrentGear == 0){
                    fd4_iVelocidadActual = 0;
                    velocidadMaxima = 0;
                    Thread.Sleep(150);
                    Debug.Log("Velocidad neutral");
                    sancionVelocidades(0);
                } */
            }else{
                if(im.v1 && !fd4_sancionesVelocidades[1]){
                  cambioVelocidad.Play();
                    noGear = false;
                    fd4_iVelocidadActual = 1;
                    velocidadMaxima = 20;
                    Thread.Sleep(150);
                    Debug.Log("Velocidad 1");
                    faltasDeficientes++;
                    def4++;
                    StartCoroutine(delayFalta(2, "No pisar el clutch para cambiar de velocidad"));
                    Debug.Log("Deficiente 4");
                    sancionVelocidades(1);
                }else if(im.v2 && !fd4_sancionesVelocidades[2]){
                  cambioVelocidad.Play();
                    fd4_iVelocidadActual = 2;
                    velocidadMaxima = 40;
                    Thread.Sleep(150);
                    Debug.Log("Velocidad 2");
                    faltasDeficientes++;
                    def4++;
                    StartCoroutine(delayFalta(2, "No pisar el clutch para cambiar de velocidad"));
                    Debug.Log("Deficiente 4");
                    sancionVelocidades(2);
                }else if(im.v3 && !fd4_sancionesVelocidades[3]){
                  cambioVelocidad.Play();
                    fd4_iVelocidadActual = 3;
                    velocidadMaxima = 60;
                    Thread.Sleep(150);
                    Debug.Log("Velocidad 3");
                    faltasDeficientes++;
                    def4++;
                    StartCoroutine(delayFalta(2, "No pisar el clutch para cambiar de velocidad"));
                    Debug.Log("Deficiente 4");
                    sancionVelocidades(3);
                }else if(im.v4 && !fd4_sancionesVelocidades[4]){
                  cambioVelocidad.Play();
                    fd4_iVelocidadActual = 4;
                    velocidadMaxima = 80;
                    Thread.Sleep(150);
                    Debug.Log("Velocidad 4");
                    faltasDeficientes++;
                    def4++;
                    StartCoroutine(delayFalta(2, "No pisar el clutch para cambiar de velocidad"));
                    Debug.Log("Deficiente 4");
                    sancionVelocidades(4);
                }else if(im.v5 && !fd4_sancionesVelocidades[5]){
                  cambioVelocidad.Play();
                    fd4_iVelocidadActual = 5;
                    velocidadMaxima = 120;
                    Thread.Sleep(150);
                    Debug.Log("Velocidad 5");
                    faltasDeficientes++;
                    def4++;
                    StartCoroutine(delayFalta(2, "No pisar el clutch para cambiar de velocidad"));
                    Debug.Log("Deficiente 4");
                    sancionVelocidades(5);
                }else if(im.vR && !fd4_sancionesVelocidades[6]){
                  cambioVelocidad.Play();
                    noGear = false;
                    fd4_iVelocidadActual = 6;
                    velocidadMaxima = 30;
                    Thread.Sleep(150);
                    Debug.Log("Velocidad reversa");
                    faltasDeficientes++;
                    def4++;
                    StartCoroutine(delayFalta(2, "No pisar el clutch para cambiar de velocidad"));
                    Debug.Log("Deficiente 4");
                    sancionVelocidades(6);
                }/* else if(im.CurrentGear == 0 && !fd4_sancionesVelocidades[0]){
                    fd4_iVelocidadActual = 0;
                    velocidadMaxima = 0;
                    Thread.Sleep(150);
                    Debug.Log("Velocidad neutral");
                    faltasDeficientes++;
                    def4++;
                    StartCoroutine(delayFalta(2, "No pisar el clutch para cambiar de velocidad"));
                    Debug.Log("Deficiente 4");
                    sancionVelocidades(0);
                } */
            }
        }
    }

    private void sancionVelocidades(int j){
        for (int i = 0; i < fd4_sancionesVelocidades.Length; i++)
        {
            if(i == j){
                fd4_sancionesVelocidades[i] = true;
            }else{
                fd4_sancionesVelocidades[i] = false;
            }
        }
    }

    private void checkVelocidadNuevo(){
        if(bEncender){
            if(im.v1){
              noGear = false;
                if(im.clutch){
                    fd4_iVelocidadActual = 1;
                    velocidadMaxima = 20;
                    Thread.Sleep(150);
                    Debug.Log("Velocidad 1");
                }else{
                    faltasDeficientes++;
                    def4++;
                    StartCoroutine(delayFalta(2, "No pisar el clutch para cambiar de velocidad"));
                    Debug.Log("Deficiente 4");
                }
            }else if(im.v2){
                if(im.clutch){
                    fd4_iVelocidadActual = 2;
                    velocidadMaxima = 40;
                    Thread.Sleep(150);
                    Debug.Log("Velocidad 2");
                }else{
                    faltasDeficientes++;
                    def4++;
                    StartCoroutine(delayFalta(2, "No pisar el clutch para cambiar de velocidad"));
                    Debug.Log("Deficiente 4");
                }
            }else if(im.v3){
                if(im.clutch){
                    fd4_iVelocidadActual = 3;
                    velocidadMaxima = 60;
                    Thread.Sleep(150);
                    Debug.Log("Velocidad 3");
                }else{
                    faltasDeficientes++;
                    def4++;
                    StartCoroutine(delayFalta(2, "No pisar el clutch para cambiar de velocidad"));
                    Debug.Log("Deficiente 4");
                }
            }else if(im.v4 ){
                if(im.clutch){
                    fd4_iVelocidadActual = 4;
                    velocidadMaxima = 80;
                    Thread.Sleep(150);
                    Debug.Log("Velocidad 4");
                }else{
                    faltasDeficientes++;
                    def4++;
                    StartCoroutine(delayFalta(2, "No pisar el clutch para cambiar de velocidad"));
                    Debug.Log("Deficiente 4");
                }
            }else if(im.v5){
                if(im.clutch){
                    fd4_iVelocidadActual = 5;
                    velocidadMaxima = 200;
                    Thread.Sleep(150);
                    Debug.Log("Velocidad 5");
                }else{
                    faltasDeficientes++;
                    def4++;
                    StartCoroutine(delayFalta(2, "No pisar el clutch para cambiar de velocidad"));
                    Debug.Log("Deficiente 4");
                }
            }else if(im.vR){
                if(im.clutch){
                    fd4_iVelocidadActual = 6;
                    velocidadMaxima = 50; 
                    Thread.Sleep(150);
                    Debug.Log("Velocidad r");
                }else{
                    faltasDeficientes++;
                    def4++;
                    StartCoroutine(delayFalta(2, "No cambiar la marcha del vehículo que se requiere"));
                    Debug.Log("Deficiente 4");
                }
            }
            
            
        }
    }

    private void faltas(){
        //Grave 1
        for(int i = 0; i < fg1_goPrimerTope.Count; i++){
            if(Physics.CheckBox(fg1_goPrimerTope[i].transform.position, fg1_goPrimerTope[i].transform.localScale / 2,
            fg1_goPrimerTope[i].transform.rotation, layerMask) && !fg1_bYaEsteTope1[i]){
                fg1_iVelocidadEntrada = rb.velocity.magnitude;
                fg1_bYaEsteTope1[i] = true;
            } 
        }
        for(int i = 0; i < fg1_goSegundoTope.Count; i++){
            if(Physics.CheckBox(fg1_goSegundoTope[i].transform.position, fg1_goSegundoTope[i].transform.localScale / 2,
            fg1_goSegundoTope[i].transform.rotation, layerMask) && !fg1_bYaEsteTope2[i]){
                if(fg1_iVelocidadEntrada + 3 <= rb.velocity.magnitude){
                    faltasGraves++;
                    grave1++;
                    StartCoroutine(delayFalta(3, "No reducir velocidad en zona peatonal"));
                    Debug.Log("Grave 1");
                }
                fg1_bYaEsteTope2[i] = true;
                
            } 
        }
        //Grave 2
        /* for(int i = 0; i < fg2_goCuboSemaforo.Count; i++){
            if(Physics.CheckBox(fg2_goCuboSemaforo[i].transform.position, fg2_goCuboSemaforo[i].transform.localScale / 2,
            fg2_goCuboSemaforo[i].transform.rotation, layerMask) && !fg2_bYaEsteCubo[i] && fg2_goCuboSemaforo[i].GetComponent<BoxCollider>().enabled){
                faltasGraves++;
                grave2++;
                StartCoroutine(delayFalta(3, "No detenerse en semáforo rojo"));
                fg2_bYaEsteCubo[i] = true;
                Debug.Log("Grave 2");
            } 
        } */
        //Grave 3
        RaycastHit hit1;
        for(int i = 0; i < fg3_goZonaPeatonal.Count; i++){    
            if(Physics.CheckBox(fg3_goZonaPeatonal[i].transform.position, fg3_goZonaPeatonal[i].transform.localScale / 2,
            fg3_goZonaPeatonal[i].transform.rotation, layerMask) && !fg3_bSeParo[i] ){            
                if(rb.velocity.magnitude < 0.1 && 
                !Physics.Raycast(fg5_goSafeZone.transform.position, fg5_goSafeZone.transform.TransformDirection(Vector3.forward), out hit1, 6)) {
                    faltasGraves++;
                    grave3++;
                    StartCoroutine(delayFalta(3, "Detenerse en paso peatonal"));
                    fg3_bSeParo[i] = true;
                    Debug.Log("Grave 3");
                }
            }            
        }
        //Grave 4
        //RaycastHit hit1;
        if(im.brakeNormal && rb.velocity.magnitude<0.5 && (time8+5 <= Time.time) &&!
        Physics.Raycast(fg5_goSafeZone.transform.position, fg5_goSafeZone.transform.TransformDirection(Vector3.forward), out hit1, 6)){
            time8 = Time.time;
            faltasGraves++;
            grave4++;
            StartCoroutine(delayFalta(3, "Detención innecesaria"));
            Debug.Log("Grave 4");
        }
        //Grave 8
        for(int i = 0; i < fg8_goLineas.Count; i++){    
            if(Physics.CheckBox(fg8_goLineas[i].transform.position, fg8_goLineas[i].transform.localScale / 2,
            fg8_goLineas[i].transform.rotation, layerMask) && !fg8_bYaLinea[i]){            
                faltasGraves++;
                grave8++;
                StartCoroutine(delayFalta(3, "Pisar línea continua"));
                fg8_bYaLinea[i] = true;
                Debug.Log("Grave 8");
            }            
        }
        //Deficiente 3
        if((fd4_iVelocidadAnterior < fd4_iVelocidadActual - 1) && (fd4_iVelocidadActual != 6)){
            faltasDeficientes++;
            def3++;
            StartCoroutine(delayFalta(2, "No cambiar la marcha del vehículo que se requiere"));
            Debug.Log("Deficiente 3");
        } 
        fd4_iVelocidadAnterior = fd4_iVelocidadActual;
        //Deficiente 5
        /* for(int i = 0; i < fd5_goCuboAmarillo.Count; i++){
            if(Physics.CheckBox(fd5_goCuboAmarillo[i].transform.position, fd5_goCuboAmarillo[i].transform.localScale / 2,
            fd5_goCuboAmarillo[i].transform.rotation, layerMask) && !fd5_bYaEsteAmarillo[i] && fd5_goCuboAmarillo[i].GetComponent<BoxCollider>().enabled){
                faltasDeficientes++;
                def5++;
                StartCoroutine(delayFalta(2, "No detenerse en semáforo amarillo"));
                fd5_bYaEsteAmarillo[i] = true;
                Debug.Log("Deficiente 5");
            } 
        } */
        //Leve 2
        if(Physics.CheckBox(fl2_goTope1.transform.position, fl2_goTope1.transform.localScale / 2,
        fl2_goTope1.transform.rotation, layerMask) && !fl2_bTope1){
            if (rb.velocity.magnitude * 7 > 15){ 
                faltasLeves++; 
                leve2++;
                StartCoroutine(delayFalta(1, "No reducir velocidad ni frenar en topes y/o baches"));
                fl2_bTope1 = true; 
            }
        }
        if(Physics.CheckBox(fl2_goTope2.transform.position, fl2_goTope2.transform.localScale / 2,
        fl2_goTope2.transform.rotation, layerMask) && !fl2_bTope2){
            if(rb.velocity.magnitude * 7 > 15){
                faltasLeves++; 
                leve2++;
                StartCoroutine(delayFalta(1, "No reducir velocidad ni frenar en topes y/o baches"));
                fl2_bTope2 = true;
            }
        }
        //Leve 3
        
    }

    private void leccion(){
        if(!LeccionFinished){
          if (bEncender) {
              if(Physics.CheckBox(avanzaObj.transform.position, avanzaObj.transform.localScale / 2,
              avanzaObj.transform.rotation , layerMask)){
                bAvanza = true;
              }
              if(Physics.CheckBox(zonaPeatonalObj.transform.position, zonaPeatonalObj.transform.localScale / 2,
              zonaPeatonalObj.transform.rotation ,layerMask) ){
                bZonaPeatonal = true;
              }
              if(Physics.CheckBox(glorietaObj.transform.position, glorietaObj.transform.localScale / 2,
              glorietaObj.transform.rotation ,layerMask) ){
                bGlorieta = true;
              }
              if(Physics.CheckBox(salGlorietaObj.transform.position, salGlorietaObj.transform.localScale / 2,
                salGlorietaObj.transform.rotation , layerMask)){
                bSalGlorieta = true;
              }
              if(Physics.CheckBox(vueltaObj.transform.position, vueltaObj.transform.localScale / 2,
                vueltaObj.transform.rotation , layerMask) ){
                bVuelta = true;
              }
              if(Physics.CheckBox(subidaObj.transform.position, subidaObj.transform.localScale / 2,
                subidaObj.transform.rotation, layerMask)){
                bSubida = true;
              }
              if(Physics.CheckBox(integracionObj.transform.position, integracionObj.transform.localScale / 2,
                integracionObj.transform.rotation, layerMask)){
                bIntegracion = true;
              }
              if(Physics.CheckBox(velZonaPeatonal.transform.position, velZonaPeatonal.transform.localScale / 2,
                velZonaPeatonal.transform.rotation, layerMask)){
                if((time+3 <= Time.time) && ((rb.velocity.magnitude * 7) > 30)){
                  time = Time.time;
                  ExcesoVelPeaton++;
                  Debug.Log("Exceso Vel peatonal"+ ExcesoVelPeaton);
                }
              }
              if(Physics.CheckBox(sigueDerObj.transform.position, sigueDerObj.transform.localScale / 2,
                sigueDerObj.transform.rotation , layerMask)){
                  if(Physics.CheckBox(incorporacion.transform.position, incorporacion.transform.localScale / 2,
                    incorporacion.transform.rotation) && (time+3 <= Time.time)){
                    time = Time.time;
                    incorporacionCal++;
                    Debug.Log("Incorporacion Incorrecta");
                  }
                  bSigueDer=true;
              }
              if(Physics.CheckBox(carreteraObj.transform.position, carreteraObj.transform.localScale / 2,
                carreteraObj.transform.rotation , layerMask) ){
                bCarretera = true;
              }
              if(Physics.CheckBox(curvasObj.transform.position, curvasObj.transform.localScale / 2,
                curvasObj.transform.rotation , layerMask) ){
                bCurvas = true;
              }
              if(Physics.CheckBox(glorietaCarril.transform.position, glorietaCarril.transform.localScale / 2,
                glorietaCarril.transform.rotation , layerMask) && (time+5 <= Time.time)){
                time = Time.time;
                glorietaError++;
                Debug.Log("Error glorieta");
              }
              if(Physics.CheckBox(FINobj.transform.position, FINobj.transform.localScale / 2,
                FINobj.transform.rotation , layerMask) ){
                bFin = true;
                StartCoroutine(coroutineExamen2);
                LeccionFinished=true;
              }
              if(!bCurvas && ((rb.velocity.magnitude * 7) > 60) && (time3+5 <= Time.time)){
                time3 = Time.time;
                Debug.Log("ExcesoVel");
                ExcesoVel++;

              }
              if(bCurvas && ((rb.velocity.magnitude * 7) > 40) && (time3+5 <= Time.time)){
                time3 = Time.time;
                Debug.Log("ExcesoVel");
                ExcesoVel++;

              }
              if(((rb.velocity.magnitude * 7) < 5) && (time4+5 <= Time.time) &&
              !Physics.Raycast(fg5_goSafeZone.transform.position, fg5_goSafeZone.transform.TransformDirection(Vector3.forward),
              out hit1, 6) && bZonaPeatonal){
                foreach (var item in pasosPeatonAlto)
                 {
                     if(Physics.CheckBox(item.transform.position, item.transform.localScale / 2,
                     item.transform.rotation , layerMask)){
                       
                     }else {
                       time4 = Time.time;
                       Debug.Log("Detencion innecesaria");
                       seParo++;
                     }
                 }


              }
              if(((rb.velocity.magnitude * 7) < 25) && (time4+5 <= Time.time) && !bCarretera && bSigueDer &&
              !Physics.Raycast(fg5_goSafeZone.transform.position, fg5_goSafeZone.transform.TransformDirection(Vector3.forward),
              out hit1, 6)){
                Debug.Log("Vas lento mi pa");
                vasLento++;
                time4 = Time.time;
              }

          }
         foreach (var item in S1_carriles)
          {
              if(Physics.CheckBox(item.transform.position, item.transform.localScale / 2,
              item.transform.rotation , layerMask) && (time2+3 <= Time.time)){
                  time2 = Time.time;
                  Debug.Log("Saliste del carril");
                  carrilesCount++;
              }
          }

        }
        if (!leccionFinished2)
        {

            if(Physics.CheckBox(vueltaS2O.transform.position, vueltaS2O.transform.localScale / 2,
                vueltaS2O.transform.rotation , layerMask) ){
                vueltaS2B = true;
              }
            if(Physics.CheckBox(obstaculoO.transform.position, obstaculoO.transform.localScale / 2,
                obstaculoO.transform.rotation , layerMask) ){
                obstaculoB = true;
              }
            if(Physics.CheckBox(reincorporateO.transform.position, reincorporateO.transform.localScale / 2,
                reincorporateO.transform.rotation , layerMask) ){
                reincorporateB = true;
              }
            if(Physics.CheckBox(rebasaO.transform.position, rebasaO.transform.localScale / 2,
                rebasaO.transform.rotation , layerMask) ){
                rebasaB = true;
              }
            if(Physics.CheckBox(giraIzqO.transform.position, giraIzqO.transform.localScale / 2,
                giraIzqO.transform.rotation , layerMask) ){
                giraIzqB = true;
              }
            if(Physics.CheckBox(giraDerO.transform.position, giraDerO.transform.localScale / 2,
                giraDerO.transform.rotation , layerMask) ){
                giraDerB = true;
                
                bEncender=false;
                engineSound.Stop();
                StopCoroutine(frenoRepentino());
              }
            if(Physics.CheckBox(carro1Box.transform.position, carro1Box.transform.localScale / 2,
                carro1Box.transform.rotation , layerMask) ){
                  s1_vehicleAiController.totalPower = 100;
                  s1_vehicleAiController.rb.useGravity = true;
                  foreach(var i in s1_vehicleAiController.wheelsX){
                        i.brakeTorque = 0;
                  }
              }
              if(Physics.CheckBox(obstaculoCheck.transform.position, obstaculoCheck.transform.localScale / 2,
                obstaculoCheck.transform.rotation , layerMask) ){
                mamoObstaculo++;
              }
              if(Physics.CheckBox(adelantacheck.transform.position, adelantacheck.transform.localScale / 2,
                adelantacheck.transform.rotation , layerMask) ){
                adelantaB = true;
              }
              if (rebasaB)
              {
                 if(Physics.CheckBox(adelantacheck.transform.position, adelantacheck.transform.localScale / 2,
                  adelantacheck.transform.rotation, layermask2) && !adelantaB ){
                  mamoArrebasada++;
                }
              }
              if(Physics.CheckBox(frenoPa.transform.position, frenoPa.transform.localScale / 2,
                frenoPa.transform.rotation, layerMask)){
                  s2_vehicleAiController.totalPower = 100;
                  s2_vehicleAiController.rb.useGravity = true;
                  foreach(var i in s2_vehicleAiController.wheelsX){
                        i.brakeTorque = 0;
                  }
                    StartCoroutine(frenoRepentino());
                    frenoSecoB = true;      
                }
            
              if(((rb.velocity.magnitude * 7) > 60) && giraDerB){
                  llego60=true;
                  Debug.Log("llego el viejon");
                }
            if(Physics.CheckBox(finDeFinales.transform.position, finDeFinales.transform.localScale / 2,
                  finDeFinales.transform.rotation, layerMask) ){
                    if (!llego60)
                    {
                       mamo60++; 
                    }
                    rb.mass = 99999999999f;
                    rb.drag = 99999999999f;
                    rb.angularDrag = 99999999999f;
                    rb.useGravity = false;
                    rb.MovePosition(vueltaIzqInicio.transform.position);
                    rb.MoveRotation(vueltaIzqInicio.transform.rotation);
                    rb.mass = 1000;
                    rb.drag = 0.05f;
                    rb.angularDrag = 0.48f;
                    rb.useGravity = true;
                    asvueltaIzq.Play();
                    
                  
                }else if(Physics.CheckBox(vueltaIzqFin.transform.position, vueltaIzqFin.transform.localScale / 2,
                  vueltaIzqFin.transform.rotation, layerMask)){
                    leccionFinished2=true;
                    StartCoroutine(felicitacion);
                }

                  

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(bEncender && !noGear){
            float throttle = 0;
            if(im.brakeNormal){
                detenerCarro();
            }else if(Math.Abs(im.throttle) > 0.1){
                if(Math.Abs(rb.velocity.magnitude * 7) > (velocidadMaxima)){
                    detenerCarro();
                }else{
                    acelerarCarro();
                }
                throttle = im.throttle;
            }else{
                desacelerarCarro(throttle);
            }
            direccionarCarro();
        }else{
            detenerCarro();
        }
        
    }

    private void OnCollisionEnter(Collision other) {
      AudioSource[] audios = FindObjectsOfType<AudioSource>();
      if(other.gameObject.name != "Tocus_Win_Front (1)"){
        Debug.Log(other.gameObject.name);
            if(other.gameObject.tag != "Altos"){
              if (other.gameObject.name.Equals("CarIA"))
              {
                  if (frenoSecoB)
                  {
                      mamoFrenoSeco++;
                  }
              }
              else if(other.relativeVelocity.magnitude > 4){
                isOver = true;
                  foreach (AudioSource a in audios) {
                    a.Pause();
                  }
                  choque = true;
                  
                  breakingGlass.Play();
                  start.DestroyMesh();

                  //Módulo de Alertas
                    alerta.SetActive(true);
                    textoMotivo.text = "Choque";
                    inmovilizarCarro();
                  bEncender = false;
              }
            }
            if(other.gameObject.tag == "Altos" && other.relativeVelocity.magnitude > 4 && (time3+3 <= Time.time)) {
              foreach (AudioSource a in audios) {
                a.Pause();
              }
              pasoRojo++;
              rojo.Play();
              Debug.Log("No respeta rojo"+pasoRojo);
              time3=Time.time;
            }
      }
    }

    IEnumerator delayFalta(int op, string faltaTexto){
      switch (op){
        case 1: {//Leves
            faltaLeve.SetActive(true);
            faltaGrave.SetActive(false);
            faltaDeficiente.SetActive(false);
            textoLeve.text = faltaTexto;
            yield return new WaitForSeconds(2.0f);
            faltaLeve.SetActive(false);

          break;
        }
        case 2: {
            faltaDeficiente.SetActive(true);
            faltaGrave.SetActive(false);
            faltaLeve.SetActive(false);
            textoDeficiente.text = faltaTexto;
            yield return new WaitForSeconds(2.0f);
            faltaDeficiente.SetActive(false);

          break;
        }
        case 3: {//Graves
            faltaGrave.SetActive(true);
            faltaDeficiente.SetActive(false);
            faltaLeve.SetActive(false);
            textoGrave.text = faltaTexto;
            yield return new WaitForSeconds(2.0f);
            faltaGrave.SetActive(false);

          break;
        }
        default: {
          break;
        }
      }
      yield return new WaitForSeconds(0.1f);
    }

    private void turnOnNuevo(){
        if(bEncender){
            if(im.turnedOn){
                bEncender = false;
                engineSound.Stop();
                Thread.Sleep(200);
            }else{
                if(!engineSound.isPlaying) engineSound.Play();
            }
        }else{
            if(im.turnedOn){
                bEncender = true;
                StartCoroutine (delayEncendidoCarro());
                Thread.Sleep(200);
            }else{
                engineSound.Stop();
            }
        }
        //Debug.Log(bEncender);
    }

    private void turnOn(){
        if(bEncender){
          Debug.Log("Estaba prendido y lo apagué");
          bEncender = false;
          engineSound.Stop();
        }else{
          bEncender = true;
          Debug.Log("Estaba apagado y lo prendí");
          StartCoroutine (delayEncendidoCarro());

        }
    }

    IEnumerator delayTurnOn(){
        while(true){
            if(im.turnedOn){
                Debug.Log("Entre a la corrutina");
                turnOn();
                yield return new WaitForSeconds(1.2f);
            }else{
                yield return new WaitForSeconds(0.00001f);
            }
        }        
    }

    IEnumerator delayLuces(){
      while(true){
        if(im.l){
          lm.ToggleHeadlights();
          yield return new WaitForSeconds(1.2f);
        }else{
          yield return new WaitForSeconds(0.00001f);
        }
      }
    }

    IEnumerator frenoRepentino(){
        s2_vehicleAiController.maxVelocity = 15;
        yield return new WaitForSeconds(8.0f);
        time3 = Time.time;
        if(!s2_bIsFirstTimeCar){
            Debug.Log("aguas wei");
            while(time3 + 3 > Time.time){
                s2_vehicleAiController.bIsBraking = true;
                s2_vehicleAiController.detenerCarro();
                yield return new WaitForSeconds(0.001f);
            }
            s2_vehicleAiController.bIsBraking = false;
        }else s2_bIsFirstTimeCar = false;
        yield return new WaitForSeconds(5.0f);
        
        
        
    }

    IEnumerator delayEncendidoCarro(){
        turnOnSound.Play();
        yield return new WaitForSeconds(1.0f);
        engineSound.Play();
    }

    IEnumerator LeccionFinal(){
      if(!isOver){

      
      while (intro.isPlaying) {
          yield return new WaitForSeconds(0.01f);
      }
      yield return new WaitForSeconds(1.0f);
      while(!bEncender){
        enciendeAuto.Play();
        yield return new WaitForSeconds(5.0f);
      }
      salEstacionamiento.Play();
      yield return new WaitForSeconds(6.0f);
      while(!bAvanza){
        yield return new WaitForSeconds(0.01f);
      }
      yield return new WaitForSeconds(3.0f);
      avanza.Play();
      yield return new WaitForSeconds(3.0f);
      while(!bZonaPeatonal){
        yield return new WaitForSeconds(0.01f);
      }
      zonaPeatonal.Play();
      yield return new WaitForSeconds(8.0f);
      while(!bGlorieta){
        yield return new WaitForSeconds(0.01f);
      }
      glorieta.Play();
      yield return new WaitForSeconds(3.0f);
      while(!bSalGlorieta){
        yield return new WaitForSeconds(0.01f);
      }
      salidaGlorieta.Play();
      yield return new WaitForSeconds(3.0f);
      while(!bVuelta){
        yield return new WaitForSeconds(0.01f);
      }
      vuelta.Play();
      yield return new WaitForSeconds(3.0f);
      while(!bSubida){
        yield return new WaitForSeconds(0.01f);
      }
      subeGasa.Play();
      yield return new WaitForSeconds(5.0f);
      while(!bIntegracion){
        yield return new WaitForSeconds(0.01f);
      }
      integracion.Play();
      yield return new WaitForSeconds(4.0f);
      while(!bSigueDer){
        yield return new WaitForSeconds(0.01f);
      }
      sigueDer.Play();
      yield return new WaitForSeconds(4.0f);
      while(!bCarretera){
        yield return new WaitForSeconds(0.01f);
      }
      carretera.Play();
      yield return new WaitForSeconds(4.0f);
      while(!bCurvas){
        yield return new WaitForSeconds(0.01f);
      }
      curvas.Play();
      yield return new WaitForSeconds(4.0f);
    

      }
    }
    IEnumerator LeccionFinal2(){
        yield return new WaitForSeconds(5.0f);
        rb.useGravity = false;
        rb.mass = 99999999999f;
        rb.drag = 99999999999f;
        rb.angularDrag = 99999999999f;
        rb.MovePosition( teletransportacion.transform.position);
        rb.MoveRotation( teletransportacion.transform.rotation);
        StopCoroutine(coroutineExamen);
        bienHecho.Play();
        bEncender=false;
        yield return new WaitForSeconds(2.0f);
        introparte2.Play();
        yield return new WaitForSeconds(9.0f);
        rb.useGravity = true;
        rb.mass = 1000;
        rb.drag = 0.05f;
        rb.angularDrag = 0.48f;
        if(!bEncender){
          enciende2.Play();
          yield return new WaitForSeconds(3.0f);
        }
        while(!bEncender){
          yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(2.0f);
        acelera.Play();
        yield return new WaitForSeconds(3.0f);
        while(!vueltaS2B){
          yield return new WaitForSeconds(0.01f);
        }
        vueltaS2.Play();
        yield return new WaitForSeconds(3.0f);
        while(!obstaculoB){
          yield return new WaitForSeconds(0.01f);
        }
        obstaculo.Play();
        yield return new WaitForSeconds(2.0f);
        while(!reincorporateB){
          yield return new WaitForSeconds(0.01f);
        }
        reincorporate.Play();
        yield return new WaitForSeconds(2.0f);
         while(!rebasaB){
          yield return new WaitForSeconds(0.01f);
        }
        rebasa.Play();
        yield return new WaitForSeconds(2.0f);
        while(!giraIzqB){
          yield return new WaitForSeconds(0.01f);
        }
        giraIzq.Play();
        yield return new WaitForSeconds(2.0f);
        while(!frenoSecoB){
          yield return new WaitForSeconds(0.01f);
        }
        cuidadito.Play();
        yield return new WaitForSeconds(7.0f);
        
        while(!giraDerB){
          yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(3.0f);
        rb.MovePosition( teletransportacion2.transform.position);
        rb.MoveRotation( teletransportacion2.transform.rotation);
        yield return new WaitForSeconds(2.0f);
        carreteraAudio.Play();
        yield return new WaitForSeconds(12.0f);



    }


    IEnumerator finalLeccion(){
        StopCoroutine(coroutineExamen2);
        bienHecho.Play();
        bEncender=false;
        yield return new WaitForSeconds(2.0f);
        felicidades.Play();
        darCalificaciones();
    }

    private void darCalificaciones()
    {
      /*if(pasoRojo < 5){
          LeccionCali -= (pasoRojo * 5)/2;
      }else {
        LeccionCali -= 25;
      }*/
      /*if(ExcesoVelPeaton < 5){
          LeccionCali -= ExcesoVel * 5;
      }else {
        LeccionCali -= 25;
      }*/

      ExamNumber.examNumber = 6;
      ExamNumber.criteriosAux = new List<Criterio>();
      ExamNumber.faltasAux = new List<Falta>();



      //faltas
        if(grave1>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 4,
                tipo = "graves",
                veces = grave1,
                texto = "No reducir velocidad en zona peatonal",
                puntos = 5
            });
        }
        if(grave2>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 4,
                tipo = "graves",
                veces = grave2,
                texto = "No detenerse en semáforo rojo",
                puntos = 5
            });
        }
        if(grave3>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 4,
                tipo = "graves",
                veces = grave3,
                texto = "Detenerse en paso peatonal",
                puntos = 5
            });
        }
        if(grave4>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 4,
                tipo = "graves",
                veces = grave4,
                texto = "Detención innecesaria",
                puntos = 5
            });
        }
        if(grave5>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 4,
                tipo = "graves",
                veces = grave5,
                texto = "No dejar pasar al peatón",
                puntos = 5
            });
        }
        if(grave8>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 4,
                tipo = "graves",
                veces = grave8,
                texto = "Pisar línea continua",
                puntos = 5
            });
        }
        if(def1>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 4,
                tipo = "deficientes",
                veces = def1,
                texto = "Arranque brusco que ocasionó el apagado del carro",
                puntos = 3
            });
        }
        if(def2>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 4,
                tipo = "deficientes",
                veces = def2,
                texto = "No poner direccionales",
                puntos = 3
            });
        }
        if(def3>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 4,
                tipo = "deficientes",
                veces = def3,
                texto = "No cambiar la marcha del vehículo que se requiere",
                puntos = 3
            });
        }
        if(def4>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 4,
                tipo = "deficientes",
                veces = def4,
                texto = "No pisar el clutch para cambiar de velocidad",
                puntos = 3
            });
        }
        if(def5>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 3,
                tipo = "deficientes",
                veces = def5,
                texto = "No detenerse en semáforo amarillo",
                puntos = 3
            });
        }
        if(def6>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 3,
                tipo = "deficientes",
                veces = def6,
                texto = "No mantener la distancia con respecto a otros vehículos",
                puntos = 3
            });
        }
        if(leve1>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 4,
                tipo = "leves",
                veces = leve1,
                texto = "Arranque brusco sin ocasionar el apagado del carro",
                puntos = 1
            });
        }
        if(leve2>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 4,
                tipo = "leves",
                veces = leve2,
                texto = "No reducir velocidad ni frenar en topes y/o baches",
                puntos = 1
            });
        }
        if(leve3>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 4,
                tipo = "leves",
                veces = leve3,
                texto = "No enciende luces en túneles",
                puntos = 1
            });
        }
        //faltas








      if(incorporacionCal > 0){
        if(incorporacionCal < 5){
            ExamNumber.criteriosAux.Add(new Criterio(){
              leccion = 6,
              seccion = 1,
              veces = incorporacionCal,
              texto = "Incorporación sin distancia correcta",
              puntos = 5
            });
        }else {
          ExamNumber.criteriosAux.Add(new Criterio(){
              leccion = 6,
              seccion = 1,
              veces = 5,
              texto = "Incorporación sin distancia correcta",
              puntos = 5
            });
        }
      }

      if(carrilesCount > 0){
        if(carrilesCount < 5){
            ExamNumber.criteriosAux.Add(new Criterio(){
              leccion = 6,
              seccion = 1,
              veces = carrilesCount,
              texto = "Salir del carril correspondiente",
              puntos = 2
            });
        }else {
          ExamNumber.criteriosAux.Add(new Criterio(){
              leccion = 6,
              seccion = 1,
              veces = 5,
              texto = "Salir del carril correspondiente",
              puntos = 2
            });
        }
      }

      if(glorietaError > 0){
        if(glorietaError < 5){
            ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 6,
                seccion = 1,
                veces = glorietaError,
                texto = "Salir del carril correspondiente en glorieta",
                puntos = 3
              });
        }else {
          ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 6,
                seccion = 1,
                veces = 5,
                texto = "Salir del carril correspondiente en glorieta",
                puntos = 3
              });
        }
      }

      if(ExcesoVel > 0){
        if(ExcesoVel < 5){
            ExamNumber.criteriosAux.Add(new Criterio(){
              leccion = 6,
              seccion = 1,
              veces = ExcesoVel,
              texto = "Fuera del rango de velocidad",
              puntos = 5
            });
        }else{
          ExamNumber.criteriosAux.Add(new Criterio(){
            leccion = 6,
            seccion = 1,
            veces = 5,
            texto = "Fuera del rango de velocidad",
            puntos = 5
          });
        }
      }

      if(vasLento > 0){
        if(vasLento < 5){
            ExamNumber.criteriosAux.Add(new Criterio(){
              leccion = 6,
              seccion = 1,
              veces = vasLento,
              texto = "Ir a menos de 25km/h en una avenida",
              puntos = 2
            });
        }else{
          ExamNumber.criteriosAux.Add(new Criterio(){
              leccion = 6,
              seccion = 1,
              veces = 5,
              texto = "Ir a menos de 25km/h en una avenida",
              puntos = 2
            });
        }
      }
      if(mamoObstaculo > 0){
        if(mamoObstaculo < 5){
            ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 6,
                seccion = 1,
                veces = mamoObstaculo,
                texto = "Hacer contacto con vallas",
                puntos = 5
              });
        }else{
          ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 6,
                seccion = 1,
                veces = 5,
                texto = "Hacer contacto con vallas",
                puntos = 5
              });
        }
      }
      if(mamoArrebasada > 0){
        if(mamoArrebasada < 5){
            ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 6,
                seccion = 1,
                veces = mamoArrebasada,
                texto = "No rebasar cuando se indica",
                puntos = 5
              });
        }else{
          ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 6,
                seccion = 1,
                veces = 5,
                texto = "No rebasar cuando se indica",
                puntos = 5
              });
        }
      }
      if(mamoFrenoSeco > 0){
        if(mamoFrenoSeco < 5){
            ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 6,
                seccion = 1,
                veces = mamoFrenoSeco,
                texto = "Hacer contacto con un automovil cuando hace freno repentino",
                puntos = 5
              });
        }else{
          ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 6,
                seccion = 1,
                veces = 5,
                texto = "Hacer contacto con un automovil cuando hace freno repentino",
                puntos = 5
              });
        }
      }
      if(mamo60 > 0){
        if(mamo60 < 5){
            ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 6,
                seccion = 1,
                veces = mamo60,
                texto = "No alcanza los 60km/h en la carretera",
                puntos = 5
              });
        }else{
          ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 6,
                seccion = 1,
                veces = 5,
                texto = "No alcanza los 60km/h en la carretera",
                puntos = 5
              });
        }
      }







      if(incorporacionCal < 5){
          LeccionCali -= incorporacionCal * 5;
      }else {
        LeccionCali -= 25;
      }
      
      if(carrilesCount < 5){
          LeccionCali -= carrilesCount * 2;
      }else {
        LeccionCali -= 10;
      }
      if(glorietaError < 5){
          LeccionCali -= glorietaError * 3;
      }else {
        LeccionCali -= 15;
      }
      if(ExcesoVel < 5){
          LeccionCali -= (ExcesoVel * 5)/2;
      }else{
        LeccionCali -= 25;
      }
      if(vasLento < 5){
          LeccionCali -= vasLento * 2;
      }else{
        LeccionCali -= 10;
      }
      if(mamoObstaculo < 5){
          LeccionCali -= mamoObstaculo * 5;
      }else{
        LeccionCali -= 10;
      }
      if(mamoArrebasada < 5){
          LeccionCali -= mamoArrebasada * 5;
      }else{
        LeccionCali -= 10;
      }
      if(mamoFrenoSeco < 5){
          LeccionCali -= mamoFrenoSeco * 5;
      }else{
        LeccionCali -= 10;
      }
      if(mamo60 < 5){
          LeccionCali -= vasLento * 5;
      }else{
        LeccionCali -= 10;
      }


      Debug.Log("La calificacion fue: " + LeccionCali);
      StopCoroutine(felicitacion);

      SceneManager.LoadScene("retro");

    }

    public void detenerCarro(){
        foreach (WheelCollider wheel in throttleWheels)
        {
            wheel.motorTorque = 0f;
            wheel.brakeTorque = brakeStrenght;
        }
    }

    private void acelerarCarro()
    {
        foreach (WheelCollider wheel in throttleWheels)
        {
            wheel.motorTorque = strenghtCoefficient * Time.deltaTime * im.throttle;
            wheel.brakeTorque = 0f;
        }
    }

    private void desacelerarCarro(float throttle)
    {
        if(throttle == 0){
            foreach (WheelCollider wheel in throttleWheels)
            {
                wheel.motorTorque = strenghtCoefficient * Time.deltaTime * im.throttle;
                wheel.brakeTorque = 0f;

            }
        }else{
            float decimalPart = throttle / 10;
            for(int i = 0; i < 9; i++){
                throttle -= decimalPart;
                foreach (WheelCollider wheel in throttleWheels)
                {
                    wheel.motorTorque = strenghtCoefficient * Time.deltaTime * throttle;
                    wheel.brakeTorque = 0f;

                }
            }
            foreach (WheelCollider wheel in throttleWheels)
            {
                wheel.motorTorque = 0f;
                wheel.brakeTorque = 0f;

            }
        }

    }

    private void direccionarCarro(){
        foreach (WheelCollider wheel in steeringWheels)
        {
            wheel.steerAngle = maxTurn * im.steer;
        }
    }


}
