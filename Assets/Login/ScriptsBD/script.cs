using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.Linq;
using UnityEngine.SceneManagement;

public class script : MonoBehaviour
{

    [SerializeField] private GameObject registerUI = null;
    [SerializeField] private GameObject loginUI = null;
    [SerializeField] private Button buttonLogin = null;
    [SerializeField] private Button buttonRegister = null;
    [SerializeField] private Text message = null;
    [SerializeField] private Text messageLogin = null;
    [SerializeField] private InputField usernameInput = null;
    [SerializeField] private InputField emailInput = null;
    [SerializeField] private InputField passwordInput = null;
    [SerializeField] private InputField passwordVerifiedInput = null;
    [SerializeField] private InputField firstnameInput = null;
    [SerializeField] private InputField lastnameInput = null;
    [SerializeField] private InputField ageInput = null;
    [SerializeField] private InputField usernameLoginInput = null;
    [SerializeField] private InputField passwordLoginInput = null;
    [SerializeField] private Text usernameError = null;
    [SerializeField] private Text emailError = null;
    [SerializeField] private Text passwordError = null;
    [SerializeField] private Text passwordVerifiedError = null;
    [SerializeField] private Text firstnameError = null;
    [SerializeField] private Text lastnameError = null;
    [SerializeField] private Text ageError = null;
    

    private NetworkManager networkManager = null;   

    public void Awake(){
        networkManager = GameObject.FindObjectOfType<NetworkManager>();
    }

    public void SubmitLogin(){
        messageLogin.text = "";

        if(usernameLoginInput.text == "" || passwordLoginInput.text == ""){
            messageLogin.text = "Completa los campos";
            messageLogin.color = Color.cyan;
        }else{
            networkManager.LoginUser(usernameLoginInput.text, passwordLoginInput.text, delegate(ResponseA response){
                String usernameAux = usernameLoginInput.text;
                messageLogin.text = response.message; 
                usernameLoginInput.text = "";
                passwordLoginInput.text = "";
                if(response.id == 1){
                    messageLogin.color = Color.cyan;
                }else{
                    ShowInformation.username = usernameAux;
                    ShowInformation.firstname= response.firstname;
                    ShowInformation.email = response.email;
                    messageLogin.color = Color.green; 
                    buttonLogin.gameObject.SetActive(false);
                    buttonRegister.gameObject.SetActive(false);
                    networkManager.loadUser(usernameAux, response.firstname, response.email);
                    
                    
                }
            });
        }
    }

