using ColossalFramework.UI;
using AssetEditorTools.UI;
using System.Collections.Generic;
using System.Linq;
using ColossalFramework;

namespace AssetEditorTools
{
	public class UICategoryPanel : UIPanel
	{
		public UIDropDown m_UICategoryDropDown;
		public UIButton m_UICategoryDropDownButton;

		public class CategoryInfo 
		{
			public string type;
		}

		public static List<CategoryInfo> categoryList;

		public override void Start() 
		{ 
			base.Start();

			backgroundSprite = "SubcategoriesPanel";
			clipChildren = true;

			categoryList = new();

			width = 393;
			height = 25;

			m_UICategoryDropDown = UIDropDowns.AddLabelledDropDown(this, 190.0f, 0.0f, "UICategory", 191.0f, 20.0f, 0.7f, 25, 8, true, "Category determines the tab an asset will appear under.");
			m_UICategoryDropDownButton = UIButtons.AddButton(m_UICategoryDropDown, 0.0f, 0.0f, "Apply", 191.0f, 20.0f, 0.9f, 4);
			m_UICategoryDropDown.triggerButton = m_UICategoryDropDownButton;
			PopulateUICategoryDropDown();

			gameObject.AddComponent<EditorController>();
		}

		private void PopulateUICategoryDropDown()
		{
			foreach(BuildingCollection buildingCollection in FindObjectsOfType<BuildingCollection>()) 
			{
				foreach(BuildingInfo buildingInfo in buildingCollection.m_prefabs) 
				{
					AddCategory("BuildingInfo");
				}
			}
			foreach(NetCollection netCollection in FindObjectsOfType<NetCollection>()) 
			{
				foreach(NetInfo netInfo in netCollection.m_prefabs) 
				{
					AddCategory("NetInfo");
				}
			}
			foreach(PropCollection propCollection in FindObjectsOfType<PropCollection>()) 
			{
				foreach(PropInfo propInfo in propCollection.m_prefabs) 
				{
					AddCategory("PropInfo");
				}
			}
			foreach(TreeCollection treeCollection in FindObjectsOfType<TreeCollection>()) 
			{
				foreach(TreeInfo treeInfo in treeCollection.m_prefabs) 
				{
					AddCategory("TreeInfo");
				}
			}
			foreach(VehicleCollection vehicleCollection in FindObjectsOfType<VehicleCollection>()) 
			{
				foreach(VehicleInfo vehicleInfo in vehicleCollection.m_prefabs) 
				{
					AddCategory("VehicleInfo");
				}
			}
			foreach(CitizenCollection citizenCollection in FindObjectsOfType<CitizenCollection>()) 
			{
				foreach(CitizenInfo citizenInfo in citizenCollection.m_prefabs) 
				{
					AddCategory("CitizenInfo");
				}
			}
			foreach(TransportCollection transportCollection in FindObjectsOfType<TransportCollection>()) 
			{
				foreach(TransportInfo transportInfo in transportCollection.m_prefabs) 
				{
					AddCategory("TransportInfo");
				}
			}

			List<CategoryInfo> reducedCategoryList = new();

			while (categoryList.Count > 0) 
			{
				CategoryInfo categoryInfo = categoryList[0];
				foreach(CategoryInfo category in categoryList.FindAll(x => x.type == categoryInfo.type)) 
				{
					if(category.type == "BuildingInfo")  {categoryInfo.type = "BuildingInfo";}
					if(category.type == "NetInfo")       {categoryInfo.type = "NetInfo";}
					if(category.type == "PropInfo")      {categoryInfo.type = "PropInfo";}
					if(category.type == "TreeInfo")      {categoryInfo.type = "TreeInfo";}
					if(category.type == "VehicleInfo")   {categoryInfo.type = "VehicleInfo";}
					if(category.type == "CitizenInfo")   {categoryInfo.type = "CitizenInfo";}
					if(category.type == "TransportInfo") {categoryInfo.type = "TransportInfo";}
				}
				reducedCategoryList.Add(categoryInfo);
				categoryList.RemoveAll(x => x.type == categoryInfo.type);
			}

			var sortedcategorylist = reducedCategoryList.OrderBy(s => s.type == "BuildingInfo").ThenBy(s => s.type == "NetInfo").ThenBy(s => s.type == "PropInfo").ThenBy(s => s.type == "TreeInfo").ThenBy(s => s.type == "VehicleInfo").ThenBy(s => s.type == "CitizenInfo").ThenBy(s => s.type == "TransportInfo").ThenBy(s => s.type);

			//we ant the appropriate categories for the currently loaded Prefab type first.
			PrefabInfo prefabInfo = Singleton<ToolManager>.instance.m_properties.m_editPrefabInfo;
			if(null != prefabInfo as BuildingInfo) 
			{
				sortedcategorylist = reducedCategoryList.OrderBy(s => s.type == "BuildingInfo").ThenBy(s => s.type).ThenBy(s => s.type == "NetInfo").ThenBy(s => s.type == "PropInfo").ThenBy(s => s.type == "TreeInfo").ThenBy(s => s.type == "VehicleInfo").ThenBy(s => s.type == "CitizenInfo").ThenBy(s => s.type == "TransportInfo");
			} 
			else if(null != prefabInfo as NetInfo) 
			{
				sortedcategorylist = reducedCategoryList.OrderBy(s => s.type == "NetInfo").ThenBy(s => s.type).ThenBy(s => s.type == "BuildingInfo").ThenBy(s => s.type == "PropInfo").ThenBy(s => s.type == "TreeInfo").ThenBy(s => s.type == "VehicleInfo").ThenBy(s => s.type == "CitizenInfo").ThenBy(s => s.type == "TransportInfo");
			} 
			else if(null != prefabInfo as PropInfo) 
			{
				sortedcategorylist = reducedCategoryList.OrderBy(s => s.type == "PropInfo").ThenBy(s => s.type).ThenBy(s => s.type == "BuildingInfo").ThenBy(s => s.type == "NetInfo").ThenBy(s => s.type == "TreeInfo").ThenBy(s => s.type == "VehicleInfo").ThenBy(s => s.type == "CitizenInfo").ThenBy(s => s.type == "TransportInfo");
			} 
			else if(null != prefabInfo as TreeInfo) 
			{
				sortedcategorylist = reducedCategoryList.OrderBy(s => s.type == "TreeInfo").ThenBy(s => s.type).ThenBy(s => s.type == "BuildingInfo").ThenBy(s => s.type == "NetInfo").ThenBy(s => s.type == "PropInfo").ThenBy(s => s.type == "VehicleInfo").ThenBy(s => s.type == "CitizenInfo").ThenBy(s => s.type == "TransportInfo");
			} 
			else if(null != prefabInfo as VehicleInfo) 
			{
				sortedcategorylist = reducedCategoryList.OrderBy(s => s.type == "VehicleInfo").ThenBy(s => s.type).ThenBy(s => s.type == "BuildingInfo").ThenBy(s => s.type == "NetInfo").ThenBy(s => s.type == "PropInfo").ThenBy(s => s.type == "TreeInfo").ThenBy(s => s.type == "CitizenInfo").ThenBy(s => s.type == "TransportInfo");
			} 
			else if(null != prefabInfo as CitizenInfo) 
			{
				sortedcategorylist = reducedCategoryList.OrderBy(s => s.type == "CitizenInfo").ThenBy(s => s.type).ThenBy(s => s.type == "BuildingInfo").ThenBy(s => s.type == "NetInfo").ThenBy(s => s.type == "PropInfo").ThenBy(s => s.type == "TreeInfo").ThenBy(s => s.type == "VehicleInfo").ThenBy(s => s.type == "TransportInfo");
			} 
			else if(null != prefabInfo as TransportInfo) 
			{
				sortedcategorylist = reducedCategoryList.OrderBy(s => s.type == "TransportInfo").ThenBy(s => s.type).ThenBy(s => s.type == "BuildingInfo").ThenBy(s => s.type == "NetInfo").ThenBy(s => s.type == "PropInfo").ThenBy(s => s.type == "TreeInfo").ThenBy(s => s.type == "VehicleInfo").ThenBy(s => s.type == "CitizenInfo");
			} 
			else 
			{
			}
			foreach(CategoryInfo categoryInfo1 in sortedcategorylist) 
			{
				m_UICategoryDropDown.AddItem(categoryInfo1.type);
			}
		}

		private void AddCategory(string type)
		{
			CategoryInfo categoryInfo = new()
			{
				type = type
			};
			if (categoryList.IndexOf(categoryInfo) == -1) 
			{
				categoryList.Add(categoryInfo);
			}
		}


	}
}
