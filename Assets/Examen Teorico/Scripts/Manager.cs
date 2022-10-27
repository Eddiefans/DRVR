using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Threading;
using UnityEngine.SceneManagement;



public class Manager : MonoBehaviour
{
    //Screens
    [SerializeField] private GameObject startScreen = null;
    [SerializeField] private GameObject feedbackScreen = null;
    [SerializeField] private GameObject questionScreen = null;
    [SerializeField] private GameObject columnScreen = null;
    [SerializeField] private GameObject imageAnswerScreen = null;
    [SerializeField] private GameObject imageQuestionScreen = null;
    [SerializeField] private GameObject quizScreen = null;

    //images
    [SerializeField] private Sprite[] imagesUsed;

    //imageAnswerScreen
    [SerializeField] private Text questionTextImage = null;
    [SerializeField] private Image BackgroundA = null;
    [SerializeField] private Image BackgroundB = null;
    [SerializeField] private Image BackgroundX = null;
    [SerializeField] private Image BackgroundY = null;
    [SerializeField] private Button imageA = null;
    [SerializeField] private Button imageB = null;
    [SerializeField] private Button imageX = null;
    [SerializeField] private Button imageY = null;
    [SerializeField] private Image questionA2 = null;
    [SerializeField] private Image questionB2 = null;
    [SerializeField] private Image questionX2 = null;
    [SerializeField] private Image questionY2 = null;

    //imageQuestionScreen
    [SerializeField] private Image questionImage = null;
    [SerializeField] private Button buttonImageA = null;
    [SerializeField] private Button buttonImageB = null;
    [SerializeField] private Button buttonImageX = null;
    [SerializeField] private Button buttonImageY = null;
    [SerializeField] private Image questionA1 = null;
    [SerializeField] private Image questionB1 = null;
    [SerializeField] private Image questionX1 = null;
    [SerializeField] private Image questionY1 = null;

    //startScreen
    [SerializeField] private Text confirmText = null;
    [SerializeField] private Button startButton = null;
    [SerializeField] private Button backButton = null;

    //feedbackScreen
    [SerializeField] private GameObject screen1 = null;
    [SerializeField] private GameObject screen2 = null;
    [SerializeField] private GameObject screen3 = null;
    [SerializeField] private Text gradeText = null;
    [SerializeField] private Text reprobado = null;
    [SerializeField] private Text aprobado = null;
    [SerializeField] private Text columnText1 = null;
    [SerializeField] private Text columnText2 = null;
    [SerializeField] private Text columnText3 = null;
    [SerializeField] private Text columnText4 = null;
    [SerializeField] private Image wrongmark1 = null;
    [SerializeField] private Image wrongmark2 = null;
    [SerializeField] private Image wrongmark3 = null;
    [SerializeField] private Image wrongmark4 = null;
    [SerializeField] private Image wrongmark5 = null;
    [SerializeField] private Image wrongmark6 = null;
    [SerializeField] private Image wrongmark7 = null;
    [SerializeField] private Image wrongmark8 = null;
    [SerializeField] private Image wrongmark9 = null;
    [SerializeField] private Image wrongmark10 = null;
    [SerializeField] private Image wrongmark11 = null;
    [SerializeField] private Image wrongmark12 = null;
    [SerializeField] private Image wrongmark13 = null;
    [SerializeField] private Image wrongmark14 = null;
    [SerializeField] private Image wrongmark15 = null;
    [SerializeField] private Image wrongmark16 = null;
    [SerializeField] private Image wrongmark17 = null;
    [SerializeField] private Image wrongmark18 = null;
    [SerializeField] private Image wrongmark19 = null;
    [SerializeField] private Image wrongmark20 = null;
    [SerializeField] private Image wrongmark21 = null;
    [SerializeField] private Image wrongmark22 = null;
    [SerializeField] private Image wrongmark23 = null;
    [SerializeField] private Image wrongmark24 = null;
    [SerializeField] private Image wrongmark25 = null;

    [SerializeField] private Image checkmark1 = null;
    [SerializeField] private Image checkmark2 = null;
    [SerializeField] private Image checkmark3 = null;
    [SerializeField] private Image checkmark4 = null;
    [SerializeField] private Image checkmark5 = null;
    [SerializeField] private Image checkmark6 = null;
    [SerializeField] private Image checkmark7 = null;
    [SerializeField] private Image checkmark8 = null;
    [SerializeField] private Image checkmark9 = null;
    [SerializeField] private Image checkmark10 = null;
    [SerializeField] private Image checkmark11 = null;
    [SerializeField] private Image checkmark12 = null;
    [SerializeField] private Image checkmark13 = null;
    [SerializeField] private Image checkmark14 = null;
    [SerializeField] private Image checkmark15 = null;
    [SerializeField] private Image checkmark16 = null;
    [SerializeField] private Image checkmark17 = null;
    [SerializeField] private Image checkmark18 = null;
    [SerializeField] private Image checkmark19 = null;
    [SerializeField] private Image checkmark20 = null;
    [SerializeField] private Image checkmark21 = null;
    [SerializeField] private Image checkmark22 = null;
    [SerializeField] private Image checkmark23 = null;
    [SerializeField] private Image checkmark24 = null;
    [SerializeField] private Image checkmark25 = null;

    [SerializeField] private Button nextFeed = null;
    [SerializeField] private Button previousFeed = null;
    [SerializeField] private Sprite checkmark = null;
    [SerializeField] private Sprite wrongmark = null;
    [SerializeField] private Sprite mark = null;

    //quizUI
    [SerializeField] private Button nextButton = null;
    [SerializeField] private Button previousButton = null;
    [SerializeField] private Text previousText = null;
    [SerializeField] private Text nextText = null;
    [SerializeField] private Sprite upDpad = null;
    [SerializeField] private Sprite rightDpad = null;

    //question
    [SerializeField] private Text questionText = null;
    [SerializeField] private Button ButtonX = null;
    [SerializeField] private Button ButtonY = null;
    [SerializeField] private Button ButtonA = null;
    [SerializeField] private Button ButtonB = null;
    [SerializeField] private Image questionA = null;
    [SerializeField] private Image questionB = null;
    [SerializeField] private Image questionX = null;
    [SerializeField] private Image questionY = null;

    //column
    [SerializeField] private Text text1 = null;
    [SerializeField] private Text text2 = null;
    [SerializeField] private Text text3 = null;
    [SerializeField] private Text text4 = null;
    [SerializeField] private Text column1 = null;
    [SerializeField] private Text column2 = null;
    [SerializeField] private Text column3 = null;
    [SerializeField] private Text column4 = null;
    [SerializeField] private Dropdown dropdown1 = null;
    [SerializeField] private Dropdown dropdown2 = null;
    [SerializeField] private Dropdown dropdown3 = null;
    [SerializeField] private Dropdown dropdown4 = null;

    //Logitech 
    LogitechGSDK.LogiControllerPropertiesData properties;
    
    private List<Question> questions = new List<Question>();
    private List<Questionpool> questionpools = new List<Questionpool>();
    private List<Answer> answers = new List<Answer>();
    private List<Questions> allQuestions = new List<Questions>();
    private Columns allColumns = new Columns();

    private NetworkManager networkManager = null;   
    private int indexQuestion = 0;
    private int leccion = 0;
    private System.Random rng = new System.Random(); 
    private bool isFeedback = false;
    private int screenFeed = 1;
    public void Start(){
        screen1.SetActive(true);
        screen2.SetActive(false);
        screen3.SetActive(false);
        nextFeed.gameObject.SetActive(true);
        previousFeed.gameObject.SetActive(false);
        leccion = ExamNumber.examNumber;
        //StartCoroutine(loadInformation());
        if(ExamNumber.directFeedback){
            if(leccion == 6){
                indexQuestion = 21;
                for(int i = 0; i < 21; i++){
                    allQuestions.Add(UserInfo.questionsArray[ExamNumber.examNumber-1][i]);
                    
                }
            }else{
                indexQuestion = 6;
                for(int i = 0; i < 6; i++){
                    allQuestions.Add(UserInfo.questionsArray[ExamNumber.examNumber-1][i]);
                }
            }
            allColumns = UserInfo.columnsArray[ExamNumber.examNumber-1];
            
            
            SubmitNextButton();
        }else{
            isFeedback = false;
            startScreen.SetActive(true);
            feedbackScreen.SetActive(false);
            questionScreen.SetActive(false);
            imageAnswerScreen.SetActive(false);
            BackgroundA.gameObject.SetActive(false);
            BackgroundB.gameObject.SetActive(false);
            BackgroundX.gameObject.SetActive(false);
            BackgroundY.gameObject.SetActive(false);
            imageQuestionScreen.SetActive(false);
            columnScreen.SetActive(false);
            quizScreen.SetActive(false);
            networkManager = GameObject.FindObjectOfType<NetworkManager>();
            startButton.gameObject.SetActive(false);
            backButton.gameObject.SetActive(false);
            LogitechGSDK.LogiSteeringInitialize(false);
            confirmText.text = "Cargando...";
            StartCoroutine(COloadExam(ExamNumber.examNumber));
        }
    }

