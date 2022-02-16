using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System.IO;

public class DialogueVariables
{
    private Dictionary<string, Ink.Runtime.Object> variables;

    public DialogueVariables(string globalsFilePath)
    {
        Debug.Log(globalsFilePath);
        string inkFileContents = File.ReadAllText(globalsFilePath);
        Ink.Compiler compiler = new Ink.Compiler(inkFileContents);
        Story globalVariablesStory = compiler.Compile();

        variables = new Dictionary<string, Ink.Runtime.Object>();

        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
        }
    }

    public void StartListening(Story story)
    {
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    public void VariableChanged(string name, Ink.Runtime.Object value)
    {
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
        }
    }

    private void VariablesToStory(Story story)
    {
        foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }

    public Ink.Runtime.Object QuestVariable()
    {
        foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            if (variable.Key == "questVariable" && variable.Value != null)
            {
                return variable.Value;
            }
        }
        return null;
    }

    //public Ink.Runtime.Object SetVariable(string key)
    //{
    //    foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables)
    //    {
    //        Debug.Log(variable);
    //        if (variable.Key == key)
    //        {
    //            return variable.Value;
    //        }
    //    }
    //    return null;
    //}

    public void SetVariable(Story story, string key, Ink.Runtime.Object value )
    {
        story.variablesState.SetGlobal(key, value);
    }

    public Dictionary<string, Ink.Runtime.Object> GetVariables()
    {
        return variables;
    }
}
