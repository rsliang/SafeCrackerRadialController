//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Input;
using Windows.Storage.Streams;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace SDKTemplate
{
    public sealed partial class Scenario3_SafeCrackerHookup : Page
    {
        private MainPage rootPage;
        private RadialController Controller;

        private int activeItemIndex;
        private List<RadialControllerMenuItem> menuItems;
        //private List<Slider> sliders;
        private List<ToggleSwitch> toggles;

        private List<int> safeCombo;
        private int curSafeComboIdx;
        private List<int> selectedNumber;

        //private Storyboard rotation = new Storyboard();
        private double curComboDialIdx;
        private double prevComboDialIdx;
        private double tempComboDialIdx;
        private double curSafeCombo;

        RadialControllerMenuItem default0;
        //RadialControllerMenuItem default1;

        private Storyboard rotation = new Storyboard();


/*
        private void Pitch_Click(object sender, RoutedEventArgs e)
        {
            Rotate("X", ref Display);
        }
        private void Flip_Click(object sender, RoutedEventArgs e)
        {
            ScaleTransform transform = new ScaleTransform();
            transform.ScaleY = -1;
            Display.RenderTransform = transform;
        }
*/

        public Scenario3_SafeCrackerHookup()
        {
            this.InitializeComponent();
            InitializeApp();
            InitializeController();
            CreateMenuItems();
            AddAllItems();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            rootPage = MainPage.Current;

            //log.Text += "\n" + " OnNavigateTo ";

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (Controller != null)
            {
                log.Text += "\n" + " OnNavigateFrom ";
                //Controller.Menu.Items.Clear();
            }
        }

        private void InitializeApp()
        {
            // Preset safe combo
            safeCombo = new List<int> { 20, 30, 20 };

            // Initialize combo position
            curSafeComboIdx = 0;
            prevComboDialIdx = 0;

            // Initialize combo numbers found
            selectedNumber = new List<int> { };

            // Create menu items to select which safe to crack open 
            default0 = RadialControllerMenuItem.CreateFromIcon("Safe 1", RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Safe1.png")));
            //default1 = RadialControllerMenuItem.CreateFromIcon("Safe 2", RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Safe2.png")));

        }

        private void InitializeController()
        {
            Controller = RadialController.CreateForCurrentView();
            //Controller.UseAutomaticHapticFeedback = false;
            Controller.RotationResolutionInDegrees = 10;

            // Wire events
            Controller.RotationChanged += Controller_RotationChanged;
            Controller.ButtonClicked += Controller_ButtonClicked;
            Controller.ScreenContactStarted += Controller_ScreenContactStarted;
            Controller.ScreenContactContinued += Controller_ScreenContactContinued;
            Controller.ScreenContactEnded += Controller_ScreenContactEnded;
            Controller.ControlAcquired += Controller_ControlAcquired;
            Controller.ControlLost += Controller_ControlLost;
        
        }

        private void CreateMenuItems()
        {
            menuItems = new List<RadialControllerMenuItem>
            {
                default0, //RadialControllerMenuItem.CreateFromIcon("Safe 1", RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Safe1.png"))),
                //default1, //RadialControllerMenuItem.CreateFromIcon("Safe 2", RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Safe2.png"))),                             
            };

            //sliders = new List<Slider> { Slider0 };  //Slider1, Slider2, Slider3, Slider4, Slider5 };
            toggles = new List<ToggleSwitch> { Toggle0 };  //Toggle1, Toggle2, Toggle3, Toggle4, Toggle5 };

            for (int i = 0; i < menuItems.Count; ++i)
            {
                RadialControllerMenuItem radialControllerItem = menuItems[i];
                int index = i;

                radialControllerItem.Invoked += (sender, args) => { OnItemInvoked(index); };
            }
        }

        private void OnItemInvoked(int selectedItemIndex)
        {
            activeItemIndex = selectedItemIndex; 
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            RadialControllerMenuItem radialControllerMenuItem = GetRadialControllerMenuItemFromSender(sender);

            if (!Controller.Menu.Items.Contains(radialControllerMenuItem))
            {
                Controller.Menu.Items.Add(radialControllerMenuItem);
            }
        }

        private void AddAllItems()
        {
            AddAllSystemItems();
        }
        private void AddAllSystemItems()
        {
            //Remove Scroll/Zoom/Undo tools not needed by Safe Cracker app
            RadialControllerConfiguration config;
            config = RadialControllerConfiguration.GetForCurrentView();

            // note: if remove all system menu items, then default system menu item will display
            //      Therefore, you must have atleast 1 system menu item...
            //config.SetDefaultMenuItems(Enumerable.Empty<RadialControllerSystemMenuItemKind>());
            //config.SetDefaultMenuItems(new RadialControllerSystemMenuItemKind[] { });
            config.SetDefaultMenuItems(new[] { RadialControllerSystemMenuItemKind.Volume });
            //config.TrySelectDefaultMenuItem(RadialControllerSystemMenuItemKind.Volume);

            // Add custom tool menu items
            if (!Controller.Menu.Items.Contains(default0))
            {
                Controller.Menu.Items.Add(default0);
            }

            //Create a handler for when a system default menu item is selected
            default0.Invoked += default0_Invoked;
            //default1.Invoked += default1_Invoked;
        }

        // Handling RadialController Input for System Default Menu items
        private void default0_Invoked(RadialControllerMenuItem sender, object args)
        {
            //PrintSelectedItem();
            //log.Text += "\n Selected : " + GetSelectedMenuItemName() + " default0";
            activeItemIndex = 0;

        }

        private void default1_Invoked(RadialControllerMenuItem sender, object args)
        {
            PrintSelectedItem();
            log.Text += "\n Selected : " + GetSelectedMenuItemName() + " default1";
            activeItemIndex =  1;
        }

        private void RemoveItem(object sender, RoutedEventArgs e)
        {
            RadialControllerMenuItem radialControllerMenuItem = GetRadialControllerMenuItemFromSender(sender);

            if (Controller.Menu.Items.Contains(radialControllerMenuItem))
            {
                Controller.Menu.Items.Remove(radialControllerMenuItem);
            }
        }

        private void SelectItem(object sender, RoutedEventArgs e)
        {
            RadialControllerMenuItem radialControllerMenuItem = GetRadialControllerMenuItemFromSender(sender);

            if (Controller.Menu.Items.Contains(radialControllerMenuItem))
            {
                Controller.Menu.SelectMenuItem(radialControllerMenuItem);
                PrintSelectedItem();
            }
        }

        private void SelectPreviouslySelectedItem(object sender, RoutedEventArgs e)
        {
            if (Controller.Menu.TrySelectPreviouslySelectedMenuItem())
            {
                PrintSelectedItem();
            }
        }

        private RadialControllerMenuItem GetRadialControllerMenuItemFromSender(object sender)
        {
            Button button = sender as Button;
            int index = Convert.ToInt32(button.CommandParameter);

            return menuItems[index];
        }

        private int GetRadialControllerMenuItemIdxFromSender(object sender)
        {
            Button button = sender as Button;
            int index = Convert.ToInt32(button.CommandParameter);

            return index;
        }

        private void PrintSelectedItem(object sender, RoutedEventArgs e)
        {
            PrintSelectedItem();
        }

        private void PrintSelectedItem()
        {
            log.Text += "\n Selected : " + GetSelectedMenuItemName();
        }

        private string GetSelectedMenuItemName()
        {
            RadialControllerMenuItem selectedMenuItem = Controller.Menu.GetSelectedMenuItem();
            
            if (selectedMenuItem == menuItems[activeItemIndex])
            {
                return selectedMenuItem.DisplayText;
            }
            else
            {
                return "System Item";
            }
            
        }
        private void OnLogSizeChanged(object sender, object e)
        {
            logViewer.ChangeView(null, logViewer.ExtentHeight, null);
        }

        //private void GridView_SelectionChanged
        private void Controller_ControlAcquired(RadialController sender, RadialControllerControlAcquiredEventArgs args)
        {
            //log.Text += "\nControl Acquired";
            //LogContactInfo(args.Contact);     
        }
        private bool IsSafeCombo(double num)
        {
            int item = (int)num;
            if (item == safeCombo[curSafeComboIdx] )
            {
                log.Text += "\nSafe " + (activeItemIndex + 1) + ": You CRACKED combo " + (curSafeComboIdx + 1) + " = " + item;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CheckSafeCombo(double combo)
        {
            // Save selected number for Safe Cracker
            RadialControllerMenuItem selectedMenuItem = Controller.Menu.GetSelectedMenuItem();

            Controller = RadialController.CreateForCurrentView();

            //string item = "";
            int item = (int)combo;

            //if (item==safeCombo[curSafeComboIdx] && (curSafeComboIdx < 3))
            if (IsSafeCombo(combo) && (curSafeComboIdx < 3))
            {
                //Controller.UseAutomaticHapticFeedback = true;

                selectedNumber.Add(item);
                //log.Text += "\nSafe "+ (activeItemIndex+1) + ": You CRACKED combo " + (curSafeComboIdx+1) + " = " + item;

                // increment current safe combo index
                curSafeComboIdx++;

                // Check if selected safe is unlocked after input
                if (curSafeComboIdx == 3)
                {
                    // All safe combo correctly inputted
                    
                    // Open the safe...
                    ToggleSwitch toggle = toggles[activeItemIndex];
                    toggle.IsOn = true; // Set selected safe to unloack state
                    

                    log.Text += "\nYou've CRACK OPENED Safe " + (activeItemIndex+1) + ".  Good job!";

                    // reset app settings
                    //InitializeApp();
                    // Initialize combo position
                    curSafeComboIdx = 0;
                    prevComboDialIdx = 0;

                    // Initialize combo numbers found
                    selectedNumber = new List<int> { };
                } 
                else
                {
                    log.Text += "\n Keep going..." + (3-curSafeComboIdx) + " more to go.";
                }

            }
            else
            {
                //log.Text += "\nSafe " + activeItemIndex + ": Sorry, " + item + " is incorrect.  Please try again.";
                //Controller.UseAutomaticHapticFeedback = false;
            }
        }
        private void Controller_ControlLost(RadialController sender, object args)
        {
            log.Text += "\nControl Lost";

        }

        private void Controller_ScreenContactStarted(RadialController sender, RadialControllerScreenContactStartedEventArgs args)
        {
            log.Text += "\nContact Started ";
            LogContactInfo(args.Contact);
        }

        private void Controller_ScreenContactContinued(RadialController sender, RadialControllerScreenContactContinuedEventArgs args)
        {

            log.Text += "\nContact Continued ";
            LogContactInfo(args.Contact);
        }

        private void Controller_ScreenContactEnded(RadialController sender, object args)
        {
            log.Text += "\nContact Ended";
        }

        private void Controller_ButtonClicked(RadialController sender, RadialControllerButtonClickedEventArgs args)
        {

            log.Text += "\nButton Clicked ";
            //LogContactInfo(args.Contact);
        }

        private void Controller_RotationChanged(RadialController sender, RadialControllerRotationChangedEventArgs args)
        {
            //log.Text += "\nRotation Changed Delta = " + args.RotationDeltaInDegrees;
            //LogContactInfo(args.Contact);

            /*
            ToggleSwitch toggle = toggles[activeItemIndex];

            if (toggle.IsOn)  // open the safe
            {
                //sliders[activeItemIndex].Value += args.RotationDeltaInDegrees;
            }
            */

            tempComboDialIdx = args.RotationDeltaInDegrees;
            prevComboDialIdx = curComboDialIdx;          
            curComboDialIdx += tempComboDialIdx; 

            if (tempComboDialIdx < 0) //rotate counterclockwise
            {
               curSafeCombo = curSafeCombo + (tempComboDialIdx)*(-1);
                if (curSafeCombo >= 100)
                {
                    curSafeCombo = curSafeCombo - 100;
                } 
            } else // rotate clockwise
            {
                curSafeCombo = curSafeCombo - tempComboDialIdx;
                if (curSafeCombo < 0)
                {
                    curSafeCombo = 100 + curSafeCombo;
                }
            }

            // rotate combo animation
            Rotate_Combo(prevComboDialIdx, curComboDialIdx);
              
            /*
            if (IsSafeCombo(curSafeCombo) )
            {
                log.Text += "\nFound safe combo";
            }
            */
        }

        private void LogContactInfo(RadialControllerScreenContact contact)
        {
            if (contact != null)
            {
                log.Text += "\nBounds = " + contact.Bounds.ToString();
                log.Text += "\nPosition = " + contact.Position.ToString();
            }
        }

        private void Toggle0_Toggled(object sender, RoutedEventArgs e)
        {

        }

        private void Remove_Defaults(object sender, RoutedEventArgs e)
        {
            RadialControllerConfiguration config;
            config = RadialControllerConfiguration.GetForCurrentView();
            config.SetDefaultMenuItems(new RadialControllerSystemMenuItemKind[] { });
        }

        private void GridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //throw new NotImplementedException();
            CreateMenuItems();

        }

        private void spinme_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //spinrect.Begin();
            /*
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = prevComboDialIdx;
            animation.To = curComboDialIdx;
            animation.BeginTime = TimeSpan.FromSeconds(1);
            //animation.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard.SetTarget(animation, spinme);
            Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(RotateTransform.Angle)" );
            spinrect.Stop();           
            spinrect.Children.Clear();
            spinrect.Children.Add(animation);
            
            //rotation.Completed += rotation_Completed;
            spinrect.Begin();
            spinrect.Completed += Rotation_Completed;
            */
        }

        private void Rotation_Completed(object sender, object e)
        {
            //throw new NotImplementedException();

            log.Text += "\nSafe " + (activeItemIndex + 1) + ": Selected combo " + (curSafeCombo);
            //CheckSafeCombo(curSafeCombo);
        }

        private void Rotate_Combo(double f, double t)
        {
            //spinrect.Begin();
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = f; // prevComboDialIdx;
            animation.To = t; // curComboDialIdx;
            animation.BeginTime = TimeSpan.FromSeconds(1);
            //animation.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard.SetTarget(animation, spinme);
            Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(RotateTransform.Angle)");
            spinrect.Stop();
            spinrect.Children.Clear();
            spinrect.Children.Add(animation);

            //rotation.Completed += rotation_Completed;
            spinrect.Begin();

            log.Text += "\nSafe " + (activeItemIndex + 1) + ": at combo = " + (curSafeCombo);

            CheckSafeCombo(curSafeCombo);
        }
        void button_Click(object sender, RoutedEventArgs e)
        {

            Storyboard storyboard = new Storyboard();
            storyboard = ((Storyboard)Resources["Storyboard2"]);
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = prevComboDialIdx;  //curComboDialIdx;
            animation.To = curComboDialIdx;
            animation.BeginTime = TimeSpan.FromSeconds(1);
            //animation.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard.SetTarget(animation, spinme);
            Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(RotateTransform.Angle)");
            //Storyboard.SetTargetProperty(animation, "(UIElement.Projection).(PlaneProjection.Rotation" + "Y" + ")");
            spinrect.Stop();
            storyboard.Children.Clear();
            storyboard.Children.Add(animation);
            //storyboard.Begin();

            //storyboard.Completed += storyBoard_Completed;
            spinrect.Begin();
        }

        private void storyBoard_Completed(object sender, EventArgs e)
        {
            // 150 -- this is final width from the storyboard (see 1.)
            //animatedControl.Width = 150;
        }




    } // Scenario3_SafeCrackerHookup
}
