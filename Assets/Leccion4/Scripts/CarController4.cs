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
public class CarController4 : MonoBehaviour
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
    private bool choque;

    private IEnumerator coroutine_s1, coroutine_s2, coroutine_s3,
                         coroutine_s4, felicitacion;
    private bool s1Finished = false, s2Finished = false, s3Finished = false, s4Finished = false;
    private bool noGear = true;

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


        //Elección de ambiente
    [SerializeField] private ambienteGeneral ambienteGeneral;

    //Letreros Sección
    [SerializeField] private GameObject  imageS1;
    [SerializeField] private GameObject  imageS2;
    [SerializeField] private GameObject  imageS3;
    [SerializeField] private GameObject  imageS4;

    public AudioSource cambioVelocidad;

    /*------------Audios y booleans seccion 1-------------*/

    public AudioSource bienvenido, introS1, s1_enciendeAuto, s1_incorporateAV, s1_circulaDer, s1_enciendeLuces,
                       s1_apagaLuces, s1_pasaTope, s1_pasoPeatones;
    private bool bintro1, bEncendido1, bIncorporacion1, bDerecha, bTunelin, bTunelOut,btope, btopeaudio ,bPeatonal, bPeatonalaudio,
                       apagalucesAudio, fueraTunel, AudioTunelIn, bIncoco;
    public GameObject s1_incorporacion, s1_tunelin, s1_salidaTunel, s1_salidaTunel2,s1_tope,s1_peatonal, s1_tunelin2,
                        s1_incoco;

    /*------------Audios y booleans seccion 2-------------*/
    public AudioSource introS2, s2_rotonda1, s2_circulaDer1, s2_vueltaPrecaucion, s2_rotonda2, s2_circulaDer2,
                       s2_curvaPrecaucion, s2_rotonda3, s2_manejaCurvas;
    private bool bintro2,rotondaAudio,derecha1,vueltaAudio,rotondaAudio2,derecha2, s2Vueltas1, s2Vueltas1Audio,
                       rotonda3Audio, curvas2,derecha1Audio,vueltaAudio2,rotondaAudio2YA,
                       derecha2Audio, rotonda3AudioYA, rotondaAudioYa, finalS2YA, curvas2Audio;
    public GameObject glorieta1,glorieta1OUT, s2Vuelta, glorieta2,glorieta2OUT, curvas1,
                       rotonda3, curvasFinales, finalS2;

    /*------------Audios y booleans seccion 3-------------*/
    public AudioSource introS3, s3_acelera70, s3_circulaDer, s3_subeAnillo, s3_vueltaAnillo, s3_bajaAnillo, s3_terminaCircuito, 
                          s3_sigueAu;
    public GameObject s3_spawn,s3_acelera, s3_subida, s3_vuelta ,s3_bajada, s3_final,s3_stop, s3_sigue, s3_sigue2;
    private bool bintro3,bAcelera, bAceleraAudio, bCirculaAudio, bSubida, bSubidaAudio, bVuelta, bVueltaAudio,
                        bBajada, bBajadaAudio, bFinal, bFinalAudio ,bStop, bSigue, bSigue2, bAudioSigue;

    /*------------Audios y booleans seccion 4-------------*/
    public AudioSource introS4, s4_paso1, s4_paso2, s4_paso3,s4_paso4, s4_paso5 , s4_paso6, s4_paso7;
    public GameObject   s4_spawn,paso1, pasito2, pasito3, pasito4, pasito5,pasito6, fins4;
    private bool bpaso1, bpaso2, bpaso3, bpaso4, bpaso5, bpaso6, fin;
    public List<GameObject> flechasViejas, flechasNuevas;


    /* Puntuaciones */
    private int s1Cali = 100, s2Cali = 100, s3Cali = 100, s4Cali = 100;
    public GameObject incorporacionS1, incorporacion2S1 , velTope, glo1,glo2,glo3,
                        incorporacionS3,incorporacionS4;
    private int choquesito = 0, pasorojo = 0;
    float time,time2, timeDef6;
    //S1 ints y demas
    private  int ExcesoVelS1=0 , ExcesoVelPeatonalS1=0, ExcesoVelTope=0 , lucesIncorrectas=0 , incorporacionesCalS1=0,S1_carrilesCount=0;
    public List<GameObject> S1_carriles;
    //S2 ints y demas
    private int glorietaCheker=0, ExcesoVelS2=0,S2_carrilesCount=0;
    public List<GameObject> S2_carriles;
    //S3 ints
    private int incorporacionesCalS3=0, noSetenta=0,S3_carrilesCount=0;
    private bool setenta;
    public List<GameObject> S3_carriles;
    //S4 ints
    private bool cien;
    private int inco4,S4_carrilesCount=0;
    public List<GameObject> S4_carriles;

    /* Faltas */
    public List<GameObject> fg1_goPrimerTope, fg1_goSegundoTope, fg2_goCuboSemaforo, fg3_goZonaPeatonal, fd5_goCuboAmarillo, fg8_goLineas;
    public GameObject fg5_goSafeZone;
    private float fg1_iVelocidadEntrada,time8;
    private int faltasGraves, fd4_iVelocidadActual, fd4_iVelocidadAnterior, faltasDeficientes, faltasLeves;
    private int grave1, grave2, grave3, grave4, grave5, grave6, grave8, def1,  def2, def3, def4, def5, def6, leve1, leve2, leve3;
    private bool[] fg1_bYaEsteTope1, fg1_bYaEsteTope2, fg2_bYaEsteCubo, fg3_bSeParo, fd5_bYaEsteAmarillo, fg8_bYaLinea, fd4_sancionesVelocidades;
    private bool fl2_bTope1, fl2_bTope2;

    public UnityEvent eventoRestart, eventoReturn;



    // Start is called before the first frame update
    void Start()
    {

        imageS1.SetActive(false);
        imageS2.SetActive(false);
        imageS3.SetActive(false);
        imageS4.SetActive(false);

       //Elección de ambiente
        // int opAmbiente;
        // opAmbiente = ambienteHandler.getAmbiente();
        // switch(opAmbiente){
        //     case 1:{
        //         ambienteGeneral.dia();
        //         break;
        //     }
        //     case 2:{
        //         ambienteGeneral.noche();
        //         break;
        //     }
        //     case 3:{
        //
        //     Debug.Log("slecc");
        //
        //         ambienteGeneral.lluvia();
        //         break;
        //     }
        //     case 4:{
        //         ambienteGeneral.niebla();
        //         break;
        //     }
        //     default:break;
        // }


        alerta.SetActive(false);
        faltaGrave.SetActive(false);
        faltaDeficiente.SetActive(false);
        faltaLeve.SetActive(false);

        Init();
        StartCoroutine(coroutine_s1);
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

        coroutine_s1 = seccion1();
        coroutine_s2 = seccion2();
        coroutine_s3 = seccion3();
        coroutine_s4 = seccion4();
        felicitacion = finalLeccion();

        fg1_bYaEsteTope1 = new bool [9];
        fg1_bYaEsteTope2 = new bool [9];
        fg2_bYaEsteCubo = new bool [15];
        fd5_bYaEsteAmarillo = new bool[fd5_goCuboAmarillo.Count];
        fg8_bYaLinea = new bool[fg8_goLineas.Count];
        fg3_bSeParo = new bool[8];
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
        checkVelocidad();
        leccion();
        //turnOn();
        checkButtons();
        faltas();
        checkFaltas();
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
    private void inmovilizarCarro(){
      isOver = true;
      audios = GameObject.Find("Leccion4").GetComponentsInChildren<AudioSource>();
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
                    Debug.Log("Deficiente 5");
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
        for(int i = 0; i < fg2_goCuboSemaforo.Count; i++){
            if(Physics.CheckBox(fg2_goCuboSemaforo[i].transform.position, fg2_goCuboSemaforo[i].transform.localScale / 2,
            fg2_goCuboSemaforo[i].transform.rotation, layerMask) && !fg2_bYaEsteCubo[i]){
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
            fg3_goZonaPeatonal[i].transform.rotation, layerMask) && !fg3_bSeParo[i] && fg2_goCuboSemaforo[i].GetComponent<BoxCollider>().enabled){
                if(rb.velocity.magnitude < 0.1 &&
                !Physics.Raycast(fg5_goSafeZone.transform.position, fg5_goSafeZone.transform.TransformDirection(Vector3.forward), out hit1, 6)){
                    faltasGraves++;
                    grave3++;
                    StartCoroutine(delayFalta(3, "Detenerse en paso peatonal"));
                    fg3_bSeParo[i] = true;
                    Debug.Log("Grave 3");
                }
            }
        }
        //Grave 4
        
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
          if (bEncender) {
          
              if(Physics.CheckBox(s1_incoco.transform.position, s1_incoco.transform.localScale / 2,
              s1_incoco.transform.rotation , layerMask)){
                bIncoco = true;
              }

              if(Physics.CheckBox(s1_incorporacion.transform.position, s1_incorporacion.transform.localScale / 2,
              s1_incorporacion.transform.rotation , layerMask)){
                bIncorporacion1 = true;
              }
              if(Physics.CheckBox(s1_tunelin.transform.position, s1_tunelin.transform.localScale / 2,
              s1_tunelin.transform.rotation ,layerMask) ){

                bTunelin = true;
              }
              if(Physics.CheckBox(s1_tunelin2.transform.position, s1_tunelin2.transform.localScale / 2,
              s1_tunelin2.transform.rotation ,layerMask) ){
                AudioTunelIn = true;
              }
              if(Physics.CheckBox(s1_salidaTunel.transform.position, s1_salidaTunel.transform.localScale / 2,
                s1_salidaTunel.transform.rotation , layerMask)){
                bTunelOut = true;
              }
              if(Physics.CheckBox(s1_salidaTunel2.transform.position, s1_salidaTunel2.transform.localScale / 2,
                s1_salidaTunel2.transform.rotation , layerMask) ){
                fueraTunel = true;
              }
              if(Physics.CheckBox(s1_tope.transform.position, s1_tope.transform.localScale / 2,
                s1_tope.transform.rotation, layerMask)){
                btope = true;
              }
              if(Physics.CheckBox(s1_peatonal.transform.position, s1_peatonal.transform.localScale / 2,
                s1_peatonal.transform.rotation, layerMask)){
                bPeatonal = true;
                if((time+3 <= Time.time) && ((rb.velocity.magnitude * 7) > 20)){
                  ExcesoVelPeatonalS1++;
                  time = Time.time;
                }
              }
              if(Physics.CheckBox(incorporacionS1.transform.position, incorporacionS1.transform.localScale / 2,
                incorporacionS1.transform.rotation , layerMask)){
                  if(Physics.CheckBox(incorporacion2S1.transform.position, incorporacion2S1.transform.localScale / 2,
                    incorporacion2S1.transform.rotation) && (time+3 <= Time.time)){
                    incorporacionesCalS1++;
                    time = Time.time;
                  }
              }
              if(bTunelin && !bTunelOut && (time+3 <= Time.time)){
                if(!lm.encendido){
                  lucesIncorrectas++;
                  time = Time.time;
                }
              }
              if (lm.encendido && (time+3 <= Time.time) && fueraTunel) {
                lucesIncorrectas++;
                time = Time.time;
              }
              if(Physics.CheckBox(velTope.transform.position, velTope.transform.localScale / 2,
                velTope.transform.rotation , layerMask)){
                if((time+3 <= Time.time) && ((rb.velocity.magnitude * 3.6) > 20)){
                  ExcesoVelTope++;
                  time = Time.time;
                }
              }
              if ((time2+3 <= Time.time) && ((rb.velocity.magnitude * 3.6) > 50)) {
                ExcesoVelS1++;
                time2 = Time.time;
              }
          }
          foreach (var item in S1_carriles)
          {
              if(Physics.CheckBox(item.transform.position, item.transform.localScale / 2,
              item.transform.rotation , layerMask) && (time+3 <= Time.time)){
                  time = Time.time;
                  Debug.Log("Saliste del carril");
                  S1_carrilesCount++;
              }
          }
        }else if(!s2Finished){
          if(!coroutine2){
            coroutine2=true;
            bienHecho.Play();
            StartCoroutine(coroutine_s2);
          }
          if(Physics.CheckBox(glorieta1.transform.position, glorieta1.transform.localScale / 2,
            glorieta1.transform.rotation, layerMask)){
            rotondaAudio = true;
          }
          if(Physics.CheckBox(glorieta1OUT.transform.position, glorieta1OUT.transform.localScale / 2,
            glorieta1OUT.transform.rotation, layerMask)){
            derecha1 = true;
          }
          if(Physics.CheckBox(s2Vuelta.transform.position, s2Vuelta.transform.localScale / 2,
            s2Vuelta.transform.rotation , layerMask)){
            vueltaAudio = true;
          }
          if(Physics.CheckBox(glorieta2.transform.position, glorieta2.transform.localScale / 2,
            glorieta2.transform.rotation, layerMask)){
            rotondaAudio2 = true;
          }
          if(Physics.CheckBox(glorieta2OUT.transform.position, glorieta2OUT.transform.localScale / 2,
            glorieta2OUT.transform.rotation, layerMask)){
            derecha2 = true;
          }
          if(Physics.CheckBox(curvas1.transform.position, curvas1.transform.localScale / 2,
            curvas1.transform.rotation, layerMask)){
            s2Vueltas1Audio = true;
          }
          if(Physics.CheckBox(rotonda3.transform.position, rotonda3.transform.localScale / 2,
            rotonda3.transform.rotation, layerMask)){
            rotonda3Audio = true;
          }
          if(Physics.CheckBox(curvasFinales.transform.position, curvasFinales.transform.localScale / 2,
            curvasFinales.transform.rotation, layerMask)){
            curvas2 = true;
          }
          if(Physics.CheckBox(finalS2.transform.position, finalS2.transform.localScale / 2,
            finalS2.transform.rotation, layerMask)){
            finalS2YA = true;
          }
          if(Physics.CheckBox(glo1.transform.position, finalS2.transform.localScale / 2,
            finalS2.transform.rotation, layerMask) && (time+3 <= Time.time)){
              time = Time.time;
              glorietaCheker++;
          }
          if(Physics.CheckBox(glo2.transform.position, finalS2.transform.localScale / 2,
            finalS2.transform.rotation, layerMask) && (time+3 <= Time.time)){
              time = Time.time;
              glorietaCheker++;
          }
          if(Physics.CheckBox(glo3.transform.position, finalS2.transform.localScale / 2,
            finalS2.transform.rotation, layerMask) && (time+3 <= Time.time) ){
              time = Time.time;
              glorietaCheker++;
          }
          if ((time2+3 <= Time.time) && ((rb.velocity.magnitude * 7) > 50)) {
            ExcesoVelS2++;
            time2 = Time.time;
          }
          foreach (var item in S2_carriles)
          {
              if(Physics.CheckBox(item.transform.position, item.transform.localScale / 2,
              item.transform.rotation , layerMask) && (time+3 <= Time.time)){
                  time = Time.time;
                  Debug.Log("Saliste del carril");
                  S2_carrilesCount++;
              }
          }

        }else if(!s3Finished){
          if(!coroutine3){
            coroutine3=true;
            bienHecho.Play();
            StartCoroutine(coroutine_s3);
          }
          if(Physics.CheckBox(s3_acelera.transform.position, s3_acelera.transform.localScale / 2,
            s3_acelera.transform.rotation, layerMask)){
            bAcelera = true;
            if(Physics.CheckBox(incorporacionS3.transform.position, incorporacionS3.transform.localScale / 2,
              incorporacionS3.transform.rotation) && (time+3 <= Time.time)){
              incorporacionesCalS3++;
              time = Time.time;
            }
          }
          if(Physics.CheckBox(s3_subida.transform.position, s3_subida.transform.localScale / 2,
            s3_subida.transform.rotation, layerMask)){
            bSubida = true;
          }
          if(Physics.CheckBox(s3_vuelta.transform.position, s3_vuelta.transform.localScale / 2,
            s3_vuelta.transform.rotation, layerMask)){
            bVuelta = true;
          }
          if(Physics.CheckBox(s3_bajada.transform.position, s3_bajada.transform.localScale / 2,
            s3_bajada.transform.rotation, layerMask)){
            bBajada = true;
          }
          if(Physics.CheckBox(s3_final.transform.position, s3_final.transform.localScale / 2,
            s3_final.transform.rotation, layerMask)){
            bFinal = true;
          }
          if(Physics.CheckBox(s3_stop.transform.position, s3_stop.transform.localScale / 2,
            s3_stop.transform.rotation, layerMask)){
            bStop = true;
          }
          if (!bSubida && ((rb.velocity.magnitude * 7) >= 70)) {
            setenta=true;
          }
          if(Physics.CheckBox(s3_sigue.transform.position, s3_sigue.transform.localScale / 2,
            s3_sigue.transform.rotation, layerMask)){
            bSigue = true;
          }
          if(Physics.CheckBox(s3_sigue2.transform.position, s3_sigue2.transform.localScale / 2,
            s3_sigue2.transform.rotation, layerMask)){
            bSigue2 = true;
          }
          foreach (var item in S3_carriles)
          {
              if(Physics.CheckBox(item.transform.position, item.transform.localScale / 2,
              item.transform.rotation , layerMask) && (time+3 <= Time.time)){
                  time = Time.time;
                  Debug.Log("Saliste del carril");
                  S3_carrilesCount++;
              }
          }




        }else if(!s4Finished){
          if(!coroutine4){
            coroutine4=true;
            bienHecho.Play();
            StartCoroutine(coroutine_s4);
            foreach (var item in flechasViejas)
            {
                item.SetActive(false);
            }
            foreach (var item in flechasNuevas)
            {
                item.SetActive(true);
            }
          }
          if(Physics.CheckBox(paso1.transform.position, paso1.transform.localScale / 2,
            paso1.transform.rotation, layerMask) && (time+3 <= Time.time)){
            bpaso1 = true;
            if(Physics.CheckBox(incorporacionS4.transform.position, incorporacionS4.transform.localScale / 2,
              incorporacionS4.transform.rotation)){
              inco4++;
            }
            time=Time.time;
          }
          if(Physics.CheckBox(pasito2.transform.position, pasito2.transform.localScale / 2,
            pasito2.transform.rotation, layerMask)){
            bpaso2 = true;
          }
          if(Physics.CheckBox(pasito3.transform.position, pasito3.transform.localScale / 2,
            pasito3.transform.rotation, layerMask)){
            bpaso3 = true;
          }
          if(Physics.CheckBox(pasito4.transform.position, pasito4.transform.localScale / 2,
            pasito4.transform.rotation, layerMask)){
            bpaso4 = true;
          }
          if(Physics.CheckBox(pasito5.transform.position, pasito5.transform.localScale / 2,
            pasito5.transform.rotation, layerMask)){
            bpaso5 = true;
          }
          if(Physics.CheckBox(pasito6.transform.position, pasito6.transform.localScale / 2,
            pasito6.transform.rotation, layerMask)){
            bpaso6 = true;
          }
          if(Physics.CheckBox(fins4.transform.position, fins4.transform.localScale / 2,
            fins4.transform.rotation, layerMask)){
            fin = true;
          }
          if (bpaso1 && !fin && ((rb.velocity.magnitude * 7) >= 90)) {
            cien=true;
          }
          foreach (var item in S4_carriles)
          {
              if(Physics.CheckBox(item.transform.position, item.transform.localScale / 2,
              item.transform.rotation , layerMask) && (time+3 <= Time.time)){
                  time = Time.time;
                  Debug.Log("Saliste del carril");
                  S4_carrilesCount++;
              }
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
                if(Math.Abs(rb.velocity.magnitude * 7) > (velocidadMaxima )){
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
            if(other.gameObject.tag != "Altos"){
              if(other.relativeVelocity.magnitude > 4){
                  foreach (AudioSource a in audios) {
                    a.Pause();
                  }
                  choque = true;
                  breakingGlass.Play();
                  start.DestroyMesh();
                  bEncender = false;
                  inmovilizarCarro();
                  //Módulo de Alertas
                   alerta.SetActive(true);
                   textoMotivo.text = "Choque";
              }
            }
            if(other.gameObject.tag == "Altos" && other.relativeVelocity.magnitude > 4) {
              foreach (AudioSource a in audios) {
                a.Pause();
              }
              rojo.Play();
              pasorojo++;
            }
            Debug.Log("Collision Detected");
      }
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

    IEnumerator delayEncendidoCarro(){
        turnOnSound.Play();
        yield return new WaitForSeconds(1.0f);
        engineSound.Play();
    }

    IEnumerator delayFalta(int op, string faltaTexto){
      switch (op){
        case 1: {
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

    IEnumerator seccion1(){
      imageS1.SetActive(true);
      yield return new WaitForSeconds(2.0f);
      imageS1.SetActive(false);
      while (true) {

        if(!bienvenido.isPlaying && !choque && !isOver) {
          if(!bEncender && !bintro1){
              bintro1 = true;
              introS1.Play();
              yield return new WaitForSeconds(4.0f);
          }else if (bintro1 && !bEncender) {
             s1_enciendeAuto.Play();
             yield return new WaitForSeconds(6.0f);
          }else if(bEncender && !bIncorporacion1 && bIncoco){
              s1_incorporateAV.Play();
              yield return new WaitForSeconds(10.0f);
          }else if (bIncorporacion1 && !bDerecha) {
              s1_circulaDer.Play();
              bDerecha = true;
              yield return new WaitForSeconds(8.0f);
          }else if (AudioTunelIn && !bTunelin) {
              s1_enciendeLuces.Play();
              yield return new WaitForSeconds(12.0f);
          }else if (bTunelOut && !apagalucesAudio) {
              s1_apagaLuces.Play();
              apagalucesAudio=true;
              yield return new WaitForSeconds(6.0f);
          }else if (btope && !btopeaudio) {
              btopeaudio=true;
              s1_pasaTope.Play();
              yield return new WaitForSeconds(5.0f);
          }else if (bPeatonal && !bPeatonalaudio) {
              bPeatonalaudio=true;
              s1_pasoPeatones.Play();
              yield return new WaitForSeconds(10.0f);
          }else if (bPeatonalaudio) {
              s1Finished=true;

              yield return new WaitForSeconds(3.0f);
              StopCoroutine(coroutine_s1);


          }else{
              yield return new WaitForSeconds(0.01f);
          }

      }else{
          yield return new WaitForSeconds(0.01f);
      }
      }
    }

    IEnumerator seccion2(){
      imageS2.SetActive(true);
      yield return new WaitForSeconds(2.0f);
      imageS2.SetActive(false);
      while (true) {
        if (!introS2.isPlaying && !choque) {
            if (!bintro2) {
              bintro2=true;
              yield return new WaitForSeconds(5.0f);
              introS2.Play();
              yield return new WaitForSeconds(8.0f);
            }else if (!rotondaAudioYa && rotondaAudio) {
              rotondaAudioYa= true;
              s2_rotonda1.Play();
              yield return new WaitForSeconds(4.0f);
            }else if (derecha1 && !derecha1Audio) {
              derecha1Audio=true;
              s2_circulaDer1.Play();
              yield return new WaitForSeconds(4.0f);
            }else if (!vueltaAudio2 && vueltaAudio) {
              vueltaAudio2=true;
              s2_vueltaPrecaucion.Play();
              yield return new WaitForSeconds(4.0f);
            }else if (rotondaAudio2 && !rotondaAudio2YA) {
              rotondaAudio2YA=true;
              s2_rotonda2.Play();
              yield return new WaitForSeconds(4.0f);
            }else if (!derecha2Audio && derecha2) {
              derecha2Audio=true;
              s2Vueltas1 = true;
              s2_circulaDer2.Play();
              yield return new WaitForSeconds(4.0f);
            }else if (s2Vueltas1 && !s2Vueltas1Audio) {
              s2_curvaPrecaucion.Play();
              yield return new WaitForSeconds(6.0f);
            }else if (!rotonda3AudioYA && rotonda3Audio) {
              rotonda3AudioYA=true;
              s2_rotonda3.Play();
              yield return new WaitForSeconds(4.0f);
            }else if (curvas2 && !curvas2Audio) {
              curvas2Audio=true;
              s2_manejaCurvas.Play();
              yield return new WaitForSeconds(8.0f);
            }else if (finalS2YA) {
              s2Finished=true;
              StopCoroutine(coroutine_s2);
              yield return new WaitForSeconds(2.0f);
            }else{
              yield return new WaitForSeconds(0.01f);
            }
        }else{
          yield return new WaitForSeconds(0.01f);
        }
      }
    }

    IEnumerator seccion3(){
      imageS3.SetActive(true);
      yield return new WaitForSeconds(2.0f);
      imageS3.SetActive(false);
      yield return new WaitForSeconds(5.0f);
      rb.MovePosition( s3_spawn.transform.position);
      rb.MoveRotation( s3_spawn.transform.rotation);
      bEncender=false;
      engineSound.Stop();
      while (true) {
        if (!introS3.isPlaying && !choque) {
            if (!bintro3) {
              bintro3=true;
              yield return new WaitForSeconds(5.0f);
              introS3.Play();
              yield return new WaitForSeconds(8.0f);
            }else if (bAcelera && !bAceleraAudio) {
              s3_acelera70.Play();
              bAceleraAudio=true;
              yield return new WaitForSeconds(6.0f);
            }else if (!bCirculaAudio && bAceleraAudio) {
              s3_circulaDer.Play();
              bCirculaAudio=true;
              yield return new WaitForSeconds(3.0f);
            }else if (bSubida && !bSubidaAudio) {
              s3_subeAnillo.Play();
              bSubidaAudio=true;
              yield return new WaitForSeconds(3.0f);
            }else if (bVuelta && !bVueltaAudio) {
              s3_vueltaAnillo.Play();
              bVueltaAudio=true;
              yield return new WaitForSeconds(3.0f);
            }else if (bBajada && !bBajadaAudio) {
              s3_bajaAnillo.Play();
              bBajadaAudio=true;
              yield return new WaitForSeconds(3.0f);
            }else if (bFinal && !bFinalAudio) {
              s3_terminaCircuito.Play();
              bFinalAudio=true;
              yield return new WaitForSeconds(3.0f);
            }else if(bSigue && !bAudioSigue){
               s3_sigueAu.Play();
               bAudioSigue = true;
               yield return new WaitForSeconds(3.0f);
            }
            else if(bSigue2 && bAudioSigue){
               s3_sigueAu.Play();
               bSigue=false;
               bAudioSigue = false;
               yield return new WaitForSeconds(3.0f);
            }
            else if (bStop) {
              bienHecho.Play();
              s3Finished = true;
              StopCoroutine(coroutine_s3);
              yield return new WaitForSeconds(3.0f);
            }else{
              yield return new WaitForSeconds(0.01f);
            }
          }else{
            yield return new WaitForSeconds(0.01f);
          }
      }
    }


    IEnumerator seccion4(){
      imageS4.SetActive(true);
      yield return new WaitForSeconds(2.0f);
      imageS4.SetActive(false);
          yield return new WaitForSeconds(5.0f);
          rb.MovePosition( s4_spawn.transform.position);
          rb.MoveRotation( s4_spawn.transform.rotation);
          bEncender=false;
          engineSound.Stop();
          introS4.Play();
          yield return new WaitForSeconds(7.0f);
          while(!bEncender){
            yield return new WaitForSeconds(0.01f);
          }
          while(!bpaso1){
            s4_paso1.Play();
            yield return new WaitForSeconds(9.0f);
          }
          if (!choque) {
            s4_paso2.Play();
            yield return new WaitForSeconds(9.0f);
          }
          while(!bpaso2){
            yield return new WaitForSeconds(0.01f);
          }
          s4_paso3.Play();
          yield return new WaitForSeconds(3.0f);
          while(!bpaso3){
            yield return new WaitForSeconds(0.01f);
          }
          s4_paso4.Play();
          yield return new WaitForSeconds(3.0f);
          while(!bpaso4){
            yield return new WaitForSeconds(0.01f);
          }
          s4_paso5.Play();
          yield return new WaitForSeconds(3.0f);
          while(!bpaso5){
            yield return new WaitForSeconds(0.01f);
          }
          s4_paso6.Play();
          yield return new WaitForSeconds(3.0f);
          while(!bpaso6){
            yield return new WaitForSeconds(0.01f);
          }
          s4_paso7.Play();
          yield return new WaitForSeconds(3.0f);
          while(!fin){
            yield return new WaitForSeconds(0.01f);
          }
          bEncender=false;
          engineSound.Stop();
          s4Finished = true;
          StartCoroutine(felicitacion);

    }

    IEnumerator finalLeccion(){
        StopCoroutine(coroutine_s4);
        bienHecho.Play();
        yield return new WaitForSeconds(2.0f);
        felicidades.Play();
        darCalificaciones();
    }

    private void darCalificaciones()
    {
      ExamNumber.examNumber = 4;
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

      if(ExcesoVelS1 > 0){
        if(ExcesoVelS1 < 8){
            ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 4,
                seccion = 1,
                veces = ExcesoVelS1,
                texto = "Fuera del rango de velocidad",
                puntos = 5
            });
        }else {
          ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 4,
                seccion = 1,
                veces = 8,
                texto = "Fuera del rango de velocidad",
                puntos = 5
            });
        }
      }


      
      if(incorporacionesCalS1 > 0){
        ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 4,
                seccion = 1,
                veces = 1,
                texto = "Incorporación sin distancia correcta",
                puntos = 30
            });
      }
      if(S1_carrilesCount > 0){
      if (S1_carrilesCount < 6) {
        ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 4,
                seccion = 1,
                veces = S1_carrilesCount,
                texto = "Salir del carril correspondiente",
                puntos = 5
            });
      }else {
        ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 4,
                seccion = 1,
                veces = 6,
                texto = "Salir del carril correspondiente",
                puntos = 5
            });
      }
      }



      if(ExcesoVelS2 > 0){
        if(ExcesoVelS2 < 8){
            ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 4,
                seccion = 2,
                veces = ExcesoVelS2,
                texto = "Fuera del rango de velocidad",
                puntos = 5
            });
        }else {
          ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 4,
                seccion = 2,
                veces = 8,
                texto = "Fuera del rango de velocidad",
                puntos = 5
            });
        }
      }

      //Glorieta evaluacion
      if(glorietaCheker > 0){
      if(glorietaCheker < 5){
          ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 4,
                seccion = 2,
                veces = glorietaCheker,
                texto = "Salir del carril correspondiente en glorieta",
                puntos = 3
            });
      }else {
        ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 4,
                seccion = 2,
                veces = 5,
                texto = "Salir del carril correspondiente en glorieta",
                puntos = 3
            });
      }
      }

      if(S2_carrilesCount > 0){
        if (S2_carrilesCount < 6) {
          ExamNumber.criteriosAux.Add(new Criterio(){
            leccion = 4,
            seccion = 2,
            veces = S2_carrilesCount,
            texto = "Salir del carril correspondiente",
            puntos = 5
          });
        }else {
          ExamNumber.criteriosAux.Add(new Criterio(){
            leccion = 4,
            seccion = 2,
            veces = 6,
            texto = "Salir del carril correspondiente",
            puntos = 5
          });
        }
      }
      
      if(incorporacionesCalS3 > 0){
        ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 4,
                seccion = 3,
                veces = 1,
                texto = "Incorporación sin distancia correcta",
                puntos = 30
            });
      }
      
      if(!setenta){
        ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 4,
                seccion = 3,
                veces = 1,
                texto = "No llegar a 70km/hr al incorporarse a la carretera",
                puntos = 10
            });
      }

      if(S3_carrilesCount > 0){
        if (S3_carrilesCount < 6) {
          ExamNumber.criteriosAux.Add(new Criterio(){
            leccion = 4,
            seccion = 3,
            veces = S3_carrilesCount,
            texto = "Salir del carril correspondiente",
            puntos = 5
          });
        }else {
          ExamNumber.criteriosAux.Add(new Criterio(){
            leccion = 4,
            seccion = 3,
            veces = 6,
            texto = "Salir del carril correspondiente",
            puntos = 5
          });
        }
      }


      if(!cien){
        ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 4,
                seccion = 4,
                veces = 1,
                texto = "No llegar a 100km/hr al incorporarse a la carretera",
                puntos = 10
            });
      }
      

      if(inco4 > 0){
        ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 4,
                seccion = 4,
                veces = 1,
                texto = "Incorporación sin distancia correcta",
                puntos = 30
            });
      }
      
      if(S4_carrilesCount > 0){
        if (S4_carrilesCount < 6) {
          ExamNumber.criteriosAux.Add(new Criterio(){
            leccion = 4,
            seccion = 4,
            veces = S4_carrilesCount,
            texto = "Salir del carril correspondiente",
            puntos = 5
          });
        }else {
          ExamNumber.criteriosAux.Add(new Criterio(){
            leccion = 4,
            seccion = 4,
            veces = 6,
            texto = "Salir del carril correspondiente",
            puntos = 5
          });
        }
      }
      












    





      if(ExcesoVelS1 < 8){
          s1Cali -= ExcesoVelS1 * 5;
      }else {
        s1Cali -= 40;
      }
      //Exceso velocidad tope
    /*  if(ExcesoVelTope < 5){
          s1Cali -= ExcesoVelTope * 1;
      }else {
        s1Cali -= 5;
      }
      //Exceso vel peatonal
      if(ExcesoVelPeatonalS1 < 5){
          s1Cali -= ExcesoVelPeatonalS1 * 5;
      }else {
        s1Cali -= 25;
      }
      //luces en tunel
      if(lucesIncorrectas < 5){
          s1Cali -= lucesIncorrectas * 1;
      }else {
        s1Cali -= 5;
      }*/
      if(incorporacionesCalS1 < 5){
          s1Cali -= incorporacionesCalS1 * 30;
      }else {
        s1Cali -= 30;
      }
      //Glorieta evaluacion
      if(glorietaCheker < 5){
          s2Cali -= glorietaCheker * 3;
      }else {
        s2Cali -= 15;
      }
      if(ExcesoVelS2 < 8){
          s2Cali -= ExcesoVelS2 * 5;
      }else {
        s2Cali -= 40;
      }
      if(incorporacionesCalS3 < 5){
          s3Cali -= incorporacionesCalS3 * 30;
      }else {
        s3Cali -= 30;
      }
      /*
      //Evaluacion de que llegue a cierta velocidad
      if(!setenta){
        s3Cali -= 10;
      }
      if(!cien){
        s4Cali -= 10;
      }
      */
      if (inco4 < 5) {
        s4Cali -= inco4*30;
      }else {
        s4Cali -= 30;
      }
      if (S1_carrilesCount < 6) {
        s1Cali -= S1_carrilesCount*5;
      }else {
        s1Cali -= 30;
      }
      if (S2_carrilesCount < 6) {
        s2Cali -= S2_carrilesCount*5;
      }else {
        s2Cali -= 30;
      }
      //s4Cali -= choquesito*3;
      int calificacionLeccion = ((s1Cali + s2Cali + s3Cali + s4Cali) / 4);
      Debug.Log("La calificacion fue: " + calificacionLeccion);
      int calificacionFinal = calificacionLeccion - (faltasGraves * 5) - (faltasDeficientes * 3) - (faltasLeves);
      Debug.Log("La calificacion FINAL fue: " + calificacionFinal);

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
