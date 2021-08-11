using System;
using OSTool.Core;
using System.Collections.Generic;
using static OSTool.Core.StringOrInt;
using static OSTool.CoreExtension.MainMenuData;

namespace OSTool.CoreExtension
{
    public class ExpanderItemsListViewModel : BaseViewModel
    {
        #region Private 

        private List<ExpanderViewModel> wItems;

        private object wName;

        #endregion

        #region Commands

        public BaseCommand Command { get; set; }

        #endregion

        #region Public

        public List<ExpanderViewModel> Items
        {
            get { return wItems; }

            set
            {
                if (wItems == value)
                    return;

                wItems = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public object Name
        {
            get { return wName; }

            set
            {
                if (value == wName)
                    return;

                wName = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        #endregion

        #region Voids

        public void CheckSelect(object[] multi)
        {
            List<ExpanderViewModel> expander = multi[0] as List<ExpanderViewModel>;

            for (int i = 0; i <= expander.Count - 1; i++)
            {
                if (expander[i].ControlTag == multi[1])
                {
                    if (expander[i].MainTag == multi[2])
                    {
                        expander[i].MainSelected = multi[3];
                    }
                    if (expander[i].SecondaryTag == multi[2])
                    {
                        expander[i].SecondarySelected = multi[3];
                    }
                }
            }
        }

        public void ApplyChanges(List<ExpanderViewModel> multi)
        {
            for (int i = 0; i <= multi.Count - 1; i++)
            {
                if (multi[i].MainSelected.ToString() == "True")
                {
                    var split = Split(multi[i].MainTag.ToString());
                    Execute(Name, split[0], Convert.ToInt32(split[1]));
                }
                if (multi[i].SecondarySelected.ToString() == "True")
                {
                    var split = Split(multi[i].SecondaryTag.ToString());
                    Execute(Name, split[0], Convert.ToInt32(split[1]));
                }
            }
        }

        #endregion
    }
}