using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour 
{
	#region CANVAS
	public Canvas mainCanvas;
	public Canvas includeCanvas;
	public Canvas reportCanvas;
	public Canvas searchCanvas;
	public Canvas categoryCanvas;
	public Canvas editorCanvas;
    public Canvas controllerDeleteCanvas;
	#endregion

	#region DROPDOWN
	public Dropdown DcategoryInclude;
	public Dropdown DcategoryController;
	public Dropdown DcategoryEditorController;
	#endregion

	#region CONTROLE ACESSO PRIMARIO CATEGORIAS (save)
	public int firstTimeActive;
    #endregion

    //este sera o principal dropdown categoria;
    //public List<string> listCategory = new List<string>();

    //public Dropdown dp;

    public AudioSource sound;
    public AudioClip click;

    void Awake()
	{


		//deixa todos canvas nao utilizados como false
		//mainCanvas.enabled = true;
		includeCanvas.enabled = false;
		reportCanvas.enabled = false;
		searchCanvas.enabled = false;
		categoryCanvas.enabled = false;
		editorCanvas.enabled = false;
        controllerDeleteCanvas.enabled = false;


		firstTimeActive = PlayerPrefs.GetInt("firstTime"); //analisa se é a primeira vez que entra no app alimenta lista de categorias
		if(firstTimeActive == 0)
		{
			DataSave.SaveDatasCategory(0);
			PlayerPrefs.SetInt("firstTime", 1);
            PlayerPrefs.SetInt("showAds", 0); //como é primeira vez que acessa app coloca zero na contagem para propagandas
		}
		else
		{
			DataSave.SaveDatasCategory(1);
		}

		/*dp.options.Clear();
		for(int i=0; i<listCategory.Count; i++)
			dp.AddOptions(listCategory);*/
	}

	public void CanvasController(int p_index)
	{
        sound.PlayOneShot(click, 0.05f); //som botao

        if (p_index == 1)
		{
			mainCanvas.enabled = false;
			includeCanvas.enabled = true;

			DcategoryInclude.ClearOptions();
			DcategoryInclude.AddOptions(DataSave.listCategory);
		}
		else if(p_index == 2)
		{
			mainCanvas.enabled = false;
			reportCanvas.enabled = true;
		}
		else if(p_index == 3)
		{
			mainCanvas.enabled = false;
			categoryCanvas.enabled = true;

			DcategoryController.ClearOptions();
			DcategoryController.AddOptions(DataSave.listCategory);
		}
		else if(p_index == 4)
		{
			mainCanvas.enabled = false;
			editorCanvas.enabled = true;

			DcategoryEditorController.ClearOptions();
			DcategoryEditorController.AddOptions(DataSave.listCategory);
		}
	}

	public void BackMainCanvas()
	{
        sound.PlayOneShot(click, 0.05f); //som botao

        mainCanvas.enabled = true;
		includeCanvas.enabled = false;
		reportCanvas.enabled = false;
		searchCanvas.enabled = false;
		categoryCanvas.enabled = false;
		editorCanvas.enabled = false;
        controllerDeleteCanvas.enabled = false;

		//DataSave.SaveDatasCategory(2);
	}


	//para ativar e voltar dados canvas report e search
	public void SearchCanvasController(int p_actived)
	{
        sound.PlayOneShot(click, 0.05f); //som botao

        if (p_actived == 0)
		{
			reportCanvas.enabled = false;
			searchCanvas.enabled = true;
		}
		else
		{
			searchCanvas.enabled = false;
			reportCanvas.enabled = true;
		}
	}

	//deleta todos dados salvos ate entao
	public void DeleteDatas()
	{
        sound.PlayOneShot(click, 0.05f); //som botao

        controllerDeleteCanvas.enabled = true;

        /*Debug.Log("deletou todos os dados");
		DataSave.listCategory.Clear();
		PlayerPrefs.DeleteAll();

		//colocando valores originais na lista
		DataSave.SaveDatasCategory(0);

		PlayerPrefs.SetInt("firstTime", 1);*/
	}


}
