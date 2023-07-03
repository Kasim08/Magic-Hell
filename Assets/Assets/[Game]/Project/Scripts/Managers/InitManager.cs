using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InitManager : MonoBehaviour
{
    private IEnumerator Start()
    {
        //PlayerPrefs.DeleteAll();//Oyun baþlarken verilerin sýfýrlanmasý isteniyor ise.
        
        //Init Game Here   
        yield return SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);

        /*
        //PlayerPrefs 2 veri alýyor ilk olan istediðimiz eðer yok ise 2. olan default olark alýnýr. Eðer Oyun açýlýrken Menu ile beraber Default bir level yüklenmek istenirse.
        //Mini oyunlar HyperCasual oyunlar ve tek level e sahip oyunlar için kullanýlmasý mantýklý olur.
        yield return SceneManager.LoadSceneAsync(PlayerPrefs.GetString("LastLevel", "Level01"), LoadSceneMode.Additive);
        // Etkin sahneyi belirler. Sahne ýþýklarý aktif etkin olmasý açýsýndn önemli.
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(PlayerPrefs.GetString("LastLevel", "Level01"))); 
        */

        //EventManager.OnBangBangGame.Invoke();

        Destroy(gameObject);
    }
}