    public void Update(){
        /*
        if(LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0)){
            LogitechGSDK.DIJOYSTATE2ENGINES rec;
            rec = LogitechGSDK.LogiGetStateUnity(0);
            for (int i = 0; i < 128; i++)
            {
                if(rec.rgbButtons[i] == 128){
                    
                    if(i == 0){
                        if(startScreen.activeSelf){
                            if(backButton.gameObject.activeSelf){
                                SubmitStart();
                            }
                        }
                        if(quizScreen.activeSelf){
                            if(questionScreen.activeSelf || imageAnswerScreen.activeSelf || imageQuestionScreen.activeSelf){
                                SubmitPressButton(1);
                            }
                        }
                    }else if(i == 1){
                        if(startScreen.activeSelf){
                            if(backButton.gameObject.activeSelf){
                                SubmitBack();
                            }
                        }
                        if(quizScreen.activeSelf){
                            if(questionScreen.activeSelf || imageAnswerScreen.activeSelf || imageQuestionScreen.activeSelf){
                                SubmitPressButton(2);
                            }
                        }
                    }else if(i == 2){
                        if(quizScreen.activeSelf){
                            if(questionScreen.activeSelf || imageAnswerScreen.activeSelf || imageQuestionScreen.activeSelf){
                                SubmitPressButton(3);
                            }
                        }
                    }else if(i == 3){
                        if(quizScreen.activeSelf){
                            if(questionScreen.activeSelf || imageAnswerScreen.activeSelf || imageQuestionScreen.activeSelf){
                                SubmitPressButton(4);
                            }
                        }
                    }
                }
            }
            switch (rec.rgdwPOV[0])
            {
                case (0): 
                //UP
                    if(quizScreen.activeSelf){
                        if(indexQuestion == 6){
                            if(nextButton.gameObject.activeSelf){
                                SubmitNextButton();
                            }
                        }
                    }
                break;
                case (9000): 
                //RIGHT 
                    if(quizScreen.activeSelf){
                        if(indexQuestion < 6){
                            SubmitNextButton();
                        }
                    }
                break;
                case (18000): 
                //DOWN 
                break;
                case (27000): 
                //LEFT
                    if(quizScreen.activeSelf){
                        if(indexQuestion > 0){
                            submitPreviousButton();
                        }
                    }
                break;
                default: break;
            }
        }
        */
    }
    public void Awake(){
        networkManager = GameObject.FindObjectOfType<NetworkManager>();
    }
    public void SubmitBack(){
        /*
        startScreen.SetActive(false);
        feedbackScreen.SetActive(false);
        questionScreen.SetActive(false);
        columnScreen.SetActive(false);
        quizScreen.SetActive(false);*/
        switch (ExamNumber.examNumber)
        {
            case 1:{
                SceneManager.LoadScene("MenuLeccion1");
                break;
            }
            case 2:{
                SceneManager.LoadScene("MenuLeccion2");
                break;
            }
            case 3:{
                SceneManager.LoadScene("MenuLeccion3");
                break;
            }
            case 4:{
                SceneManager.LoadScene("MenuLeccion4");
                break;
            }
            case 5:{
                SceneManager.LoadScene("MenuLeccion5");
                break;
            }
            case 6:{
                SceneManager.LoadScene("ExamenFinalTeorico");
                break;
            }
            default:
            break;
        }
        
    }

