using AssetEditorTools.Panels.GroupContainer;
using ColossalFramework.UI;

namespace AssetEditorTools.Panels
{
	public class GroupContianerPanel : UIPanel
	{

		public override void Start() 
		{ 
			base.Start();

			backgroundSprite = "SubcategoriesPanel";
			clipChildren = true;

			width = 393;
			height = 110;

			relativePosition = new UnityEngine.Vector3(5f, 38f, 0f);

            var servicePanel = AddUIComponent<ServicePanel>();
			AttachUIComponent(servicePanel.gameObject);

			var subServicePanel = AddUIComponent<SubServicePanel>();
			AttachUIComponent(subServicePanel.gameObject);

			var levelPanel = AddUIComponent<LevelPanel>();
			AttachUIComponent(levelPanel.gameObject);

		}

	}
}
