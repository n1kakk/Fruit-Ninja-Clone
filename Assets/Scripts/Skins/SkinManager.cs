 using UnityEngine;

[CreateAssetMenu(fileName = "SkinManager", menuName = "SkinManager", order = 0)]
public class SkinManager : ScriptableObject {
        
    [SerializeField] public Skin[] skins;
    private const string Prefix = "Skin_";
    private const string SelectedSkin = "SelectedSkin";
    public void SelectSkin(int skinIndex) => PlayerPrefs.SetInt(SelectedSkin, skinIndex);

    public Skin GetSelectedSkin(){
        int skinIndex = PlayerPrefs.GetInt(SelectedSkin, 0);
        if(skinIndex >= 0 && skinIndex< skins.Length){
            return skins[skinIndex];
        }else return null;
    }

    public void Unlock(int skinIndex) => PlayerPrefs.SetInt(Prefix + skinIndex, 1);
    public bool IsUnlocked(int skinIndex) => PlayerPrefs.GetInt(Prefix + skinIndex, 0) == 1;
} 



