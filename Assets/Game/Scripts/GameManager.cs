using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TMP_InputField inputEmail;
    public TMP_InputField inputPassword;
    private FirebaseManager firebaseManager;
    // Start is called before the first frame update
    void Start()
    {
        firebaseManager = GetComponent<FirebaseManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Register()
    {
        firebaseManager.Register(inputEmail.text, inputPassword.text);
    }

    public void Login()
    {

    }
}
