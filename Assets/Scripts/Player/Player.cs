using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 




public enum PlayerNumber {Player1, Player2} 


public class Player : MonoBehaviour 
{
    // parameters 
    [SerializeField] float speed = 10; 
    [SerializeField] PlayerNumber playerNumber; 
    // connections 
    Platform platform; 



    void Awake () 
    {
        platform = GetComponent<Platform>(); 
        InitEvents(); 
    }

    public void Init (PlayerNumber playerNumber) 
    {
        this.playerNumber = playerNumber; 
    }

    void Start()
    {
        InitMotion(); 
    }

    void Update()
    {
        UpdateMotion(); 
    }

    void OnDestroy () 
    {
        ClearEvents(); 
    }





    //  Events  ----------------------------------------------------- 
    void InitEvents () 
    {
        GameSettings.onChanged += OnSettingsChanged; 
    }

    void ClearEvents () 
    {
        GameSettings.onChanged -= OnSettingsChanged; 
    }

    public void OnSettingsChanged () 
    {
        speed = GameSettings.platformSpeed; 
    }





    //  Motion  ----------------------------------------------------- 
    void InitMotion () 
    {
        speed = GameSettings.platformSpeed; 
    }

    void UpdateMotion () 
    {
        float input = GetInput(); 
        float motion = input * speed * Time.deltaTime; 
        platform.Move(motion); 
    }

    float GetInput () 
    {
        float input = 0; 

        switch (playerNumber) 
        {
            case PlayerNumber.Player1: 
                input += Keyboard.current.wKey.isPressed ? 1 : 0; 
                input += Keyboard.current.sKey.isPressed ? -1 : 0; 
                break; 
            case PlayerNumber.Player2: 
                input += Keyboard.current.upArrowKey.isPressed ? 1 : 0; 
                input += Keyboard.current.downArrowKey.isPressed ? -1 : 0; 
                break; 
        }
        return input; 
    }

}
