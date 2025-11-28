using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DialogueHandler : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputManager;
    public UnityEvent onClick = new UnityEvent();
    [SerializeField] private Transform player;
    private bool hasClicked;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("e") && !hasClicked)
        {
            hasClicked = true;
            onClick.Invoke();
        }
        else
        {
            hasClicked = false;
        }
    }
}
