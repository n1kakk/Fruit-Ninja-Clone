 using UnityEngine;

[CreateAssetMenu(fileName = "SkinManager", menuName = "SkinManager", order = 0)]
public class SkinManager : ScriptableObject {
        
    [SerializeField] public Skin[] skins;


    // Constants for PlayerPrefs keys
    private const string Prefix = "Skin_";
    private const string SelectedSkin = "SelectedSkin";


    // Method to select a skin by index and save the selection in PlayerPrefs
    public void SelectSkin(int skinIndex){
        PlayerPrefs.SetInt(SelectedSkin, skinIndex);
    }


    // Method to get the currently selected skin
    public Skin GetSelectedSkin(){
        int skinIndex = PlayerPrefs.GetInt(SelectedSkin, 0);
        if(skinIndex >= 0 && skinIndex< skins.Length){
            return skins[skinIndex];
        }else return null;
    }


    // Method to unlock a skin by setting its value in PlayerPrefs
    public void Unlock(int skinIndex) => PlayerPrefs.SetInt(Prefix + skinIndex, 1);

    

    // Method to check if a skin is unlocked by reading its value from PlayerPrefs
    public bool IsUnlocked(int skinIndex) => PlayerPrefs.GetInt(Prefix + skinIndex, 0) == 1;
} 



