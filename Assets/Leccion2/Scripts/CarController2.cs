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
public class CarController2 : MonoBehaviour
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
    public AudioSource engineSound, turnOnSound, bienHecho, felicidades;

    private IEnumerator coroutine_s1, coroutine_s2, coroutine_s3Conos, coroutine_s3Carros,
                         coroutine_s4, felicitacion;
    private bool s1Finished = false, s2Finished = false, s3Finished = false, s4Finished = false;

    //Elección de ambiente
    [SerializeField] private ambienteGeneral ambienteGeneral;

    
    //Alerta Choque
    [SerializeField] private GameObject  alerta;
    [SerializeField] private Text  textoMotivo;
    private AudioSource[] audios;
    private bool isOver;
    //Letreros Sección
    [SerializeField] private GameObject  imageS1;
    [SerializeField] private GameObject  imageS2;
    [SerializeField] private GameObject  imageS3;
    [SerializeField] private GameObject  imageS4;
    public AudioSource cambioVelocidad;

    /*------------Audios y booleans seccion 1-------------*/

    public AudioSource bienvenido, introS1, s1_primera, s1_segunda, s1_tercera, s1_avisoBajarVelocidad,
                       s1_segundaAgain, s1_primeraAgain, s1_freno;
    private bool bV1, bV2, bV3, bV2aux, bV1aux, bEncender;

    /*------------Audios y booleans seccion 2-------------*/
    public AudioSource introS2, s2_ingresa, s2_tresVueltas, s2_sal;

    private float velocidadMaxima = 80;
    public GameObject s2_ingresoGlorieta, s2_salirGlorieta, s2_spawn;
    private bool s2_bIngreso, s2_bTerceraVuelta, s2_estaDentro;
    private int s2_contadorVueltas = 0;

    /*------------Audios y booleans seccion 3-------------*/
    public AudioSource introS3, s3_conos, s3_masCuidado, s3_introEstacionamiento, s3_bateria, s3_espiga1, s3_espiga2,
                        s3_enLinea, s3_enLineaPaso1, s3_enLineaPaso2, s3_enLineaPaso3,
                         s3_enLineaPaso4, s3_enLineaPaso5, s3_enLineaPaso6, s3_enLineaPaso7;
    public GameObject s3_spawn;
    public GameObject s3_guiaConos, s3_guiaBateria, s3_guiaEspiga, s3_guiaEnLinea, s3_carros, s3_lineaConos,
                        s3_objetoCono1, s3_objetoCono2, s3_objetoCono3, s3_objetoCono4, s3_objetoCono5,
                        s3_objetoBateria, s3_objetoEspiga, s3_objetoEnLinea,
                        s3_objetoEnLineaPaso1, s3_objetoEnLineaPaso3, s3_objetoEnLineaPaso4, s3_objetoEnLineaPaso6;
    private bool s3_conosYa, s3_bateriaYa, s3_espigaYa, s3_enLineaYa, s3_carrosYa;
    private bool s3_enLineaPaso1Ya, s3_enLineaPaso2Ya, s3_enLineaPaso3Ya, s3_enLineaPaso4Ya,
                s3_enLineaPaso5Ya, s3_enLineaPaso6Ya, s3_enLineaPaso7Ya, s3_enLineaPaso4TocoObjeto, s3_enLineaPaso6TocoObjeto;
    private bool [] conosBoolean = new bool [5];

    /*------------Audios y booleans seccion 4-------------*/
    public AudioSource introS4;
    public GameObject  s4_vallas, s4_spawn, s4_finDelCamino, s4_guiaVallas;

    /* Puntuaciones */
    public List<GameObject> s1_salioCarrilObject, s2_salioCarrilObject;
    private int s1Cali = 100, s2Cali = 100, s3Cali = 100, s4Cali = 100;
    private int s1_salioCarril = 0, s2_fueraRangoVelocidad = - 2, s2_salioCarril, s3_fueraRangoVelocidad = -2, s3_tocoCono, s4_tocoValla;
    float time, time2;

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
        //         ambienteGeneral.lluvia();
        //         break;
        //     }
        //     case 4:{
        //         ambienteGeneral.niebla();
        //         break;
        //     }
        //     default:break;
        // }
        // Debug.Log(opAmbiente);

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
        bV1 = false;
        bV2 = false;
        bV3 = false;
        bV1aux = false;
        bV2aux = false;

        s3_guiaConos.SetActive(false);
        s3_guiaBateria.SetActive(false);
        s3_guiaEnLinea.SetActive(false);
        s3_guiaEspiga.SetActive(false);
        s3_carros.SetActive(false);
        s4_vallas.SetActive(false);
        s4_guiaVallas.SetActive(false);

        coroutine_s1 = seccion1();
        coroutine_s2 = seccion2();
        coroutine_s3Conos = seccion3Conos();
        coroutine_s3Carros = seccion3Carros();
        coroutine_s4 = seccion4();
        felicitacion = finalLeccion();
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
            tl.GetComponent<Renderer>().material.SetColor("_EmissionColor", im.brake ? new Color(0.5f, 0.111f, 0.111f) : Color.black);
        }


        /* if(im.turnedOn){
            turnOn();
            Thread.Sleep(100);
        } */

        leccion();
        checkButtons();
        //turnOn();


    }

    private void inmovilizarCarro(){
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

    private void leccion(){
        if(!s1Finished){
            if(bEncender){
                if(im.clutch){
                    if(im.v1 && !bV2){
                        cambioVelocidad.Play();
                        bV1 = true;
                        rb.useGravity = true;
                        velocidadMaxima = 20;
                    }else if(im.v2 && bV1 && !bV3){
                        cambioVelocidad.Play();
                        bV2 = true;
                        velocidadMaxima = 40;
                    }else if(im.v3 && bV2){
                        cambioVelocidad.Play();
                        bV3 = true;
                        velocidadMaxima = 60;
                    }else if(im.v2 && bV3){
                        cambioVelocidad.Play();
                        bV2aux = true;
                        velocidadMaxima = 40;
                    }else if(im.v1 && bV2aux){
                        cambioVelocidad.Play();
                        bV1aux = true;
                        velocidadMaxima = 20;
                    }
                    Thread.Sleep(150);
                }
            }else if(bV1aux && !s1_freno.isPlaying){ //&& audio frenar.isPlaying == false
                StopCoroutine(coroutine_s1);
                s1Finished = true;

                rb.mass = 99999999999f;
                rb.drag = 99999999999f;
                rb.angularDrag = 99999999999f;
                StartCoroutine(coroutine_s2);
            }
            //Evaluación
            foreach (var item in s1_salioCarrilObject)
            {
                if(Physics.CheckBox(item.transform.position, item.transform.localScale / 2,
                item.transform.rotation) && (time+3 <= Time.time)){
                    time = Time.time;
                    Debug.Log("Estoy tocando");
                    s1_salioCarril++;
                }
            }
        }else if(!s2Finished){
            if(Physics.CheckBox(s2_ingresoGlorieta.transform.position, s2_ingresoGlorieta.transform.localScale / 2) && !s2_bIngreso && !s2_estaDentro){
                s2_estaDentro = true;
                s2_tresVueltas.Play();
                s2_bIngreso = true;
                s2_contadorVueltas++;
            }else if(s2_bIngreso && Physics.CheckBox(s2_ingresoGlorieta.transform.position, s2_ingresoGlorieta.transform.localScale / 2) && !s2_bTerceraVuelta && !s2_estaDentro){
                s2_estaDentro = true;
                s2_contadorVueltas++;
                if(s2_contadorVueltas == 4){
                    s2_sal.Play();
                    s2_bTerceraVuelta = true;
                } 
            }else if(s2_bTerceraVuelta && Physics.CheckBox(s2_salirGlorieta.transform.position, s2_salirGlorieta.transform.localScale / 2)){
                StopCoroutine(coroutine_s2);
                s2Finished = true;

                rb.mass = 99999999999f;
                rb.drag = 99999999999f;
                rb.angularDrag = 99999999999f;
                bEncender = false;
                
                StartCoroutine(coroutine_s3Conos);
            }else{
                if(!Physics.CheckBox(s2_ingresoGlorieta.transform.position, s2_ingresoGlorieta.transform.localScale / 2))
                    s2_estaDentro = false;
            }
            //Evaluación
            foreach (var item in s2_salioCarrilObject)
            {
                if(Physics.CheckBox(item.transform.position, item.transform.localScale / 2,
                item.transform.rotation) && (time+3 <= Time.time)){
                    time = Time.time;
                    Debug.Log("Estoy tocando");
                    s2_salioCarril++;
                }
            }
            if(Math.Abs(transform.InverseTransformVector(rb.velocity).z * 3.6) < 5 && (time2+4 <= Time.time) && !s2_ingresa.isPlaying && !introS2.isPlaying){
                time2 = Time.time;
                s2_fueraRangoVelocidad++;
                Debug.Log("Fuera del rango de velocidad: " + s2_fueraRangoVelocidad + " veces");
            }
        }else if(!s3Finished){
            if(!s3_conosYa){
                s3_guiaConos.SetActive(true);
                /* if(!conosBoolean[0] && Physics.CheckBox(s3_objetoCono1.transform.position, s3_objetoCono1.transform.localScale / 2)){
                    conosBoolean[0] = true;
                }else if(conosBoolean[0] && Physics.CheckBox(s3_objetoCono2.transform.position, s3_objetoCono2.transform.localScale / 2)){
                    conosBoolean[1] = true;
                }else if(conosBoolean[1] && Physics.CheckBox(s3_objetoCono3.transform.position, s3_objetoCono3.transform.localScale / 2)){
                    conosBoolean[2] = true;
                }else if(conosBoolean[2] && Physics.CheckBox(s3_objetoCono4.transform.position, s3_objetoCono4.transform.localScale / 2)){
                    conosBoolean[3] = true;
                }else if (conosBoolean[3] && Physics.CheckBox(s3_objetoCono5.transform.position, s3_objetoCono5.transform.localScale / 2)){
                    s3_conosYa = true;
                    s3_lineaConos.SetActive(false);
                    s3_carros.SetActive(true);
                    s3_guiaConos.SetActive(false);
                    StopCoroutine(coroutine_s3Conos);
                    StartCoroutine(coroutine_s3Carros);
                    s3_guiaBateria.SetActive(true);
                } */
                if(Physics.CheckBox(s3_objetoCono1.transform.position, s3_objetoCono1.transform.localScale / 2)){
                    conosBoolean[0] = true;
                }else if(Physics.CheckBox(s3_objetoCono2.transform.position, s3_objetoCono2.transform.localScale / 2)){
                    conosBoolean[1] = true;
                }else if(Physics.CheckBox(s3_objetoCono3.transform.position, s3_objetoCono3.transform.localScale / 2)){
                    conosBoolean[2] = true;
                }else if(Physics.CheckBox(s3_objetoCono4.transform.position, s3_objetoCono4.transform.localScale / 2)){
                    conosBoolean[3] = true;
                }else if (Physics.CheckBox(s3_objetoCono5.transform.position, s3_objetoCono5.transform.localScale / 2)){
                    s3_conosYa = true;
                    s3_lineaConos.SetActive(false);
                    s3_carros.SetActive(true);
                    s3_guiaConos.SetActive(false);
                    StopCoroutine(coroutine_s3Conos);
                    StartCoroutine(coroutine_s3Carros);
                    s3_guiaBateria.SetActive(true);
                }
                //Evaluación
                if(Math.Abs(transform.InverseTransformVector(rb.velocity).z * 3.6) < 5 && (time2+4 <= Time.time)){
                    time2 = Time.time;
                    s3_fueraRangoVelocidad++;
                    Debug.Log("Fuera del rango de velocidad: " + s3_fueraRangoVelocidad + " veces");
                }
            }else if(!s3_carrosYa){
                if(!s3_bateriaYa && Physics.CheckBox(s3_objetoBateria.transform.position, s3_objetoBateria.transform.localScale / 2)){
                    s3_bateriaYa = true;
                    s3_guiaBateria.SetActive(false);
                    s3_guiaEspiga.SetActive(true);
                }else if(s3_bateriaYa && Physics.CheckBox(s3_objetoEspiga.transform.position, s3_objetoEspiga.transform.localScale / 2)){
                    s3_espigaYa = true;
                    s3_guiaEspiga.SetActive(false);
                    s3_guiaEnLinea.SetActive(true);
                }else if(s3_espigaYa){
                    if(!s3_enLineaPaso1Ya && Physics.CheckBox(s3_objetoEnLineaPaso1.transform.position, s3_objetoEnLineaPaso1.transform.localScale / 2)){
                        s3_enLineaPaso1Ya = true;
                    }else if(!s3_enLineaPaso2Ya && im.brakeNormal && im.steer == -1 && s3_enLineaPaso1Ya){
                        s3_enLineaPaso2Ya = true;
                    }else if(!s3_enLineaPaso3Ya && !im.brakeNormal && s3_enLineaPaso2Ya){
                        s3_enLineaPaso3Ya = true;
                    }else if(!s3_enLineaPaso4Ya && Physics.CheckBox(s3_objetoEnLineaPaso3.transform.position, s3_objetoEnLineaPaso3.transform.localScale / 2) && s3_enLineaPaso3Ya){
                        if(!s3_enLineaPaso4TocoObjeto){
                            s3_enLineaPaso4.Play();
                            s3_enLineaPaso4TocoObjeto = true;
                        }
                        if(im.steer == 0){
                            s3_enLineaPaso4Ya = true;
                        }
                    }else if(!s3_enLineaPaso5Ya && !im.brakeNormal && s3_enLineaPaso4Ya){
                        s3_enLineaPaso5Ya = true;
                    }else if(!s3_enLineaPaso6Ya && Physics.CheckBox(s3_objetoEnLineaPaso4.transform.position, s3_objetoEnLineaPaso4.transform.localScale / 2) && s3_enLineaPaso5Ya){
                        if(!s3_enLineaPaso6TocoObjeto){
                            s3_enLineaPaso6.Play();
                            s3_enLineaPaso6TocoObjeto = true;
                        }
                        if(Physics.CheckBox(s3_objetoEnLineaPaso6.transform.position, s3_objetoEnLineaPaso6.transform.localScale / 2))
                            s3_enLineaPaso6Ya = true;
                    }else if(!s3_enLineaPaso7Ya && Physics.CheckBox(s3_objetoEnLineaPaso6.transform.position, s3_objetoEnLineaPaso6.transform.localScale / 2) && s3_enLineaPaso6Ya && im.steer == 0){
                        if(!s3_enLineaPaso6.isPlaying && !s3_enLineaPaso7.isPlaying){
                            s3_enLineaPaso7Ya = true;
                            s3_carrosYa = true;
                            s3Finished = true;
                            
                            StopCoroutine(coroutine_s3Carros);
                            s3_guiaEnLinea.SetActive(false);
                            s3_carros.SetActive(false);

                            rb.mass = 99999999999f;
                            rb.drag = 99999999999f;
                            rb.angularDrag = 99999999999f;
                            StartCoroutine(coroutine_s4);
                            s4_guiaVallas.SetActive(true);
                            s4_vallas.SetActive(true);
                        }
                    }else{
                        Debug.Log("Todo bien mi pana");
                        if(s3_enLineaPaso3Ya) Debug.Log("Se acabó el paso 3 mi pana");
                    }
                }
            }
        }else if(!s4Finished){
            if(Physics.CheckBox(s4_finDelCamino.transform.position, s4_finDelCamino.transform.localScale / 2)){
                s4Finished = true;
                StopCoroutine(coroutine_s4);
                s4_guiaVallas.SetActive(false);
                StartCoroutine(felicitacion);
            }
        }
    }

    private void OnDrawGizmos() {
        /* foreach (var item in delimiters)
        {
            Gizmos.DrawWireCube(item.transform.position, item.transform.localScale);


        } */

        //Gizmos.DrawWireCube(finDelCamino.transform.position, finDelCamino.transform.localScale);
        Gizmos.DrawWireCube(s2_ingresoGlorieta.transform.position, s2_ingresoGlorieta.transform.localScale);
        Gizmos.DrawWireCube(s2_salirGlorieta.transform.position, s2_salirGlorieta.transform.localScale);
        //Gizmos.DrawWireCube(s2_ingresoGlorieta.transform.position, s2_ingresoGlorieta.transform.localScale);
        Gizmos.DrawWireCube(s3_objetoCono1.transform.position, s3_objetoCono1.transform.localScale);
        Gizmos.DrawWireCube(s3_objetoCono2.transform.position, s3_objetoCono2.transform.localScale);
        Gizmos.DrawWireCube(s3_objetoCono3.transform.position, s3_objetoCono3.transform.localScale);
        Gizmos.DrawWireCube(s3_objetoCono4.transform.position, s3_objetoCono4.transform.localScale);
        Gizmos.DrawWireCube(s3_objetoCono5.transform.position, s3_objetoCono5.transform.localScale);
        Gizmos.DrawWireCube(s3_objetoBateria.transform.position, s3_objetoBateria.transform.localScale);
        Gizmos.DrawWireCube(s3_objetoEspiga.transform.position, s3_objetoEspiga.transform.localScale);
        Gizmos.DrawWireCube(s3_objetoEnLineaPaso1.transform.position, s3_objetoEnLineaPaso1.transform.localScale);
        Gizmos.DrawWireCube(s3_objetoEnLineaPaso3.transform.position, s3_objetoEnLineaPaso3.transform.localScale);
        Gizmos.DrawWireCube(s3_objetoEnLineaPaso6.transform.position, s3_objetoEnLineaPaso6.transform.localScale);
        Gizmos.DrawWireCube(s3_objetoEnLineaPaso4.transform.position, s3_objetoEnLineaPaso4.transform.localScale);
        Gizmos.DrawWireCube(s4_finDelCamino.transform.position, s4_finDelCamino.transform.localScale);


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(bEncender){
            foreach (WheelCollider wheel in throttleWheels)
            {
                if(im.brakeNormal){
                    detenerCarro();
                }else{
                    if(Math.Abs(rb.velocity.magnitude * 7) > velocidadMaxima){
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
        }
        //leccion();
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.name != "Tocus_Win_Front (1)"){
            if(other.gameObject.tag != "S3_Cono"){
                if(other.gameObject.tag == "S4_Vallas"){
                    s4_tocoValla++;
                }else
                if(other.relativeVelocity.magnitude > 4){
                    breakingGlass.Play();
                    alerta.SetActive(true);
                    textoMotivo.text = "Choque";
                    isOver = true;
                    audios = GameObject.Find("Leccion2").GetComponentsInChildren<AudioSource>();
                    foreach (var item in audios)
                    {
                        item.Pause();
                    }
                    inmovilizarCarro();
                    start.DestroyMesh();
                }
                Debug.Log("Collision Detected");
            }else{
                s3_masCuidado.Play();
                s3_tocoCono ++ ;
            }


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

    /* private void turnOn(){
        if(bEncender){
            bEncender = false;
            engineSound.Stop();
        }else{
            bEncender = true;

            StartCoroutine (delayEncendidoCarro());

        }
    } */

    IEnumerator delayEncendidoCarro(){
        turnOnSound.Play();
        yield return new WaitForSeconds(1.0f);
        engineSound.Play();
    }

    IEnumerator seccion1(){
        imageS1.SetActive(true);
      yield return new WaitForSeconds(2.0f);
      imageS1.SetActive(false);
        bool rapido = true;
        while(true){
            Debug.Log("Estoy en la rutina 1");
            if(!bienvenido.isPlaying && !isOver){
                rapido = true;
                if(!bEncender && !bV1){
                    introS1.Play();
                    rapido = false;
                    yield return new WaitForSeconds(27.0f);
                    
                }else if(!bV1){
                    
                    s1_primera.Play();
                    rapido = false;
                    yield return new WaitForSeconds(13.0f);
                }else if(!bV2 && (rb.velocity.magnitude * 7) >= velocidadMaxima){

                    s1_segunda.Play();
                    rapido = false;
                    yield return new WaitForSeconds(7.0f);
                }else if(!bV3 && (rb.velocity.magnitude * 7) >= velocidadMaxima){
                    s1_tercera.Play();
                    rapido = false;
                    yield return new WaitForSeconds(5.0f);
                    s1_avisoBajarVelocidad.Play();
                    yield return new WaitForSeconds(6.0f);
                }else if(!bV2aux && bV3 && (rb.velocity.magnitude * 7) <= 42){
                    s1_segundaAgain.Play();
                    rapido = false;
                    yield return new WaitForSeconds(8.0f);
                }else if(!bV1aux && bV2aux && (rb.velocity.magnitude * 7) <= 22){
                    s1_primeraAgain.Play();
                    rapido = false;
                    yield return new WaitForSeconds(8.0f);
                }else if(!(Math.Truncate(Math.Abs(transform.InverseTransformVector(rb.velocity).z)) == 0) && bV1aux){
                    s1_freno.Play();
                    rapido = false;
                    yield return new WaitForSeconds(11.0f);
                }

            }
            if(rapido){
                yield return new WaitForSeconds(0.5f);
            }else{
                yield return new WaitForSeconds(0.01f);
            }
        }
    }

    IEnumerator seccion2(){
        imageS2.SetActive(true);
      yield return new WaitForSeconds(2.0f);
      imageS2.SetActive(false);
        bienHecho.Play();
        yield return new WaitForSeconds(2.0f);
        rb.MovePosition( s2_spawn.transform.position);
        rb.MoveRotation( s2_spawn.transform.rotation);
        /* transform.position = s2_spawn.transform.position;
        transform.rotation = s2_spawn.transform.rotation; */
        introS2.Play();
        yield return new WaitForSeconds(18.5f);
        s2_ingresa.Play();
        yield return new WaitForSeconds(7.0f);
        rb.mass = 1000;
        rb.drag = 0.05f;
        rb.angularDrag = 0.48f;
        bEncender = true;

    }

    IEnumerator seccion3Conos(){
        imageS3.SetActive(true);
      yield return new WaitForSeconds(2.0f);
      imageS3.SetActive(false);
        bienHecho.Play();
        yield return new WaitForSeconds(2.0f);
        /* transform.position = s3_spawn.transform.position;
        transform.rotation = s3_spawn.transform.rotation; */
        rb.MovePosition( s3_spawn.transform.position);
        rb.MoveRotation( s3_spawn.transform.rotation);
        introS3.Play();
        yield return new WaitForSeconds(7.5f);
        s3_conos.Play();
        yield return new WaitForSeconds(7.6f);
        rb.mass = 1000;
        rb.drag = 0.05f;
        rb.angularDrag = 0.48f;
    }

    IEnumerator seccion3Carros(){
        bool introEnLineaYa = false;
        s3_introEstacionamiento.Play();
        yield return new WaitForSeconds(17.5f);
        while(true){
            if(!s3_bateriaYa){
                s3_bateria.Play();
                yield return new WaitForSeconds(21.0f);
            }else if(!s3_espigaYa){
                s3_espiga1.Play();
                yield return new WaitForSeconds(12f);
                s3_espiga2.Play();
                yield return new WaitForSeconds(21.5f);
            }else if(!s3_enLineaYa){
                if(!introEnLineaYa){
                    s3_enLinea.Play();
                    introEnLineaYa = true;
                    yield return new WaitForSeconds(17.0f);
                }
                if(!s3_enLineaPaso1Ya){
                    s3_enLineaPaso1.Play();
                    yield return new WaitForSeconds(9.5f);
                }else if(!s3_enLineaPaso2Ya){
                    s3_enLineaPaso2.Play();
                    yield return new WaitForSeconds(13.0f);
                }else if (!s3_enLineaPaso3Ya){
                    s3_enLineaPaso3.Play();
                    yield return new WaitForSeconds(10.0f);
                /* }else if(!s3_enLineaPaso4Ya){
                    s3_enLineaPaso4.Play();
                    yield return new WaitForSeconds(13.2f); */
                }else if(!s3_enLineaPaso5Ya && s3_enLineaPaso4Ya && !s3_enLineaPaso4.isPlaying){
                    s3_enLineaPaso5.Play();
                    yield return new WaitForSeconds(7.5f);
               /*  }else if(!s3_enLineaPaso6Ya){
                    s3_enLineaPaso6.Play();
                    yield return new WaitForSeconds(19.5f); */
                }else if(!s3_enLineaPaso7Ya && s3_enLineaPaso6Ya && !s3_enLineaPaso6.isPlaying){
                    s3_enLineaPaso7.Play();
                    yield return new WaitForSeconds(9.8f);
                }else{
                    yield return new WaitForSeconds(0.1f);
                }

            }

        }
    }

    IEnumerator seccion4(){
        imageS4.SetActive(true);
      yield return new WaitForSeconds(2.0f);
      imageS4.SetActive(false);
        bienHecho.Play();
        yield return new WaitForSeconds(2.0f);
        transform.position = s4_spawn.transform.position;
        transform.rotation = s4_spawn.transform.rotation;
        introS4.Play();
        yield return new WaitForSeconds(16.0f);
        rb.mass = 1000;
        rb.drag = 0.05f;
        rb.angularDrag = 0.48f;
    }

    IEnumerator finalLeccion(){
        bienHecho.Play();
        yield return new WaitForSeconds(2.0f);
        felicidades.Play();
        darCalificaciones();

    }

    private void darCalificaciones()
    {
        ExamNumber.examNumber = 2;
        ExamNumber.criteriosAux = new List<Criterio>();
        ExamNumber.faltasAux = new List<Falta>();

        if(s1_salioCarril>0){
            if(s1_salioCarril < 5){
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 2,
                    seccion = 1,
                    veces = s1_salioCarril,
                    texto = "Salir del carril correspondiente",
                    puntos = 2
                });
            }else{
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 2,
                    seccion = 1,
                    veces = 5,
                    texto = "Salir del carril correspondiente",
                    puntos = 2
                });
            }
        }
        if(s2_salioCarril>0){
            if(s2_salioCarril < 5){
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 2,
                    seccion = 2,
                    veces = s2_salioCarril,
                    texto = "Salir del carril de la glorieta",
                    puntos = 2
                });
            }else{
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 2,
                    seccion = 2,
                    veces = 5,
                    texto = "Salir del carril de la glorieta",
                    puntos = 2
                });
            }
        }
        if(s3_fueraRangoVelocidad>0){
            if(s3_fueraRangoVelocidad < 5){
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 2,
                    seccion = 3,
                    veces = s3_fueraRangoVelocidad,
                    texto = "Fuera del rango de velocidad",
                    puntos = 5
                });
            }else{
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 2,
                    seccion = 3,
                    veces = 5,
                    texto = "Fuera del rango de velocidad",
                    puntos = 5
                });
            }
        }
        if(s3_tocoCono>0){
            if(s3_tocoCono < 5){
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 2,
                    seccion = 3,
                    veces = s3_tocoCono,
                    texto = "Tocar el cono",
                    puntos = 6
                });
            }else{
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 2,
                    seccion = 3,
                    veces = 5,
                    texto = "Tocar el cono",
                    puntos = 6
                });
            }
        }
        int vecesConos = 0;
        for(int i = 0; i < 4; i++){
            if(conosBoolean[i] == false)
                vecesConos++;
        }
        if(vecesConos>0){
            ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 2,
                seccion = 3,
                veces = vecesConos,
                texto = "Evadir un segmento dentro del Zigzag",
                puntos = 10
            });
        }
        if(s4_tocoValla>0){
            if(s4_tocoValla < 5){
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 2,
                    seccion = 4,
                    veces = s4_tocoValla,
                    texto = "Tocar una valla",
                    puntos = 2
                });
            }else{
                ExamNumber.criteriosAux.Add(new Criterio(){
                    leccion = 2,
                    seccion = 4,
                    veces = 5,
                    texto = "Tocar una valla",
                    puntos = 2
                });
            }
        }
        



        if(s1_salioCarril < 5){
            s1Cali -= s1_salioCarril * 2;
        }else{
            s1Cali -= 10;
        }
        if(s2_salioCarril < 5){
            s2Cali -= s2_salioCarril * 2;
        }else{
            s2Cali -= 10;
        }
        if(s3_fueraRangoVelocidad < 5){
            s3Cali -= s3_fueraRangoVelocidad * 5;
        }else{
            s3Cali -= 25;
        }
        if(s3_tocoCono < 5){
            s3Cali -= s3_tocoCono * 6;
        }else{
            s3Cali -= 30;
        }
        for(int i = 0; i < 4; i++){
            if(conosBoolean[i] == false)
                s3Cali -= 10;
        }
        if(s4_tocoValla < 5){
            s4Cali -= s4_tocoValla * 2;
        }else{
            s4Cali -= 10;
        }
        Debug.Log("La calificacion fue: " + ((s1Cali + s2Cali + s3Cali + s4Cali) / 4));
        
        
        
        SceneManager.LoadScene("retro");
    }

    public void detenerCarro(){
        foreach (WheelCollider wheel in throttleWheels)
        {
            wheel.motorTorque = 0f;
            wheel.brakeTorque = brakeStrenght;
        }
    }


}
