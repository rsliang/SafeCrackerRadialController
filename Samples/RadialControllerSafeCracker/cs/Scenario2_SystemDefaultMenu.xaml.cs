using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Input;
using Windows.Storage.Streams;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    public sealed partial class Scenario2_SystemDefaultMenu : Page
    {
        RadialControllerConfiguration config;
        RadialController controller;
        RadialControllerMenuItem customItem;

        public Scenario2_SystemDefaultMenu()
        {
            this.InitializeComponent();

            controller = RadialController.CreateForCurrentView();
            controller.RotationChanged += Controller_RotationChanged;


            customItem = RadialControllerMenuItem.CreateFromKnownIcon("Item1", RadialControllerMenuKnownIcon.InkColor);

            // Create an icon for the custom tool.
            RandomAccessStreamReference iconNum1 = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Num1.png"));
            RandomAccessStreamReference iconNum2 = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Num2.png"));
            RandomAccessStreamReference iconNum3 = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Num3.png"));
            RandomAccessStreamReference iconNum4 = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Num4.png"));
            RandomAccessStreamReference iconNum5 = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Num5.png"));
            RandomAccessStreamReference iconNum6 = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Num6.png"));
            RandomAccessStreamReference iconNum7 = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Num7.png"));
            RandomAccessStreamReference iconNum8 = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Num8.png"));
            RandomAccessStreamReference iconNum9 = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Num9.png"));
            RandomAccessStreamReference iconNum0 = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Num0.png"));

            // Create a menu item for the custom tool.
            RadialControllerMenuItem myItemNum1 = RadialControllerMenuItem.CreateFromIcon("1", iconNum1);
            RadialControllerMenuItem myItemNum2 = RadialControllerMenuItem.CreateFromIcon("2", iconNum2);
            RadialControllerMenuItem myItemNum3 = RadialControllerMenuItem.CreateFromIcon("3", iconNum3);
            RadialControllerMenuItem myItemNum4 = RadialControllerMenuItem.CreateFromIcon("4", iconNum4);
            RadialControllerMenuItem myItemNum5 = RadialControllerMenuItem.CreateFromIcon("5", iconNum5);
            RadialControllerMenuItem myItemNum6 = RadialControllerMenuItem.CreateFromIcon("6", iconNum6);
            RadialControllerMenuItem myItemNum7 = RadialControllerMenuItem.CreateFromIcon("7", iconNum7);
            RadialControllerMenuItem myItemNum8 = RadialControllerMenuItem.CreateFromIcon("8", iconNum8);
            RadialControllerMenuItem myItemNum9 = RadialControllerMenuItem.CreateFromIcon("9", iconNum9);
            RadialControllerMenuItem myItemNum0 = RadialControllerMenuItem.CreateFromIcon("0", iconNum0);

            // Add the custom tool to the RadialController menu.
            controller.Menu.Items.Add(myItemNum1);
            controller.Menu.Items.Add(myItemNum2);
            controller.Menu.Items.Add(myItemNum3);
            controller.Menu.Items.Add(myItemNum4);
            controller.Menu.Items.Add(myItemNum5);
            controller.Menu.Items.Add(myItemNum6);
            controller.Menu.Items.Add(myItemNum7);
            controller.Menu.Items.Add(myItemNum8);
            controller.Menu.Items.Add(myItemNum9);
            controller.Menu.Items.Add(myItemNum0);


        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            controller.Menu.Items.Clear();
        }

        private void Controller_RotationChanged(RadialController sender, RadialControllerRotationChangedEventArgs args)
        {
            log.Text += "\nRotation Changed Delta = " + args.RotationDeltaInDegrees;
            if (args.Contact != null)
            {
                log.Text += "\nBounds = " + args.Contact.Bounds.ToString();
                log.Text += "\nPosition = " + args.Contact.Position.ToString();
            }

            Slider1.Value += args.RotationDeltaInDegrees;
        }

        private void ModifySystemDefaults(object sender, RoutedEventArgs e)
        {
            config = RadialControllerConfiguration.GetForCurrentView();
            config.SetDefaultMenuItems(new[] { RadialControllerSystemMenuItemKind.Volume, RadialControllerSystemMenuItemKind.Scroll });

        }
        private void InitSafeCrackerSystemDefaults(object sender, RoutedEventArgs e)
        {
            config = RadialControllerConfiguration.GetForCurrentView();
            //config.SetDefaultMenuItems(new[] { RadialControllerSystemMenuItemKind.Volume, RadialControllerSystemMenuItemKind.Scroll });
            config.SetDefaultMenuItems(new[] { RadialControllerSystemMenuItemKind.Volume, RadialControllerSystemMenuItemKind.Scroll, RadialControllerSystemMenuItemKind.NextPreviousTrack, RadialControllerSystemMenuItemKind.UndoRedo, RadialControllerSystemMenuItemKind.Zoom });
        }

        private void Select_Volume(object sender, RoutedEventArgs e)
        {
            config = RadialControllerConfiguration.GetForCurrentView();
            config.TrySelectDefaultMenuItem(RadialControllerSystemMenuItemKind.Volume);
        }

        private void SelectPreviouslySelectedItem(object sender, RoutedEventArgs e)
        {
            if (controller.Menu.TrySelectPreviouslySelectedMenuItem())
            {
                PrintSelectedItem();
            }
        }

        private void OnLogSizeChanged(object sender, object e)
        {
            logViewer.ChangeView(null, logViewer.ExtentHeight, null);
        }

        private void PrintSelectedItem()
        {
            RadialControllerMenuItem selectedMenuItem = controller.Menu.GetSelectedMenuItem();

            if (selectedMenuItem == customItem)
            {
                log.Text += "\n Selected : " + selectedMenuItem.DisplayText;
            }
            else
            {
                log.Text += "\n Selected System Item";
            }
        }

        private void Reset_ToDefault(object sender, RoutedEventArgs e)
        {
            config = RadialControllerConfiguration.GetForCurrentView();
            //config.ResetToDefaultMenuItems();

            //config.SetDefaultMenuItems(new[] { RadialControllerSystemMenuItemKind.Volume, RadialControllerSystemMenuItemKind.Scroll });
            config.SetDefaultMenuItems(new[] { RadialControllerSystemMenuItemKind.Volume, RadialControllerSystemMenuItemKind.Scroll, RadialControllerSystemMenuItemKind.NextPreviousTrack, RadialControllerSystemMenuItemKind.UndoRedo, RadialControllerSystemMenuItemKind.Zoom });
        }

        // add all number custom menu items for Safe Cracker
        private void AddAllCustomItems(object sender, RoutedEventArgs e)
        {
            if (!controller.Menu.Items.Contains(customItem))
            {
                //controller.Menu.Items.Add(customItem);


                // Create an icon for the custom tool.
                RandomAccessStreamReference iconNum1 = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Num1.png"));
                RandomAccessStreamReference iconNum2 = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Num2.png"));
                RandomAccessStreamReference iconNum3 = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Num3.png"));
                RandomAccessStreamReference iconNum4 = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Num4.png"));
                RandomAccessStreamReference iconNum5 = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Num5.png"));
                RandomAccessStreamReference iconNum6 = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Num6.png"));
                RandomAccessStreamReference iconNum7 = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Num7.png"));
                RandomAccessStreamReference iconNum8 = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Num8.png"));
                RandomAccessStreamReference iconNum9 = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Num9.png"));
                RandomAccessStreamReference iconNum0 = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Num0.png"));

                // Create a menu item for the custom tool.
                RadialControllerMenuItem myItemNum1 = RadialControllerMenuItem.CreateFromIcon("1", iconNum1);
                RadialControllerMenuItem myItemNum2 = RadialControllerMenuItem.CreateFromIcon("2", iconNum2);
                RadialControllerMenuItem myItemNum3 = RadialControllerMenuItem.CreateFromIcon("3", iconNum3);
                RadialControllerMenuItem myItemNum4 = RadialControllerMenuItem.CreateFromIcon("4", iconNum4);
                RadialControllerMenuItem myItemNum5 = RadialControllerMenuItem.CreateFromIcon("5", iconNum5);
                RadialControllerMenuItem myItemNum6 = RadialControllerMenuItem.CreateFromIcon("6", iconNum6);
                RadialControllerMenuItem myItemNum7 = RadialControllerMenuItem.CreateFromIcon("7", iconNum7);
                RadialControllerMenuItem myItemNum8 = RadialControllerMenuItem.CreateFromIcon("8", iconNum8);
                RadialControllerMenuItem myItemNum9 = RadialControllerMenuItem.CreateFromIcon("9", iconNum9);
                RadialControllerMenuItem myItemNum0 = RadialControllerMenuItem.CreateFromIcon("0", iconNum0);

                // Add the custom tool to the RadialController menu.
                controller.Menu.Items.Add(myItemNum1);
                controller.Menu.Items.Add(myItemNum2);
                controller.Menu.Items.Add(myItemNum3);
                controller.Menu.Items.Add(myItemNum4);
                controller.Menu.Items.Add(myItemNum5);
                controller.Menu.Items.Add(myItemNum6);
                controller.Menu.Items.Add(myItemNum7);
                controller.Menu.Items.Add(myItemNum8);
                controller.Menu.Items.Add(myItemNum9);
                controller.Menu.Items.Add(myItemNum0);
            }
        }
        private void AddCustomItem(object sender, RoutedEventArgs e)
        {
            if (!controller.Menu.Items.Contains(customItem))
            {
                controller.Menu.Items.Add(customItem);
            }
        }

        private void RemoveCustomItem(object sender, RoutedEventArgs e)
        {
            if (controller.Menu.Items.Contains(customItem))
            {
                controller.Menu.Items.Remove(customItem);
            }
        }

        private void SelectCustomItem(object sender, RoutedEventArgs e)
        {
            if (controller.Menu.Items.Contains(customItem))
            {
                controller.Menu.SelectMenuItem(customItem);
            }
        }

        private void Remove_Defaults(object sender, RoutedEventArgs e)
        {
            config = RadialControllerConfiguration.GetForCurrentView();
            config.SetDefaultMenuItems(new RadialControllerSystemMenuItemKind[] { });
        }
    }
}
