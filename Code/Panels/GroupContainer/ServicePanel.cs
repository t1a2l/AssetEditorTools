using ColossalFramework.UI;
using AssetEditorTools.UI;
using System.Collections.Generic;

namespace AssetEditorTools
{
	public class ServicePanel : UIPanel
	{
		public UIDropDown m_serviceDropDown;
		public UIButton m_serviceApplyButton;

		public override void Start() 
		{ 
			base.Start();

			backgroundSprite = "SubcategoriesPanel";
			clipChildren = true;

			width = 393;
			height = 35;

			relativePosition = new UnityEngine.Vector3 (0f, 0f, 0f);

			m_serviceDropDown = UIDropDowns.AddLabelledDropDown(this, 20.0f, 0.0f, "Service", 180.0f, 25.0f, 0.7f, 20, 8, true, "Allow you to change the Asset Service");
			m_serviceApplyButton = UIButtons.AddButton(this, 300.0f, 0.0f, "Apply", 90.0f, 30.0f, 0.9f, 4);
			PopulateServiceDropDown();

			gameObject.AddComponent<ServiceEditorController>();
		}

		private void PopulateServiceDropDown() 
		{
			var sortServices = new List<ItemClass.Service>();
			foreach(ItemClassCollection collection in FindObjectsOfType<ItemClassCollection>()) 
			{
				foreach(ItemClass itemClass in collection.m_classes) 
				{
					if(sortServices.IndexOf(itemClass.m_service) == -1)
					{
						sortServices.Add(itemClass.m_service);
					}
				}
			}
			sortServices.Sort();

			foreach(ItemClass.Service service in sortServices) 
			{
				m_serviceDropDown.AddItem(service.ToString());
			}

			m_serviceDropDown.selectedIndex = 0;
		}

	}
}