    public void SubmitStart(){
        startScreen.SetActive(false);
        questionScreen.SetActive(false);
        imageAnswerScreen.SetActive(false);
        imageQuestionScreen.SetActive(false);
        columnScreen.SetActive(false);
        quizScreen.SetActive(true);
        previousButton.gameObject.SetActive(false);
        previousText.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);
        nextText.gameObject.SetActive(true);
        buttonImageA.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        buttonImageB.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        buttonImageX.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        buttonImageY.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        ButtonA.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        ButtonB.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        ButtonX.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        ButtonY.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        imageA.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        imageB.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        imageX.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        imageY.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        if(allQuestions[0].is_image==0){
            questionScreen.SetActive(true);
            questionText.text = allQuestions[0].question;
            GameObject.Find("buttonA").GetComponentInChildren<Text>().text = allQuestions[0].order[0];
            GameObject.Find("buttonB").GetComponentInChildren<Text>().text = allQuestions[0].order[1];
            GameObject.Find("buttonX").GetComponentInChildren<Text>().text = allQuestions[0].order[2];
            GameObject.Find("buttonY").GetComponentInChildren<Text>().text = allQuestions[0].order[3];
        }else if(allQuestions[0].is_image==1){
            imageAnswerScreen.SetActive(true);
            questionTextImage.text = allQuestions[0].question;
            foreach (var item in imagesUsed)
            {
                if(allQuestions[0].order[0] == (item.name + ".png")){
                    imageA.GetComponent<Image>().sprite = item;
                }
                if(allQuestions[0].order[1] == (item.name + ".png")){
                    imageB.GetComponent<Image>().sprite = item;
                }
                if(allQuestions[0].order[2] == (item.name + ".png")){
                    imageX.GetComponent<Image>().sprite = item;
                }
                if(allQuestions[0].order[3] == (item.name + ".png")){
                    imageY.GetComponent<Image>().sprite = item;
                }
            }
            // imageA.GetComponent<Image>().sprite = Resources.Load(allQuestions[0].order[0], typeof(Sprite)) as Sprite;
            // imageB.GetComponent<Image>().sprite = Resources.Load(allQuestions[0].order[1], typeof(Sprite)) as Sprite;
            // imageX.GetComponent<Image>().sprite = Resources.Load(allQuestions[0].order[2], typeof(Sprite)) as Sprite;
            // imageY.GetComponent<Image>().sprite = Resources.Load(allQuestions[0].order[3], typeof(Sprite)) as Sprite;
        }else{
            imageQuestionScreen.SetActive(true);
            foreach (var item in imagesUsed)
            {
                if(allQuestions[0].question == (item.name + ".png")){
                    questionImage.GetComponent<Image>().sprite = item;
                }
            }
            GameObject.Find("buttonImageA").GetComponentInChildren<Text>().text = allQuestions[0].order[0];
            GameObject.Find("buttonImageB").GetComponentInChildren<Text>().text = allQuestions[0].order[1];
            GameObject.Find("buttonImageX").GetComponentInChildren<Text>().text = allQuestions[0].order[2];
            GameObject.Find("buttonImageY").GetComponentInChildren<Text>().text = allQuestions[0].order[3];
        }
        
        
    }

    public void SubmitPressButton(int pressed){
        allQuestions[indexQuestion].answered = pressed;
        /*var colorss = ButtonA.GetComponent<Button>().colors;
        colorss.normalColor = Color.red;
        colorss.highlightedColor = Color.red;
        colorss.pressedColor = Color.red;
        colorss.selectedColor = Color.red;
        ButtonA.GetComponent<Button>().colors = colorss;*/
        if(allQuestions[indexQuestion].is_image==2){
            buttonImageA.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            buttonImageB.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            buttonImageX.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            buttonImageY.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            if(pressed==1){
                buttonImageA.GetComponent<Image>().color = new Color32(160, 224, 209, 255);
            }else if(pressed == 2){
                buttonImageB.GetComponent<Image>().color = new Color32(160, 224, 209, 255);
            }else if(pressed == 3){
                buttonImageX.GetComponent<Image>().color = new Color32(160, 224, 209, 255);  
            }else{
                buttonImageY.GetComponent<Image>().color = new Color32(160, 224, 209, 255);
            }
        }else if(allQuestions[indexQuestion].is_image == 0){
            ButtonA.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            ButtonB.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            ButtonX.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            ButtonY.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            if(pressed==1){
                ButtonA.GetComponent<Image>().color = new Color32(160, 224, 209, 255);
            }else if(pressed == 2){
                ButtonB.GetComponent<Image>().color = new Color32(160, 224, 209, 255);
            }else if(pressed == 3){
                ButtonX.GetComponent<Image>().color = new Color32(160, 224, 209, 255);  
            }else{
                ButtonY.GetComponent<Image>().color = new Color32(160, 224, 209, 255);
            }
        }else{
            imageA.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            imageB.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            imageX.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            imageY.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            if(pressed==1){
                BackgroundA.gameObject.SetActive(true);
                BackgroundB.gameObject.SetActive(false);
                BackgroundX.gameObject.SetActive(false);
                BackgroundY.gameObject.SetActive(false);
                imageA.GetComponent<Image>();
            }else if(pressed == 2){
                BackgroundB.gameObject.SetActive(true);
                BackgroundA.gameObject.SetActive(false);
                BackgroundX.gameObject.SetActive(false);
                BackgroundY.gameObject.SetActive(false);
                imageB.GetComponent<Image>();
            }else if(pressed == 3){
                BackgroundX.gameObject.SetActive(true);
                BackgroundA.gameObject.SetActive(false);
                BackgroundB.gameObject.SetActive(false);
                BackgroundY.gameObject.SetActive(false);
                imageX.GetComponent<Image>();  
            }else{
                BackgroundY.gameObject.SetActive(true);
                BackgroundA.gameObject.SetActive(false);
                BackgroundB.gameObject.SetActive(false);
                BackgroundX.gameObject.SetActive(false);
                imageY.GetComponent<Image>();
            }
        }
        
    }
    

    public void SubmitNextButton(){
        BackgroundA.gameObject.SetActive(false);
        BackgroundX.gameObject.SetActive(false);
        BackgroundB.gameObject.SetActive(false);
        BackgroundY.gameObject.SetActive(false);
        /*int indexQuestion = 0;
        for (int i = 0; i < 10; i++)
        {
            if(questionText.text == allQuestions[i].question){
                indexQuestion = i;
            }
        }*/
        Debug.Log(indexQuestion);
        Debug.Log(leccion);
        if((indexQuestion==6 && leccion != 6) || (indexQuestion == 21 && leccion == 6)){
            if(leccion == 6){
                columnText1.text = "Pregunta 7";
                columnText2.text = "Pregunta 8";
                columnText3.text = "Pregunta 9";
                columnText4.text = "Pregunta 10";
                nextFeed.gameObject.SetActive(true);
                previousFeed.gameObject.SetActive(false);
            }else{
                nextFeed.gameObject.SetActive(false);
                previousFeed.gameObject.SetActive(false);
            }
            
            if(ExamNumber.directFeedback){
                quizScreen.SetActive(false);
                feedbackScreen.SetActive(true);
            }else{
                startScreen.SetActive(true);
                feedbackScreen.SetActive(false);
                questionScreen.SetActive(false);
                imageAnswerScreen.SetActive(false);
                BackgroundA.gameObject.SetActive(false);
                BackgroundB.gameObject.SetActive(false);
                BackgroundX.gameObject.SetActive(false);
                BackgroundY.gameObject.SetActive(false);
                imageQuestionScreen.SetActive(false);
                columnScreen.SetActive(false);
                quizScreen.SetActive(false);
                startButton.gameObject.SetActive(false);
                backButton.gameObject.SetActive(false);
                confirmText.text = "Cargando...";
            }
            
            int valor = 10;
            if(leccion == 6){valor = 4;}
            int grade = 0;
            if(allQuestions[0].answered == allQuestions[0].correct){
                checkmark1.gameObject.SetActive(true);
                wrongmark1.gameObject.SetActive(false);
                grade = grade + valor;
            }else{
                checkmark1.gameObject.SetActive(false);
                wrongmark1.gameObject.SetActive(true);
            }
            if(allQuestions[1].answered == allQuestions[1].correct){
                checkmark2.gameObject.SetActive(true);
                wrongmark2.gameObject.SetActive(false);
                grade = grade + valor;
            }else{
                checkmark2.gameObject.SetActive(false);
                wrongmark2.gameObject.SetActive(true);
            }
            if(allQuestions[2].answered == allQuestions[2].correct){
                checkmark3.gameObject.SetActive(true);
                wrongmark3.gameObject.SetActive(false);
                grade = grade + valor;
            }else{
                checkmark3.gameObject.SetActive(false);
                wrongmark3.gameObject.SetActive(true);
            }
            if(allQuestions[3].answered == allQuestions[3].correct){
                checkmark4.gameObject.SetActive(true);
                wrongmark4.gameObject.SetActive(false);
                grade = grade + valor;
            }else{
                checkmark4.gameObject.SetActive(false);
                wrongmark4.gameObject.SetActive(true);
            }
            if(allQuestions[4].answered == allQuestions[4].correct){
                checkmark5.gameObject.SetActive(true);
                wrongmark5.gameObject.SetActive(false);
                grade = grade + valor;
            }else{
                checkmark5.gameObject.SetActive(false);
                wrongmark5.gameObject.SetActive(true);
            }
            if(allQuestions[5].answered == allQuestions[5].correct){
                checkmark6.gameObject.SetActive(true);
                wrongmark6.gameObject.SetActive(false);
                grade = grade + valor;
            }else{
                checkmark6.gameObject.SetActive(false);
                wrongmark6.gameObject.SetActive(true);
            }
            
            if(leccion == 6){
                if(allQuestions[6].answered == allQuestions[6].correct){
                    checkmark7.gameObject.SetActive(true);
                    wrongmark7.gameObject.SetActive(false);
                    grade = grade + valor;
                }else{
                    checkmark7.gameObject.SetActive(false);
                    wrongmark7.gameObject.SetActive(true);
                }
                if(allQuestions[7].answered == allQuestions[7].correct){
                    checkmark8.gameObject.SetActive(true);
                    wrongmark8.gameObject.SetActive(false);
                    grade = grade + valor;
                }else{
                    checkmark8.gameObject.SetActive(false);
                    wrongmark8.gameObject.SetActive(true);
                }
                if(allQuestions[8].answered == allQuestions[8].correct){
                    checkmark9.gameObject.SetActive(true);
                    wrongmark9.gameObject.SetActive(false);
                    grade = grade + valor;
                }else{
                    checkmark9.gameObject.SetActive(false);
                    wrongmark9.gameObject.SetActive(true);
                }
                if(allQuestions[9].answered == allQuestions[9].correct){
                    checkmark10.gameObject.SetActive(true);
                    wrongmark10.gameObject.SetActive(false);
                    grade = grade + valor;
                }else{
                    checkmark10.gameObject.SetActive(false);
                    wrongmark10.gameObject.SetActive(true);
                }
                if(allQuestions[10].answered == allQuestions[10].correct){
                    checkmark11.gameObject.SetActive(true);
                    wrongmark11.gameObject.SetActive(false);
                    grade = grade + valor;
                }else{
                    checkmark11.gameObject.SetActive(false);
                    wrongmark11.gameObject.SetActive(true);
                }
                if(allQuestions[11].answered == allQuestions[11].correct){
                    checkmark12.gameObject.SetActive(true);
                    wrongmark12.gameObject.SetActive(false);
                    grade = grade + valor;
                }else{
                    checkmark12.gameObject.SetActive(false);
                    wrongmark12.gameObject.SetActive(true);
                }
                if(allQuestions[12].answered == allQuestions[12].correct){
                    checkmark13.gameObject.SetActive(true);
                    wrongmark13.gameObject.SetActive(false);
                    grade = grade + valor;
                }else{
                    checkmark13.gameObject.SetActive(false);
                    wrongmark13.gameObject.SetActive(true);
                }
                if(allQuestions[13].answered == allQuestions[13].correct){
                    checkmark14.gameObject.SetActive(true);
                    wrongmark14.gameObject.SetActive(false);
                    grade = grade + valor;
                }else{
                    checkmark14.gameObject.SetActive(false);
                    wrongmark14.gameObject.SetActive(true);
                }
                if(allQuestions[14].answered == allQuestions[14].correct){
                    checkmark15.gameObject.SetActive(true);
                    wrongmark15.gameObject.SetActive(false);
                    grade = grade + valor;
                }else{
                    checkmark15.gameObject.SetActive(false);
                    wrongmark15.gameObject.SetActive(true);
                }
                if(allQuestions[15].answered == allQuestions[15].correct){
                    checkmark16.gameObject.SetActive(true);
                    wrongmark16.gameObject.SetActive(false);
                    grade = grade + valor;
                }else{
                    checkmark16.gameObject.SetActive(false);
                    wrongmark16.gameObject.SetActive(true);
                }
                if(allQuestions[16].answered == allQuestions[16].correct){
                    checkmark17.gameObject.SetActive(true);
                    wrongmark17.gameObject.SetActive(false);
                    grade = grade + valor;
                }else{
                    checkmark17.gameObject.SetActive(false);
                    wrongmark17.gameObject.SetActive(true);
                }
                if(allQuestions[17].answered == allQuestions[17].correct){
                    checkmark18.gameObject.SetActive(true);
                    wrongmark18.gameObject.SetActive(false);
                    grade = grade + valor;
                }else{
                    checkmark18.gameObject.SetActive(false);
                    wrongmark18.gameObject.SetActive(true);
                }
                if(allQuestions[18].answered == allQuestions[18].correct){
                    checkmark19.gameObject.SetActive(true);
                    wrongmark19.gameObject.SetActive(false);
                    grade = grade + valor;
                }else{
                    checkmark19.gameObject.SetActive(false);
                    wrongmark19.gameObject.SetActive(true);
                }
                if(allQuestions[19].answered == allQuestions[19].correct){
                    checkmark20.gameObject.SetActive(true);
                    wrongmark20.gameObject.SetActive(false);
                    grade = grade + valor;
                }else{
                    checkmark20.gameObject.SetActive(false);
                    wrongmark20.gameObject.SetActive(true);
                }
                if(allQuestions[20].answered == allQuestions[20].correct){
                    checkmark21.gameObject.SetActive(true);
                    wrongmark21.gameObject.SetActive(false);
                    grade = grade + valor;
                }else{
                    checkmark21.gameObject.SetActive(false);
                    wrongmark21.gameObject.SetActive(true);
                }
                if(allColumns.corrects[0]==allColumns.answered[0]){
                    checkmark22.gameObject.SetActive(true);
                    wrongmark22.gameObject.SetActive(false);
                    grade = grade + valor;
                }else{
                    checkmark22.gameObject.SetActive(false);
                    wrongmark22.gameObject.SetActive(true);
                }
                if(allColumns.corrects[1]==allColumns.answered[1]){
                    checkmark23.gameObject.SetActive(true);
                    wrongmark23.gameObject.SetActive(false);
                    grade = grade + valor;
                }else{
                    checkmark23.gameObject.SetActive(false);
                    wrongmark23.gameObject.SetActive(true);
                }
                if(allColumns.corrects[2]==allColumns.answered[2]){
                    checkmark24.gameObject.SetActive(true);
                    wrongmark24.gameObject.SetActive(false);
                    grade = grade + valor;
                }else{
                    checkmark24.gameObject.SetActive(false);
                    wrongmark24.gameObject.SetActive(true);
                }
                if(allColumns.corrects[3]==allColumns.answered[3]){
                    checkmark25.gameObject.SetActive(true);
                    wrongmark25.gameObject.SetActive(false);
                    grade = grade + valor;
                }else{
                    checkmark25.gameObject.SetActive(false);
                    wrongmark25.gameObject.SetActive(true);
                }
            }else{
                if(allColumns.corrects[0]==allColumns.answered[0]){
                    checkmark7.gameObject.SetActive(true);
                    wrongmark7.gameObject.SetActive(false);
                    grade = grade + valor;
                }else{
                    checkmark7.gameObject.SetActive(false);
                    wrongmark7.gameObject.SetActive(true);
                }
                if(allColumns.corrects[1]==allColumns.answered[1]){
                    checkmark8.gameObject.SetActive(true);
                    wrongmark8.gameObject.SetActive(false);
                    grade = grade + valor;
                }else{
                    checkmark8.gameObject.SetActive(false);
                    wrongmark8.gameObject.SetActive(true);
                }
                if(allColumns.corrects[2]==allColumns.answered[2]){
                    checkmark9.gameObject.SetActive(true);
                    wrongmark9.gameObject.SetActive(false);
                    grade = grade + valor;
                }else{
                    checkmark9.gameObject.SetActive(false);
                    wrongmark9.gameObject.SetActive(true);
                }
                if(allColumns.corrects[3]==allColumns.answered[3]){
                    checkmark10.gameObject.SetActive(true);
                    wrongmark10.gameObject.SetActive(false);
                    grade = grade + valor;
                }else{
                    checkmark10.gameObject.SetActive(false);
                    wrongmark10.gameObject.SetActive(true);
                }
            }
            gradeText.text = grade + "";
            if(grade > 89){
                reprobado.gameObject.SetActive(false);
                aprobado.gameObject.SetActive(true);
            }else{
                reprobado.gameObject.SetActive(true);
                aprobado.gameObject.SetActive(false);
            }
            if(ExamNumber.directFeedback){
                ExamNumber.directFeedback = false;
            }else if (grade > UserInfo.examPuntaje[ExamNumber.examNumber-1]){
                StartCoroutine(RegisterInfo(grade));
                

            }else{
                quizScreen.SetActive(false);
                feedbackScreen.SetActive(true);
                startScreen.SetActive(false);
            }
        }else if((indexQuestion==5 && leccion != 6) || (indexQuestion == 20 && leccion == 6)){
            bool finished = true;
            indexQuestion++;
            questionScreen.SetActive(false);
            columnScreen.SetActive(true);
            imageAnswerScreen.SetActive(false);
            imageQuestionScreen.SetActive(false);
            text1.text = allColumns.questions[0];
            text2.text = allColumns.questions[1];
            text3.text = allColumns.questions[2];
            text4.text = allColumns.questions[3];
            column1.text = allColumns.answers[0];
            column2.text = allColumns.answers[1];
            column3.text = allColumns.answers[2];
            column4.text = allColumns.answers[3];
            int dimension = 6;
            if(leccion == 6){dimension = 21;}
            for (int i = 0; i < dimension; i++)
            {
                if(allQuestions[i].answered == 0){
                    finished = false;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if(allColumns.answered[i]==0){
                    finished = false;
                }
            }
            if(finished){
                nextText.text = "Terminar";
                nextButton.GetComponent<Image>().sprite = upDpad;
            }else{
                nextButton.gameObject.SetActive(false);
                nextText.gameObject.SetActive(false);
            }
        }else{
            indexQuestion++;
            if(allQuestions[indexQuestion].is_image==0){
                questionScreen.SetActive(true);
                imageAnswerScreen.SetActive(false);
                imageQuestionScreen.SetActive(false);
                ButtonA.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                ButtonB.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                ButtonX.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                ButtonY.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                if(allQuestions[indexQuestion].answered==1){
                    ButtonA.GetComponent<Image>().color = new Color32(160, 224, 209, 255);
                }else if(allQuestions[indexQuestion].answered == 2){
                    ButtonB.GetComponent<Image>().color = new Color32(160, 224, 209, 255);
                }else if(allQuestions[indexQuestion].answered == 3){
                    ButtonX.GetComponent<Image>().color = new Color32(160, 224, 209, 255);  
                }else if(allQuestions[indexQuestion].answered == 4){
                    ButtonY.GetComponent<Image>().color = new Color32(160, 224, 209, 255);
                }
                questionText.text = allQuestions[indexQuestion].question;
                GameObject.Find("buttonA").GetComponentInChildren<Text>().text = allQuestions[indexQuestion].order[0];
                GameObject.Find("buttonB").GetComponentInChildren<Text>().text = allQuestions[indexQuestion].order[1];
                GameObject.Find("buttonX").GetComponentInChildren<Text>().text = allQuestions[indexQuestion].order[2];
                GameObject.Find("buttonY").GetComponentInChildren<Text>().text = allQuestions[indexQuestion].order[3];
            }else if(allQuestions[indexQuestion].is_image==1){
                questionScreen.SetActive(false);
                imageAnswerScreen.SetActive(true);
                imageQuestionScreen.SetActive(false);
                imageA.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                imageB.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                imageX.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                imageY.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                if(allQuestions[indexQuestion].answered==1){
                    BackgroundA.gameObject.SetActive(true);
                    BackgroundX.gameObject.SetActive(false);
                    BackgroundB.gameObject.SetActive(false);
                    BackgroundY.gameObject.SetActive(false);
                    imageA.GetComponent<Image>();
                }else if(allQuestions[indexQuestion].answered == 2){
                    BackgroundB.gameObject.SetActive(true);
                    BackgroundA.gameObject.SetActive(false);
                    BackgroundX.gameObject.SetActive(false);
                    BackgroundY.gameObject.SetActive(false);
                    imageB.GetComponent<Image>();
                }else if(allQuestions[indexQuestion].answered == 3){
                    BackgroundX.gameObject.SetActive(true);
                    BackgroundA.gameObject.SetActive(false);
                    BackgroundB.gameObject.SetActive(false);
                    BackgroundY.gameObject.SetActive(false);
                    imageX.GetComponent<Image>();  
                }else if(allQuestions[indexQuestion].answered == 4){
                    BackgroundY.gameObject.SetActive(true);
                    BackgroundA.gameObject.SetActive(false);
                    BackgroundB.gameObject.SetActive(false);
                    BackgroundX.gameObject.SetActive(false);
                    imageY.GetComponent<Image>();
                }
                questionTextImage.text = allQuestions[indexQuestion].question;
                foreach (var item in imagesUsed)
                {
                    if(allQuestions[indexQuestion].order[0] == (item.name + ".png")){
                        imageA.GetComponent<Image>().sprite = item;
                    }
                    if(allQuestions[indexQuestion].order[1] == (item.name + ".png")){
                        imageB.GetComponent<Image>().sprite = item;
                    }
                    if(allQuestions[indexQuestion].order[2] == (item.name + ".png")){
                        imageX.GetComponent<Image>().sprite = item;
                    }
                    if(allQuestions[indexQuestion].order[3] == (item.name + ".png")){
                        imageY.GetComponent<Image>().sprite = item;
                    }
                }
            }else{
                questionScreen.SetActive(false);
                imageAnswerScreen.SetActive(false);
                imageQuestionScreen.SetActive(true);
                buttonImageA.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                buttonImageB.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                buttonImageX.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                buttonImageY.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                if(allQuestions[indexQuestion].answered==1){
                    buttonImageA.GetComponent<Image>().color = new Color32(160, 224, 209, 255);
                }else if(allQuestions[indexQuestion].answered == 2){
                    buttonImageB.GetComponent<Image>().color = new Color32(160, 224, 209, 255);
                }else if(allQuestions[indexQuestion].answered == 3){
                    buttonImageX.GetComponent<Image>().color = new Color32(160, 224, 209, 255);  
                }else if(allQuestions[indexQuestion].answered == 4){
                    buttonImageY.GetComponent<Image>().color = new Color32(160, 224, 209, 255);
                }
                foreach (var item in imagesUsed)
                {
                    if(allQuestions[indexQuestion].question == (item.name + ".png")){
                        questionImage.GetComponent<Image>().sprite = item;
                    }
                }
                GameObject.Find("buttonImageA").GetComponentInChildren<Text>().text = allQuestions[indexQuestion].order[0];
                GameObject.Find("buttonImageB").GetComponentInChildren<Text>().text = allQuestions[indexQuestion].order[1];
                GameObject.Find("buttonImageX").GetComponentInChildren<Text>().text = allQuestions[indexQuestion].order[2];
                GameObject.Find("buttonImageY").GetComponentInChildren<Text>().text = allQuestions[indexQuestion].order[3];
            }
            previousButton.gameObject.SetActive(true);
            previousText.gameObject.SetActive(true);
        }
        
    }
    public void submitPreviousButton(){
        if(isFeedback){
            quizScreen.SetActive(false);
            feedbackScreen.SetActive(true);
        }else{
            BackgroundA.gameObject.SetActive(false);
            BackgroundX.gameObject.SetActive(false);
            BackgroundB.gameObject.SetActive(false);
            BackgroundY.gameObject.SetActive(false);
            indexQuestion--;
            if((indexQuestion==5 && leccion != 6) || (indexQuestion == 20 && leccion == 6)){
                columnScreen.SetActive(false);
                nextButton.gameObject.SetActive(true);
                nextText.gameObject.SetActive(true);
                nextText.text = "Siguiente";
                nextButton.GetComponent<Image>().sprite = rightDpad;
                
            }
            if(allQuestions[indexQuestion].is_image==0){
                questionScreen.SetActive(true);
                imageAnswerScreen.SetActive(false);
                imageQuestionScreen.SetActive(false);
                ButtonA.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                ButtonB.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                ButtonX.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                ButtonY.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                if(allQuestions[indexQuestion].answered==1){
                    ButtonA.GetComponent<Image>().color = new Color32(160, 224, 209, 255);
                }else if(allQuestions[indexQuestion].answered == 2){
                    ButtonB.GetComponent<Image>().color = new Color32(160, 224, 209, 255);
                }else if(allQuestions[indexQuestion].answered == 3){
                    ButtonX.GetComponent<Image>().color = new Color32(160, 224, 209, 255);  
                }else if(allQuestions[indexQuestion].answered == 4){
                    ButtonY.GetComponent<Image>().color = new Color32(160, 224, 209, 255);
                }
                questionText.text = allQuestions[indexQuestion].question;
                GameObject.Find("buttonA").GetComponentInChildren<Text>().text = allQuestions[indexQuestion].order[0];
                GameObject.Find("buttonB").GetComponentInChildren<Text>().text = allQuestions[indexQuestion].order[1];
                GameObject.Find("buttonX").GetComponentInChildren<Text>().text = allQuestions[indexQuestion].order[2];
                GameObject.Find("buttonY").GetComponentInChildren<Text>().text = allQuestions[indexQuestion].order[3];
            }else if(allQuestions[indexQuestion].is_image==1){
                questionScreen.SetActive(false);
                imageAnswerScreen.SetActive(true);
                imageQuestionScreen.SetActive(false);
                imageA.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                imageB.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                imageX.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                imageY.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                if(allQuestions[indexQuestion].answered==1){
                    BackgroundA.gameObject.SetActive(true);
                    BackgroundX.gameObject.SetActive(false);
                    BackgroundB.gameObject.SetActive(false);
                    BackgroundY.gameObject.SetActive(false);
                    imageA.GetComponent<Image>();
                }else if(allQuestions[indexQuestion].answered == 2){
                    BackgroundB.gameObject.SetActive(true);
                    BackgroundX.gameObject.SetActive(false);
                    BackgroundA.gameObject.SetActive(false);
                    BackgroundY.gameObject.SetActive(false);
                    imageB.GetComponent<Image>();
                }else if(allQuestions[indexQuestion].answered == 3){
                    BackgroundX.gameObject.SetActive(true);
                    BackgroundA.gameObject.SetActive(false);
                    BackgroundB.gameObject.SetActive(false);
                    BackgroundY.gameObject.SetActive(false);
                    imageX.GetComponent<Image>();  
                }else if(allQuestions[indexQuestion].answered == 4){
                    BackgroundY.gameObject.SetActive(true);
                    BackgroundA.gameObject.SetActive(false);
                    BackgroundB.gameObject.SetActive(false);
                    BackgroundX.gameObject.SetActive(false);
                    imageY.GetComponent<Image>();;
                }
                questionTextImage.text = allQuestions[indexQuestion].question;
                foreach (var item in imagesUsed)
                {
                    if(allQuestions[indexQuestion].order[0] == (item.name + ".png")){
                        imageA.GetComponent<Image>().sprite = item;
                    }
                    if(allQuestions[indexQuestion].order[1] == (item.name + ".png")){
                        imageB.GetComponent<Image>().sprite = item;
                    }
                    if(allQuestions[indexQuestion].order[2] == (item.name + ".png")){
                        imageX.GetComponent<Image>().sprite = item;
                    }
                    if(allQuestions[indexQuestion].order[3] ==(item.name + ".png")){
                        imageY.GetComponent<Image>().sprite = item;
                    }
                }
            }else{
                questionScreen.SetActive(false);
                imageAnswerScreen.SetActive(false);
                imageQuestionScreen.SetActive(true);
                buttonImageA.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                buttonImageB.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                buttonImageX.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                buttonImageY.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                if(allQuestions[indexQuestion].answered==1){
                    buttonImageA.GetComponent<Image>().color = new Color32(160, 224, 209, 255);
                }else if(allQuestions[indexQuestion].answered == 2){
                    buttonImageB.GetComponent<Image>().color = new Color32(160, 224, 209, 255);
                }else if(allQuestions[indexQuestion].answered == 3){
                    buttonImageX.GetComponent<Image>().color = new Color32(160, 224, 209, 255);  
                }else if(allQuestions[indexQuestion].answered == 4){
                    buttonImageY.GetComponent<Image>().color = new Color32(160, 224, 209, 255);
                }
                foreach (var item in imagesUsed)
                {
                    if(allQuestions[indexQuestion].question == (item.name + ".png")){
                        
                        questionImage.GetComponent<Image>().sprite = item;
                    }
                }
                GameObject.Find("buttonImageA").GetComponentInChildren<Text>().text = allQuestions[indexQuestion].order[0];
                GameObject.Find("buttonImageB").GetComponentInChildren<Text>().text = allQuestions[indexQuestion].order[1];
                GameObject.Find("buttonImageX").GetComponentInChildren<Text>().text = allQuestions[indexQuestion].order[2];
                GameObject.Find("buttonImageY").GetComponentInChildren<Text>().text = allQuestions[indexQuestion].order[3];
            }
            if(indexQuestion==0){
                previousButton.gameObject.SetActive(false);
                previousText.gameObject.SetActive(false);
            }
        }
    }
    

    public void submitDropdownPressed(int pressed){
        switch (pressed)
        {
            case 1:
                allColumns.answered[0] = dropdown1.value;
                break;
            case 2:
                allColumns.answered[1] = dropdown2.value;
                break;
            case 3:
                allColumns.answered[2] = dropdown3.value;
                break;
            case 4:
                allColumns.answered[3] = dropdown4.value;
                break;
            default:
                break;
        }
        bool finished = true;
        int dimension = 6;
        if(leccion == 6){dimension = 21;}
        for (int i = 0; i < dimension; i++)
        {
            if(allQuestions[i].answered == 0){
                finished = false;
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if(allColumns.answered[i]==0){
                finished = false;
            }
        }
        if(finished){
            nextText.text = "Terminar";
            nextButton.GetComponent<Image>().sprite = upDpad;
            nextButton.gameObject.SetActive(true);
            nextText.gameObject.SetActive(true);
        }else{
            nextButton.gameObject.SetActive(false);
            nextText.gameObject.SetActive(false);
        }
    }
    public void changeFeedback(int option){
        if(option == 1){
            screenFeed++;
            if(screenFeed == 2){
                screen1.SetActive(false);
                screen2.SetActive(true);
                screen3.SetActive(false);
                nextFeed.gameObject.SetActive(true);
                previousFeed.gameObject.SetActive(true);
            }else{
                screen1.SetActive(false);
                screen2.SetActive(false);
                screen3.SetActive(true);
                nextFeed.gameObject.SetActive(false);
                previousFeed.gameObject.SetActive(true);
            }
        }else{
            screenFeed--;
            if(screenFeed == 2){
                screen1.SetActive(false);
                screen2.SetActive(true);
                screen3.SetActive(false);
                nextFeed.gameObject.SetActive(true);
                previousFeed.gameObject.SetActive(true);
            }else{
                screen1.SetActive(true);
                screen2.SetActive(false);
                screen3.SetActive(false);
                nextFeed.gameObject.SetActive(true);
                previousFeed.gameObject.SetActive(false);
            }
        }
    }
    public void SubmitFeedback(int option){
        nextText.text = "Regresar";
        nextButton.gameObject.SetActive(false);
        nextText.gameObject.SetActive(false);
        isFeedback = true;
        feedbackScreen.SetActive(false);
        questionScreen.SetActive(true);
        imageAnswerScreen.SetActive(false);
        imageQuestionScreen.SetActive(false);
        columnScreen.SetActive(false);
        quizScreen.SetActive(true);
        buttonImageA.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        buttonImageB.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        buttonImageX.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        buttonImageY.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        ButtonA.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        ButtonB.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        ButtonX.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        ButtonY.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        BackgroundA.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        BackgroundB.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        BackgroundX.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        BackgroundY.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        questionA.GetComponent<Image>().sprite = mark;
        questionB.GetComponent<Image>().sprite = mark;
        questionX.GetComponent<Image>().sprite = mark;
        questionY.GetComponent<Image>().sprite = mark;
        questionA1.GetComponent<Image>().sprite = mark;
        questionB1.GetComponent<Image>().sprite = mark;
        questionX1.GetComponent<Image>().sprite = mark;
        questionY1.GetComponent<Image>().sprite = mark;
        questionA2.GetComponent<Image>().sprite = mark;
        questionB2.GetComponent<Image>().sprite = mark;
        questionX2.GetComponent<Image>().sprite = mark;
        questionY2.GetComponent<Image>().sprite = mark;
        ButtonA.interactable = false;
        ButtonB.interactable = false;
        ButtonX.interactable = false;
        ButtonY.interactable = false;
        imageA.interactable = false;
        imageB.interactable = false;
        imageX.interactable = false;
        imageY.interactable = false;
        buttonImageA.interactable = false;
        buttonImageB.interactable = false;
        buttonImageX.interactable = false;
        buttonImageY.interactable = false;
        int dimensionQ = 6;
        if(leccion == 6){dimensionQ = 21;}
        if(option>dimensionQ){
            questionText.text = allColumns.questions[option-(dimensionQ+1)];
            GameObject.Find("buttonA").GetComponentInChildren<Text>().text = allColumns.answers[0];
            GameObject.Find("buttonB").GetComponentInChildren<Text>().text = allColumns.answers[1];
            GameObject.Find("buttonX").GetComponentInChildren<Text>().text = allColumns.answers[2];
            GameObject.Find("buttonY").GetComponentInChildren<Text>().text = allColumns.answers[3];
            
            if(allColumns.answered[option-(dimensionQ+1)]==allColumns.corrects[option-(dimensionQ+1)]){
                switch(allColumns.answered[option-(dimensionQ+1)]){
                    case 1:{
                        ButtonA.GetComponent<Image>().color = new Color32(18, 200, 170, 255);
                        questionA.GetComponent<Image>().sprite = checkmark;
                        break;
                    }
                    case 2:{
                        ButtonB.GetComponent<Image>().color = new Color32(18, 200, 170, 255);
                        questionB.GetComponent<Image>().sprite = checkmark;
                        break;
                    }
                    case 3:{
                        ButtonX.GetComponent<Image>().color = new Color32(18, 200, 170, 255);
                        questionX.GetComponent<Image>().sprite = checkmark;
                        break;
                    }
                    case 4:{
                        ButtonY.GetComponent<Image>().color = new Color32(18, 200, 170, 255);
                        questionY.GetComponent<Image>().sprite = checkmark;
                        break;
                    }
                    default:{
                        break;
                    }
                }
            }else{
                switch(allColumns.answered[option-(dimensionQ+1)]){
                    case 1:{
                        ButtonA.GetComponent<Image>().color = new Color32(214, 47, 47, 255);
                        questionA.GetComponent<Image>().sprite = wrongmark;
                        break;
                    }
                    case 2:{
                        ButtonB.GetComponent<Image>().color = new Color32(214, 47, 47, 255);
                        questionB.GetComponent<Image>().sprite = wrongmark;
                        break;
                    }
                    case 3:{
                        ButtonX.GetComponent<Image>().color = new Color32(214, 47, 47, 255);
                        questionX.GetComponent<Image>().sprite = wrongmark;
                        break;
                    }
                    case 4:{
                        ButtonY.GetComponent<Image>().color = new Color32(214, 47, 47, 255);
                        questionY.GetComponent<Image>().sprite = wrongmark;
                        break;
                    }
                    default:{
                        break;
                    }
                } 
                switch(allColumns.corrects[option-(dimensionQ+1)]){
                    case 1:{
                        ButtonA.GetComponent<Image>().color = new Color32(235, 231, 114, 255);
                        questionA.GetComponent<Image>().sprite = checkmark;
                        break;
                    }
                    case 2:{
                        ButtonB.GetComponent<Image>().color = new Color32(235, 231, 114, 255);
                        questionB.GetComponent<Image>().sprite = checkmark;
                        break;
                    }
                    case 3:{
                        ButtonX.GetComponent<Image>().color = new Color32(235, 231, 114, 255);
                        questionX.GetComponent<Image>().sprite = checkmark;
                        break;
                    }
                    case 4:{
                        ButtonY.GetComponent<Image>().color = new Color32(235, 231, 114, 255);
                        questionY.GetComponent<Image>().sprite = checkmark;
                        break;
                    }
                    default:{
                        break;
                    }
                }
            }
        }else{
            imageAnswerScreen.SetActive(false);
            questionScreen.SetActive(false);
            imageQuestionScreen.SetActive(false);
            if(allQuestions[option-1].is_image==0){
                questionScreen.SetActive(true);
                questionText.text = allQuestions[option-1].question;
                GameObject.Find("buttonA").GetComponentInChildren<Text>().text = allQuestions[option-1].order[0];
                GameObject.Find("buttonB").GetComponentInChildren<Text>().text = allQuestions[option-1].order[1];
                GameObject.Find("buttonX").GetComponentInChildren<Text>().text = allQuestions[option-1].order[2];
                GameObject.Find("buttonY").GetComponentInChildren<Text>().text = allQuestions[option-1].order[3];
                if(allQuestions[option-1].answered==allQuestions[option-1].correct){
                    switch(allQuestions[option-1].answered){
                        case 1:{
                            ButtonA.GetComponent<Image>().color = new Color32(18, 200, 170, 255);
                            questionA.GetComponent<Image>().sprite = checkmark;
                            break;
                        }
                        case 2:{
                            ButtonB.GetComponent<Image>().color = new Color32(18, 200, 170, 255);
                            questionB.GetComponent<Image>().sprite = checkmark;
                            break;
                        }
                        case 3:{
                            ButtonX.GetComponent<Image>().color = new Color32(18, 200, 170, 255);
                            questionX.GetComponent<Image>().sprite = checkmark;
                            break;
                        }
                        case 4:{
                            ButtonY.GetComponent<Image>().color = new Color32(18, 200, 170, 255);
                            questionY.GetComponent<Image>().sprite = checkmark;
                            break;
                        }
                        default:{
                            break;
                        }
                    }
                }else{
                    switch(allQuestions[option-1].answered){
                        case 1:{
                            ButtonA.GetComponent<Image>().color = new Color32(214, 47, 47, 255);
                            questionA.GetComponent<Image>().sprite = wrongmark;
                            break;
                        }
                        case 2:{
                            ButtonB.GetComponent<Image>().color = new Color32(214, 47, 47, 255);
                            questionB.GetComponent<Image>().sprite = wrongmark;
                            break;
                        }
                        case 3:{
                            ButtonX.GetComponent<Image>().color = new Color32(214, 47, 47, 255);
                            questionX.GetComponent<Image>().sprite = wrongmark;
                            break;
                        }
                        case 4:{
                            ButtonY.GetComponent<Image>().color = new Color32(214, 47, 47, 255);
                            questionY.GetComponent<Image>().sprite = wrongmark;
                            break;
                        }
                        default:{
                            break;
                        }
                    }
                    switch(allQuestions[option-1].correct){
                        case 1:{
                            ButtonA.GetComponent<Image>().color = new Color32(235, 231, 114, 255);
                            questionA.GetComponent<Image>().sprite = checkmark;
                            break;
                        }
                        case 2:{
                            ButtonB.GetComponent<Image>().color = new Color32(235, 231, 114, 255);
                            questionB.GetComponent<Image>().sprite = checkmark;
                            break;
                        }
                        case 3:{
                            ButtonX.GetComponent<Image>().color = new Color32(235, 231, 114, 255);
                            questionX.GetComponent<Image>().sprite = checkmark;
                            break;
                        }
                        case 4:{
                            ButtonY.GetComponent<Image>().color = new Color32(235, 231, 114, 255);
                            questionY.GetComponent<Image>().sprite = checkmark;
                            break;
                        }
                        default:{
                            break;
                        }
                    }
                }
            }else if(allQuestions[option-1].is_image==1){
                imageAnswerScreen.SetActive(true);
                questionTextImage.text = allQuestions[option-1].question;
                foreach (var item in imagesUsed)
                {
                    if(allQuestions[option-1].order[0] == (item.name + ".png")){
                        imageA.GetComponent<Image>().sprite = item;
                    }
                    if(allQuestions[option-1].order[1] == (item.name + ".png")){
                        imageB.GetComponent<Image>().sprite = item;
                    }
                    if(allQuestions[option-1].order[2] == (item.name + ".png")){
                        imageX.GetComponent<Image>().sprite = item;
                    }
                    if(allQuestions[option-1].order[3] == (item.name + ".png")){
                        imageY.GetComponent<Image>().sprite = item;
                    }
                }
                BackgroundA.gameObject.SetActive(false);
                BackgroundB.gameObject.SetActive(false);
                BackgroundX.gameObject.SetActive(false);
                BackgroundY.gameObject.SetActive(false);
                if(allQuestions[option-1].answered==allQuestions[option-1].correct){
                    switch(allQuestions[option-1].answered){
                        case 1:{
                            BackgroundA.gameObject.SetActive(true);
                            BackgroundA.GetComponent<Image>().color = new Color32(18, 200, 170, 255);
                            questionA2.GetComponent<Image>().sprite = checkmark;
                            break;
                        }
                        case 2:{
                            BackgroundB.gameObject.SetActive(true);
                            BackgroundB.GetComponent<Image>().color = new Color32(18, 200, 170, 255);
                            questionB2.GetComponent<Image>().sprite = checkmark;
                            break;
                        }
                        case 3:{
                            BackgroundX.gameObject.SetActive(true);
                            BackgroundX.GetComponent<Image>().color = new Color32(18, 200, 170, 255);
                            questionX2.GetComponent<Image>().sprite = checkmark;
                            break;
                        }
                        case 4:{
                            BackgroundY.gameObject.SetActive(true);
                            BackgroundY.GetComponent<Image>().color = new Color32(18, 200, 170, 255);
                            questionY2.GetComponent<Image>().sprite = checkmark;
                            break;
                        }
                        default:{
                            break;
                        }
                    }
                }else{
                    switch(allQuestions[option-1].answered){
                        case 1:{
                            BackgroundA.gameObject.SetActive(true);
                            BackgroundA.GetComponent<Image>().color = new Color32(214, 47, 47, 255);
                            questionA2.GetComponent<Image>().sprite = wrongmark;
                            break;
                        }
                        case 2:{
                            BackgroundB.gameObject.SetActive(true);
                            BackgroundB.GetComponent<Image>().color = new Color32(214, 47, 47, 255);
                            questionB2.GetComponent<Image>().sprite = wrongmark;
                            break;
                        }
                        case 3:{
                            BackgroundX.gameObject.SetActive(true);
                            BackgroundX.GetComponent<Image>().color = new Color32(214, 47, 47, 255);
                            questionX2.GetComponent<Image>().sprite = wrongmark;
                            break;
                        }
                        case 4:{
                            BackgroundY.gameObject.SetActive(true);
                            BackgroundY.GetComponent<Image>().color = new Color32(214, 47, 47, 255);
                            questionY2.GetComponent<Image>().sprite = wrongmark;
                            break;
                        }
                        default:{
                            break;
                        }
                    }
                    switch(allQuestions[option-1].correct){
                        case 1:{
                            BackgroundA.gameObject.SetActive(true);
                            BackgroundA.GetComponent<Image>().color = new Color32(235, 231, 114, 255);
                            questionA2.GetComponent<Image>().sprite = checkmark;
                            break;
                        }
                        case 2:{
                            BackgroundB.gameObject.SetActive(true);
                            BackgroundB.GetComponent<Image>().color = new Color32(235, 231, 114, 255);
                            questionB2.GetComponent<Image>().sprite = checkmark;
                            break;
                        }
                        case 3:{
                            BackgroundX.gameObject.SetActive(true);
                            BackgroundX.GetComponent<Image>().color = new Color32(235, 231, 114, 255);
                            questionX2.GetComponent<Image>().sprite = checkmark;
                            break;
                        }
                        case 4:{
                            BackgroundY.gameObject.SetActive(true);
                            BackgroundY.GetComponent<Image>().color = new Color32(235, 231, 114, 255);
                            questionY2.GetComponent<Image>().sprite = checkmark;
                            break;
                        }
                        default:{
                            break;
                        }
                    }
                }
            }else{
                imageQuestionScreen.SetActive(true);
                foreach (var item in imagesUsed)
                {
                    if(allQuestions[option-1].question == (item.name + ".png")){
                        questionImage.GetComponent<Image>().sprite = item;
                    }
                }
                GameObject.Find("buttonImageA").GetComponentInChildren<Text>().text = allQuestions[option-1].order[0];
                GameObject.Find("buttonImageB").GetComponentInChildren<Text>().text = allQuestions[option-1].order[1];
                GameObject.Find("buttonImageX").GetComponentInChildren<Text>().text = allQuestions[option-1].order[2];
                GameObject.Find("buttonImageY").GetComponentInChildren<Text>().text = allQuestions[option-1].order[3];
                if(allQuestions[option-1].answered==allQuestions[option-1].correct){
                    switch(allQuestions[option-1].answered){
                        case 1:{
                            buttonImageA.GetComponent<Image>().color = new Color32(18, 200, 170, 255);
                            questionA1.GetComponent<Image>().sprite = checkmark;
                            break;
                        }
                        case 2:{
                            buttonImageB.GetComponent<Image>().color = new Color32(18, 200, 170, 255);
                            questionB1.GetComponent<Image>().sprite = checkmark;
                            break;
                        }
                        case 3:{
                            buttonImageX.GetComponent<Image>().color = new Color32(18, 200, 170, 255);
                            questionX1.GetComponent<Image>().sprite = checkmark;
                            break;
                        }
                        case 4:{
                            buttonImageY.GetComponent<Image>().color = new Color32(18, 200, 170, 255);
                            questionY1.GetComponent<Image>().sprite = checkmark;
                            break;
                        }
                        default:{
                            break;
                        }
                    }
                }else{
                    switch(allQuestions[option-1].answered){
                        case 1:{
                            buttonImageA.GetComponent<Image>().color = new Color32(214, 47, 47, 255);
                            questionA1.GetComponent<Image>().sprite = wrongmark;
                            break;
                        }
                        case 2:{
                            buttonImageB.GetComponent<Image>().color = new Color32(214, 47, 47, 255);
                            questionB1.GetComponent<Image>().sprite = wrongmark;
                            break;
                        }
                        case 3:{
                            buttonImageX.GetComponent<Image>().color = new Color32(214, 47, 47, 255);
                            questionX1.GetComponent<Image>().sprite = wrongmark;
                            break;
                        }
                        case 4:{
                            buttonImageY.GetComponent<Image>().color = new Color32(214, 47, 47, 255);
                            questionY1.GetComponent<Image>().sprite = wrongmark;
                            break;
                        }
                        default:{
                            break;
                        }
                    }
                    switch(allQuestions[option-1].correct){
                        case 1:{
                            buttonImageA.GetComponent<Image>().color = new Color32(235, 231, 114, 255);
                            questionA1.GetComponent<Image>().sprite = checkmark;
                            break;
                        }
                        case 2:{
                            buttonImageB.GetComponent<Image>().color = new Color32(235, 231, 114, 255);
                            questionB1.GetComponent<Image>().sprite = checkmark;
                            break;
                        }
                        case 3:{
                            buttonImageX.GetComponent<Image>().color = new Color32(235, 231, 114, 255);
                            questionX1.GetComponent<Image>().sprite = checkmark;
                            break;
                        }
                        case 4:{
                            buttonImageY.GetComponent<Image>().color = new Color32(235, 231, 114, 255);
                            questionY1.GetComponent<Image>().sprite = checkmark;
                            break;
                        }
                        default:{
                            break;
                        }
                    }
                }
            }
        }
    }
    private IEnumerator RegisterInfo(int grade){
        int dimension = 6;
        if(leccion == 6){dimension = 21;}
        WWWForm form = new WWWForm();
        WWW w;
        if(UserInfo.examPuntaje[ExamNumber.examNumber-1]!=-1){
            form = new WWWForm();
            form.AddField("option", 1);
            form.AddField("username", UserInfo.username);
            form.AddField("lesson", ExamNumber.examNumber);
            w = new WWW("http://izyventa.com/elena/Examen/registerInfo.php", form);
            yield return w;
            for(int i = 0; i < dimension; i++){
                UserInfo.questionsArray[ExamNumber.examNumber-1][i] = allQuestions[i];
            }
        }else{
            UserInfo.questionsArray[ExamNumber.examNumber-1] = new List<Questions>();
            for(int i = 0; i < dimension; i++){
                UserInfo.questionsArray[ExamNumber.examNumber-1].Add(allQuestions[i]);
            }
            
        }
        UserInfo.examPuntaje[ExamNumber.examNumber-1] = grade;
        
        UserInfo.columnsArray[ExamNumber.examNumber-1] = allColumns;

        form = new WWWForm();
        form.AddField("option", 2);
        form.AddField("questions1", UserInfo.columnsArray[ExamNumber.examNumber-1].questions[0]);
        form.AddField("questions2", UserInfo.columnsArray[ExamNumber.examNumber-1].questions[1]);
        form.AddField("questions3", UserInfo.columnsArray[ExamNumber.examNumber-1].questions[2]);
        form.AddField("questions4", UserInfo.columnsArray[ExamNumber.examNumber-1].questions[3]);
        form.AddField("answers1", UserInfo.columnsArray[ExamNumber.examNumber-1].answers[0]);
        form.AddField("answers2", UserInfo.columnsArray[ExamNumber.examNumber-1].answers[1]);
        form.AddField("answers3", UserInfo.columnsArray[ExamNumber.examNumber-1].answers[2]);
        form.AddField("answers4", UserInfo.columnsArray[ExamNumber.examNumber-1].answers[3]);
        form.AddField("corrects1", UserInfo.columnsArray[ExamNumber.examNumber-1].corrects[0]);
        form.AddField("corrects2", UserInfo.columnsArray[ExamNumber.examNumber-1].corrects[1]);
        form.AddField("corrects3", UserInfo.columnsArray[ExamNumber.examNumber-1].corrects[2]);
        form.AddField("corrects4", UserInfo.columnsArray[ExamNumber.examNumber-1].corrects[3]);
        form.AddField("answered1", UserInfo.columnsArray[ExamNumber.examNumber-1].answered[0]);
        form.AddField("answered2", UserInfo.columnsArray[ExamNumber.examNumber-1].answered[1]);
        form.AddField("answered3", UserInfo.columnsArray[ExamNumber.examNumber-1].answered[2]);
        form.AddField("answered4", UserInfo.columnsArray[ExamNumber.examNumber-1].answered[3]);
        w = new WWW("http://izyventa.com/elena/Examen/registerInfo.php", form);
        yield return w;
        int idModulos = Int32.Parse(w.text);

        form = new WWWForm();
        form.AddField("option", 3);
        form.AddField("username", UserInfo.username);
        form.AddField("lesson", ExamNumber.examNumber);
        form.AddField("id_columns", idModulos);
        form.AddField("score", UserInfo.examPuntaje[ExamNumber.examNumber-1]);
        w = new WWW("http://izyventa.com/elena/Examen/registerInfo.php", form);
        yield return w;

        for(int i = 0; i < dimension; i++){
            form = new WWWForm();
            form.AddField("option", 4);
            form.AddField("username", UserInfo.username);
            form.AddField("lesson", ExamNumber.examNumber);
            form.AddField("question", UserInfo.questionsArray[ExamNumber.examNumber-1][i].question);
            form.AddField("order1", UserInfo.questionsArray[ExamNumber.examNumber-1][i].order[0]);
            form.AddField("order2", UserInfo.questionsArray[ExamNumber.examNumber-1][i].order[1]);
            form.AddField("order3", UserInfo.questionsArray[ExamNumber.examNumber-1][i].order[2]);
            form.AddField("order4", UserInfo.questionsArray[ExamNumber.examNumber-1][i].order[3]);
            form.AddField("correct", UserInfo.questionsArray[ExamNumber.examNumber-1][i].correct);
            form.AddField("answered", UserInfo.questionsArray[ExamNumber.examNumber-1][i].answered);
            form.AddField("is_image", UserInfo.questionsArray[ExamNumber.examNumber-1][i].is_image);
            w = new WWW("http://izyventa.com/elena/Examen/registerInfo.php", form);
            yield return w;
        }
        quizScreen.SetActive(false);
        feedbackScreen.SetActive(true);
        startScreen.SetActive(false);
    }

     private IEnumerator COloadExam(int lesson){
        int numberQuestions = 0, numberAnswers = 0, numberQuestionspool = 0;

        WWW ww = new WWW("http://izyventa.com/elena/Examen/loadNumber.php");
        yield return ww;
        numberQuestions = Int32.Parse(ww.text);
        numberAnswers = numberQuestions*4;
        numberQuestionspool = numberQuestions*3;
        List<int> numbers = new List<int>();
        if(questions.Count == 0){
            
            WWWForm form = new WWWForm();
            form.AddField("id", -1);
            WWW w = new WWW("http://izyventa.com/elena/Examen/loadQuestion.php", form);
            yield return w;
            string [] questionsDB = w.text.Split('[');
            for(int i = 0; i < questionsDB.Count()-1; i++){
                string [] questionDB = questionsDB[i].Split(']');
                if(Int32.Parse(questionDB[1]) == lesson || lesson == 6){
                    bool imageAux = false;
                    if(questionDB[3] == "true"){
                        imageAux = true;
                    }
                    int id_question_aux = Int32.Parse(questionDB[0]);
                    int id_theoretical_lesson_aux = Int32.Parse(questionDB[1]);
                    questions.Add(new Question(){
                        id_question = id_question_aux,
                        id_theoretical_lesson = id_theoretical_lesson_aux, 
                        question = questionDB[2],
                        is_image = imageAux
                    });
                }
            }

            form = new WWWForm();
            form.AddField("id", -1);
            w = new WWW("http://izyventa.com/elena/Examen/loadAnswer.php", form);
            yield return w;
            string [] answersDB = w.text.Split('[');
            for(int i = 0; i < answersDB.Count()-1; i++){
                string [] answerDB = answersDB[i].Split(']');
                foreach (var item in questions)
                {
                    if(Int32.Parse(answerDB[1]) == item.id_question){
                        int id_answer_aux = Int32.Parse(answerDB[0]);
                        int id_question_aux = Int32.Parse(answerDB[1]);
                        int question_number_aux = Int32.Parse(answerDB[2]);
                        bool correctAux = false;
                        if(answerDB[4] == "true"){
                            correctAux = true;
                        }
                        answers.Add(new Answer(){
                            id_answer = id_answer_aux,
                            id_question = id_question_aux,
                            question_number = question_number_aux,
                            answer = answerDB[3],
                            is_correct = correctAux
                        });
                    }
                }
            }

            form = new WWWForm();
            form.AddField("id", -1);
            w = new WWW("http://izyventa.com/elena/Examen/loadQuestionpool.php", form);
            yield return w;
            string [] questionpoolsDB = w.text.Split('[');
            for(int i = 0; i < questionpoolsDB.Count()-1; i++){
                string [] questionpoolDB = questionpoolsDB[i].Split(']');
                foreach (var item in questions)
                {
                    if(Int32.Parse(questionpoolDB[1]) == item.id_question){
                        bool imageAux = false;
                        if(questionpoolDB[3] == "true"){
                            imageAux = true;
                        }
                        int id_question_aux = Int32.Parse(questionpoolDB[1]);
                        int id_question_pool_aux = Int32.Parse(questionpoolDB[0]);
                        questionpools.Add(new Questionpool(){
                            id_question = id_question_aux,
                            id_question_pool = id_question_pool_aux, 
                            question = questionpoolDB[2],
                            is_image = imageAux
                        });
                    }
                }
            }
            /*
            for(int i = 0; i < numberQuestions; i++){
                WWWForm form = new WWWForm();
                form.AddField("id", i+1);

                WWW w = new WWW("http://izyventa.com/elena/Examen/loadQuestion.php", form);

                yield return w;
                Question questionAux = new Question();
                questionAux = JsonUtility.FromJson<Question>(w.text);
                if(questionAux.id_theoretical_lesson == lesson || lesson == 6){
                    questions.Add(questionAux);
                }
                
            }
            for(int i = 0; i < numberQuestionspool; i++){
                WWWForm form = new WWWForm();
                form.AddField("id", i+1);

                WWW w = new WWW("http://izyventa.com/elena/Examen/loadQuestionpool.php", form);

                yield return w;
                Questionpool questionAux = new Questionpool();
                questionAux = JsonUtility.FromJson<Questionpool>(w.text);
                foreach (var item in questions)
                {
                    if(questionAux.id_question == item.id_question){
                        questionpools.Add(questionAux);
                    }
                }
            }
            for(int i = 0; i < numberAnswers; i++){
                
                WWWForm form = new WWWForm();
                form.AddField("id", i+1);

                WWW w = new WWW("http://izyventa.com/elena/Examen/loadAnswer.php", form);
                

                yield return w;
                Answer answerAux = new Answer();
                answerAux = JsonUtility.FromJson<Answer>(w.text);
                foreach (var item in questions)
                {
                    if(answerAux.id_question == item.id_question){
                        answers.Add(answerAux);
                    }
                }
            }
            */
        }
        numberQuestions = questions.Count;
        numberQuestionspool = questionpools.Count;
        numberAnswers = answers.Count;
        if(lesson == 6){
            List<Question> questionsAux = new List<Question>();
            for(int i = 1;  i<6; i++){
                for(int j = 0; j<5; j++){
                    int indexQuestion = rng.Next(questions[0].id_question, questions[0].id_question+numberQuestions);
                    bool ya = false;
                    foreach (var item in questionsAux)
                    {
                        if(item.id_question == indexQuestion){
                            ya = true;
                        }
                    }
                    if(!ya){
                        foreach (var item in questions)
                        {
                            if(item.id_question == indexQuestion){
                                if(item.id_theoretical_lesson==i){
                                    questionsAux.Add(item);
                                }else{
                                    j--;
                                }
                            }
                        }
                    }else{
                        j--;
                    }
                }
            }
            questions = new List<Question>();
            bool repeat = false;
            do{
                numbers = new List<int>();
                for(int i = 0; i<25; i++){
                    numbers.Add(i);
                }
                numbers = Shuffle(numbers);
                repeat = false;
                for(int i = 21; i<25; i++){
                    if(numbers[i]>=10 && numbers[i] <= 14){
                        repeat = true;
                    }
                }
            }while(repeat);
            for(int i = 0; i<25; i++){
                questions.Add(questionsAux[numbers[i]]);
            }
            /*for(int i = 9; i > -1; i--){
                int numberAux = rng.Next(0, 9);
                bool ya = false;
                foreach (var item in questions)
                {
                    if(item.id_question == questionsAux[numberAux].id_question){
                        ya = true;
                    }
                }
                if(!ya){
                    if(i > 5 && questionsAux[numberAux].id_theoretical_lesson == 3){
                        i++;
                    }else{
                        questions.Add(questionsAux[numberAux]);
                    }
                }else{
                    i++;
                }
            }*/
            numbers = new List<int>();
            for(int i = 0; i < 25; i++){
                numbers.Add(questions[i].id_question);
            }
            numberQuestions = questions.Count;
            foreach (var item in questions)
            {
                Debug.Log(item.id_theoretical_lesson);
            }

            












            for(int i = 0; i < 25; i++){
                List<Questionpool> questionpoolAux = new List<Questionpool>();
                List<Answer> answersAux = new List<Answer>();
                List<Answer> answersAux1 = new List<Answer>();
                List<String> stringAux = new List<String>();
                Answer answerAux = new Answer();
                if(i<10){
                    foreach (var item in answers)
                    {
                        if(item.id_question==numbers[i]){
                            answersAux.Add(item);
                        }
                    }
                    foreach(var item in questions){
                        if(item.id_question==numbers[i]){
                            allQuestions.Add(new Questions(){
                                question = item.question
                            });
                            if(item.is_image){
                                allQuestions[i].is_image = 2;
                            }else{
                                allQuestions[i].is_image = 0;
                            }
                        }
                    }
                    foreach (var item in answersAux)
                    {
                        if(item.is_correct){
                            allQuestions[i].answer1 = item.answer;
                        }else{
                            answersAux1.Add(item);
                        }
                    }
                    allQuestions[i].answer2 = answersAux1[0].answer;
                    allQuestions[i].answer3 = answersAux1[1].answer;
                    allQuestions[i].answer4 = answersAux1[2].answer;
                    stringAux.Add(allQuestions[i].answer1);
                    stringAux.Add(allQuestions[i].answer2);
                    stringAux.Add(allQuestions[i].answer3);
                    stringAux.Add(allQuestions[i].answer4);
                    stringAux = Shuffle(stringAux);
                    for(int j = 0; j < 4; j++){
                        allQuestions[i].order[j] = stringAux[j];
                        if(stringAux[j]==allQuestions[i].answer1){
                            allQuestions[i].correct = j+1;
                        }
                    }
                }else if(i>20){
                    int removeNumber = -1;
                    foreach (var item in answers)
                    {
                        if(item.id_question==numbers[i] && item.is_correct==true){
                            answerAux = item;
                        }
                    }
                    foreach(var item in questions){
                        if(item.id_question==numbers[i]){
                            if(item.is_image){
                                i--;
                                //numbers.RemoveAt(i+1);
                                removeNumber = i+1;
                                break;
                            }else{
                                allColumns.questions[i-21] = item.question;
                                allColumns.answers[i-21] = answerAux.answer;
                            }
                        }
                    }
                    if(removeNumber > -1){
                        while(removeNumber < numbers.Count-1){
                            numbers[removeNumber] = numbers[removeNumber+1];
                            removeNumber++;
                        }
                        numbers[numbers.Count-1] = -1;
                    }
                }else{
                    foreach (var item in answers)
                    {
                        if(item.id_question==numbers[i] && item.is_correct==true){
                            answerAux = item;
                        }
                    }
                    foreach (var item in questionpools)
                    {
                        if(item.id_question==numbers[i]){
                            questionpoolAux.Add(item);
                        }
                    }
                    foreach(var item in questions){
                        if(item.id_question==numbers[i]){

                            allQuestions.Add(new Questions(){
                                question = answerAux.answer,
                                answer1 = item.question, 
                                answer2 = questionpoolAux[0].question,
                                answer3 = questionpoolAux[1].question,
                                answer4 = questionpoolAux[2].question
                            });
                            if(item.is_image){
                                allQuestions[i].is_image = 1;
                            }else{
                                allQuestions[i].is_image = 0;
                            }
                        }
                    }
                    stringAux.Add(allQuestions[i].answer1);
                    stringAux.Add(allQuestions[i].answer2);
                    stringAux.Add(allQuestions[i].answer3);
                    stringAux.Add(allQuestions[i].answer4);
                stringAux =  Shuffle(stringAux);
                    for(int j = 0; j < 4; j++){
                        allQuestions[i].order[j] = stringAux[j];
                        if(stringAux[j]==allQuestions[i].answer1){
                            allQuestions[i].correct = j+1;
                        }
                    }
                }
                
            }














        }else{
            for(int i = questions[0].id_question; i<questions[0].id_question+numberQuestions; i++)
            {
                numbers.Add(i);
            }
            numbers = Shuffle(numbers);
        
        
            for(int i = 0; i < 10; i++){
                List<Questionpool> questionpoolAux = new List<Questionpool>();
                List<Answer> answersAux = new List<Answer>();
                List<Answer> answersAux1 = new List<Answer>();
                List<String> stringAux = new List<String>();
                Answer answerAux = new Answer();
                if(i<3){
                    foreach (var item in answers)
                    {
                        if(item.id_question==numbers[i]){
                            answersAux.Add(item);
                        }
                    }
                    foreach(var item in questions){
                        if(item.id_question==numbers[i]){
                            allQuestions.Add(new Questions(){
                                question = item.question
                            });
                            if(item.is_image){
                                allQuestions[i].is_image = 2;
                            }else{
                                allQuestions[i].is_image = 0;
                            }
                        }
                    }
                    foreach (var item in answersAux)
                    {
                        if(item.is_correct){
                            allQuestions[i].answer1 = item.answer;
                        }else{
                            answersAux1.Add(item);
                        }
                    }
                    allQuestions[i].answer2 = answersAux1[0].answer;
                    allQuestions[i].answer3 = answersAux1[1].answer;
                    allQuestions[i].answer4 = answersAux1[2].answer;
                    stringAux.Add(allQuestions[i].answer1);
                    stringAux.Add(allQuestions[i].answer2);
                    stringAux.Add(allQuestions[i].answer3);
                    stringAux.Add(allQuestions[i].answer4);
                    stringAux = Shuffle(stringAux);
                    for(int j = 0; j < 4; j++){
                        allQuestions[i].order[j] = stringAux[j];
                        if(stringAux[j]==allQuestions[i].answer1){
                            allQuestions[i].correct = j+1;
                        }
                    }
                }else if(i>5){
                    int removeNumber = -1;
                    foreach (var item in answers)
                    {
                        if(item.id_question==numbers[i] && item.is_correct==true){
                            answerAux = item;
                        }
                    }
                    foreach(var item in questions){
                        if(item.id_question==numbers[i]){
                            if(item.is_image){
                                i--;
                                //numbers.RemoveAt(i+1);
                                removeNumber = i+1;
                                break;
                            }else{
                                allColumns.questions[i-6] = item.question;
                                allColumns.answers[i-6] = answerAux.answer;
                            }
                        }
                    }
                    if(removeNumber > -1){
                        while(removeNumber < numbers.Count-1){
                            numbers[removeNumber] = numbers[removeNumber+1];
                            removeNumber++;
                        }
                        numbers[numbers.Count-1] = -1;
                    }
                }else{
                    foreach (var item in answers)
                    {
                        if(item.id_question==numbers[i] && item.is_correct==true){
                            answerAux = item;
                        }
                    }
                    foreach (var item in questionpools)
                    {
                        if(item.id_question==numbers[i]){
                            questionpoolAux.Add(item);
                        }
                    }
                    foreach(var item in questions){
                        if(item.id_question==numbers[i]){

                            allQuestions.Add(new Questions(){
                                question = answerAux.answer,
                                answer1 = item.question, 
                                answer2 = questionpoolAux[0].question,
                                answer3 = questionpoolAux[1].question,
                                answer4 = questionpoolAux[2].question
                            });
                            if(item.is_image){
                                allQuestions[i].is_image = 1;
                            }else{
                                allQuestions[i].is_image = 0;
                            }
                        }
                    }
                    stringAux.Add(allQuestions[i].answer1);
                    stringAux.Add(allQuestions[i].answer2);
                    stringAux.Add(allQuestions[i].answer3);
                    stringAux.Add(allQuestions[i].answer4);
                stringAux =  Shuffle(stringAux);
                    for(int j = 0; j < 4; j++){
                        allQuestions[i].order[j] = stringAux[j];
                        if(stringAux[j]==allQuestions[i].answer1){
                            allQuestions[i].correct = j+1;
                        }
                    }
                }
                
            }
        }
        List<int> intAux = new List<int>();
        intAux.Add(0);
        intAux.Add(1);
        intAux.Add(2);
        intAux.Add(3);
        intAux = Shuffle( intAux);
        String[] columnsAux = new String[]{allColumns.answers[0], allColumns.answers[1], allColumns.answers[2], allColumns.answers[3]};
        
        for(int j = 0; j < 4; j++){
            allColumns.corrects[intAux[j]] = j + 1;
            allColumns.answers[j] = columnsAux[intAux[j]];
        }
        
        startButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
        confirmText.text = "¿Deseas iniciar tu examen?";
        
    }
    
    public List<T> Shuffle<T>( List<T> list)  
    {  
        
        int n = list.Count;  
        List<int> orderAux = new List<int>();
        for (int i = 0; i < n; i++)
        {
            int k = 0;
            do
            {
                k = rng.Next(n);
            } while (orderAux.Exists(x => x == k));
            orderAux.Add(k);
        }
        List<T> returnvalue = new List<T>();
        for (int i = 0; i < n; i++)
        {
            returnvalue.Add(list[orderAux[i]]);
        }
        return returnvalue;
    }
}
