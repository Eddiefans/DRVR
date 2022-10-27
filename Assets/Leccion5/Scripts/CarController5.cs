using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class CarController5 : MonoBehaviour
{
    [SerializeField] private GameObject  imageS1;
    [SerializeField] private GameObject  imageS2;
    [SerializeField] private GameObject  imageS3;
    [SerializeField] private GameObject  imageS4;
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
    public AudioSource engineSound, turnOnSound, bienHecho, felicidades, rojo;
    private bool bEncender = false;
    private float velocidadMaxima = 80;
    private bool coroutine2,coroutine3, coroutine4;
    private int layerMask = 1 << 6;
    private bool choque;

    private IEnumerator coroutine_s1, coroutine_s2, coroutine_s3,
                         coroutine_s4, felicitacion;
    private bool s1Finished = false, s2Finished = false, s3Finished = false, s4Finished = false;


    public bool dia, noche, neblina, lluvia;
    private string etapa;
    public GameObject inicio;

    public GameObject canvasAmbiente;
    private bool noGear = true;

    public AudioSource cambioVelocidad;

    /*------------Audios y booleans seccion 1-------------*/
    public AudioSource bienvenido, s1_asEnciende, s1_asEntraAv, s1_asEntraGlo, s1_asSalGlo, s1_asObstaculo, s1_asSalGlorietaExt, s1_asContinuaExt;
    public GameObject s1_goEntroAvenida, s1_goEntraGlorieta, s1_goSalGlorieta, s1_goObstaculo, s1_goFinExtension, s1_goSalirGlorieta1,
        s1_goContinua1, s1_goSalirGlorieta2, s1_goContinua2, s1_goEntrarGlorieta2;
    public GameObject [] s1_goExtension1, s1_goExtension2;
    private bool s1_bYaDioVuelta, s1_bEntrarGlorieta, s1_bSalirGlorieta, s1_bObstaculo, s1_bYaFinExtension, s1_bSalirGlorieta1,
        s1_bContinua1, s1_bSalirGlorieta2, s1_bContinua2, s1_bEntrarGlorieta2;

    /*------------Audios y booleans seccion 2-------------*/
    public AudioSource s2_asBaches, s2_asGiraDerecha, s2_asFrena, s2_asBienHecho, s2_asPeaton1, s2_asPeaton2, s2_asGiraCont;
    public GameObject s2_goBaches, s2_goGirarDerecha, s2_goFrena, s2_goPeaton1, s2_goPeaton2, s2_goGiraCont;

    public VehicleAiController s2_vehicleAiController;
    public PedestrianSpawnerL5[] s2_pedestrianSpawnersL5;
    public GameObject s2_goVehicleController, s2_goVehicleInicio;
    public carNode s2_nodeVehicle;
    private bool s2_bEsquivaBaches, s2_bGirarDerecha, s2_bFrena, s2_bPeaton1, s2_bPeaton2, s2_bGiraCont;
    public Waypoint s2_Waypoint1, s2_Waypoint2, s2_Waypoint3, s2_Waypoint4;

    /*------------Audios y booleans seccion 3-------------*/
    public AudioSource s3_asIntro, s3_asEnciende, s3_asFin;
    public GameObject s3_goIntro, s3_goEmpinada, s3_goContinua, s3_goFin;
    public bool s3_bIntro, s3_bEmpinada, s3_bContinua, s3_bFin;

    /* Puntuaciones */
    private int s1Cali = 100, s2Cali = 100, s3Cali = 100, s4Cali = 100, s5Cali = 100 ;
    public GameObject s1p_goSalirCarril, s2p_goObstaculo, s3p_goBache1, s3p_goBache2, s3p_goPeaton1, s3p_goPeaton2;
    private int s1p_iSalioCarril, s1p_iFueraRangoVelocidad, s3p_iFueraRangoVelocidad;
    private float s2p_fDistMin, s4p_fX;
    private bool s2p_bTocoCarro, s3p_bBache1, s3p_bBache2, s3p_bYaFreno1, s3p_bYaFreno2, s3p_bPeaton1, s3p_bPeaton2, s4p_bFueAtras;
    private RaycastHit p_Hit;
    private float time, time2, time3, time4, time5, timeDef6;
    private float caliFinDia, caliFinNoche, caliFinNeblina, caliFinLluvia, calificacionLeccion, calificacionFinal;


    /* Faltas */
    public List<GameObject> fg1_goPrimerTope, fg1_goSegundoTope, fg2_goCuboSemaforo, fg3_goZonaPeatonal, fg8_goLineas, fd5_goCuboAmarillo;
    public GameObject fg5_goSafeZone;
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

        imageS1.SetActive(false);
        imageS2.SetActive(false);
        imageS3.SetActive(false);
        imageS4.SetActive(false);

        alerta.SetActive(false);
        faltaGrave.SetActive(false);
        faltaDeficiente.SetActive(false);
        faltaLeve.SetActive(false);
        Init();
        StartCoroutine(coroutine_s1);
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
        //s1Finished = true;
    }

    private void Init(){
        etapa = "dia";

        coroutine_s1 = seccion1();
        coroutine_s2 = seccion2();
        coroutine_s3 = seccion3();
        
        coroutine_s4 = seccion4();
        felicitacion = finalLeccion();

        fg1_bYaEsteTope1 = new bool [9];
        fg1_bYaEsteTope2 = new bool [9];
        fg2_bYaEsteCubo = new bool [15];
        fd5_bYaEsteAmarillo = new bool[fd5_goCuboAmarillo.Count];
        fg3_bSeParo = new bool[11];
        fg8_bYaLinea = new bool[fg8_goLineas.Count];
        fd4_sancionesVelocidades = new bool[7];
        fd4_sancionesVelocidades[0] = true;
    }

    // Update is called once per frame
    void Update()
    {
        /* if(im.l){
            lm.ToggleHeadlights();
        } */
        
        foreach (GameObject tl in tailLights)
        {
            tl.GetComponent<Renderer>().material.SetColor("_EmissionColor", im.brake ? new Color(0.5f, 0.111f, 0.111f) : Color.black);
        }

        
        checkVelocidad();

        leccion();

        //turnOn();

        faltas();

        checkFaltas();
        checkButtons();
        

        /*                    */
        Vector3 forward = fg5_goSafeZone.transform.TransformDirection(Vector3.forward) * 6;
        Debug.DrawRay(fg5_goSafeZone.transform.position, forward, Color.green);
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
        audios = GameObject.Find("Leccion5").GetComponentsInChildren<AudioSource>();
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
        for(int i = 0; i < fg2_goCuboSemaforo.Count; i++){
            if(Physics.CheckBox(fg2_goCuboSemaforo[i].transform.position, fg2_goCuboSemaforo[i].transform.localScale / 2,
            fg2_goCuboSemaforo[i].transform.rotation, layerMask) && !fg2_bYaEsteCubo[i] && fg2_goCuboSemaforo[i].GetComponent<BoxCollider>().enabled){
                faltasGraves++;
                grave2++;
                StartCoroutine(delayFalta(3, "No detenerse en semáforo rojo"));
                fg2_bYaEsteCubo[i] = true;
                Debug.Log("Grave 2");
            } 
        }
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
                    Debug.Log("Grave 3 en el " + fg3_goZonaPeatonal[i].name);
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
        for(int i = 0; i < fd5_goCuboAmarillo.Count; i++){
            if(Physics.CheckBox(fd5_goCuboAmarillo[i].transform.position, fd5_goCuboAmarillo[i].transform.localScale / 2,
            fd5_goCuboAmarillo[i].transform.rotation, layerMask) && !fd5_bYaEsteAmarillo[i] && fd5_goCuboAmarillo[i].GetComponent<BoxCollider>().enabled){
                faltasDeficientes++;
                def5++;
                StartCoroutine(delayFalta(2, "No detenerse en semáforo amarillo"));
                fd5_bYaEsteAmarillo[i] = true;
                Debug.Log("Deficiente 5");
            } 
        }
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
        if(!s1Finished){
            if(!s1_bYaDioVuelta && Physics.CheckBox(s1_goEntroAvenida.transform.position, s1_goEntroAvenida.transform.localScale / 2,
            s1_goEntroAvenida.transform.rotation, layerMask)){
                s1_bYaDioVuelta = true;
            }else if(!s1_bEntrarGlorieta && Physics.CheckBox(s1_goEntraGlorieta.transform.position, s1_goEntraGlorieta.transform.localScale / 2,
            s1_goEntraGlorieta.transform.rotation, layerMask)){
                s1_bEntrarGlorieta = true;
            }else if(!s1_bSalirGlorieta1 && Physics.CheckBox(s1_goSalirGlorieta1.transform.position, s1_goSalirGlorieta1.transform.localScale / 2,
            s1_goSalirGlorieta1.transform.rotation, layerMask)){
                s1_bSalirGlorieta1 = true;
            }else if(!s1_bContinua1 && Physics.CheckBox(s1_goContinua1.transform.position, s1_goContinua1.transform.localScale / 2,
            s1_goContinua1.transform.rotation, layerMask)){
                s1_bContinua1 = true;
            }else if(!s1_bSalirGlorieta2 && Physics.CheckBox(s1_goSalirGlorieta2.transform.position, s1_goSalirGlorieta2.transform.localScale / 2,
            s1_goSalirGlorieta2.transform.rotation, layerMask)){
                s1_bSalirGlorieta2 = true;
            }else if(!s1_bContinua2 && Physics.CheckBox(s1_goContinua2.transform.position, s1_goContinua2.transform.localScale / 2,
            s1_goContinua2.transform.rotation, layerMask)){
                s1_bContinua2 = true;
            }else if(!s1_bEntrarGlorieta2 && Physics.CheckBox(s1_goEntrarGlorieta2.transform.position, s1_goEntrarGlorieta2.transform.localScale / 2,
            s1_goEntrarGlorieta2.transform.rotation, layerMask)){
                s1_bEntrarGlorieta2 = true;
            }
            
            else if(!s1_bSalirGlorieta && Physics.CheckBox(s1_goSalGlorieta.transform.position, s1_goSalGlorieta.transform.localScale / 2,
            s1_goSalGlorieta.transform.rotation, layerMask)){
                s1_bSalirGlorieta = true;
            }else if(!s1_bObstaculo && Physics.CheckBox(s1_goObstaculo.transform.position, s1_goObstaculo.transform.localScale / 2,
            s1_goObstaculo.transform.rotation, layerMask)){
                s1_bObstaculo = true;

                s2p_fDistMin = Vector3.Distance(transform.position, s2p_goObstaculo.transform.position);

                s1Finished = true;
                StartCoroutine(coroutine_s2);
            }

            //Evaluación
            if(!s1_asEnciende.isPlaying && !s1_asEntraAv.isPlaying && !bienvenido.isPlaying){
                if(Physics.CheckBox(s1p_goSalirCarril.transform.position, s1p_goSalirCarril.transform.localScale / 2,
                s1p_goSalirCarril.transform.rotation, layerMask) && (time+3 <= Time.time)){
                    time = Time.time;
                    Debug.Log("Salio Carril seccion 1");
                    s1p_iSalioCarril++;
                }
                if(rb.velocity.magnitude * 7 > 40 && time2+3 <= Time.time){
                    time2 = Time.time;
                    Debug.Log("Muy rapido seccion 1");
                    s1p_iFueraRangoVelocidad++;
                }
                if(rb.velocity.magnitude * 7 < 30 && time3+3 <= Time.time && 
                !Physics.Raycast(fg5_goSafeZone.transform.position, fg5_goSafeZone.transform.TransformDirection(Vector3.forward), out p_Hit, 6) ){
                    time3 = Time.time;
                    Debug.Log("Muy lento seccion 1");
                    s1p_iFueraRangoVelocidad++;
                }
            }

        }else if(!s2Finished){
            if(!s1_bYaFinExtension && Physics.CheckBox(s1_goFinExtension.transform.position, s1_goFinExtension.transform.localScale / 2,
                s1_goFinExtension.transform.rotation, layerMask)){
                s1_bYaFinExtension = true;
                foreach (var item in s1_goExtension2)
                {
                    item.SetActive(false);
                }
                foreach (var item in s1_goExtension1)
                {
                    item.SetActive(true);
                }
            }
            if(!s2_bEsquivaBaches && Physics.CheckBox(s2_goBaches.transform.position, s2_goBaches.transform.localScale / 2,
            s2_goBaches.transform.rotation, layerMask)){
                s2_bEsquivaBaches = true;
            }else if(!s2_bGirarDerecha && Physics.CheckBox(s2_goGirarDerecha.transform.position, s2_goGirarDerecha.transform.localScale / 2,
            s2_goGirarDerecha.transform.rotation, layerMask)){
                s2_bGirarDerecha = true;
                
            }else if(!s2_bFrena && Physics.CheckBox(s2_goFrena.transform.position, s2_goFrena.transform.localScale / 2,
            s2_goFrena.transform.rotation, layerMask)){
                s2_bFrena = true;
                s2_vehicleAiController.rb.useGravity = true;
                s2_goVehicleController.GetComponent<Rigidbody>().isKinematic = false;
                
            }else if(!s2_bPeaton1 && Physics.CheckBox(s2_goPeaton1.transform.position, s2_goPeaton1.transform.localScale / 2,
            s2_goPeaton1.transform.rotation, layerMask)){
                s2_Waypoint1.nextWaypoint = s2_Waypoint2;
                s2_bPeaton1 = true;
            }else if(!s2_bPeaton2 && Physics.CheckBox(s2_goPeaton2.transform.position, s2_goPeaton2.transform.localScale / 2,
            s2_goPeaton2.transform.rotation, layerMask)){
                s2_Waypoint3.nextWaypoint = s2_Waypoint4;
                s2_bPeaton2 = true;

                
            }else if(!s2_bGiraCont && Physics.CheckBox(s2_goGiraCont.transform.position, s2_goGiraCont.transform.localScale / 2,
            s2_goGiraCont.transform.rotation, layerMask)){
                s2_bGiraCont = true;

                StopCoroutine(coroutine_s2);
                StartCoroutine(coroutine_s3);
                s2Finished = true;
            }

            //Evaluación
            if(Vector3.Distance(transform.position, s2p_goObstaculo.transform.position) < s2p_fDistMin){
                s2p_fDistMin = Vector3.Distance(transform.position, s2p_goObstaculo.transform.position);
            }
            if(!s3p_bBache1 && Physics.CheckBox(s3p_goBache1.transform.position, s3p_goBache1.transform.localScale / 2,
            s3p_goBache1.transform.rotation, layerMask)){
                s3p_bBache1 = true;
            }
            if(!s3p_bBache2 && Physics.CheckBox(s3p_goBache2.transform.position, s3p_goBache2.transform.localScale / 2,
            s3p_goBache2.transform.rotation, layerMask)){
                s3p_bBache2 = true;
            }
            if(!s3p_bPeaton1 && Physics.CheckBox(s3p_goPeaton1.transform.position, s3p_goPeaton1.transform.localScale / 2,
            s3p_goPeaton1.transform.rotation, layerMask)){
                s3p_bPeaton1 = true;
            }
            if(!s3p_bPeaton2 && Physics.CheckBox(s3p_goPeaton2.transform.position, s3p_goPeaton2.transform.localScale / 2,
            s3p_goPeaton2.transform.rotation, layerMask)){
                s3p_bPeaton2 = true;
            }
            if(s2_bPeaton1 && !s3p_bPeaton1 && !s3p_bYaFreno1 && rb.velocity.magnitude < 1 && Vector3.Distance(transform.position, s3p_goPeaton1.transform.position) > 4){
                s3p_bYaFreno1 = true;
            }
            if(s2_bPeaton2 && !s3p_bPeaton2 && !s3p_bYaFreno2 && rb.velocity.magnitude < 1 && Vector3.Distance(transform.position, s3p_goPeaton2.transform.position) > 4){
                s3p_bYaFreno2 = true;
            }
            if(rb.velocity.magnitude * 7 > 40 && time4+3 <= Time.time){
                time4 = Time.time;
                Debug.Log("Muy rapido seccion 3");
                s3p_iFueraRangoVelocidad++;
            }
            if(rb.velocity.magnitude * 7 < 30 && time5+3 <= Time.time && 
            !Physics.Raycast(fg5_goSafeZone.transform.position, fg5_goSafeZone.transform.TransformDirection(Vector3.forward), out p_Hit, 6) ){
                time5 = Time.time;
                Debug.Log("Muy lento seccion 3");
                s3p_iFueraRangoVelocidad++;
            }

        }else if(!s3Finished){
            if(!s3_bIntro && Physics.CheckBox(s3_goIntro.transform.position, s3_goIntro.transform.localScale / 2,
            s3_goIntro.transform.rotation, layerMask)){
                s3_bIntro = true;

            }else if(!s3_bEmpinada && Physics.CheckBox(s3_goEmpinada.transform.position, s3_goEmpinada.transform.localScale / 2,
            s3_goEmpinada.transform.rotation, layerMask) && im.brakeNormal && !bEncender){
                s3_bEmpinada = true;
                s4p_fX = transform.position.x;
            }else if(!s3_bContinua && Physics.CheckBox(s3_goContinua.transform.position, s3_goContinua.transform.localScale / 2,
            s3_goContinua.transform.rotation, layerMask)){
                s3_bContinua = true;
            }else if(!s3_bFin && Physics.CheckBox(s3_goFin.transform.position, s3_goFin.transform.localScale / 2,
            s3_goFin.transform.rotation, layerMask)){
                s3_bFin = true;

                s3Finished = true;
                StartCoroutine(felicitacion);
                StopCoroutine(coroutine_s3);
            }

            //Evaluación
            if(s3_bEmpinada){
                if(!s4p_bFueAtras && transform.position.x < s4p_fX - 0.5f){
                    s4p_bFueAtras = true;
                }
                s4p_fX = transform.position.x;
                Thread.Sleep(10);
            }
            
        }
    }

    private void FixedUpdate() {
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
            if(im.brakeNormal){
                rb.drag = 0.25f;
            }else{
                rb.drag = 0.05f;
            }
            detenerCarro();
        }    
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

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.name != "Tocus_Win_Front (1)"){
            
                if(other.relativeVelocity.magnitude > 4){
                    breakingGlass.Play();
                    ventana.SetActive(true);
                    start.DestroyMesh();
                    //Módulo de Alertas
                    alerta.SetActive(true);
                     textoMotivo.text = "Choque";
                     inmovilizarCarro();

                }else{
                    if(!s2Finished && other.gameObject.tag == "CarIA" && !s2p_bTocoCarro){
                        s2p_bTocoCarro = true;
                    }
                }
                Debug.Log("Collision Detected");
            
            
           
        }
    }

    IEnumerator seccion1(){
        imageS1.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        imageS1.SetActive(false);
        bool s1_bYaDijoEntraGlorieta = false, s1_bYaDijoSalGlorieta = false, s1_bYaDijoObst = false;
        bool s1_bYaDijoSalirGlorieta1 = false, s1_bYaDijoContinua1 = false, s1_bYaDijoSalirGlorieta2 = false,
            s1_bYaDijoContinua2 = false, s1_bYaDijoEntrarGlorieta2 = false;
        while(bienvenido.isPlaying){
            yield return new WaitForSeconds(0.01f);
        }
        s1_asEnciende.Play();if(isOver) s1_asEnciende.Stop();
        yield return new WaitForSeconds(1.91f);
        while(true && !isOver){
           
            if(!s1_bYaDioVuelta){
                s1_asEntraAv.Play();
                yield return new WaitForSeconds(12.0f);
            }else if(s1_bEntrarGlorieta && !s1_bYaDijoEntraGlorieta){
                s1_asEntraGlo.Play();
                yield return new WaitForSeconds(3.5f);
                s1_bYaDijoEntraGlorieta = true;
            }else if(s1_bSalirGlorieta1 && !s1_bYaDijoSalirGlorieta1){
                s1_asSalGlorietaExt.Play();
                yield return new WaitForSeconds(s1_asSalGlorietaExt.clip.length + 2);
                s1_bYaDijoSalirGlorieta1 = true;
            }else if(s1_bContinua1 && !s1_bYaDijoContinua1){
                s1_asContinuaExt.Play();
                yield return new WaitForSeconds(s1_asContinuaExt.clip.length + 2);
                s1_bYaDijoContinua1 = true;
            }else if(s1_bSalirGlorieta2 && !s1_bYaDijoSalirGlorieta2){
                s1_asSalGlorietaExt.Play();
                yield return new WaitForSeconds(s1_asSalGlorietaExt.clip.length + 2);
                s1_bYaDijoSalirGlorieta2 = true;
            }else if(s1_bContinua2 && !s1_bYaDijoContinua2){
                s1_asContinuaExt.Play();
                yield return new WaitForSeconds(s1_asContinuaExt.clip.length + 2);
                s1_bYaDijoContinua2 = true;
            }else if(s1_bEntrarGlorieta2 && !s1_bYaDijoEntrarGlorieta2){
                s1_asEntraGlo.Play();
                yield return new WaitForSeconds(s1_asEntraGlo.clip.length);
                s1_bYaDijoEntrarGlorieta2 = true;
            }
            
            else if(s1_bSalirGlorieta && !s1_bYaDijoSalGlorieta){
                s1_asSalGlo.Play();
                yield return new WaitForSeconds(3.5f);
                s1_bYaDijoSalGlorieta = true;
            }else if(s1_bObstaculo && !s1_bYaDijoObst){
                s1_asObstaculo.Play();
                yield return new WaitForSeconds(5.0f);
                s1_bYaDijoObst = true;
                StopCoroutine(coroutine_s1);
            }
            
            yield return new WaitForSeconds(0.01f);
        }   
    }

    IEnumerator seccion2(){
        imageS2.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        imageS2.SetActive(false);
        bool s2_bYaDijoEsquivar = false, s2_bYaDijoGiraDerecha = false, s2_bYaDijoFrena = false, s2_bYaDijoPeaton1 = false,
        s2_bYaDijoPeaton2 = false;
        while(true){
            if(s2_bEsquivaBaches && !s2_bYaDijoEsquivar){
                s2_asBaches.Play();
                yield return new WaitForSeconds(6.5f);
                s2_bYaDijoEsquivar = true;
            }else if(s2_bGirarDerecha && !s2_bYaDijoGiraDerecha){
                s2_asGiraDerecha.Play();
                yield return new WaitForSeconds(2.55f);
                s2_bYaDijoGiraDerecha = true;
                
            }else if(s2_bFrena && !s2_bYaDijoFrena){
                s2_asFrena.Play();
                yield return new WaitForSeconds(s2_asFrena.clip.length + 0.5f);
                s2_bYaDijoFrena = true;
                s2_asBienHecho.Play();
                yield return new WaitForSeconds(s2_asBienHecho.clip.length);
            }else if(s2_bPeaton1 && !s2_bYaDijoPeaton1){
                yield return new WaitForSeconds(1.0f);
                s2_asPeaton1.Play();
                yield return new WaitForSeconds(s2_asPeaton1.clip.length + 0.2f);
                s2_bYaDijoPeaton1 = true;
            }else if(s2_bPeaton2 && !s2_bYaDijoPeaton2){
                yield return new WaitForSeconds(1.0f);
                s2_asPeaton2.Play();
                yield return new WaitForSeconds(s2_asPeaton2.clip.length + 0.5f);
                s2_bYaDijoPeaton2 = true;
                s2_asGiraDerecha.Play();
                yield return new WaitForSeconds(s2_asGiraDerecha.clip.length + 0.2f);
            }
            yield return new WaitForSeconds(0.1f);
        }
        
    }

    IEnumerator seccion3(){
        imageS3.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        imageS3.SetActive(false);
        bool s3_bYaDijoIntro = false, s3_bYaDijoEnciende = false, s3_bYaDijoContinua = false;
        while(true){
            if(s3_bIntro && !s3_bYaDijoIntro){
                s3_asIntro.Play();
                yield return new WaitForSeconds(s3_asIntro.clip.length + 0.1f);
                s3_bYaDijoIntro = true;
            }else if(s3_bEmpinada && !s3_bYaDijoEnciende){
                s3_asEnciende.Play();
                yield return new WaitForSeconds(s3_asEnciende.clip.length + 0.1f);
                s3_bYaDijoEnciende = true;
            }else if(s3_bContinua && !s3_bYaDijoContinua){
                s3_asFin.Play();
                yield return new WaitForSeconds(s3_asFin.clip.length + 0.1f);
                s3_bYaDijoContinua = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
        
    }

    IEnumerator seccion4(){
        imageS4.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        imageS4.SetActive(false);
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator finalLeccion(){
        bienHecho.Play();
        yield return new WaitForSeconds(2.0f);
        felicidades.Play();
        darCalificaciones();
    }

    private void darCalificaciones()
    {
        int contadorEtapa = 0;
        if(noche){
            contadorEtapa++;
        }
        if(neblina){
            contadorEtapa++;
        }
        if(lluvia){
            contadorEtapa++;
        }
        if(dia){
            contadorEtapa++;
        }
        if(contadorEtapa == 0){
            ExamNumber.criteriosAux = new List<Criterio>();
            ExamNumber.faltasAux = new List<Falta>();
            
        }
        ExamNumber.examNumber = 5;

        switch(etapa){
            case "dia":{
                contadorEtapa = 0;
                break;
            }
            case "noche":{
                contadorEtapa = 4;
                break;
            }
            case "neblina":{
                contadorEtapa = 8;
                break;
            }
            case "lluvia":{
                contadorEtapa = 12;
                break;
            }
            default:{
                break;
            }
        }
        
        if(s1p_iFueraRangoVelocidad > 0){
            if(s1p_iFueraRangoVelocidad > 10){
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 5,
                    seccion = 1 + contadorEtapa,
                    veces = 10,
                    texto = "Fuera del rango de velocidad",
                    puntos = 5
                });
            }else{
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 5,
                    seccion = 1 + contadorEtapa,
                    veces = s1p_iFueraRangoVelocidad,
                    texto = "Fuera del rango de velocidad",
                    puntos = 5
                });
            }
        }
        if(s1p_iSalioCarril > 0){
            if(s1p_iSalioCarril > 5){
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 5,
                    seccion = 1 + contadorEtapa,
                    veces = 5,
                    texto = "Salir del carril correspondiente",
                    puntos = 10
                });
            }else{
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 5,
                    seccion = 1 + contadorEtapa,
                    veces = s1p_iSalioCarril,
                    texto = "Salir del carril correspondiente",
                    puntos = 10
                });
            }
            
        }

        if(s2p_fDistMin < 0.5f){
            ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 5,
                seccion = 2 + contadorEtapa,
                veces = 1,
                texto = "Demasiado cerca del obstáculo",
                puntos = 50
            });
        }
        if(s2p_bTocoCarro){
            ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 5,
                seccion = 2 + contadorEtapa,
                veces = 1,
                texto = "Ligero contacto con otro automóvil",
                puntos = 50
            });
        }

        if(s3p_bBache1 || s3p_bBache2){
            int contadorAux = 0;
            if(s3p_bBache1){
                contadorAux++;
            }
            if(s3p_bBache2){
                contadorAux++;
            }
            ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 5,
                seccion = 3 + contadorEtapa,
                veces = contadorAux,
                texto = "Pasar por encima de un bache",
                puntos = 15
            });
        }
        if(!s3p_bYaFreno1 || !s3p_bYaFreno2){
            int contadorAux = 0;
            if(!s3p_bYaFreno1){
                contadorAux++;
            }
            if(!s3p_bYaFreno2){
                contadorAux++;
            }
            ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 5,
                seccion = 3 + contadorEtapa,
                veces = contadorAux,
                texto = "Incorrecto o inexistente frenado antes del peatón",
                puntos = 20
            });
        }
        if(s3p_iFueraRangoVelocidad > 0){
            if(s3p_iFueraRangoVelocidad > 3){
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 5,
                    seccion = 3 + contadorEtapa,
                    veces = 3,
                    texto = "Fuera del rango de velocidad",
                    puntos = 10
                });
            }else{
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 5,
                    seccion = 3 + contadorEtapa,
                    veces = s3p_iFueraRangoVelocidad,
                    texto = "Fuera del rango de velocidad",
                    puntos = 10
                });
            }
            
        }

        if(s4p_bFueAtras){
            ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 5,
                seccion = 4 + contadorEtapa,
                veces = 1,
                texto = "El carro se fue para atrás",
                puntos = 40
            });
        }


        //faltas
        if(grave1>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 5,
                tipo = "graves",
                veces = grave1,
                texto = "No reducir velocidad en zona peatonal",
                puntos = 5,
                etapa = (contadorEtapa/4) + 1
            });
        }
        if(grave2>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 5,
                tipo = "graves",
                veces = grave2,
                texto = "No detenerse en semáforo rojo",
                puntos = 5,
                etapa = (contadorEtapa/4) + 1
            });
        }
        if(grave3>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 5,
                tipo = "graves",
                veces = grave3,
                texto = "Detenerse en paso peatonal",
                puntos = 5,
                etapa = (contadorEtapa/4) + 1
            });
        }
        if(grave4>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 5,
                tipo = "graves",
                veces = grave4,
                texto = "Detención innecesaria",
                puntos = 5,
                etapa = (contadorEtapa/4) + 1
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
                leccion = 3,
                tipo = "graves",
                veces = grave8,
                texto = "Pisar línea continua",
                puntos = 5,
                etapa = (contadorEtapa/4) + 1
            });
        }
        if(def1>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 5,
                tipo = "deficientes",
                veces = def1,
                texto = "Arranque brusco que ocasionó el apagado del carro",
                puntos = 3,
                etapa = (contadorEtapa/4) + 1
            });
        }
        if(def2>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 5,
                tipo = "deficientes",
                veces = def2,
                texto = "No poner direccionales",
                puntos = 3,
                etapa = (contadorEtapa/4) + 1
            });
        }
        if(def3>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 5,
                tipo = "deficientes",
                veces = def3,
                texto = "No cambiar la marcha del vehículo que se requiere",
                puntos = 3,
                etapa = (contadorEtapa/4) + 1
            });
        }
        if(def4>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 5,
                tipo = "deficientes",
                veces = def4,
                texto = "No pisar el clutch para cambiar de velocidad",
                puntos = 3,
                etapa = (contadorEtapa/4) + 1
            });
        }
        if(def5>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 5,
                tipo = "deficientes",
                veces = def5,
                texto = "No detenerse en semáforo amarillo",
                puntos = 3,
                etapa = (contadorEtapa/4) + 1
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
                leccion = 5,
                tipo = "leves",
                veces = leve1,
                texto = "Arranque brusco sin ocasionar el apagado del carro",
                puntos = 1,
                etapa = (contadorEtapa/4) + 1
            });
        }
        if(leve2>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 5,
                tipo = "leves",
                veces = leve2,
                texto = "No reducir velocidad ni frenar en topes y/o baches",
                puntos = 1,
                etapa = (contadorEtapa/4) + 1
            });
        }
        if(leve3>0){
            ExamNumber.faltasAux.Add(new Falta(){
                leccion = 5,
                tipo = "leves",
                veces = leve3,
                texto = "No enciende luces en túneles",
                puntos = 1,
                etapa = (contadorEtapa/4) + 1
            });
        }
        //faltas

        if(s1p_iFueraRangoVelocidad > 0){
            if(s1p_iFueraRangoVelocidad > 10){
                s1Cali -= 50;
            }else{
                s1Cali -= s1p_iFueraRangoVelocidad * 5;
            }
        }
        if(s1p_iSalioCarril > 0){
            if(s1p_iSalioCarril > 5){
                s1Cali -= 50;
            }else{
                s1Cali -= s1p_iSalioCarril * 10;
            }
        }

        if(s2p_fDistMin < 0.5f){
            s2Cali -= 50;
        }
        if(s2p_bTocoCarro){
            s2Cali -= 50;
        }

        if(s3p_bBache1 || s3p_bBache2){
            int contadorAux = 0;
            if(s3p_bBache1){
                s3Cali -= 15;
                contadorAux++;
            }
            if(s3p_bBache2){
                s3Cali -= 15;
                contadorAux++;
            }
        }
        if(!s3p_bYaFreno1 || !s3p_bYaFreno2){
            int contadorAux = 0;
            if(!s3p_bYaFreno1){
                s3Cali -= 20;
                contadorAux++;
            }
            if(!s3p_bYaFreno2){
                s3Cali -= 20;
                contadorAux++;
            }
        }
        if(s3p_iFueraRangoVelocidad > 0){
            if(s3p_iFueraRangoVelocidad > 3){
                s3Cali -= 30;
            }else{
                s3Cali -= s3p_iFueraRangoVelocidad * 10;
            }
        }

        if(s4p_bFueAtras){
            s4Cali -= 40;
        }



        calificacionLeccion = ((s1Cali + s2Cali + s3Cali + s4Cali ) / 4);
        Debug.Log("La calificacion fue: " + calificacionLeccion);
        
        switch(etapa){
            case "dia":{
                caliFinDia = calificacionLeccion - (faltasGraves * 5) - (faltasDeficientes * 3) - (faltasLeves);
                Debug.Log("La calificacion FINAL DE DIA fue: " + caliFinDia);
                dia = true;
                break;
            }
            case "noche":{
                caliFinNoche = calificacionLeccion - (faltasGraves * 5) - (faltasDeficientes * 3) - (faltasLeves);
                Debug.Log("La calificacion FINAL DE NOCHE fue: " + caliFinNoche);
                noche = true;
                break;
            }
            case "neblina":{
                caliFinNeblina = calificacionLeccion - (faltasGraves * 5) - (faltasDeficientes * 3) - (faltasLeves);
                Debug.Log("La calificacion FINAL DE NEBLINA fue: " + caliFinNeblina);
                neblina = true;
                break;
            }
            case "lluvia":{
                caliFinLluvia = calificacionLeccion - (faltasGraves * 5) - (faltasDeficientes * 3) - (faltasLeves);
                Debug.Log("La calificacion FINAL DE LLUVIA fue: " + caliFinLluvia);
                lluvia = true;
                break;
            }
            default:{
                break;
            }
        }
        canvasAmbiente.SetActive(true);
        if(dia && lluvia && neblina && noche){
            calificacionFinal = (caliFinDia * 0.25f) + (caliFinLluvia * 0.25f) + (caliFinNeblina * 0.25f) + (caliFinNoche * 0.25f);
            Debug.Log("La calificacion FINAL DEFINITIVA fue: " + calificacionFinal);
            SceneManager.LoadScene("retro");
        }

        
    }

    public void restart(string etapa){
        this.etapa = etapa;

        canvasAmbiente.SetActive(false);

        bEncender = false;
        s1Finished = false;
        s2Finished = false;
        s3Finished = false;
        s4Finished = false;
        s1_bEntrarGlorieta = false;
        s1_bObstaculo = false;
        s1_bSalirGlorieta = false;
        s1_bYaDioVuelta = false;
        s2_bEsquivaBaches = false;
        s2_bFrena = false;
        s2_bGiraCont = false;
        s2_bGirarDerecha = false;
        s2_bPeaton1 = false;
        s2_bPeaton2 = false;
        s3_bContinua = false;
        s3_bEmpinada = false;
        s3_bFin = false;
        s3_bIntro = false;
        fg1_bYaEsteTope1 = new bool [9];
        fg1_bYaEsteTope2 = new bool [9];
        fg2_bYaEsteCubo = new bool [15];
        fg3_bSeParo = new bool[11];
        s1Cali = 100;
        s2Cali = 100;
        s3Cali = 100;
        s4Cali = 100;
        s1p_iFueraRangoVelocidad = 0;
        s1p_iSalioCarril = 0;
        s3p_iFueraRangoVelocidad = 0;
        s2p_fDistMin = 0f;
        s4p_fX = 0f;
        s2p_bTocoCarro = false;
        s3p_bBache1 = false;
        s3p_bBache2 = false;
        s3p_bYaFreno1 = false;
        s3p_bYaFreno2 = false;
        s3p_bPeaton1 = false;
        s3p_bPeaton2 = false;
        s4p_bFueAtras = false;
        time = 0f;
        time2 = 0f;
        time3 = 0f;
        time4 = 0f;
        time5 = 0f;

        faltasGraves = 0;
        faltasDeficientes = 0;
        faltasLeves = 0;
        grave1 = 0;
        grave2 = 0;
        grave3 = 0;
        grave4 = 0;
        grave8 = 0;
        def1 = 0;
        def2 = 0;
        def3 = 0;
        def4 = 0;
        def5 = 0;
        leve1 = 0;
        leve2 = 0;
        leve3 = 0;

        for (int i = 0; i < s2_pedestrianSpawnersL5.Length; i++)
        {
            s2_pedestrianSpawnersL5[i].restart();
        }

        s2_goVehicleController.transform.position = s2_goVehicleInicio.transform.position;
        s2_goVehicleController.transform.rotation = s2_goVehicleInicio.transform.rotation;
        s2_goVehicleController.GetComponent<Rigidbody>().MovePosition(s2_goVehicleInicio.transform.position);
        s2_goVehicleController.GetComponent<Rigidbody>().MoveRotation(s2_goVehicleInicio.transform.rotation);
        Thread.Sleep(500);
        s2_goVehicleController.GetComponent<Rigidbody>().isKinematic = true;
        s2_vehicleAiController.bForceVelocity = true;
        s2_vehicleAiController.rb.useGravity = false;
        s2_vehicleAiController.maxVelocity = 150;
        s2_vehicleAiController.currentNode = s2_nodeVehicle;

        rb.MovePosition(inicio.transform.position);
        rb.MoveRotation(inicio.transform.rotation);

        s2_Waypoint1.nextWaypoint = null;
        s2_Waypoint3.nextWaypoint = null;

        coroutine_s1 = seccion1();
        coroutine_s2 = seccion2();
        coroutine_s3 = seccion3();
        
        coroutine_s4 = seccion4();
        felicitacion = finalLeccion();

        StartCoroutine(coroutine_s1);
        

    }

    private void detenerCarro(){
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