    public void SubmitRegister(){
        string aux;
        bool correctSubmit = true;


        message.text = "";
        usernameError.text = "";
        emailError.text = "";
        passwordError.text = "";
        passwordVerifiedError.text = "";
        firstnameError.text = "";
        lastnameError.text = "";
        ageError.text = "";


        aux = usernameInput.text;
        if(usernameInput.text == ""){
            usernameError.text = "Completa el campo";
            correctSubmit = false;
        }else if(aux.Length>20 || aux.Length<6){
            usernameError.text = "Caracteres de 6 - 20";
            usernameInput.text = "";
            correctSubmit = false;
        }
        
        aux = emailInput.text;
        if(emailInput.text == ""){
            emailError.text = "Completa el campo";
            correctSubmit = false;
        }else if(aux.Length>50){
            firstnameError.text = "El máximo de caracteres es de 50";
            firstnameInput.text = "";
            correctSubmit = false;
        }else{
            bool correctEmail = true, dots = false, spaces = false; 
            int atNumber = 0;
            for(int i = 0; i < aux.Length; i++){
                if(aux[i] == '@'){
                    atNumber++;
                }
                if(aux[i] == ' '){
                    spaces = true;
                }
            }
            for(int i = 0; i < aux.Length - 1; i++){
                if(aux[i] == '.' && aux[i+1] == '.'){
                    dots = true; 
                }
            }
            if(dots || atNumber != 1 || spaces || aux[aux.Length-1] == '.'){
                correctEmail = false; 
            }else{
                string[] frases = aux.Split('@');
                int puntos = 0;
                for(int i = 0; i < frases[1].Length; i++){
                    if(frases[1][i] == '.'){
                        puntos++;
                    }
                }
                if(puntos == 0){
                    correctEmail = false;
                }else{
                    string[] frases1 = frases[1].Split('.');
                    string[] checkFrases = {frases[0], frases1[0], frases1[1]};
                    for(int i = 0; i < 3; i++){
                        if(checkFrases[i] == null || checkFrases[i].Length < 1){
                            correctEmail = false;
                        }else{
                            puntos = 0;
                            int indexPuntos = 0;
                            foreach (char item in checkFrases[i])
                            {
                                indexPuntos++;
                                if(item == '.'){
                                    puntos++;
                                }
                            }
                            if(puntos == indexPuntos){
                                correctEmail = false;
                            }else{

                            }
                        }
                    }
                }
            }
            if(!correctEmail){
                emailError.text = "Email incorrecto";
                emailInput.text = "";
                correctSubmit = false; 
            }
        }

        aux = firstnameInput.text;
        if(firstnameInput.text == ""){
            firstnameError.text = "Completa el campo";
            correctSubmit = false;
        }else if(aux.Length>40){
            firstnameError.text = "El máximo de caracteres es de 40";
            firstnameInput.text = "";
            correctSubmit = false;
        }else{
            bool isCharacter = false;
            for(int i = 0; i < aux.Length; i++){
                if((Encoding.ASCII.GetBytes(aux.ToString())[i] > 64 && Encoding.ASCII.GetBytes(aux.ToString())[i] < 91) || 
                (Encoding.ASCII.GetBytes(aux.ToString())[i] > 96 && Encoding.ASCII.GetBytes(aux.ToString())[i] < 123) ||
                Encoding.ASCII.GetBytes(aux.ToString())[i] == 130 ||
                Encoding.ASCII.GetBytes(aux.ToString())[i] == 160 ||
                Encoding.ASCII.GetBytes(aux.ToString())[i] == 161 ||
                Encoding.ASCII.GetBytes(aux.ToString())[i] == 162 ||
                Encoding.ASCII.GetBytes(aux.ToString())[i] == 163 ||
                Encoding.ASCII.GetBytes(aux.ToString())[i] == 164 ||
                Encoding.ASCII.GetBytes(aux.ToString())[i] == 165 ||
                Encoding.ASCII.GetBytes(aux.ToString())[i] == 63 ||
                Encoding.ASCII.GetBytes(aux.ToString())[i] == 32
                ){

                }else{
                    isCharacter = true;
                }
            }
            if(isCharacter){
                firstnameError.text = "Debe ser alfabetico";
                firstnameInput.text = "";
                correctSubmit = false;
            }
        }

        aux = lastnameInput.text;
        if(lastnameInput.text == ""){
            lastnameError.text = "Completa el campo";
            correctSubmit = false;
        }else if(aux.Length>60){
            lastnameError.text = "El máximo de caracteres es de 60";
            lastnameInput.text = "";
            correctSubmit = false;
        }else{
            bool isCharacter = false;
            for(int i = 0; i < aux.Length; i++){
                if((Encoding.ASCII.GetBytes(aux.ToString())[i] > 64 && Encoding.ASCII.GetBytes(aux.ToString())[i] < 91) || 
                (Encoding.ASCII.GetBytes(aux.ToString())[i] > 96 && Encoding.ASCII.GetBytes(aux.ToString())[i] < 123) ||
                Encoding.ASCII.GetBytes(aux.ToString())[i] == 130 ||
                Encoding.ASCII.GetBytes(aux.ToString())[i] == 160 ||
                Encoding.ASCII.GetBytes(aux.ToString())[i] == 161 ||
                Encoding.ASCII.GetBytes(aux.ToString())[i] == 162 ||
                Encoding.ASCII.GetBytes(aux.ToString())[i] == 163 ||
                Encoding.ASCII.GetBytes(aux.ToString())[i] == 164 ||
                Encoding.ASCII.GetBytes(aux.ToString())[i] == 165 ||
                Encoding.ASCII.GetBytes(aux.ToString())[i] == 63 ||
                Encoding.ASCII.GetBytes(aux.ToString())[i] == 32
                ){

                }else{
                    isCharacter = true;
                }
            }
            if(isCharacter){
                lastnameError.text = "Debe ser alfabetico";
                lastnameInput.text = "";
                correctSubmit = false;
            }
        }

        aux = ageInput.text;
        bool notNumber = false;
        for(int i = 0; i < aux.Length; i++){
            if(!Char.IsNumber(aux, i)){
                notNumber = true; 
            }
        }
        if(ageInput.text == ""){
            ageError.text = "Completa el campo";
            correctSubmit = false;
        }else if(notNumber){
            ageError.text = "Valor Invalido";
            ageInput.text = "";
            correctSubmit = false;
        }else{
            int age = Int32.Parse(aux);
            if(age < 13 || age > 69){
                ageError.text = "Entre 13 y 69 años";
                ageInput.text = "";
                correctSubmit = false; 
            }
        }

        aux = passwordInput.text;
        if(passwordInput.text == ""){
            passwordError.text = "Completa el campo";
            passwordInput.text = "";
            passwordVerifiedInput.text = "";
            correctSubmit = false;
        }else if(passwordVerifiedInput.text == ""){
            passwordVerifiedError.text = "Completa el campo";
            passwordInput.text = "";
            passwordVerifiedInput.text = "";
            correctSubmit = false;
        }
        if(passwordVerifiedInput.text != "" && passwordInput.text != ""){
            if(passwordInput.text != passwordVerifiedInput.text){
                passwordError.text = "Contraseñas no coinciden";
                passwordInput.text = "";
                passwordVerifiedInput.text = "";
                correctSubmit = false;
            }else if(aux.Length>20 || aux.Length<8){
                passwordError.text = "8 - 20 caracteres";
                passwordInput.text = "";
                passwordVerifiedInput.text = "";
                correctSubmit = false;
            }else if(!aux.Any(char.IsUpper) || !aux.Any(char.IsLower) || !aux.Any(char.IsDigit)){
                passwordError.text = "Debe contener minúscula, mayúscula y numero";
                passwordInput.text = "";
                passwordVerifiedInput.text = "";
                correctSubmit = false;
            }
        }

        
        if(correctSubmit){
            networkManager.CreateUser(usernameInput.text, emailInput.text, passwordInput.text, firstnameInput.text, lastnameInput.text, int.Parse(ageInput.text), delegate(Response response){
                if(response.id==1){
                    usernameError.text = response.message;
                }else if(response.id == 2){
                    emailError.text = response.message;
                }else if(response.id == 0){
                    message.text = response.message;
                    usernameInput.text = "";
                    emailInput.text = "";
                    passwordInput.text = "";
                    passwordVerifiedInput.text = "";
                    firstnameInput.text = "";
                    lastnameInput.text = "";
                    ageInput.text = "";
                } 
            });

        }
        
        
    }

    public void ShowLogin(){
        loginUI.SetActive(true);
        registerUI.SetActive(false);
        usernameLoginInput.text = "";
        passwordLoginInput.text = "";
        messageLogin.text = "";
        message.text = "";
    }

    public void ShowRegister(){
        loginUI.SetActive(false);
        registerUI.SetActive(true);
        usernameInput.text = "";
        emailInput.text = "";
        passwordInput.text = "";
        passwordVerifiedInput.text = "";
        firstnameInput.text = "";
        lastnameInput.text = "";
        ageInput.text = "";
        message.text = "";
        usernameError.text = "";
        emailError.text = "";
        passwordError.text = "";
        passwordVerifiedError.text = "";
        firstnameError.text = "";
        lastnameError.text = "";
        ageError.text = "";

    }
     public static void ExitToDesktop()
        {
            Application.Quit();
 
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        }

}
