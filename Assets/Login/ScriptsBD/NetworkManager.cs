using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour 
{

    [SerializeField] private Button buttonLogin = null;
    [SerializeField] private Button buttonRegister = null;

    public void CreateUser(string username, string email, string pass, string firstName, string lastName, int age, Action<Response> response){
        StartCoroutine(CO_CreateUser(username, email, pass, firstName, lastName, age, response));
    }
    private IEnumerator CO_CreateUser(string username, string email, string pass, string firstName, string lastName, int age, Action<Response> response){
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("email", email);
        form.AddField("password_", pass);
        form.AddField("age", age);
        form.AddField("first_name", firstName);
        form.AddField("last_name", lastName);

        WWW w = new WWW("http://izyventa.com/elena/RegisterLogin/createUser.php", form);

        yield return w;
        Debug.Log(w.text);
        response(JsonUtility.FromJson<Response>(w.text));
    }
    public void LoginUser(string username, string pass, Action<ResponseA> response){
        StartCoroutine(CO_LoginUser(username, pass, response));
    }
    private IEnumerator CO_LoginUser(string username, string pass, Action<ResponseA> response){
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password_", pass);

        WWW w = new WWW("http://izyventa.com/elena/RegisterLogin/loginUser.php", form);

        yield return w;
        Debug.Log(w.text);
        response(JsonUtility.FromJson<ResponseA>(w.text));
    }

    public void loadQuestion(int id, Action<Question1> question1){
        StartCoroutine(CO_LoadQuestion(id, question1));
    }
    private IEnumerator CO_LoadQuestion(int id, Action<Question1> question1){
        WWWForm form = new WWWForm();
        form.AddField("id", id);

        WWW w = new WWW("http://izyventa.com/elena/Examen/loadQuestion.php", form);

        yield return w;
        question1(JsonUtility.FromJson<Question1>(w.text));
    }
    public void loadQuestionpool(int id, Action<Questionpool1> questionpool1){
        StartCoroutine(CO_LoadQuestionpool(id, questionpool1));
    }
    private IEnumerator CO_LoadQuestionpool(int id, Action<Questionpool1> questionpool1){
        WWWForm form = new WWWForm();
        form.AddField("id", id);

        WWW w = new WWW("http://izyventa.com/elena/Examen/loadQuestionpool.php", form);

        yield return w;
        questionpool1(JsonUtility.FromJson<Questionpool1>(w.text));
    }
    public void loadAnswer(int id, Action<Answer1> answer1){
        StartCoroutine(CO_LoadAnswer(id, answer1));
    }
    private IEnumerator CO_LoadAnswer(int id, Action<Answer1> answer1){
        WWWForm form = new WWWForm();
        form.AddField("id", id);

        WWW w = new WWW("http://izyventa.com/elena/Examen/loadAnswer.php", form);

        yield return w;
        answer1(JsonUtility.FromJson<Answer1>(w.text));
    }


    public void loadUser(String username, String firstname, String email){
        StartCoroutine(CO_LoadUser(username, firstname, email));

    }
    private IEnumerator CO_LoadUser(String username, String firstname, String email){
        int[] exams = new int[6];
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("option", 1);
        WWW w = new WWW("http://izyventa.com/elena/Menu/loadUser.php", form);
        yield return w;
        Debug.Log(w.text);
        ExamPuntajes e = JsonUtility.FromJson<ExamPuntajes>(w.text);
        UserInfo.examPuntaje = e.examPuntaje;


        

        UserInfo.username = username;
        UserInfo.email = email;
        UserInfo.firstname = firstname;
        int[] videosDimension = {5, 7, 4, 5, 6};
        for(int i = 0; i < 6; i++){
            if(i!= 5){
                videoList listingVideo = new videoList();
                for(int j = 0; j < videosDimension[i]; j++ ){
                    form = new WWWForm();
                    form.AddField("username", username);
                    form.AddField("option", 0);
                    form.AddField("lesson", i+1);
                    form.AddField("index", j+1);
                    w = new WWW("http://izyventa.com/elena/Menu/loadUser.php", form);
                    yield return w;
                    listingVideo.videosArray.Add(JsonUtility.FromJson<Video>(w.text));
                }
                UserInfo.videosArray[i] = listingVideo.videosArray;
            }
            if(UserInfo.examPuntaje[i] != -1){
                questionsList listing = new questionsList();
                int dimension = 6;
                if(i == 5){dimension = 21;}
                for(int j = 0; j < dimension; j++ ){
                    form = new WWWForm();
                    form.AddField("username", username);
                    form.AddField("option", 2);
                    form.AddField("lesson", i+1);
                    form.AddField("index", j+1);
                    w = new WWW("http://izyventa.com/elena/Menu/loadUser.php", form);
                    yield return w;
                    listing.questionsArray.Add(JsonUtility.FromJson<Questions>(w.text));
                }
                UserInfo.questionsArray[i] = listing.questionsArray;
                form = new WWWForm();
                form.AddField("username", username);
                form.AddField("option", 3);
                form.AddField("lesson", i+1);
                 w = new WWW("http://izyventa.com/elena/Menu/loadUser.php", form);
                yield return w;
                UserInfo.columnsArray[i] = JsonUtility.FromJson<Columns>(w.text);
            }
        }
        form = new WWWForm();
        form.AddField("username", username);
        form.AddField("option", 4);
        w = new WWW("http://izyventa.com/elena/Menu/loadUser.php", form);
        yield return w;
        Debug.Log(w.text);
        ExamPuntajes e1 = JsonUtility.FromJson<ExamPuntajes>(w.text);
        UserInfo.leccionPuntaje = e1.examPuntaje;

        faltasList listingFaltas = new faltasList();
        criteriosList listingCriterios = new criteriosList();

        for(int j = 1; j < 7; j++){
            form = new WWWForm();
            form.AddField("username", username);
            form.AddField("option", 5);
            form.AddField("lesson", j);
            w = new WWW("http://izyventa.com/elena/Menu/loadUser.php", form);
            yield return w;
            Debug.Log(w.text);
            Numeros numerosPractico = JsonUtility.FromJson<Numeros>(w.text);
            
            for(int i = 0; i < numerosPractico.numCriterios; i++){
                form = new WWWForm();
                form.AddField("username", username);
                form.AddField("option", 6);
                form.AddField("index", i);
                form.AddField("lesson", j);
                w = new WWW("http://izyventa.com/elena/Menu/loadUser.php", form);
                yield return w;
                listingCriterios.criteriosArray.Add(JsonUtility.FromJson<Criterio>(w.text));
            }
            for(int i = 0; i < numerosPractico.numFaltas; i++){
                form = new WWWForm();
                form.AddField("username", username);
                form.AddField("option", 7);
                form.AddField("index", i);
                form.AddField("lesson", j);
                w = new WWW("http://izyventa.com/elena/Menu/loadUser.php", form);
                yield return w;
                listingFaltas.faltasArray.Add(JsonUtility.FromJson<Falta>(w.text));    
            }
        }
        UserInfo.criteriosArray = listingCriterios.criteriosArray;
        UserInfo.faltasArray = listingFaltas.faltasArray;

        buttonLogin.gameObject.SetActive(true);
        buttonRegister.gameObject.SetActive(true);
        SceneManager.LoadScene("MenuPrincipal");
    }
}

[Serializable]
public class questionsList{
    public List<Questions> questionsArray = new List<Questions>();
}

[Serializable]
public class faltasList{
    public List<Falta> faltasArray = new List<Falta>();
}

[Serializable]
public class criteriosList{
    public List<Criterio> criteriosArray = new List<Criterio>();
}

[Serializable]
public class Numeros{
    public int numCriterios = 0;
    public int numFaltas = 0;
}

public class videoList{
    public List<Video> videosArray = new List<Video>();
}

[Serializable]
public class ExamPuntajes{
    public int[] examPuntaje = new int[6];
}

[Serializable]
public class Response{
    public bool done = false;
    public string message = "";
    public int id = 0;
}


[Serializable]
public class ResponseA{
    public bool done = false;
    public string message = "";
    public int id = 0;
    public string email;
    public string firstname;
}


[Serializable]
public class Answer1
{
    public int id_answer = 0;
    public int id_question = 0;
    public string answer = "";
    public int is_correct = 0;
    
        
}
[Serializable]
public class Questionpool1
{
    public int id_question_pool = 0;
    public int id_question = 0;
    public string question = "";
}


[Serializable]
public class Question1
{
    public int id_question = 0;
    public int id_theoretical_lesson = 0;
    public string question = "";
}


