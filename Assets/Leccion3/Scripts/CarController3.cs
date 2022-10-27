using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LightingManager))]
public class CarController3 : MonoBehaviour
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
    public GameObject ventana;
    public AudioSource breakingGlass;
    public AudioSource engineSound, turnOnSound, bienHecho, felicidades;

    private IEnumerator coroutine_s1p1, coroutine_s1p2, coroutine_s2, coroutine_s3,
                         coroutine_s4, coroutine_s5, felicitacion;
    private bool s1p1Finished = false, s1p2Finished, s2Finished = false, s3Finished = false, s4Finished = false, s5Finished = false, bEncender;
    private float velocidadMaxima = 20;
    private int layerMask = 1 << 6;
    private bool noGear = true;

    /*------------Audios y booleans seccion 1-------------*/

    public AudioSource bienvenido, s1_asIntro, s1_asInstruccion, s1_asFinParte1, s1_asIntroP2, s1_asIntroFreno3, s1_asFrenaYa, s1_asDaVuelta;
    public GameObject s1_goFinParte1, s1_goFinTecnica2, s1_goFinTecnica3;
    private bool s1_bStarted, s1_bInstruccionDada, s1_bTerminoParte1, s1_bTipoFreno2, s1_bTipoFreno3, s1_bIsFirstTimeCar = true, s1_bYaEmpezoCorrutina;
    public VehicleAiController s1_vehicleAiController;
    private float time3;
    
    /*------------Audios y booleans seccion 2-------------*/
    public AudioSource s2_asIntro, s2_asIngresa, s2_asCirculaGlorieta, s2_asSalirGlorieta;
    public GameObject s2_goIngresoGlorieta, s2_goSalirGlorieta, s2_goYaSalio, s2_goGlorietaExtensión;
    private bool s2_bIngreso, s2_bHayQueSalir, s2_estaDentro, s2_bYaSalio, s2_bActivarExtension, s2_bDesactivarExtension;
    private int s2_contadorVueltas = 0;

    /*------------Audios y booleans seccion 3-------------*/
    public AudioSource s3_asIntro, s3_asPasaTope1, s3_asPasaTope2, s3_asGiraALaDerecha, s3_asCarrilDerecho, s3_asCambiaACentral, 
        s3_asCarrilCentral, s3_asCambiaAIzquierdo, s3_asCarrilIzquierdo, s3_asDaVuelta;
    public GameObject s3_goInicio, s3_goTope1, s3_goTope2, s3_goGiraALaDerecha, s3_goCarrilDerecho, s3_goCarrilCentral, 
        s3_goCarrilIzq, s3_goVuelta, s3_goInicioExtension, s3_goExtension;
    private bool s3_bTope1, s3_bTope2, s3_bGiraDerecha, s3_bCarrilDerecho, s3_bYaCambioACentral, 
        s3_bYaCambioAIzquierdo;

    /*------------Audios y booleans seccion 4-------------*/
    public AudioSource s4_asIntro, s4_asCuidado, s4_asConclusión, s4_asCambiaCarril;
    public GameObject  s4_goCuidado, s4_goFin, s4_goInicioExtension, s4_goExtension, s4_goExtensionParte, s4_goMedioExtension;
    public VehicleAiController s4_vehicleAiController;

    /*------------Audios y booleans seccion 5-------------*/
    public AudioSource s5_asIntro, s5_asRebasaIzq, s5_asRebasaDer, s5_asConclusion, s5_asVueltaIzq;
    public GameObject[] s5_goCarros, s5_goSpawners, s5_goGuiasVisuales;
    public GameObject s5_goInicio, s5_goCarrilIzquierdo, s5_goFin, s5_goConclusion, s5_goVueltaIzq, s5_goVueltaIzqPunto;
    private bool s5_bDebeDecirIntro, s5_bYaRebasoIzq, s5_bFin, s5_bConclusion;
    
    /* Puntuaciones */
    private int s1Cali = 100, s2Cali = 100, s3Cali = 100, s4Cali = 100, s5Cali = 100, calificacionLeccion, calificacionFinal;
    private int s1p_iChoqueCaso1, s1p_iChoqueCaso2, s2p_iSalioCarril, s2p_iFueraRangoVelocidad, s3p_iPasaTopeRapido,
                s3p_iFueraRangoVelocidadDer, s3p_iFueraRangoVelocidadCen, s3p_iFueraRangoVelocidadIzq, s3p_iPusoDireccionales,
                s5p_iFueraRangoVelocidad, s5p_iPusoDireccionales;
    private bool s1p_bChoqueCaso3, s3p_bPrimeraVezTope1, s3p_bPrimeraVezTope2, s4p_bTocoCarro, s5p_bHizoCambio1, s5p_bHizoCambio2;
    private float s4p_fMinDistance;
    public List<GameObject> s2p_goSalioCarril;
    public GameObject s3p_goVelocidadTope1, s3p_goVelocidadTope2, s5p_goCarrilCentral;
    
    float time, time2, time4, time5, time6, time7, timeDef6;

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
    [SerializeField] private GameObject  imageS1;
    [SerializeField] private GameObject  imageS2;
    [SerializeField] private GameObject  imageS3;
    [SerializeField] private GameObject  imageS4;
    [SerializeField] private GameObject  imageS5;

    public AudioSource cambioVelocidad;

    /* Faltas */
    public List<GameObject> fg1_goPrimerTope, fg1_goSegundoTope, fg2_goCuboSemaforo, fg3_goZonaPeatonal, fg8_goLineas, fd5_goCuboAmarillo;
    public GameObject fg5_goSafeZone;
    private float fg1_iVelocidadEntrada,time8;
    private int faltasGraves, fd4_iVelocidadActual, fd4_iVelocidadAnterior, faltasDeficientes, faltasLeves;
    private int grave1, grave2, grave3, grave4, grave5, grave6, grave8, def1,  def2, def3, def4, def5, def6, leve1, leve2, leve3;
    private bool[] fg1_bYaEsteTope1, fg1_bYaEsteTope2, fg2_bYaEsteCubo, fg3_bSeParo, fg8_bYaLinea, fd5_bYaEsteAmarillo, fd4_sancionesVelocidades;
    private bool fl2_bTope1, fl2_bTope2;

    public UnityEvent eventoRestart, eventoReturn;
     

    // Start is called before the first frame update
    void Start()
    {

        imageS1.SetActive(false);
        imageS2.SetActive(false);
        imageS3.SetActive(false);
        imageS4.SetActive(false);
        imageS5.SetActive(false);

        
        faltaGrave.SetActive(false);
        faltaDeficiente.SetActive(false);
        faltaLeve.SetActive(false);
        Init();
        StartCoroutine(coroutine_s1p1);
        StartCoroutine(delayTurnOn());
        StartCoroutine(delayLuces());
        start = ventana.GetComponent<MeshDestroy>();
        ventana.SetActive(false);
    
        im = GetComponent<InputManager>();
        //im = GetComponent<InputManagerForKeyboard>();
        rb = GetComponent<Rigidbody>();

        if(cm){
            rb.centerOfMass = cm.localPosition;
            
        }

        s1_vehicleAiController.totalPower = 0;
        foreach(var i in s1_vehicleAiController.wheelsX){
            i.brakeTorque = brakeStrenght;
        }
        
        s5_bDebeDecirIntro = true;
        
    }
    
    private void Init(){
        coroutine_s1p1 = seccion1p1();
        coroutine_s1p2 = seccion1p2();
        coroutine_s2 = seccion2();
        coroutine_s3 = seccion3();
        
        coroutine_s4 = seccion4();
        coroutine_s5 = seccion5();
        felicitacion = finalLeccion();

        fg1_bYaEsteTope1 = new bool [9];
        fg1_bYaEsteTope2 = new bool [9];
        fg2_bYaEsteCubo = new bool [15];
        fd5_bYaEsteAmarillo = new bool[fd5_goCuboAmarillo.Count];
        fg3_bSeParo = new bool[8];
        fg8_bYaLinea = new bool[fg8_goLineas.Count];
        fd4_sancionesVelocidades = new bool[7];
        fd4_sancionesVelocidades[0] = true;
    }

    private void Awake() {
        
    }

    void Update()
    {

        /* if(im.l){
            lm.ToggleHeadlights();
        } */
        
        foreach (GameObject tl in tailLights)
        {
            tl.GetComponent<Renderer>().material.SetColor("_EmissionColor", im.brakeNormal ? new Color(0.5f, 0.111f, 0.111f) : Color.black);
        }

        
        checkVelocidad();

        leccion();

        //turnOn();

        faltas();

        checkFaltas();

        checkButtons();
    }

    private void checkButtons(){
        if(isOver){
            if(im.restart){
                eventoRestart?.Invoke();
                Debug.Log("ghola chaval");
            }
            
        }
        if(im.returned){
            eventoReturn?.Invoke();
        }
    }

    private void inmovilizarCarro(){
        isOver = true;
        audios = GameObject.Find("Leccion3").GetComponentsInChildren<AudioSource>();
        foreach (var item in audios)
        {
            item.Pause();
        }
        rb.MovePosition(transform.position + new Vector3(0,10,0));
        rb.useGravity = false;
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
                    Debug.Log("Velocidad 4");
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
                    StartCoroutine(delayFalta(2, "No pisar el clutch para cambiar de velocidad"));
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
        for(int i = 0; i < fg3_goZonaPeatonal.Count; i++){    
            if(Physics.CheckBox(fg3_goZonaPeatonal[i].transform.position, fg3_goZonaPeatonal[i].transform.localScale / 2,
            fg3_goZonaPeatonal[i].transform.rotation, layerMask) && !fg3_bSeParo[i]){            
                if(rb.velocity.magnitude < 0.1){
                    faltasGraves++;
                    grave3++;
                    StartCoroutine(delayFalta(3, "Detenerse en paso peatonal"));
                    fg3_bSeParo[i] = true;
                    Debug.Log("Grave 3");
                }
            }            
        }
        //Grave 4
        RaycastHit hit1;
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
        /*if(Physics.CheckBox(s3_goTope1.transform.position, s3_goTope1.transform.localScale / 2,
        s3_goTope1.transform.rotation, layerMask) && !fl2_bTope1){
            if (rb.velocity.magnitude * 3.6f > 15){ 
                faltasLeves++; 
                fl2_bTope1 = true; 
            }
        }
        if(Physics.CheckBox(s3_goTope2.transform.position, s3_goTope2.transform.localScale / 2,
        s3_goTope2.transform.rotation, layerMask) && !fl2_bTope2){
            if(rb.velocity.magnitude * 3.6f > 15){
                faltasLeves++; 
                leve2++;
                fl2_bTope2 = true;
            }
        }*/
        //Leve 3
        
    }

    private void leccion(){
        if(!s1p1Finished){
            /* Debug.Log("Lo que vale es:" + Convert.ToString(layerMask, toBase: 2) + " + "  + layerMask ); */
            if(!s1_bStarted && bEncender && im.throttle > 0){
                s1_bStarted = true;
            }else if(!s1_bTerminoParte1 && Physics.CheckBox(s1_goFinParte1.transform.position, s1_goFinParte1.transform.localScale / 2,
                s1_goFinParte1.transform.rotation, layerMask)){
                    s1_bTerminoParte1 = true;
                    s1p1Finished = true;
                    StopCoroutine(coroutine_s1p1);
                    StartCoroutine(coroutine_s2);
            }
            /* Evaluación */
            
                   
        }else if(!s2Finished){
            if(!s2_bIngreso && Physics.CheckBox(s2_goIngresoGlorieta.transform.position, s2_goIngresoGlorieta.transform.localScale / 2,
                s2_goIngresoGlorieta.transform.rotation, layerMask)){
                    s2_bIngreso = true;
                    //s2_asCirculaGlorieta.Play();
            }else if(!s2_bHayQueSalir && Physics.CheckBox(s2_goSalirGlorieta.transform.position, s2_goSalirGlorieta.transform.localScale / 2,
                s2_goSalirGlorieta.transform.rotation, layerMask)){
                    s2_bHayQueSalir = true;
                    
                    
            }else if(!s2_bYaSalio && Physics.CheckBox(s2_goYaSalio.transform.position, s2_goYaSalio.transform.localScale / 2,
                s2_goYaSalio.transform.rotation, layerMask)){
                    s2_bYaSalio = true;
                    s2Finished = true;
                    StopCoroutine(coroutine_s2);
                    StartCoroutine(coroutine_s1p2);
                    s1_vehicleAiController.totalPower = 100;
                    s1_vehicleAiController.rb.useGravity = true;
                    foreach(var i in s1_vehicleAiController.wheelsX){
                        i.brakeTorque = 0;
                    }
                    
            }
            /* Evaluación */
            foreach (var item in s2p_goSalioCarril)
            {
                if((Physics.CheckBox(item.transform.position, item.transform.localScale / 2,
                item.transform.rotation, layerMask) && (time+3 <= Time.time))){
                    time = Time.time;
                    s2p_iSalioCarril++;
                }
            }
            if((rb.velocity.magnitude * 7 < 5 || rb.velocity.magnitude * 7 > 15) && (time2+4 <= Time.time)){
                time2 = Time.time;
                s2p_iFueraRangoVelocidad++;
            }

        }else if(!s1p2Finished){
            if(!s1_bTipoFreno2){
                //vehicleController.throttleVehicle();
                if(!s1_bYaEmpezoCorrutina) {StartCoroutine(carroFrenando()); s1_bYaEmpezoCorrutina = true;}
                if(Physics.CheckBox(s1_goFinTecnica2.transform.position, s1_goFinTecnica2.transform.localScale / 2,
                s1_goFinTecnica2.transform.rotation, layerMask)){
                    s1_bTipoFreno2 = true;
                    //s2_goGlorietaExtensión.SetActive(true);
                    StopCoroutine(carroFrenando());
                    s1_bYaEmpezoCorrutina = false;
                }
            }else if(!s1_bTipoFreno3){
                Debug.Log("El carro no debería estar avanzando");
                if(!s1_bYaEmpezoCorrutina) {StartCoroutine(frenoRepentino()); s1_bYaEmpezoCorrutina = true;}
                if(Physics.CheckBox(s1_goFinTecnica3.transform.position, s1_goFinTecnica3.transform.localScale / 2,
                s1_goFinTecnica3.transform.rotation, layerMask)){
                    StopCoroutine(frenoRepentino());
                    s1_bTipoFreno3 = true;
                    s1p2Finished = true;
                    StartCoroutine(coroutine_s3);
                    
                }
            }/* else if(s1_bTipoFreno3){
                if(Physics.CheckBox(s1_goFinTecnica2.transform.position, s1_goFinTecnica2.transform.localScale / 2,
                s1_goFinTecnica2.transform.rotation, layerMask)){

                    s2_goGlorietaExtensión.SetActive(false);

                }else 
                if(Physics.CheckBox(s3_goInicio.transform.position, s3_goInicio.transform.localScale / 2,
                s3_goInicio.transform.rotation, layerMask)){
                    
                    StartCoroutine(coroutine_s3);
                    s1p2Finished = true;
                }
            } */
        }else if(!s3Finished){
            
            if(Physics.CheckBox(s3_goTope1.transform.position, s3_goTope1.transform.localScale / 2,
                s3_goTope1.transform.rotation, layerMask)){
                    s3_bTope1 = true;
            }else if(Physics.CheckBox(s3_goTope2.transform.position, s3_goTope2.transform.localScale / 2,
                s3_goTope2.transform.rotation, layerMask)){
                    s3_bTope2 = true;
            }else if(Physics.CheckBox(s3_goGiraALaDerecha.transform.position, s3_goGiraALaDerecha.transform.localScale / 2,
                s3_goGiraALaDerecha.transform.rotation, layerMask)){
                    s3_bGiraDerecha = true;
            }else if(Physics.CheckBox(s3_goCarrilDerecho.transform.position, s3_goCarrilDerecho.transform.localScale / 2,
                s3_goCarrilDerecho.transform.rotation, layerMask)){
                    s3_bCarrilDerecho = true;
            }else if(Physics.CheckBox(s3_goCarrilCentral.transform.position, s3_goCarrilCentral.transform.localScale / 2,
                s3_goCarrilCentral.transform.rotation, layerMask)){
                    s3_bYaCambioACentral = true;
            }else if(Physics.CheckBox(s3_goCarrilIzq.transform.position, s3_goCarrilIzq.transform.localScale / 2,
                s3_goCarrilIzq.transform.rotation, layerMask)){
                    s3_bYaCambioAIzquierdo = true;
            }else if(Physics.CheckBox(s3_goInicioExtension.transform.position, s3_goInicioExtension.transform.localScale / 2,
                s3_goInicioExtension.transform.rotation, layerMask)){
                    s3_goExtension.SetActive(true);
            }else if(Physics.CheckBox(s3_goVuelta.transform.position, s3_goVuelta.transform.localScale / 2,
                s3_goVuelta.transform.rotation, layerMask)){
                    s3Finished = true;
                    s3_goExtension.SetActive(false);
                    StartCoroutine(coroutine_s4);
            }
            /* Evaluación */
            if(Physics.CheckBox(s3p_goVelocidadTope1.transform.position, s3p_goVelocidadTope1.transform.localScale / 2,
                s3p_goVelocidadTope1.transform.rotation, layerMask) && s3p_bPrimeraVezTope1){
                s3p_bPrimeraVezTope1 = false;
                if(rb.velocity.magnitude * 7 > 10){
                    s3p_iPasaTopeRapido++;
                }
            }
            if(Physics.CheckBox(s3p_goVelocidadTope2.transform.position, s3p_goVelocidadTope2.transform.localScale / 2,
                s3p_goVelocidadTope2.transform.rotation, layerMask) && s3p_bPrimeraVezTope2){
                s3p_bPrimeraVezTope2 = false;
                if(rb.velocity.magnitude * 7f > 10){
                    s3p_iPasaTopeRapido++;
                }
            }
            if(!s3_bYaCambioACentral && s3_bCarrilDerecho){
                if((rb.velocity.magnitude * 7 > 50) && (time4+3 <= Time.time)){
                    time4 = Time.time;
                    s3p_iFueraRangoVelocidadDer++;
                }
            }else if(!s3_bYaCambioAIzquierdo){
                if((rb.velocity.magnitude * 7 > 70) && (time5+3 <= Time.time)){
                    time5 = Time.time;
                    s3p_iFueraRangoVelocidadCen++;
                }
            }else{
                if((rb.velocity.magnitude * 7 > 70) && (time6+3 <= Time.time)){
                    time6 = Time.time;
                    s3p_iFueraRangoVelocidadIzq++;
                }
            }
            if(!s3_bYaCambioACentral && s3_bCarrilDerecho && !s3_asCarrilDerecho.isPlaying && s3p_iPusoDireccionales == 0){
                if(/*codigo para verificar las direccionales*/true){
                    s3p_iPusoDireccionales++;
                }
            }
            if(!s3_bYaCambioAIzquierdo && s3_bYaCambioACentral && !s3_asCarrilCentral.isPlaying && s3p_iPusoDireccionales == 1){
                if(/*codigo para verificar las direccionales*/true){
                    s3p_iPusoDireccionales++;
                }
            }
            
        }else if(!s4Finished){
            if(Physics.CheckBox(s4_goCuidado.transform.position, s4_goCuidado.transform.localScale / 2,
                s4_goCuidado.transform.rotation, layerMask)){
                    s4_vehicleAiController.rb.useGravity = true;
                    s4p_fMinDistance = Vector3.Distance(transform.position, s4_vehicleAiController.gameObject.transform.position);
            }else if(Vector3.Distance(transform.position, s4_vehicleAiController.gameObject.transform.position) < s4p_fMinDistance){
                s4p_fMinDistance = Vector3.Distance(transform.position, s4_vehicleAiController.gameObject.transform.position);
            }else if(Physics.CheckBox(s4_goInicioExtension.transform.position, s4_goInicioExtension.transform.localScale / 2,
                s4_goInicioExtension.transform.rotation, layerMask)){
                    s4_goExtension.SetActive(true);
            }else if(Physics.CheckBox(s4_goMedioExtension.transform.position, s4_goMedioExtension.transform.localScale / 2,
                s4_goMedioExtension.transform.rotation, layerMask)){
                    s4_goMedioExtension.SetActive(false);
            }else if(Physics.CheckBox(s4_goFin.transform.position, s4_goFin.transform.localScale / 2,
                s4_goFin.transform.rotation, layerMask)){
                    s4Finished = true;
                    s4_goExtension.SetActive(false);
                    for(int i = 0; i < s5_goCarros.Length; i++){
                        Debug.Log("holawasssssss");
                        GameObject go = Instantiate(s5_goCarros[i]);
                        go.transform.position = s5_goSpawners[i].transform.position;
                        go.transform.Rotate(0 , s5_goSpawners[i].transform.eulerAngles.y , 0);
                        go.GetComponent<VehicleAiController>().bForceVelocity = true;
                        go.GetComponent<VehicleAiController>().maxVelocity = 6;
                        go.GetComponent<VehicleAiController>().currentNode = s5_goSpawners[i].GetComponent<carNode>();
                    }
            }
        }else if(!s5Finished){
            if(Physics.CheckBox(s5_goInicio.transform.position, s5_goInicio.transform.localScale / 2,
                s5_goInicio.transform.rotation, layerMask) && s5_bDebeDecirIntro){
                    StartCoroutine(coroutine_s5);
                    s5_bDebeDecirIntro = false;
            }else if(Physics.CheckBox(s5_goCarrilIzquierdo.transform.position, s5_goCarrilIzquierdo.transform.localScale / 2,
                s5_goCarrilIzquierdo.transform.rotation, layerMask)){
                    s5_bYaRebasoIzq = true;
                    s5p_bHizoCambio1 = true;
            }else if(Physics.CheckBox(s5p_goCarrilCentral.transform.position, s5p_goCarrilCentral.transform.localScale / 2,
                s5p_goCarrilCentral.transform.rotation, layerMask) && s5p_bHizoCambio1){
                    s5p_bHizoCambio2 = true;
            }else if(Physics.CheckBox(s5_goConclusion.transform.position, s5_goConclusion.transform.localScale / 2,
                s5_goConclusion.transform.rotation, layerMask) && !s5_bConclusion){
                    s5_asConclusion.Play();
                    s5_bConclusion = true;
            }else if(Physics.CheckBox(s5_goVueltaIzq.transform.position, s5_goVueltaIzq.transform.localScale / 2,
                s5_goVueltaIzq.transform.rotation, layerMask)){
                    rb.MovePosition(s5_goVueltaIzqPunto.transform.position);
                    rb.MoveRotation(s5_goVueltaIzqPunto.transform.rotation);
                    foreach (var item in s5_goGuiasVisuales)
                    {
                        item.SetActive(false);
                    }
                    s5_asVueltaIzq.Play();
            }
            else if(Physics.CheckBox(s5_goFin.transform.position, s5_goFin.transform.localScale / 2,
                s5_goFin.transform.rotation, layerMask)){ 
                    s5_bFin = true;
                    s5Finished = true;
                    StartCoroutine(felicitacion);
                    StopCoroutine(coroutine_s5);
            }

            if((rb.velocity.magnitude * 7 > 70) && (time7+3 <= Time.time) && !s5_bDebeDecirIntro && !s5_bFin){
                time7 = Time.time;
                s5p_iFueraRangoVelocidad++;
            }
            if(!s5_bYaRebasoIzq && !s5_bDebeDecirIntro && s5p_iPusoDireccionales == 0){
                if(/*codigo para verificar las direccionales*/true){
                    s5p_iPusoDireccionales++;
                }
            }
            if(!s5p_bHizoCambio2 && !s5_bDebeDecirIntro && s5p_iPusoDireccionales == 1 && s5_bYaRebasoIzq){
                if(/*codigo para verificar las direccionales*/true){
                    s5p_iPusoDireccionales++;
                }
            }
        }
    }

    private void InstanciarCochesS5(){
        for(int i = 0; i < s5_goCarros.Length; i++){
            Instantiate(s5_goCarros[i], s5_goSpawners[i].transform.position, s5_goSpawners[i].transform.rotation);
        }
    }   

    private void OnDrawGizmos() {     }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(bEncender && !noGear){
            float throttle = 0;
            if(im.brakeNormal){
                detenerCarro();
            }else if(Math.Abs(im.throttle) > 0.1){
                if(Math.Abs(rb.velocity.magnitude * 7f) > (velocidadMaxima)){
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

        /* if(bEncender){
            foreach (WheelCollider wheel in throttleWheels)
            {
                if(im.brake){
                    detenerCarro();
                }else{
                    if(Math.Abs(transform.InverseTransformVector(rb.velocity).z * 3.6) > velocidadMaxima + 5){
                        detenerCarro();
                    }else{
                        wheel.motorTorque = strenghtCoefficient * Time.deltaTime * im.throttle;
                        wheel.brakeTorque = 0f;
                    }
                }
            }

            foreach (WheelCollider wheel in steeringWheels)
            {
                wheel.steerAngle = maxTurn * im.steer;
            }
        }else{
            detenerCarro();
        } */
        
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.name != "Tocus_Win_Front (1)"){
            
                if(other.relativeVelocity.magnitude > 4){
                    breakingGlass.Play();
                    ventana.SetActive(true);
                    start.DestroyMesh();

                    //Módulo de Alertas
                    inmovilizarCarro();
                    alerta.SetActive(true);
                    textoMotivo.text = "Choque";
                   
                }else{
                    if(other.gameObject.name == "Tocus Variant"){
                        s1p_iChoqueCaso1++;
                    }else if(other.gameObject.name == "Tocus Variant(1)"){
                        if(!s1_bTipoFreno2){
                            s1p_iChoqueCaso2++;
                        }else{
                            s1p_bChoqueCaso3 = true;
                        }
                    }else if(other.gameObject.name == "Tocus Variant(2)"){
                        s4p_bTocoCarro = true;
                    }else if(other.gameObject.name == "Tope"){
                        Debug.Log("Hola");
                    }
                }
                Debug.Log("Collision Detected");
        }
    }

    private void turnOnNuevo(){
        if(bEncender){
            if(im.turnedOn){
                bEncender = false;
                engineSound.Stop(); 
                Thread.Sleep(150);    
            }else{
                if(!engineSound.isPlaying) engineSound.Play();
            }
        }else{
            if(im.turnedOn){
                bEncender = true; 
                StartCoroutine (delayEncendidoCarro());
                Thread.Sleep(150);
            }else{
                engineSound.Stop();
            }
            
            
        }
        
    }

    private void turnOn(){
        if(bEncender){
            bEncender = false;
            engineSound.Stop();
        }else{
            bEncender = true;

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


    IEnumerator carroFrenando(){
        int contador = 0;
       

        while(contador < 5){
            if(!s1_bIsFirstTimeCar){
                Debug.Log("hola papichulo");
                while(time3 + 2 > Time.time){
                    s1_vehicleAiController.bIsBraking = true;
                    s1_vehicleAiController.detenerCarro();
                    yield return new WaitForSeconds(0.001f);
                }
                s1_vehicleAiController.bIsBraking = false;
            }else s1_bIsFirstTimeCar = false;
            yield return new WaitForSeconds(8.0f);
            time3 = Time.time;
            contador++;            
        }
        yield return new WaitForSeconds(0.1f);
        
        
    }

    IEnumerator frenoRepentino(){
        s1_vehicleAiController.maxVelocity = 15;
        yield return new WaitForSeconds(10.5f);
        time3 = Time.time;
        if(!s1_bIsFirstTimeCar){
            Debug.Log("hola papichulo");
            while(time3 + 4 > Time.time){
                s1_vehicleAiController.bIsBraking = true;
                s1_vehicleAiController.detenerCarro();
                yield return new WaitForSeconds(0.001f);
            }
            s1_vehicleAiController.bIsBraking = false;
        }else s1_bIsFirstTimeCar = false;
        yield return new WaitForSeconds(8.0f);
        
        
        
    }

    IEnumerator delayEncendidoCarro(){
        turnOnSound.Play();
        yield return new WaitForSeconds(1.0f);
        engineSound.Play();
    }
    
     IEnumerator delayFalta(int op, string faltaTexto){
      switch (op){
        case 1: {//Leves
            faltaLeve.SetActive(true);
            faltaGrave.SetActive(false);
            faltaDeficiente.SetActive(false);
            textoLeve.text = faltaTexto;
            yield return new WaitForSeconds(4.0f);
            faltaLeve.SetActive(false);

          break;
        }
        case 2: {//Deficientes
            faltaDeficiente.SetActive(true);
            faltaGrave.SetActive(false);
            faltaLeve.SetActive(false);
            textoDeficiente.text = faltaTexto;
            yield return new WaitForSeconds(4.0f);
            faltaDeficiente.SetActive(false);

          break;
        }
        case 3: {//Graves
            faltaGrave.SetActive(true);
            faltaDeficiente.SetActive(false);
            faltaLeve.SetActive(false);
            textoGrave.text = faltaTexto;
            yield return new WaitForSeconds(4.0f);
            faltaGrave.SetActive(false);

          break;
        }
        default: {
          break;
        }
      }
      yield return new WaitForSeconds(0.1f);
    }

    IEnumerator seccion1p1(){
        imageS1.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        imageS1.SetActive(false);
        while(true){
            if(!bienvenido.isPlaying && !isOver){
                if(!s1_bStarted){
                    s1_asIntro.Play();
                    yield return new WaitForSeconds(6.5f);
                }else if(!s1_bInstruccionDada){
                    s1_asInstruccion.Play();
                    yield return new WaitForSeconds(10.0f);
                    s1_bInstruccionDada = true;
                }

                

            }
            yield return new WaitForSeconds(0.01f);
        }   
    }

    IEnumerator seccion1p2(){
        imageS1.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        imageS1.SetActive(false);
        bool s1_bYaSonoIntroFreno3 = false, s1_bYaSonoFrenaYa = false;
        
        s1_asIntroP2.Play();
        yield return new WaitForSeconds(13.2f);
        
        while(true){
            if(s1_bTipoFreno2 && !s1_bYaSonoIntroFreno3){ 
                s1_asIntroFreno3.Play();
                yield return new WaitForSeconds(8.0f);
                s1_bYaSonoIntroFreno3 = true;
            }else if(s1_bTipoFreno2 && !s1_bYaSonoFrenaYa){
                s1_asFrenaYa.Play();
                yield return new WaitForSeconds(5.0f);
                s1_bYaSonoFrenaYa = true;
                bienHecho.Play();
                yield return new WaitForSeconds(3.0f);
            }else if(s1_bYaSonoFrenaYa && s1_bTipoFreno3){
                s1_asDaVuelta.Play();
                yield return new WaitForSeconds(6.0f);
                StopCoroutine(coroutine_s1p2);
            }
            yield return new WaitForSeconds(0.01f);
        }   
    }

    IEnumerator seccion2(){
        s1_asFinParte1.Play();
        yield return new WaitForSeconds(9.0f);
        imageS2.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        imageS2.SetActive(false);
        s2_asIntro.Play();
        yield return new WaitForSeconds(9.5f);
        s2_asIngresa.Play();
        yield return new WaitForSeconds(3.0f);
        while(true){
            if(!s2_bIngreso){
                s2_asCirculaGlorieta.Play();
                yield return new WaitForSeconds(11.0f);
            }else if(s2_bHayQueSalir){
                s2_asSalirGlorieta.Play();
                yield return new WaitForSeconds(6.0f);
                s2_bHayQueSalir = false;
            }else{
                yield return new WaitForSeconds(0.01f);
            }
        }
        
        
        
    }

    IEnumerator seccion3(){
        imageS3.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        imageS3.SetActive(false);
        bool s3_bYaDijoPasaTope1 = false, s3_bYaDijoPasaTope2 = false, s3_bYaDijoGiraALaDerecha = false, 
            s3_bYaDijoCarrilDerecho = false, s3_bYaDijoCarrilCentral = false, s3_bYadijoCarrilIzquierdo = false;
        //yield return new WaitForSeconds(4.5f);
        s3_asIntro.Play();
        yield return new WaitForSeconds(4.9f);
        while(true){
            if(s3_bTope1 && !s3_bYaDijoPasaTope1){
                s3_asPasaTope1.Play();
                yield return new WaitForSeconds(22.0f);
                s3_bYaDijoPasaTope1 = true;
            }else if(!s3_bYaDijoPasaTope2 && s3_bTope2){
                s3_asPasaTope2.Play();
                yield return new WaitForSeconds(5.0f);
                s3_bYaDijoPasaTope2 = true;
            }else if(!s3_bYaDijoGiraALaDerecha && s3_bGiraDerecha){
                s3_asGiraALaDerecha.Play();
                yield return new WaitForSeconds(5.0f);
                s3_bYaDijoGiraALaDerecha = true;
            }else if(!s3_bYaDijoCarrilDerecho && s3_bCarrilDerecho){
                s3_asCarrilDerecho.Play();
                yield return new WaitForSeconds(9.0f);
                s3_bYaDijoCarrilDerecho = true;
            }else if(s3_bYaDijoCarrilDerecho && !s3_bYaCambioACentral){
                s3_asCambiaACentral.Play();
                yield return new WaitForSeconds(7.5f);
            }else if(s3_bYaCambioACentral && !s3_bYaDijoCarrilCentral){
                s3_asCarrilCentral.Play();
                yield return new WaitForSeconds(6.7f);
                s3_bYaDijoCarrilCentral = true;
            }else if(s3_bYaDijoCarrilCentral && !s3_bYaCambioAIzquierdo){
                s3_asCambiaAIzquierdo.Play();
                yield return new WaitForSeconds(7.0f);
                
            }else if(s3_bYaCambioAIzquierdo && !s3_bYadijoCarrilIzquierdo){
                s3_asCarrilIzquierdo.Play();
                yield return new WaitForSeconds(6.0f);
                s3_bYadijoCarrilIzquierdo = true;
            }else if(s3_bYadijoCarrilIzquierdo){
                s3_asDaVuelta.Play();
                yield return new WaitForSeconds(8.0f);
                StopCoroutine(coroutine_s3);
                
            }
            yield return new WaitForSeconds(0.01f);
        }
        
    }
    
    IEnumerator seccion4(){
        imageS4.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        imageS4.SetActive(false);
        bool s4_bFirstTime = true, s4_bYaDijoConclusion = false, s4_bYaDijoCambiaCarril = false;
        s4_asIntro.Play();
        yield return new WaitForSeconds(13.5f);
        while(true){
            if(s4_vehicleAiController.rb.useGravity && s4_bFirstTime){
                s4_asCuidado.Play();
                yield return new WaitForSeconds(2.5f);
                s4_bFirstTime = false;
            }else if(!s4_bFirstTime && !s4_bYaDijoConclusion){
                s4_asConclusión.Play();
                yield return new WaitForSeconds(s4_asConclusión.clip.length + 1.0f);
                s4_bYaDijoConclusion = true;
            }else if(s4_bYaDijoConclusion && !s4_bYaDijoCambiaCarril){
                s4_asCambiaCarril.Play();
                yield return new WaitForSeconds(s4_asCambiaCarril.clip.length + 1.0f);
                s4_bYaDijoCambiaCarril = true;
                StopCoroutine(coroutine_s4);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator seccion5(){
        imageS5.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        imageS5.SetActive(false);
        bool s5_bYaDijoRebasaDer = false;
        s5_asIntro.Play();
        yield return new WaitForSeconds(8.0f);
        s5_asRebasaIzq.Play();
        yield return new WaitForSeconds(12.5f);
        while(true){
            if(s5_bYaRebasoIzq && !s5_bYaDijoRebasaDer){
                s5_asRebasaDer.Play();
                yield return new WaitForSeconds(11.0f);
                s5_bYaDijoRebasaDer = true;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator finalLeccion(){
        bienHecho.Play();
        yield return new WaitForSeconds(2.0f);
        felicidades.Play();
        darCalificaciones();
        
    }

    private void darCalificaciones()
    {

        ExamNumber.examNumber = 3;
        ExamNumber.criteriosAux = new List<Criterio>();
        ExamNumber.faltasAux = new List<Falta>();

        if(s1p_iChoqueCaso1 > 0){
            if(s1p_iChoqueCaso1 > 2){
                s1Cali -= 33;
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 3,
                    seccion = 1,
                    veces = 3,
                    texto = "Ligero contacto con el automóvil de adelante en el frenado 1",
                    puntos = 11
                });
            }else{
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 3,
                    seccion = 1,
                    veces = s1p_iChoqueCaso1,
                    texto = "Ligero contacto con el automóvil de adelante en el frenado 1",
                    puntos = 11
                });
            }
        }
        if(s1p_iChoqueCaso2 > 0){
            if(s1p_iChoqueCaso2 > 2){
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 3,
                    seccion = 1,
                    veces = 3,
                    texto = "Ligero contacto con el automóvil de adelante en el frenado 2",
                    puntos = 11
                });
            }else{
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 3,
                    seccion = 1,
                    veces = s1p_iChoqueCaso2,
                    texto = "Ligero contacto con el automóvil de adelante en el frenado 2",
                    puntos = 11
                });
            }
        }
        if(s1p_bChoqueCaso3){
            ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 3,
                seccion = 1,
                veces = 1,
                texto = "Ligero contacto con el automóvil de adelante en el frenado 3",
                puntos = 34
            });
        }
        

        if(s2p_iSalioCarril > 0){
            if(s2p_iSalioCarril > 7){
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 3,
                    seccion = 2,
                    veces = 7,
                    texto = "Salir del carril de la glorieta",
                    puntos = 10
                });
            }else{
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 3,
                    seccion = 2,
                    veces = s2p_iSalioCarril,
                    texto = "Salir del carril de la glorieta",
                    puntos = 10
                });
            }
        }
        if(s2p_iFueraRangoVelocidad > 0){
            if(s2p_iFueraRangoVelocidad > 3){
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 3,
                    seccion = 2,
                    veces = 3,
                    texto = "Fuera del rango de velocidad",
                    puntos = 10
                });
            }else{
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 3,
                    seccion = 2,
                    veces = s2p_iFueraRangoVelocidad,
                    texto = "Fuera del rango de velocidad",
                    puntos = 10
                });
            }
        }

        if(s3p_iPasaTopeRapido > 0){
            ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 3,
                seccion = 3,
                veces = s3p_iPasaTopeRapido,
                texto = "Más de 10km/h al cruzar un tope",
                puntos = 10
            });
        }
        if(s3p_iFueraRangoVelocidadIzq > 0){
            if(s3p_iFueraRangoVelocidadIzq > 2){
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 3,
                    seccion = 3,
                    veces = 2,
                    texto = "Fuera del rango de velocidad en el carril izquierdo",
                    puntos = 10
                });
            }else{
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 3,
                    seccion = 3,
                    veces = s3p_iFueraRangoVelocidadIzq,
                    texto = "Fuera del rango de velocidad en el carril izquierdo",
                    puntos = 10
                });
            }
        }
        if(s3p_iFueraRangoVelocidadDer > 0){
            if(s3p_iFueraRangoVelocidadDer > 2){
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 3,
                    seccion = 3,
                    veces = 2,
                    texto = "Fuera del rango de velocidad en el carril derecho",
                    puntos = 10
                });
            }else{
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 3,
                    seccion = 3,
                    veces = s3p_iFueraRangoVelocidadDer,
                    texto = "Fuera del rango de velocidad en el carril derecho",
                    puntos = 10
                });
            }
        }
        if(s3p_iFueraRangoVelocidadCen > 0){
            if(s3p_iFueraRangoVelocidadCen > 2){
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 3,
                    seccion = 3,
                    veces = 2,
                    texto = "Fuera del rango de velocidad en el carril central",
                    puntos = 10
                });
            }else{
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 3,
                    seccion = 3,
                    veces = s3p_iFueraRangoVelocidadCen,
                    texto = "Fuera del rango de velocidad en el carril central",
                    puntos = 10
                });
            }
        }
        if(s3p_iPusoDireccionales == 1 || s3p_iPusoDireccionales == 2){
            ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 3,
                seccion = 3,
                veces = s3p_iPusoDireccionales,
                texto = "No utilizar las direccionales",
                puntos = 10
            });
        }
        
        if(s4p_bTocoCarro){
            ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 3,
                seccion = 4,
                veces = 1,
                texto = "Ligero contacto con el automóvil de adelante",
                puntos = 100
            });
        }if(s4p_fMinDistance < 0.5f){
            ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 3,
                seccion = 4,
                veces = 1,
                texto = "Distancia con el automóvil de adelante menor a 0.5 metros",
                puntos = 30
            });
        }

        if(s5p_bHizoCambio1){
            if(!s5p_bHizoCambio2){
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 3,
                    seccion = 5,
                    veces = 1,
                    texto = "No realizar 2do cambio",
                    puntos = 30
                });
            }
        }else{
            ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 3,
                seccion = 5,
                veces = 1,
                texto = "No realizar 1er cambio",
                puntos = 30
            });
            ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 3,
                seccion = 5,
                veces = 1,
                texto = "No realizar 2do cambio",
                puntos = 30
            });
        }
        if(s5p_iFueraRangoVelocidad > 0){
            if(s5p_iFueraRangoVelocidad > 4){
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 3,
                    seccion = 5,
                    veces = 4,
                    texto = "Fuera del rango de velocidad",
                    puntos = 5
                });
            }else{
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 3,
                    seccion = 5,
                    veces = s5p_iFueraRangoVelocidad,
                    texto = "Fuera del rango de velocidad",
                    puntos = 5
                });
            }
        }
        if(s5p_iPusoDireccionales == 1 || s5p_iPusoDireccionales == 2){
            ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 3,
                seccion = 5,
                veces = s5p_iPusoDireccionales,
                texto = "No utilizar las direccionales",
                puntos = 10
            });
        }

        //faltas
        if(grave1>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 3,
                tipo = "graves",
                veces = grave1,
                texto = "No reducir velocidad en zona peatonal",
                puntos = 5
            });
        }
        if(grave2>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 3,
                tipo = "graves",
                veces = grave2,
                texto = "No detenerse en semáforo rojo",
                puntos = 5
            });
        }
        if(grave3>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 3,
                tipo = "graves",
                veces = grave3,
                texto = "Detenerse en paso peatonal",
                puntos = 5
            });
        }
        if(grave4>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 3,
                tipo = "graves",
                veces = grave4,
                texto = "Detención innecesaria",
                puntos = 5
            });
        }
        if(grave5>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 3,
                tipo = "graves",
                veces = grave5,
                texto = "No dejar pasar al peatón",
                puntos = 5
            });
        }
        if(grave8>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 3,
                tipo = "graves",
                veces = grave8,
                texto = "Pisar línea continua",
                puntos = 5
            });
        }
        if(def1>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 3,
                tipo = "deficientes",
                veces = def1,
                texto = "Arranque brusco que ocasionó el apagado del carro",
                puntos = 3
            });
        }
        if(def2>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 3,
                tipo = "deficientes",
                veces = def2,
                texto = "No poner direccionales",
                puntos = 3
            });
        }
        if(def3>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 3,
                tipo = "deficientes",
                veces = def3,
                texto = "No cambiar la marcha del vehículo que se requiere",
                puntos = 3
            });
        }
        if(def4>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 3,
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
                leccion = 3,
                tipo = "leves",
                veces = leve1,
                texto = "Arranque brusco sin ocasionar el apagado del carro",
                puntos = 1
            });
        }
        if(leve2>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 3,
                tipo = "leves",
                veces = leve2,
                texto = "No reducir velocidad ni frenar en topes y/o baches",
                puntos = 1
            });
        }
        if(leve3>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 3,
                tipo = "leves",
                veces = leve3,
                texto = "No enciende luces en túneles",
                puntos = 1
            });
        }
        //faltas

        if(s1p_iChoqueCaso1 > 2){
            s1Cali -= 33;
        }else{
            s1Cali -= s1p_iChoqueCaso1 * 11;
        }
        if(s1p_iChoqueCaso2 > 2){
            s1Cali -= 33;
        }else{
            s1Cali -= s1p_iChoqueCaso2 * 11;
        }
        if(s1p_bChoqueCaso3){
            s1Cali -= 34;
        }
        
        if(s2p_iSalioCarril > 7){
            s2Cali -= 80;
        }else{
            s2Cali -= s2p_iSalioCarril * 10;
        }
        if(s2p_iFueraRangoVelocidad > 3){
            s2Cali -= 30;
        }else{
            s2Cali -= s2p_iFueraRangoVelocidad * 10;
        }
        
        s3Cali -= s3p_iPasaTopeRapido * 10;
        if(s3p_iFueraRangoVelocidadDer > 2){
            s3Cali -= 20;
        }else{
            s3Cali -= s3p_iFueraRangoVelocidadDer * 10;
        }
        if(s3p_iFueraRangoVelocidadCen > 2){
            s3Cali -= 20;
        }else{
            s3Cali -= s3p_iFueraRangoVelocidadCen * 10;
        }
        if(s3p_iFueraRangoVelocidadIzq > 2){
            s3Cali -= 20;
        }else{
            s3Cali -= s3p_iFueraRangoVelocidadIzq * 10;
        }
        switch(s3p_iPusoDireccionales){
            case 1:{
                s3Cali -= 10;
                break;
            }
            case 2:{
                s3Cali -= 20;
                break;
            }
            default:{
                break;
            }
        }

        if(s4p_bTocoCarro){
            s4Cali = 0;
        }else if(s4p_fMinDistance < 0.5f){
            s4Cali -= 30;
        }

        if(s5p_bHizoCambio1){
            if(!s5p_bHizoCambio2){
                s5Cali -= 30;
            }
        }else{
            s5Cali -= 60;
        }
        if(s5p_iFueraRangoVelocidad > 4){
            s5Cali -= 20;
        }else{
            s5Cali -= s5p_iFueraRangoVelocidad * 5;
        }
        switch(s5p_iPusoDireccionales){
            case 1:{
                s5Cali -= 10;
                break;
            }
            case 2:{
                s5Cali -= 20;
                break;
            }
            default:{
                break;
            }
        }


        calificacionLeccion = ((s1Cali + s2Cali + s3Cali + s4Cali + s5Cali) / 5);
        Debug.Log("La calificacion fue: " + calificacionLeccion);
        calificacionFinal = calificacionLeccion - (faltasGraves * 5) - (faltasDeficientes * 3) - (faltasLeves);
        Debug.Log("La calificacion FINAL fue: " + calificacionFinal);



        SceneManager.LoadScene("retro");
         
    }

    private void detenerCarro(){
        rb.drag = 2.0f;
        foreach (WheelCollider wheel in throttleWheels)
        {
            wheel.motorTorque = 0f;
            wheel.brakeTorque = brakeStrenght;
        }
    }

    private void acelerarCarro()
    {
        rb.drag = 0.5f;
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

