using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class JournalPageSaveData
{
    public string documentName;
    public int pageNumber;

    public JournalPageSaveData(string documentName, int pageNumber)
    {
        this.documentName = documentName;
        this.pageNumber = pageNumber;
    }
}
