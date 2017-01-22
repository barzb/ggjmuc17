using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class BillboardText : MonoBehaviour {

    public static BillboardText Instance;

    public TextMesh billboardTextPrefab;

    public TextMesh CreateBillboardText(string text, Vector3 position, int showDuration)
    {

        TextMesh billboardText = Instantiate(billboardTextPrefab);
        billboardText.transform.rotation = Camera.main.transform.rotation;
        SetText(billboardText, text);
        if (showDuration > 0)
        {
            Invoke("DestroyText", showDuration);
        }
        return billboardText;
    }


public void SetText(TextMesh billboardText , string newText)
    {
        billboardText.text = newText;
    }

    public void DestroyText()
    {
        Destroy(this.gameObject);
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        BillboardText.Instance.CreateBillboardText("Test", Vector3.zero, 4);
    }
}
