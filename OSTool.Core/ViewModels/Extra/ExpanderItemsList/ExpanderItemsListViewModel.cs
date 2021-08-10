using System;
using System.Collections.Generic;

namespace OSTool.Core
{
    public class ExpanderItemsListViewModel : BaseViewModel
    {
        #region Private 

        private List<ExpanderViewModel> wItems;

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

        #endregion

        #region Voids

        public void Test(object[] multi)
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
                    else
                    {
                        expander[i].SecondarySelected = multi[3];
                    }
                }
            }
        }

        public void Test2(List<ExpanderViewModel> multi)
        {
            for (int i = 0; i <= multi.Count - 1; i++)
            {
                Console.WriteLine(multi[i].MainTag + ":" + multi[i].MainSelected + "\n" + multi[i].SecondaryTag + ":" + multi[i].SecondarySelected);
                //DialogBox.Show("Stats", multi[i].MainTag + ":" + multi[i].MainSelected + "\n" + multi[i].SecondaryTag + ":" + multi[i].SecondarySelected);
            }
        }

        #endregion
    }
}