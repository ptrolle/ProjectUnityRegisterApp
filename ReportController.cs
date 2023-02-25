using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportController : MonoBehaviour 
{
	public Dropdown Dmonth;
	public InputField Iyear;

	public Text scrollText;
	
	void Start () 
	{
		//PEGA ANO ATUAL
		Iyear.text = System.DateTime.Today.Year.ToString();

		Iyear.characterLimit = 4;

		//MESES EM DROPDOWN
		Dmonth.ClearOptions();
		List<string> monthsList = new List<string>{"JANEIRO", "FEVEREIRO", "MARÇO", "ABRIL", "MAIO", "JUNHO", "JULHO", "AGOSTO",
		"SETEMBRO","OUTUBRO", "NOVEMBRO", "DEZEMBRO"};
		Dmonth.AddOptions(monthsList);

		Dmonth.value = (System.DateTime.Today.Month - 1); //para o drop pegar o atual mes

	}

	public void OnBtnSearch() //CARREGA VALORES DE CADA CATEGORIA
	{
		if(Iyear.text != " ")
		{
			scrollText.text = "";
			string __keyAux = (Dmonth.value + 1).ToString() + Iyear.text;
			string __buff = __keyAux;
			float __total = 0.0f;

			for(int i=0; i < DataSave.listCategory.Count; i++)
			{
				__keyAux += DataSave.listCategory[i];
				Debug.Log(__keyAux);
				float getValueCategory = PlayerPrefs.GetFloat(__keyAux);
				__total += getValueCategory;
				Debug.Log(getValueCategory);
				//scrollText.text += DataSave.listCategory[i] + " R$ " + getValueCategory.ToString("##.00")+ "\n";
				scrollText.text += DataSave.listCategory[i] + " R" + getValueCategory.ToString("C")+ "\n";
				__keyAux = __buff;
			}

			scrollText.text += "------------------" + "\n" + "TOTAL R" + __total.ToString("C"); 

			//IyearAux = Iyear.text; //pega o ano joga em uma string para usar parse int
			//SearchController.year = int.Parse(IyearAux); //passa o ano selecionado
			//SearchController.month = Dmonth.value + 1; //passa o mes como numero
			//Debug.Log(SearchController.month + " / " + SearchController.year);

		}
		else
		{
		 	Debug.Log("precisa digitar um ano qualquer");
		}
	}

	public void OnBtnSearchRelease() //CARREGA ULTIMOS LANÇAMENTOS EM CADA CATEGORIA
	{
		if(Iyear.text != " ")
		{
			scrollText.text = "";
			string __keyAux = (Dmonth.value + 1).ToString() + Iyear.text;
			string __auxDescription;
			float __auxValueDescription;
			string __buff = __keyAux;
			float __total = 0.0f;

			for(int i=0; i < DataSave.listCategory.Count; i++)
			{
				__keyAux += DataSave.listCategory[i]; //recebe a key tipo (72018FARMACIA)
				__auxDescription = PlayerPrefs.GetString(__keyAux+"0").ToLower(); //recebe (72018FARMACIA0), PEGA DESCRIÇÃO DA CATEGORIA
				__auxValueDescription = PlayerPrefs.GetFloat(__keyAux+1); //recebe (72018FARMACIA1), PEGA VALOR DA DESCRIÇÃO DA CATEGORIA
				float getValueCategory = PlayerPrefs.GetFloat(__keyAux); //recebe o valor total de cada categoria para somar na final
				__total += getValueCategory; //o total vai somando cada valor obtido de cada categoria

									
				scrollText.text += DataSave.listCategory[i] + " : " + "\n";
				scrollText.text += __auxDescription + " R" + __auxValueDescription.ToString("C") + "\n";
				scrollText.text += " --- " + " \n ";
				__keyAux = __buff;
			}

			//scrollText.text += "TOTAL MÊS R" + __total.ToString("C"); 
		}
		else
		{
			Debug.Log("precisa digitar um ano qualquer");
		}
	}

}
