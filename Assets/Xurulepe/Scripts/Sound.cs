using UnityEngine;

[System.Serializable]
public class Sound 
{
    /// <summary>
    /// Nome do som. Este nome � usado para identificar o som no sistema de �udio.
    /// </summary>
    [Tooltip("Nome do som. Este nome � usado para identificar o som no sistema de �udio.")]
    public string name;

    /// <summary>
    /// O clip de �udio que ser� reproduzido. 
    /// </summary>
    [Tooltip("O clip de �udio que ser� reproduzido.")]
    public AudioClip audioClip;
}
