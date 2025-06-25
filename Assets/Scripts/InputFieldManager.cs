using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputFieldManager : MonoBehaviour
{
    public bool inputFieldisFocused = false;
    public TMPro.TMP_InputField inputField;

    public static InputFieldManager Instance;
    // Start is called before the first frame update
    public void Awake()
    {
        InputFieldManager.Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputField.IsActive())
        {
            inputFieldisFocused = inputField.isFocused;
        }
    }
}
