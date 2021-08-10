using OSTool.Data;
using System.Collections.Generic;

namespace OSTool.Core
{
    public class ExpanderItemsListDesignModel : ExpanderItemsListViewModel
    {
        #region Singles

        public static ExpanderItemsListDesignModel Instance => new ExpanderItemsListDesignModel();

        #endregion

        public ExpanderItemsListDesignModel()
        {
            Items = new List<ExpanderViewModel>();
            Command = new BaseCommand(e => Test2(e as List<ExpanderViewModel>));

            for (int i = 0; i <= RegistryMenuData.Execute(); i++)
            {
                Items.Add(new ExpanderViewModel
                {
                    Caption = RegistryMenuData.Load(0, i),
                    Info = RegistryMenuData.Load(1, i),
                    ControlTag = "ctrl" + i.ToString(),
                    MainTag = "a" + i.ToString(),
                    SecondaryTag = "b" + i.ToString(),
                    ButtonCommand = new BaseCommand(e => Test(e as object[]))
                });
            }
        }
    }
}