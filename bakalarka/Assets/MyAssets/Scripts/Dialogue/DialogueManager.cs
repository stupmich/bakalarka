using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using Ink.UnityIntegration;

public class DialogueManager : MonoBehaviour
{
    #region Singleton

    private static DialogueManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        dialogueVariables = new DialogueVariables(globalsInkFile.filePath);
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }
    #endregion

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    [Header("Globals Ink File")]
    [SerializeField] private InkFile globalsInkFile;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

    private DialogueVariables dialogueVariables;

    public static event System.Action<string> OnQuestChoice;

    // Start is called before the first frame update
    void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        dialogueVariables.StartListening(currentStory);

        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        dialogueVariables.StopListening(currentStory);
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
            DisplayChoices();

            Ink.Runtime.Object value = dialogueVariables.QuestVariable();

            if (OnQuestChoice != null && value != null)
            {
                OnQuestChoice(value.ToString());
            }
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices than UI can support.");
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        //EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int index)
    {
        currentStory.ChooseChoiceIndex(index);
        Ink.Runtime.Object value = dialogueVariables.QuestVariable();

        if (OnQuestChoice != null && value != null)
        {
            OnQuestChoice(value.ToString());
        }

        ContinueStory();
    }

    public DialogueVariables GetDialogueVariables()
    {
        return dialogueVariables;
    }

    public void SetDialogueBoolVariable(string key, bool value)
    {
        currentStory.variablesState[key] = value;
        dialogueVariables.VariableChanged(key, currentStory.variablesState.GetVariableWithName(key));
    }
}
