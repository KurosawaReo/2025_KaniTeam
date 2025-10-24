using UnityEngine;
using UnityEngine.Rendering;

public class PanelManagement : MonoBehaviour
{
    [SerializeField] private GameObject panel1;
    [SerializeField] private GameObject panel2;
    [SerializeField] private GameObject button;

    private bool isPanel1Active = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ç≈èâÇÕîÒï\é¶Ç…Ç∑ÇÈ
        panel1.SetActive(true);
        panel2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PushChangeButton()
    {
        isPanel1Active = !isPanel1Active;

        // êÿÇËë÷Ç¶é¿çs
        panel1.SetActive(isPanel1Active);
        panel2.SetActive(!isPanel1Active);

    }
}
