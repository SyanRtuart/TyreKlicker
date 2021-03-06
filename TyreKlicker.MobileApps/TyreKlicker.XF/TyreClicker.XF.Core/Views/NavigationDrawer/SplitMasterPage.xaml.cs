﻿using System;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using TyreKlicker.XF.Core.ViewModels;
using Xamarin.Forms;

namespace TyreKlicker.XF.Core.Pages
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Master)]
    public partial class SplitMasterPage : MvxContentPage<SplitMasterViewModel>
    {
        public SplitMasterPage()
        {
            InitializeComponent();
        }

        public void ToggleClicked(object sender, EventArgs e)
        {
            if (Parent is MasterDetailPage md)
            {
                md.MasterBehavior = MasterBehavior.Popover;
                md.IsPresented = !md.IsPresented;
                ((ListView) sender).SelectedItem = null;
            }
        }
    }
}