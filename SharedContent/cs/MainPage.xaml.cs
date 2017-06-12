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
		private RadialControllerConfiguration config;
		private RadialController controller;
		private double curRotationAngle;
		private int curComboIndex;
		private List<RadialControllerMenuItem> customMenuItems;
		private int lastCombo = -1;
		private MainPage rootPage;
		private RadialControllerMenuItem safeMenuItem; // SafeCracker custom tool menu
		private List<int> safeCombo;
		#endregion // Member Variables

		public MainPage()
        {
            this.InitializeComponent();
            InitializeApp();
            InitializeController();
            AddAllItems();

            // This is a static public property that allows downstream pages to get a handle to the MainPage instance
            // in order to call methods that are in this class.
            Current = this;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            rootPage = MainPage.Current;
		}

		private void InitializeApp()
        {
            // Preset safe combo
            safeCombo = new List<int> { 30, 60, 90 };

            // Initialize combination index
            curComboIndex = 0;
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

        private void AddAllItems()
        {
			AddSystemItems();
			AddCustomItems();
		}

        private void AddSystemItems()
        {
            //Remove Volume/Scroll/Zoom/Undo tools not needed by SafeCracker app
            config = RadialControllerConfiguration.GetForCurrentView();

			// Allow volume control in addition to safe cracking
			// config.SetDefaultMenuItems(new [] { RadialControllerSystemMenuItemKind.Volume });
			config.SetDefaultMenuItems(new RadialControllerSystemMenuItemKind[] { });
		}

		private void AddCustomItems()
		{
			// Create menu items to select which safe to crack open 
			safeMenuItem = RadialControllerMenuItem.CreateFromIcon("Safe 1", RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Safe1.png")));

			//Create a handler for when custom tool menu item is selected
			safeMenuItem.Invoked += SafeMenuItem_Invoked;

			// Add custom tool menu
			controller.Menu.Items.Add(safeMenuItem);

			// Create a list to keep track of custom items
			customMenuItems = new List<RadialControllerMenuItem>
			{
				safeMenuItem,
			};
		}

		// Handling RadialController Input for Menu items
		private void SafeMenuItem_Invoked(RadialControllerMenuItem sender, object args)
        {
			log.Text += "\n Selected : " + sender.DisplayText;
        }

        private void OnLogSizeChanged(object sender, object e)
        {
            logViewer.ChangeView(null, logViewer.ExtentHeight, null);
        }

        private void Controller_ControlAcquired(RadialController sender, RadialControllerControlAcquiredEventArgs args)
        {
            log.Text += "\nControl Acquired";
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

                log.Text += "You CRACKED combo " + (curComboIndex + 1) + " = " + num;

                // increment current safe combo index
                curComboIndex++;

                // Check if selected safe is unlocked after input
                if (curComboIndex == 3)
                {
					// All safe combo correctly inputted
					// Open the safe...
					UnlockedToggle.IsOn = true; // Set selected safe to unloack state

                    log.Text += "\nYou've CRACKed the Safe.  Good job!";
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

				// If safe is not already unlocked
				if (!UnlockedToggle.IsOn)
				{
					// Check to see if the user found a match
					if (CheckSafeCombo(curCombo))
					{
						// If so, vibrate the dial
						SendHapticFeedback(args.SimpleHapticsController, 1.0, TimeSpan.MaxValue);
					}
				}
			}
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
            if (!UnlockedToggle.IsOn)
            {
                // re-initalize app
                Current.InitializeComponent();
                InitializeApp();

                // Initialize combination index
                curComboIndex = 0;
            }
        }

        private RadialControllerMenuItem GetRadialControllerMenuItemFromSender(object sender)
        {
            Button button = sender as Button;
            int index = Convert.ToInt32(button.CommandParameter);

            return customMenuItems[index];
        }
    }
}
