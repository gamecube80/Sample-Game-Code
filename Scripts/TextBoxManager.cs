using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextBoxManager : MonoBehaviour {

    private PlayerController player;

    public GameObject textBox;

    public Text theText;

    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    // Use this for initialization
    void Start() {
        textBox.SetActive(false);

        if (textFile != null) {
            textLines = (textFile.text.Split('\n'));
        }

        if (endAtLine == 0) {
            endAtLine = textLines.Length - 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (GameObject.Find("Player").GetComponent<Weapon>()) {
            GameObject.Find("Player").GetComponent<Weapon>().enabled = false;
        }
        textBox.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D collision) {
        theText.text = textLines[currentLine];

        //disable firing when textbox is active


        if (currentLine < endAtLine) {
            if (Input.GetButtonDown("Fire1")) { //bug where you have to move or press fire twice before text triggers next line
                currentLine += 1;
            }
        }
        else if (currentLine >= endAtLine) {
            textBox.SetActive(false);
            if (GameObject.Find("Player").GetComponent<Weapon>()) {
                GameObject.Find("Player").GetComponent<Weapon>().enabled = true;
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (GameObject.Find("Player").GetComponent<Weapon>()) {
            GameObject.Find("Player").GetComponent<Weapon>().enabled = true;
        }

        textBox.SetActive(false);
    }
}
