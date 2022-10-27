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
public class CarController : MonoBehaviour
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
    public UIManager uim;
    public List<GameObject> tailLights;
    public GameObject windowFront;
    public GameObject destroyedWindowFront;
    private MeshDestroy start;
    public AudioSource breakingGlass;
    public AudioSource engineSound, turnOnSound, bienHecho, felicidades;

    private IEnumerator coroutine_s1, coroutine_s2, coroutine_s3, coroutine_s4, felicitacion, coroutine_cambios;
    private bool s1Finished, s2Finished, s3Finished, s4inished, sCambiosFinished;
    public float steerAnglexD;


    //Elección de ambiente
    [SerializeField] private ambienteGeneral ambienteGeneral;

    //Alerta Choque
    [SerializeField] private GameObject  alerta;
    [SerializeField] private Text  textoMotivo;
    private AudioSource[] audios;
    private bool isOver, continuer = true;

    //Letreros Sección
    [SerializeField] private GameObject  imageS1;
    [SerializeField] private GameObject  imageS2;
    [SerializeField] private GameObject  imageS3;
    [SerializeField] private GameObject  imageS4;
    [SerializeField] private GameObject  imageScambios;

    /*------------Audios y booleans seccion 1-------------*/
    public AudioSource orPonerCinturon, bienvenido, orPisaClutch, sCinturon, orPalancaNeutral, sCambioVelocidad,
                       orPisarFreno, orFrenoMano, orEncenderCarro;

    private bool bCinturon, bClutch, bEncender, bFreno, bPalanca, bFrenoMano;

    /*------------Audios y booleans seccion cambios-------------*/
    public AudioSource introCambios, primera, segunda, tercera, cuarta, quinta, reversa, finCambios;
    private bool cV1, cV2, cV3, cV4, cV5, cReversa;

    /*------------Audios y booleans seccion 2-------------*/
    public AudioSource introS2, v1, v2, v3, v4, v5, freno;
    private bool bV1, bV2, bV3, bV4, bV5;
    private float velocidadMaxima = 20;
    public GameObject finDelCamino, spawnOriginal;

    /*------------Audios y booleans seccion 3-------------*/
    public AudioSource introS3, primeraVuelta, segundaVuelta, conclusión;
    public GameObject spawn, primeraCurva, segundaCurva, final, conclusion, otroFinal;
    private bool primeraVueltaYa, segundaVueltaYa, finalYa, conclusionYa;
    public List<GameObject> delimiters;

    /*------------Audios y booleans seccion 4-------------*/
    public AudioSource introS4, reversa1, reversa2;
    private bool s4p1 = false, s4p2 = false;
    public GameObject cono, spawn2, spawn3, final2;

    /* Puntuaciones */
    private int s1Cali = 100, s2Cali = 100, s3Cali = 100, s4Cali = 100;
    private int s3SalioCarril = 0, s3FueraRangoVelocidad = -2;
    float time, time2, distanciaConCono;
    public UnityEvent eventoRestart, eventoReturn;



    // Start is called before the first frame update
    void Start()
    {

        imageS1.SetActive(false);
        imageS2.SetActive(false);
        imageS3.SetActive(false);
        imageS4.SetActive(false);
        imageScambios.SetActive(false);
        //Moví este código a ambienteGeneral.cs para que hiciera más sentido
        //Elección de ambiente
        // int opAmbiente;
        // opAmbiente = ambienteHandler.getAmbiente(); 
        // switch(opAmbiente)
        // {
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


        bCinturon = false;
        bClutch = false;
        bEncender = false;
        bFreno = false;
        bPalanca = false;
        bFrenoMano = false;

        bV1 = false;
        bV2 = false;
        bV3 = false;
        bV4 = false;
        bV5 = false;

        cV1 = false;
        cV2 = false;
        cV3 = false;
        cV4 = false;
        cV5 = false;
        cReversa = false;

        primeraVueltaYa = false;
        segundaVueltaYa = false;
        finalYa = false;

        coroutine_s1 = seccion1();
        coroutine_cambios = seccionCambios();
        coroutine_s2 = seccion2();
        coroutine_s3 = seccion3();
        coroutine_s4 = seccion4();
        felicitacion = finalLeccion();
        StartCoroutine(delayTurnOn());
        StartCoroutine(delayLuces());
        StartCoroutine(coroutine_s1);

        start = GameObject.FindGameObjectWithTag("VentanaARomper").GetComponent<MeshDestroy>();
        im = GetComponent<InputManager>();
        //im = GetComponent<InputManagerForKeyboard>();
        rb = GetComponent<Rigidbody>();

        if(cm){
            rb.centerOfMass = cm.localPosition;
        }
    }

    private void Awake() {

    }

    private void inmovilizarCarro(){
        rb.MovePosition(transform.position + new Vector3(0,10,0));
        rb.useGravity = false;
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

        if(bCinturon && !bClutch){
            if(im.clutch){
                sCambioVelocidad.Play();
                bClutch = true;
            }
        }

        if(bClutch && !bPalanca){
            if(im.CurrentGear == 0){
                sCambioVelocidad.Play();
                bPalanca = true;
            }
        }

        if(bPalanca && !bFreno){
            if(im.brakeNormal){
                sCambioVelocidad.Play();
                bFreno = true;
            }
        }

        checkButtons();

       

        
        if(!s1Finished){
            if(im.cinturon && continuer){

                if(!bCinturon){
                    sCinturon.Play();
                    bCinturon = true;

                }/* else if(!bClutch){
                    sCambioVelocidad.Play();
                    bClutch = true;

                }else if(!bPalanca){
                    sCambioVelocidad.Play();
                    bPalanca = true;

                }else if(!bFreno){
                    sCambioVelocidad.Play();
                    bFreno = true;

                } */else if(!bFrenoMano && bFreno){
                    
                    if(!bienvenido.isPlaying){
                    sCambioVelocidad.Play();
                    bFrenoMano = true;
                    StopCoroutine(coroutine_s1);
                    s1Finished = true;
                    if(orFrenoMano.isPlaying) orFrenoMano.Stop();
                    introCambios.Play();
                    StartCoroutine(coroutine_cambios);  
                    }

                    
                }else{
                }
                //Thread.Sleep(150);
                StartCoroutine(continueCor());
            }
        }else if(!sCambiosFinished){
            if(im.clutch){
                if(im.v1){
                    cV1 = true;
                    sCambioVelocidad.Play();
                }else if(im.v2 && cV1){
                    cV2 = true;
                    sCambioVelocidad.Play();
                }else if(im.v3 && cV2){
                    cV3 = true;
                    sCambioVelocidad.Play();
                }
                else if(im.v4 && cV3){
                    cV4 = true;
                    sCambioVelocidad.Play();
                }
                else if(im.v5 && cV4){
                    cV5 = true;
                    sCambioVelocidad.Play();

                    StopCoroutine(coroutine_cambios);
                    sCambiosFinished = true;

                    introS2.Play();
                    StartCoroutine(coroutine_s2);



                }else{

                }
                Thread.Sleep(100);
            }
        }else if(!s2Finished){

            if(Physics.CheckBox(finDelCamino.transform.position, finDelCamino.transform.localScale / 2) && !bV5){
                rb.mass = 99999999999f;
                rb.drag = 99999999999f;
                rb.angularDrag = 99999999999f;

                transform.position = spawnOriginal.transform.position;
                transform.rotation = spawnOriginal.transform.rotation;

                rb.mass = 1000;
                rb.drag = 0.05f;
                rb.angularDrag = 0.48f;

            }
            if(im.clutch){
                if(im.v1){
                    bV1 = true;
                    sCambioVelocidad.Play();
                    rb.useGravity = true;
                    velocidadMaxima = 20;
                }else if(im.v2 && bV1){
                    bV2 = true;
                    sCambioVelocidad.Play();
                    velocidadMaxima = 40;
                }else if(im.v3 && bV2){
                    bV3 = true;
                    sCambioVelocidad.Play();
                    velocidadMaxima = 60;
                }
                else if(im.v4 && bV3){
                    bV4 = true;
                    sCambioVelocidad.Play();
                    velocidadMaxima = 80;
                }
                else if(im.v5 && bV4){
                    bV5 = true;
                    sCambioVelocidad.Play();
                    velocidadMaxima = 30;

                    StopCoroutine(coroutine_s2);
                    s2Finished = true;

                    rb.mass = 9999999;
                    rb.drag = 9999999;
                    rb.angularDrag = 999999999;

                    StartCoroutine(coroutine_s3);

                }else{

                }
                Thread.Sleep(100);
            }
        }else if(!s3Finished){
            if(!primeraVueltaYa){
                if(Physics.CheckSphere(primeraCurva.transform.position, 2.0f)){
                    primeraVuelta.Play();
                    primeraVueltaYa = true;
                    Debug.Log("parte1");
                }
            }else if(!segundaVueltaYa){
                if(Physics.CheckSphere(segundaCurva.transform.position, 2.0f)){
                    segundaVuelta.Play();
                    segundaVueltaYa = true;
                    Debug.Log("parte2");
                }
            }else if(!conclusionYa){
                if(Physics.CheckSphere(conclusion.transform.position, 2.0f)){
                    conclusión.Play();
                    conclusionYa = true;
                    Debug.Log("parte3");
                }
            }else{
                if(Physics.CheckBox(otroFinal.transform.position, otroFinal.transform.localScale / 2, otroFinal.transform.rotation)){
                    bienHecho.Play();
                    Debug.Log("parte4");
                    finalYa = true;
                    StopCoroutine(coroutine_s3);

                    s3Finished = true;

                    StartCoroutine(coroutine_s4);

                    transform.position = spawn2.transform.position;
                    transform.rotation = spawn2.transform.rotation;

                    cono.SetActive(true);


                }
            }
            //CheckSphere(d.transform.position, 2.0f)
            foreach (var d in delimiters)
            {
                if(Physics.CheckBox(d.transform.position, d.transform.localScale / 2) && (time+3 <= Time.time)){
                    time = Time.time;
                    s3SalioCarril++;
                    Debug.Log("salio " + s3SalioCarril + " veces por el cubo " + d.name);
                }
            }
            if(Math.Abs(transform.InverseTransformVector(rb.velocity).z * 3.6) < 10 && (time2+3 <= Time.time)){
                time2 = Time.time;
                s3FueraRangoVelocidad++;
                Debug.Log("Fuera del rango de velocidad: " + s3FueraRangoVelocidad + " veces");
            }
        }else{
            if(!s4p1){
                finalYa = false;
                if( (Math.Truncate(Math.Abs(transform.InverseTransformVector(rb.velocity).z)) == 0) && (Vector3.Distance(transform.position,cono.transform.position) < 2f)){ //(Math.Abs(Math.Abs(transform.position.z) - Math.Abs(cono.transform.position.z)) < 3.5f)
                    distanciaConCono = Math.Abs(Math.Abs(transform.position.z) - Math.Abs(cono.transform.position.z));
                    s4p1 = true;
                    StartCoroutine(traspaso());
                }
            }else if(!finalYa && Physics.CheckSphere(final2.transform.position, 2.0f)){
                finalYa = true;
                StopCoroutine(coroutine_s4);
                StartCoroutine(felicitacion);
            }
        }


        //uim.changeText(transform.InverseTransformVector(rb.velocity).z);

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

    private void OnDrawGizmos() {
        foreach (var item in delimiters)
        {
            Gizmos.DrawWireCube(item.transform.position, item.transform.localScale);


        }
        Gizmos.DrawWireCube(finDelCamino.transform.position, finDelCamino.transform.localScale);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(bEncender){
            //Debug.Log("Si Toy prendido");
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
                steerAnglexD = wheel.steerAngle;
                //wheel.steerAngle = 0f;
            }
        }else{
            //Debug.Log("No Toy prendido");
            detenerCarro();
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.name != "Tocus_Win_Front (1)"){
            if(other.gameObject.name != "Cono"){
                if(other.relativeVelocity.magnitude > 4){
                    breakingGlass.Play();
                    alerta.SetActive(true);
                    textoMotivo.text = "Choque";
                    isOver = true;
                    audios = GameObject.Find("Leccion1").GetComponentsInChildren<AudioSource>();
                    foreach (var item in audios)
                    {
                        item.Pause();
                    }
                    inmovilizarCarro();
                    start.DestroyMesh();
                }
                Debug.Log("Collision Detected");
            }else{
                breakingGlass.Play();

                transform.position = spawn2.transform.position;
                transform.rotation = spawn2.transform.rotation;
            }


        }
    }

    private void turnOn(){
        if(bEncender){
            bEncender = false;
            engineSound.Stop();
        }else{
            bEncender = true;

            StartCoroutine (delay());

        }
    }

    IEnumerator continueCor(){
        continuer = false;
        yield return new WaitForSeconds(0.5f);
        continuer = true;
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

    IEnumerator delay(){
        turnOnSound.Play();
        yield return new WaitForSeconds(1.0f);
        engineSound.Play();
    }

    IEnumerator seccion1(){
            imageS1.SetActive(true);
            yield return new WaitForSeconds(2.0f);
            imageS1.SetActive(false);
        while(true){
            Debug.Log("Estoy en la rutina 1");
            if(!bienvenido.isPlaying && !isOver){
                if(!bCinturon){
                    orPonerCinturon.Play();
                    yield return new WaitForSeconds(2.0f);
                }else if(!bClutch){
                    orPisaClutch.Play();
                    yield return new WaitForSeconds(3.0f);
                }else if(!bPalanca){
                    orPalancaNeutral.Play();
                    yield return new WaitForSeconds(7.0f);
                }else if(!bEncender){
                    orEncenderCarro.Play();
                    yield return new WaitForSeconds(2.0f);
                }else if(!bFreno){
                    orPisarFreno.Play();
                    yield return new WaitForSeconds(2.0f);
                }else if(!bFrenoMano){
                    orFrenoMano.Play();
                    yield return new WaitForSeconds(10.0f);
                }
            }
            yield return new WaitForSeconds(1.0f);
        }

    }

    IEnumerator seccionCambios(){
        bool yaDijoV1 = false, yaDijoV2 = false, yaDijoV3 = false, yaDijoV4 = false, yaDijoV5 = false;
        imageScambios.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        imageScambios.SetActive(false);
        while(true){
            Debug.Log("Estoy en la rutina cambios");
            if(!introCambios.isPlaying){

                if(!cV1 && !yaDijoV1){
                    primera.Play();
                    yield return new WaitForSeconds(primera.clip.length + 1);
                    yaDijoV1 = true;
                }else if(!cV2 && !yaDijoV2){
                    segunda.Play();
                    yield return new WaitForSeconds(segunda.clip.length + 1);
                    yaDijoV2 = true;
                }else if(!cV3 && !yaDijoV3){
                    tercera.Play();
                    yield return new WaitForSeconds(tercera.clip.length + 1);
                    yaDijoV3 = true;
                }else if(!cV4 && !yaDijoV4){
                    cuarta.Play();
                    yield return new WaitForSeconds(cuarta.clip.length + 1);
                    yaDijoV4 = true;
                }else if(!cV5 && !yaDijoV5){
                    quinta.Play();
                    yield return new WaitForSeconds(quinta.clip.length + 1);
                    yaDijoV5 = true;
                }else if(!cReversa){
                    reversa.Play();
                }
            }
            yield return new WaitForSeconds(8.0f);
        }
    }

    IEnumerator seccion2(){
        imageS2.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        imageS2.SetActive(false);
        bool rapido = true;
        while(true){
            Debug.Log("Estoy en la rutina 2");
            imageS1.SetActive(false);
            if(!introS2.isPlaying){
                rapido = true;
                if(!bV1){
                    v1.Play();
                    rapido = false;
                }else if(!bV2 && rb.velocity.magnitude * 7 >= velocidadMaxima){
                    v2.Play();
                    rapido = false;
                }
                else if(!bV3 && rb.velocity.magnitude * 7 >= velocidadMaxima){
                    v3.Play();
                    rapido = false;
                }
                else if(!bV4 && rb.velocity.magnitude * 7 >= velocidadMaxima){
                    v4.Play();
                    rapido = false;
                }
                else if(!bV5 && rb.velocity.magnitude * 7 >= velocidadMaxima){
                    v5.Play();
                    rapido = false;
                }

            }
            if(rapido){
                yield return new WaitForSeconds(0.5f);
            }else{
                yield return new WaitForSeconds(15.0f);
            }

        }

    }

    IEnumerator seccion3(){
        imageS3.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        imageS3.SetActive(false);
        bienHecho.Play();
        yield return new WaitForSeconds(2.0f);
        rb.MovePosition(spawn.transform.position);
        rb.MoveRotation(spawn.transform.rotation);
        rb.useGravity = false;
        introS3.Play();
        yield return new WaitForSeconds(5.0f);
        rb.useGravity = true;
        
        foreach (var item in throttleWheels)
        {
            item.motorTorque = 0;
            item.brakeTorque = brakeStrenght;
            item.steerAngle = 0;
        }
        rb.mass = 1000;
        rb.drag = 0.05f;
        rb.angularDrag = 0.48f;
    }

    IEnumerator seccion4(){
        imageS4.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        imageS4.SetActive(false);
        bienHecho.Play();
        yield return new WaitForSeconds(2.0f);
        introS4.Play();
        yield return new WaitForSeconds(18.0f);
        while(true){
            if(!s4p1){
                reversa1.Play();
            }
            yield return new WaitForSeconds(16.0f);
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
        ExamNumber.criteriosAux = new List<Criterio>();
        ExamNumber.faltasAux = new List<Falta>();

        if(s3SalioCarril>0){
            ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 1,
                seccion = 3,
                veces = s3SalioCarril,
                texto = "Salir del carril correspondiente",
                puntos = 2
            });
        }
        if(s3FueraRangoVelocidad>0){
            ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 1,
                seccion = 3,
                veces = s3FueraRangoVelocidad,
                texto = "Fuera del rango de velocidad",
                puntos = 2
            });
        }
        if(distanciaConCono > 2.5f){
            ExamNumber.criteriosAux.Add(new Criterio(){
                leccion = 1,
                seccion = 4,
                veces = 1,
                texto = "Distancia con el cono mayor a 2.5 metros",
                puntos = 10
            });
        }

        ExamNumber.examNumber = 1;

        s3Cali -= s3SalioCarril * 2;
        s3Cali -= s3FueraRangoVelocidad * 2;
        if(distanciaConCono > 2.5){
            s4Cali -= 10;
        }
        int puntajeNumber = ((s1Cali + s2Cali + s3Cali + s4Cali) / 4);
        Debug.Log("La calificacion fue: " + puntajeNumber);
        SceneManager.LoadScene("retro");
    }


    IEnumerator traspaso(){
        bienHecho.Play();
        yield return new WaitForSeconds(2.0f);
        reversa2.Play();
        transform.position = spawn3.transform.position;
        transform.rotation = spawn3.transform.rotation;
        yield return new WaitForSeconds(1.0f);
    }

    public void detenerCarro(){
        foreach (WheelCollider wheel in throttleWheels)
        {
            wheel.motorTorque = 0f;
            wheel.brakeTorque = im.BrakeInput;
        }
    }


}
