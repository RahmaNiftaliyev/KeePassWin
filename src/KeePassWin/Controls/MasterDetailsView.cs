﻿using Prism.Windows.AppModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace KeePass.Win.Controls
{
    public enum MasterDetailsViewState
    {
        /// <summary>
        /// Only the Master view is shown
        /// </summary>
        Master,

        /// <summary>
        /// Only the Details view is shown
        /// </summary>
        Details,

        /// <summary>
        /// Both the Master and Details views are shown
        /// </summary>
        Both
    }

    public class MasterDetailsView : Microsoft.Toolkit.Uwp.UI.Controls.MasterDetailsView
    {
        public static readonly DependencyProperty FooterTemplateProperty =
            DependencyProperty.Register(nameof(FooterTemplate), typeof(DataTemplate), typeof(MasterDetailsView), new PropertyMetadata(null));

        public static readonly DependencyProperty FooterContentProperty =
            DependencyProperty.Register(nameof(FooterContent), typeof(object), typeof(MasterDetailsView), new PropertyMetadata(null));

        public static readonly DependencyProperty ItemClickCommandProperty =
            DependencyProperty.Register(nameof(ItemClickCommand), typeof(ICommand), typeof(MasterDetailsView), new PropertyMetadata(null));

        public static readonly DependencyProperty SelectedMasterItemProperty =
            DependencyProperty.Register(nameof(SelectedMasterItem), typeof(object), typeof(MasterDetailsView), new PropertyMetadata(null, ListSelectedItemPropertyChanged));

        public static readonly DependencyProperty DeviceGestureServiceProperty =
            DependencyProperty.Register(nameof(DeviceGestureService), typeof(IDeviceGestureService), typeof(MasterDetailsView), new PropertyMetadata(null, DeviceGestureServicePropertyChanged));

        public IDeviceGestureService DeviceGestureService
        {
            get { return (IDeviceGestureService)GetValue(DeviceGestureServiceProperty); }
            set { SetValue(DeviceGestureServiceProperty, value); }
        }

        public object SelectedMasterItem
        {
            get { return (object)GetValue(SelectedMasterItemProperty); }
            set { SetValue(SelectedMasterItemProperty, value); }
        }

        public DataTemplate FooterTemplate
        {
            get { return (DataTemplate)GetValue(FooterTemplateProperty); }
            set { SetValue(FooterTemplateProperty, value); }
        }

        public object FooterContent
        {
            get { return GetValue(FooterContentProperty); }
            set { SetValue(FooterContentProperty, value); }
        }

        public ICommand ItemClickCommand
        {
            get { return (ICommand)GetValue(ItemClickCommandProperty); }
            set { SetValue(ItemClickCommandProperty, value); }
        }

        public MasterDetailsViewState ViewState
        {
            get
            {
                var groups = VisualStateManager.GetVisualStateGroups(VisualTreeHelper.GetChild(this, 0) as FrameworkElement);
                var currentState = groups.First(g => string.Equals(g.Name, "WidthStates", StringComparison.Ordinal)).CurrentState;

                if (string.Equals(currentState?.Name, "NarrowState", StringComparison.Ordinal))
                {
                    return SelectedItem == null ? MasterDetailsViewState.Master : MasterDetailsViewState.Details;
                }
                else
                {
                    return MasterDetailsViewState.Both;
                }
            }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var list = (ListView)GetTemplateChild("MasterList");
            list.ItemClick -= ListItemClick;
            list.ItemClick += ListItemClick;
        }

        private bool IsInState(IEnumerable<VisualStateGroup> groups, string groupName, string stateName)
        {
            var currentState = groups.First(g => string.Equals(g.Name, groupName, StringComparison.Ordinal)).CurrentState;

            return string.Equals(currentState?.Name, stateName, StringComparison.Ordinal);
        }

        private static void DeviceGestureServicePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = (MasterDetailsView)d;

            var previous = e.OldValue as IDeviceGestureService;
            var newValue = e.NewValue as IDeviceGestureService;

            if (previous != null)
            {
                previous.GoBackRequested -= view.DeviceGoBackRequested;
            }

            if (newValue != null)
            {
                newValue.GoBackRequested += view.DeviceGoBackRequested;
            }
        }

        private void DeviceGoBackRequested(object sender, DeviceGestureEventArgs e)
        {
            if (SelectedItem != null)
            {
                e.Handled = true;
                e.Cancel = true;

                SelectedItem = null;
            }
        }

        private static void ListSelectedItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = (MasterDetailsView)d;

            if (view.ViewState == MasterDetailsViewState.Both && view.ItemClickCommand?.CanExecute(e.NewValue) == false)
            {
                view.SelectedItem = e.NewValue;
            }
            else
            {
                view.SelectedItem = null;
            }
        }

        private void ListItemClick(object sender, ItemClickEventArgs e)
        {
            var command = ItemClickCommand;
            var newItem = e.ClickedItem;

            if (command?.CanExecute(newItem) == true)
            {
                command.Execute(newItem);
            }
            else
            {
                SelectedItem = newItem;
            }
        }
    }
}