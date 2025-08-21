using UnityEngine;

[System.Serializable]
public class Sound 
{
    /// <summary>
    /// Nome do som. Este nome é usado para identificar o som no sistema de áudio.
    /// </summary>
    [Tooltip("Nome do som. Este nome é usado para identificar o som no sistema de áudio.")]
    public string name;

    /// <summary>
    /// O clip de áudio que será reproduzido. 
    /// </summary>
    [Tooltip("O clip de áudio que será reproduzido.")]
    public AudioClip audioClip;
}
