using UnityEngine;

[CreateAssetMenu(fileName = "FlagInfo", menuName = "Info/FlagInfo")]
public class FlagInfo : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private string _title;
    [SerializeField] private string _start;
    [SerializeField] private string _score;
    [SerializeField] private string _record;
    [SerializeField] private string _gameover;
    [SerializeField] private string _thanks;
    [SerializeField] private string _graphics;
    [SerializeField] private string _code;
    [SerializeField] private string _music;
    [SerializeField] private string _tap;
    [SerializeField] private string _stage;
    [SerializeField] private Sprite _flagSprite;
    [SerializeField] private string _authText;
    [SerializeField] private string _yes;
    [SerializeField] private string _no;
    [SerializeField] private string _error;

    public string id => _id;
    public string title => _title;
    public string start => _start;
    public string score => _score;
    public string record => _record;
    public string gameover => _gameover;
    public string thanks => _thanks;
    public string graphics => _graphics;
    public string music => _music;
    public string code => _code;
    public string tap => _tap;
    public string stage => _stage;
    public Sprite flagSprite => _flagSprite;
    public string authText => _authText;
    public string yes => _yes;
    public string no => _no;
    public string error => _error;
}
