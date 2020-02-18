using UnityEngine;

[CreateAssetMenu(fileName = "penjelasanke-", menuName = "Penjelasan")]
public class penjelasan : ScriptableObject
{
    public string tag;
    [TextArea]
    public string benar;
    [TextArea]
    public string salah;
}
