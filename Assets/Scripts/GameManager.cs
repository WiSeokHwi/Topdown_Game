using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI talking_TMP;
    public GameObject scanObject;
    public GameObject talkPanel;
    public bool isAction;

    private void Start()
    {
        isAction = false;
        talkPanel.SetActive(false);
    }

    public void Action(GameObject scanObj)
    {
        if (isAction)
        {
            isAction = false;
        }
        else
        {
            isAction = true;
            talkPanel.SetActive(true);
            scanObject = scanObj;
            talking_TMP.text = "�̰��� �̸��� " + scanObject.name + "�̶�� �Ѵ�.";
        }
        talkPanel.SetActive(isAction);
    }

}
