using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TestScript : MonoBehaviour
{
    private Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(Test);
    }
    
    private void OnDisable()
    {
        button.onClick.AddListener(Test);
    }

    public void Test()
    {
        Debug.Log("TEST");
    }
}
