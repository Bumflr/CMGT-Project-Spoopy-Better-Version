using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteAsset : MonoBehaviour
{
    private static NoteAsset _Instance;

    public static NoteAsset Instance
    {
        get { if (_Instance == null) _Instance = Instantiate(Resources.Load<NoteAsset>("NotesDatabase")); return _Instance; }
    }

    [System.Serializable]
    public class NoteItems
    {
        public ItemType itemType;
        [TextArea]
        public string[] textPages;
    }

    public NoteItems[] noteItems;
    private void Awake()
    {
        foreach (var noteItem in noteItems)
        {
            textDictionary.Add(noteItem.itemType, noteItem.textPages);
        }
    }

    public Dictionary<ItemType, string[]> textDictionary = new Dictionary<ItemType, string[]>();
} 
    
