using ICities;
using System;
using AssetEditorTools.Utils;
using ColossalFramework.UI;

namespace AssetEditorTools
{
    public class Mod : LoadingExtensionBase, IUserMod
    {
        public string Name => "Asset Editor Tools";

        public string Description => "Allows you to change the PrefabAI, UICategory, UIPriority and ItemClass in the Asset Editor. including building, vehicle, citizen, network etc.";
    
        public override void OnLevelLoaded(LoadMode mode) 
        {
			try 
            {
				if (mode == LoadMode.LoadAsset || mode == LoadMode.NewAsset) 
                {
					var view = UIView.GetAView();
			        var FullScreenContainer = view.FindUIComponent("FullScreenContainer");
			        var DecorationProperties = FullScreenContainer.Find<UIPanel>("DecorationProperties");
			        var Container = DecorationProperties.Find<UIScrollablePanel>("Container");

                    var itemClassGroupContianerPanel = Container.AddUIComponent<ItemClassGroupContianerPanel>();
                    Container.AttachUIComponent(itemClassGroupContianerPanel.gameObject);

                    var uICategoryPanel = Container.AddUIComponent<UICategoryPanel>();
                    Container.AttachUIComponent(uICategoryPanel.gameObject);

                    var prefabAIPanel = Container.AddUIComponent<PrefabAIPanel>();
                    Container.AttachUIComponent(prefabAIPanel.gameObject);

                    Container.AddUIComponent<UIPriorityPanel>();
                    Container.AddUIComponent<SpritePanel>();

                    view.AddUIComponent(typeof(ShowPropertiesPanel));
				}
			} 
            catch (Exception ex) 
            {
                LogHelper.Error(ex.Message);
                LogHelper.Error("Asset Editor Tools caused an error");
			}
		}
    }

}