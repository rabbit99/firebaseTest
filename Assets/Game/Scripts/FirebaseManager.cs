using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    public FirebaseAuth auth;
    public FirebaseUser user;
    // Start is called before the first frame update
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Register(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("Register IsCanceled");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("Register IsFaulted " + task.Exception);
                Firebase.FirebaseException firebaseEx = task.Exception.InnerExceptions[0].InnerException as Firebase.FirebaseException;
                if (firebaseEx != null)
                {
                    var errorCode = (AuthError)firebaseEx.ErrorCode;
                    Debug.Log(errorCode.ToString());
                }
                return;
            }
            if (task.IsCompletedSuccessfully)
            {
                Debug.Log("Register IsCompletedSuccessfully");
                // Firebase user has been created.
                user = task.Result;
                Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                    user.DisplayName, user.UserId);
                return;
            }
        });
    }

    private static string GetErrorMessage(AuthError errorCode)
    {
        var message = "";
        switch (errorCode)
        {
            case AuthError.AccountExistsWithDifferentCredentials:
                message = "Ya existe la cuenta con credenciales diferentes";
                break;
            case AuthError.MissingPassword:
                message = "Hace falta el Password";
                break;
            case AuthError.WeakPassword:
                message = "El password es debil";
                break;
            case AuthError.WrongPassword:
                message = "El password es Incorrecto";
                break;
            case AuthError.EmailAlreadyInUse:
                message = "Ya existe la cuenta con ese correo electrónico";
                break;
            case AuthError.InvalidEmail:
                message = "Correo electronico invalido";
                break;
            case AuthError.MissingEmail:
                message = "Hace falta el correo electrónico";
                break;
            default:
                message = "Ocurrió un error";
                break;
        }
        return message;
    }
}
