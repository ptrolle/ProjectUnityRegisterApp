using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryController : MonoBehaviour 
{

	public InputField Icategory;

	public Text Tinformation; //texto informativo se consegue adicionar ou remover categoria

	public Dropdown DmainCategory;
    //public List<string> listCategory = new List<string>();

    public AudioSource sound;
    public AudioClip click;

    //para controlar tempo de popup na tela
    private float timeController;


    void Start () 
	{
		Icategory.characterLimit = 10;

		DmainCategory.ClearOptions();
		DmainCategory.AddOptions(DataSave.listCategory);

	}

	public void DeleteCategory()
	{
        sound.PlayOneShot(click, 0.05f); //som botao 

		if(Icategory.text == "")
		{
			Debug.Log("digite alguma categoria");
		}
		else
		{
			//Debug.Log(DataSave.listCategory.Count);

			string item = Icategory.text.ToUpper(); //string item recebe o texto do input Icategory, colocando o texto como letra maiuscula
			Icategory.text = "";

            timeController = 0; //zera o tempoController para durar 1 seg a mensagem em Tinformation

            if (DataSave.listCategory.Contains(item)) //testa se categoria digitada ja existe ou nao na lista
			{
				Debug.Log("Categoria encontrada e removida !");
				DataSave.listCategory.Remove(item); //remove categoria
				Tinformation.color = Color.green;
				Tinformation.text = "Categoria Removida !";
               
				//int __auxDeleteLastItemList = DataSave.listCategory.Count;
				//Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>: "+__auxDeleteLastItemList+" / "+ (DataSave.listCategory[__auxDeleteLastItemList - 1]));
				//DataSave.listCategory.RemoveAt(__auxDeleteLastItemList - 1);

				DataSave.SaveDatasCategory(2); //salva na memoria as atuais categorias
			}
			else
			{
				Tinformation.color = Color.red;
				Tinformation.text = "Categoria Não Encontrada !";
			}

			DmainCategory.ClearOptions();
			DmainCategory.AddOptions(DataSave.listCategory);
		}
	}

	public void AddCategory()
	{
        sound.PlayOneShot(click, 0.05f); //som botao

        if (Icategory.text == "")
		{
			Debug.Log("digite alguma categoria");
		}
		else
		{
			//adiciona categoria a lista
			string item = Icategory.text.ToUpper();
			Icategory.text = "";

            timeController = 0; //zera o tempoController para durar 1 seg a mensagem em Tinformation

            if (DataSave.listCategory.Contains(item)) //testa se categoria digitada ja existe ou nao na lista
			{
				Debug.Log("esta categoria ja existe, nao pode ser adicionada!!!");
				Tinformation.color = Color.red;
				Tinformation.text = "Essa Categoria já existe !";
               
			}
			else
			{
				DataSave.listCategory.Add(item); //adiciona categoria
				Tinformation.color = Color.green;
				Tinformation.text = "Categoria Adicionada !";

				for(int i=0; i<DataSave.listCategory.Count; i++)
				{
					Debug.Log(i+" "+DataSave.listCategory[i]);
				}

				DmainCategory.ClearOptions();
				DmainCategory.AddOptions(DataSave.listCategory);

				DataSave.SaveDatasCategory(3); //salva na memoria as atuais categorias
			}
		}
	}

    public void Update()
    {
        timeController += Time.deltaTime;

        if (timeController > 1.5f) //mais de 1.5 seg apaga mensagem Tinformation;
        {
            Tinformation.text = "";
        }
    }
}
