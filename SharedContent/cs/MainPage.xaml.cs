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

using System;
using System.Collections.Generic;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//using Windows.UI.Xaml;
//using Windows.UI.Xaml.Controls;
//using Windows.UI.Xaml.Navigation;
using Windows.UI.Input;
using Windows.Storage.Streams;
//using System;
using System.Linq;
//using System.Collections.Generic;
using System.Windows.Input;
//using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Devices.Haptics;
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
		#region Constants
		private const double DEGREES_PER_COMBO = 18;	// Every 18 degrees is a possible combo (total of 20 possible combinations)
		private const int COMBOS_MULTIPLIER = 5;		// Multiply each combo by 5 to get possible combintions between 0 and 100
		#endregion // Constants

		#region Public Fields
		public static MainPage Current;
		#endregion // Public Fields

		#region Member Variables
		private int activeItemIndex;
		private RadialControllerConfiguration config;
		private RadialController controller;
		private double curRotationAngle;
		private int curComboIndex;
		private int lastCombo = -1;
		private RadialControllerMenuItem menuItem; // SafeCracker custom tool menu
		private List<RadialControllerMenuItem> menuItems;
		private MainPage rootPage;
		private List<int> safeCombo;
		private List<int> selectedNumber;
		private List<ToggleSwitch> toggles;
		#endregion // Member Variables

		public MainPage()
        {
            this.InitializeComponent();
            InitializeApp();
            InitializeController();
            CreateMenuItems();
            AddAllItems();

            // This is a static public property that allows downstream pages to get a handle to the MainPage instance
            // in order to call methods that are in this class.
            Current = this;
            //SampleTitle.Text = FEATURE_NAME;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            rootPage = MainPage.Current;
            //InitializeController();
            //log.Text += "\n" + " OnNavigateTo ";
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (controller != null)
            {
                log.Text += "\n" + " OnNavigateFrom ";
                //Controller.Menu.Items.Clear();
            }
        }

        private void InitializeApp()
        {
            // Preset safe combo
            safeCombo = new List<int> { 30, 60, 90 };

            // Initialize combination index
            curComboIndex = 0;

            // Initialize combo numbers found
            selectedNumber = new List<int> { };

            // Create menu items to select which safe to crack open 
            menuItem = RadialControllerMenuItem.CreateFromIcon("Safe 1", RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Safe1.png")));
        }

        private void InitializeController()
        {
            // Create instance of controller for SafeCracker
            controller = RadialController.CreateForCurrentView();
            controller.UseAutomaticHapticFeedback = false;
            controller.RotationResolutionInDegrees = 0.5;

            // Wire events
            controller.RotationChanged += Controller_RotationChanged;
            controller.ButtonClicked += Controller_ButtonClicked;
            controller.ScreenContactStarted += Controller_ScreenContactStarted;
            controller.ScreenContactContinued += Controller_ScreenContactContinued;
            controller.ScreenContactEnded += Controller_ScreenContactEnded;
            controller.ControlAcquired += Controller_ControlAcquired;
            controller.ControlLost += Controller_ControlLost;
        }

        private void CreateMenuItems()
        {
            menuItems = new List<RadialControllerMenuItem>
            {
                menuItem,
            };
             
            toggles = new List<ToggleSwitch> { UnlockedToggle };  

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

            if (!controller.Menu.Items.Contains(radialControllerMenuItem))
            {
                controller.Menu.Items.Add(radialControllerMenuItem);
            }
        }

        private void AddAllItems()
        {
            AddAllSystemItems();
        }
        private void AddAllSystemItems()
        {
            //Remove Volume/Scroll/Zoom/Undo tools not needed by SafeCracker app
            config = RadialControllerConfiguration.GetForCurrentView();

            // note: 
            //  1) Controller must have atleast 1 menu item.
            //  2) Add custom tool and then remove all system tools in menu
            //config.SetDefaultMenuItems(new[] { RadialControllerSystemMenuItemKind.Volume });

            // Add custom tool menu items if not exist
            if (!controller.Menu.Items.Contains(menuItem))
            {
                controller.Menu.Items.Add(menuItem);
            }

            // Remove all system default tool menu items
            config.SetDefaultMenuItems(new RadialControllerSystemMenuItemKind[] { });

            //Create a handler for when custom tool menu item is selected
            menuItem.Invoked += MenuItem_Invoked;
        }

        // Handling RadialController Input for System Default Menu items
        private void MenuItem_Invoked(RadialControllerMenuItem sender, object args)
        {
            //PrintSelectedItem();
            //log.Text += "\n Selected : " + GetSelectedMenuItemName() + " default0";
            activeItemIndex = 0;

        }

        private void OnLogSizeChanged(object sender, object e)
        {
            logViewer.ChangeView(null, logViewer.ExtentHeight, null);
        }

        private void Controller_ControlAcquired(RadialController sender, RadialControllerControlAcquiredEventArgs args)
        {
            //log.Text += "\nControl Acquired";
            //LogContactInfo(args.Contact);     
        }
        private bool IsSafeCombo(int num)
        {
            // Check if number is the currnt safe combination we are looking for
            if (num == safeCombo[curComboIndex] && curComboIndex <= safeCombo.Count())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckSafeCombo(int num)
        {
            bool isCombo = false;

            if (IsSafeCombo(num))
            {
                isCombo = true; //check if found combo

                selectedNumber.Add(num);
                log.Text += "\nSafe "+ (activeItemIndex+1) + ": You CRACKED combo " + (curComboIndex + 1) + " = " + num;

                // increment current safe combo index
                curComboIndex++;

                // Check if selected safe is unlocked after input
                if (curComboIndex == 3)
                {
                    // All safe combo correctly inputted
                    // Open the safe...
                    ToggleSwitch toggle = toggles[activeItemIndex];
                    toggle.IsOn = true; // Set selected safe to unloack state

                    log.Text += "\nYou've CRACKed the Safe " + (activeItemIndex + 1) + ".  Good job!";

                    // Initialize combo numbers found
                    selectedNumber = new List<int> { };
                }
                else
                {
                    log.Text += "\n Keep going..." + (3 - curComboIndex) + " more to go.";
                }
            }
            else
            {
                //log.Text += "\nSafe " + activeItemIndex + ": Sorry, " + item + " is incorrect.  Please try again.";
            }
            return isCombo;
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
            // log.Text += "\nRotation Changed Delta = " + args.RotationDeltaInDegrees;

			// Convert Delta to Absolute rotation
			curRotationAngle += args.RotationDeltaInDegrees;

			// Normalize to 0 - 359 degrees
			if (curRotationAngle < 0) { curRotationAngle += 360; }
			if (curRotationAngle > 359) { curRotationAngle -= 360; }

			// Rotate graphic to match absolute rotation angle
			ComboImageRotation.Angle = curRotationAngle;

			// Convert rotation to actual combo
			int curCombo = (int)Math.Round((360 - curRotationAngle) / DEGREES_PER_COMBO, 0) * COMBOS_MULTIPLIER;

			// Did the combo change?
			if (curCombo != lastCombo)
			{
				// Update
				lastCombo = curCombo;
				Debug.WriteLine("Current Combo: {0}", curCombo);

				// Check
				if (!UnlockedToggle.IsOn)
				{
					if (CheckSafeCombo(curCombo))
					{
						SendHapticFeedback(args.SimpleHapticsController, 1.0, TimeSpan.MaxValue);
					}
				}
			}

			/*
			tempComboDialIdx = args.RotationDeltaInDegrees;
            prevComboDialIdx = curComboDialIdx;
            curComboDialIdx += tempComboDialIdx*3.6; // Calibrate Dial controller for SafeCracker

            if (tempComboDialIdx < 0) //rotate counterclockwise
            {
                curSafeCombo = curSafeCombo + (args.RotationDeltaInDegrees) * (-1);
                if (curSafeCombo >= 100)
                {
                    curSafeCombo = curSafeCombo - 100;
                }
            }
            else // rotate clockwise
            {
                curSafeCombo = curSafeCombo - args.RotationDeltaInDegrees;
                if (curSafeCombo < 0)
                {
                    curSafeCombo = 100 + curSafeCombo;
                }
            }

            // rotate combo animation
            // Rotate_Combo(prevComboDialIdx, curComboDialIdx);

            
            // Validate combo if safe is locked
            ToggleSwitch toggle = toggles[activeItemIndex];

            if (!toggle.IsOn)
            {
                //if (CheckSafeCombo(curSafeCombo))
                if (IsSafeCombo(curSafeCombo))
                {
                    SendHapticFeedback(args.SimpleHapticsController,1.0, TimeSpan.MaxValue);
                }                
            } 
			*/
		}

        private void SendHapticFeedback(SimpleHapticsController hapticController, Double intensity, TimeSpan duration)
        {
            var feedbacks = hapticController.SupportedFeedback;

            foreach (SimpleHapticsControllerFeedback feedback in feedbacks)
            {
                if (feedback.Waveform == KnownSimpleHapticsControllerWaveforms.Click)
                {
                    //hapticController.SendHapticFeedback(feedback, intensity);
                    hapticController.SendHapticFeedbackForDuration(feedback, intensity, duration);
                    return;
                }
            }
        }

        private void LogContactInfo(RadialControllerScreenContact contact)
        {
            if (contact != null)
            {
                log.Text += "\nBounds = " + contact.Bounds.ToString();
                log.Text += "\nPosition = " + contact.Position.ToString();
            }
        }

        private void UnlockedToggle_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggle = toggles[activeItemIndex];
            if (!toggle.IsOn)
            {
                // re-initalize app
                Current.InitializeComponent();
                InitializeApp();

                // Initialize combination index
                curComboIndex = 0;
            }
        }

        private void Remove_Defaults(object sender, RoutedEventArgs e)
        {
            //RadialControllerConfiguration config;
            config = RadialControllerConfiguration.GetForCurrentView();
            config.SetDefaultMenuItems(new RadialControllerSystemMenuItemKind[] { });
        }

        private void GridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //throw new NotImplementedException();
            //CreateMenuItems();

        }

        private void ComboImage_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            // testing automation when safe combo dial is clicked
            
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0; //prevComboDialIdx;
            animation.To = 100; // curComboDialIdx;
            animation.BeginTime = TimeSpan.FromSeconds(1);
            //animation.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard.SetTarget(animation, ComboImage);
            Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(RotateTransform.Angle)" );
            //spinrect.Stop();           
            //spinrect.Children.Clear();
            //spinrect.Children.Add(animation);
            
            ////rotation.Completed += rotation_Completed;
            //spinrect.Begin();
            //spinrect.Completed += Rotation_Completed;
            
        }

        private void RemoveItem(object sender, RoutedEventArgs e)
        {
            RadialControllerMenuItem radialControllerMenuItem = GetRadialControllerMenuItemFromSender(sender);

            if (controller.Menu.Items.Contains(radialControllerMenuItem))
            {
                controller.Menu.Items.Remove(radialControllerMenuItem);
            }
        }

        private void SelectItem(object sender, RoutedEventArgs e)
        {
            RadialControllerMenuItem radialControllerMenuItem = GetRadialControllerMenuItemFromSender(sender);

            if (controller.Menu.Items.Contains(radialControllerMenuItem))
            {
                controller.Menu.SelectMenuItem(radialControllerMenuItem);
                //PrintSelectedItem();
            }
        }

        private void SelectPreviouslySelectedItem(object sender, RoutedEventArgs e)
        {
            if (controller.Menu.TrySelectPreviouslySelectedMenuItem())
            {
                //PrintSelectedItem();
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
            RadialControllerMenuItem selectedMenuItem = controller.Menu.GetSelectedMenuItem();

            if (selectedMenuItem == menuItems[activeItemIndex])
            {
                return selectedMenuItem.DisplayText;
            }
            else
            {
                return "System Item";
            }

        }

    }

}
