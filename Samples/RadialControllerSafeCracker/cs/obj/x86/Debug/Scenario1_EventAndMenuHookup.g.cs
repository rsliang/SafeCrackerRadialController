﻿#pragma checksum "C:\projects\RadialController-master\Samples\RadialControllerSafeCracker\shared\Scenario1_EventAndMenuHookup.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A7FB1F0CCBA833942704E655511736D2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SDKTemplate
{
    partial class Scenario1_EventAndMenuHookup : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        private static class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_Controls_ToggleSwitch_IsOn(global::Windows.UI.Xaml.Controls.ToggleSwitch obj, global::System.Boolean value)
            {
                obj.IsOn = value;
            }
        };

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        private class Scenario1_EventAndMenuHookup_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IScenario1_EventAndMenuHookup_Bindings
        {
            private global::SDKTemplate.Scenario1_EventAndMenuHookup dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.ToggleSwitch obj7;

            private Scenario1_EventAndMenuHookup_obj1_BindingsTracking bindingsTracking;

            public Scenario1_EventAndMenuHookup_obj1_Bindings()
            {
                this.bindingsTracking = new Scenario1_EventAndMenuHookup_obj1_BindingsTracking(this);
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 7: // Scenario1_EventAndMenuHookup.xaml line 119
                        this.obj7 = (global::Windows.UI.Xaml.Controls.ToggleSwitch)target;
                        (this.obj7).RegisterPropertyChangedCallback(global::Windows.UI.Xaml.Controls.ToggleSwitch.IsOnProperty,
                            (global::Windows.UI.Xaml.DependencyObject sender, global::Windows.UI.Xaml.DependencyProperty prop) =>
                            {
                            if (this.initialized)
                            {
                                // Update Two Way binding
                                this.dataRoot.Controller.UseAutomaticHapticFeedback = this.obj7.IsOn;
                            }
                        });
                        break;
                    default:
                        break;
                }
            }

            // IScenario1_EventAndMenuHookup_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
                this.bindingsTracking.ReleaseAllListeners();
                this.initialized = false;
            }

            public void DisconnectUnloadedObject(int connectionId)
            {
                throw new global::System.ArgumentException("No unloadable elements to disconnect.");
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                this.bindingsTracking.ReleaseAllListeners();
                if (newDataRoot != null)
                {
                    this.dataRoot = (global::SDKTemplate.Scenario1_EventAndMenuHookup)newDataRoot;
                    return true;
                }
                return false;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::SDKTemplate.Scenario1_EventAndMenuHookup obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_Controller(obj.Controller, phase);
                    }
                }
            }
            private void Update_Controller(global::Windows.UI.Input.RadialController obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_Controller_UseAutomaticHapticFeedback(obj.UseAutomaticHapticFeedback, phase);
                    }
                }
            }
            private void Update_Controller_UseAutomaticHapticFeedback(global::System.Boolean obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // Scenario1_EventAndMenuHookup.xaml line 119
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ToggleSwitch_IsOn(this.obj7, obj);
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
            private class Scenario1_EventAndMenuHookup_obj1_BindingsTracking
            {
                private global::System.WeakReference<Scenario1_EventAndMenuHookup_obj1_Bindings> weakRefToBindingObj; 

                public Scenario1_EventAndMenuHookup_obj1_BindingsTracking(Scenario1_EventAndMenuHookup_obj1_Bindings obj)
                {
                    weakRefToBindingObj = new global::System.WeakReference<Scenario1_EventAndMenuHookup_obj1_Bindings>(obj);
                }

                public Scenario1_EventAndMenuHookup_obj1_Bindings TryGetBindingObject()
                {
                    Scenario1_EventAndMenuHookup_obj1_Bindings bindingObject = null;
                    if (weakRefToBindingObj != null)
                    {
                        weakRefToBindingObj.TryGetTarget(out bindingObject);
                        if (bindingObject == null)
                        {
                            weakRefToBindingObj = null;
                            ReleaseAllListeners();
                        }
                    }
                    return bindingObject;
                }

                public void ReleaseAllListeners()
                {
                }

            }
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Scenario1_EventAndMenuHookup.xaml line 40
                {
                    this.RootGrid = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3: // Scenario1_EventAndMenuHookup.xaml line 124
                {
                    this.logViewer = (global::Windows.UI.Xaml.Controls.ScrollViewer)(target);
                }
                break;
            case 4: // Scenario1_EventAndMenuHookup.xaml line 125
                {
                    this.log = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBlock)this.log).SizeChanged += this.OnLogSizeChanged;
                }
                break;
            case 5: // Scenario1_EventAndMenuHookup.xaml line 117
                {
                    this.GetSelected = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.GetSelected).Click += this.PrintSelectedItem;
                }
                break;
            case 6: // Scenario1_EventAndMenuHookup.xaml line 118
                {
                    this.SelectPreviousItem = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.SelectPreviousItem).Click += this.SelectPreviouslySelectedItem;
                }
                break;
            case 8: // Scenario1_EventAndMenuHookup.xaml line 108
                {
                    this.AddButton5 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.AddButton5).Click += this.AddItem;
                }
                break;
            case 9: // Scenario1_EventAndMenuHookup.xaml line 109
                {
                    this.SelectButton5 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.SelectButton5).Click += this.SelectItem;
                }
                break;
            case 10: // Scenario1_EventAndMenuHookup.xaml line 110
                {
                    this.RemoveButton5 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.RemoveButton5).Click += this.RemoveItem;
                }
                break;
            case 11: // Scenario1_EventAndMenuHookup.xaml line 111
                {
                    this.Slider5 = (global::Windows.UI.Xaml.Controls.Slider)(target);
                }
                break;
            case 12: // Scenario1_EventAndMenuHookup.xaml line 112
                {
                    this.Toggle5 = (global::Windows.UI.Xaml.Controls.ToggleSwitch)(target);
                }
                break;
            case 13: // Scenario1_EventAndMenuHookup.xaml line 99
                {
                    this.AddButton4 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.AddButton4).Click += this.AddItem;
                }
                break;
            case 14: // Scenario1_EventAndMenuHookup.xaml line 100
                {
                    this.SelectButton4 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.SelectButton4).Click += this.SelectItem;
                }
                break;
            case 15: // Scenario1_EventAndMenuHookup.xaml line 101
                {
                    this.RemoveButton4 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.RemoveButton4).Click += this.RemoveItem;
                }
                break;
            case 16: // Scenario1_EventAndMenuHookup.xaml line 102
                {
                    this.Slider4 = (global::Windows.UI.Xaml.Controls.Slider)(target);
                }
                break;
            case 17: // Scenario1_EventAndMenuHookup.xaml line 103
                {
                    this.Toggle4 = (global::Windows.UI.Xaml.Controls.ToggleSwitch)(target);
                }
                break;
            case 18: // Scenario1_EventAndMenuHookup.xaml line 90
                {
                    this.AddButton3 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.AddButton3).Click += this.AddItem;
                }
                break;
            case 19: // Scenario1_EventAndMenuHookup.xaml line 91
                {
                    this.SelectButton3 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.SelectButton3).Click += this.SelectItem;
                }
                break;
            case 20: // Scenario1_EventAndMenuHookup.xaml line 92
                {
                    this.RemoveButton3 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.RemoveButton3).Click += this.RemoveItem;
                }
                break;
            case 21: // Scenario1_EventAndMenuHookup.xaml line 93
                {
                    this.Slider3 = (global::Windows.UI.Xaml.Controls.Slider)(target);
                }
                break;
            case 22: // Scenario1_EventAndMenuHookup.xaml line 94
                {
                    this.Toggle3 = (global::Windows.UI.Xaml.Controls.ToggleSwitch)(target);
                }
                break;
            case 23: // Scenario1_EventAndMenuHookup.xaml line 81
                {
                    this.AddButton2 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.AddButton2).Click += this.AddItem;
                }
                break;
            case 24: // Scenario1_EventAndMenuHookup.xaml line 82
                {
                    this.SelectButton2 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.SelectButton2).Click += this.SelectItem;
                }
                break;
            case 25: // Scenario1_EventAndMenuHookup.xaml line 83
                {
                    this.RemoveButton2 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.RemoveButton2).Click += this.RemoveItem;
                }
                break;
            case 26: // Scenario1_EventAndMenuHookup.xaml line 84
                {
                    this.Slider2 = (global::Windows.UI.Xaml.Controls.Slider)(target);
                }
                break;
            case 27: // Scenario1_EventAndMenuHookup.xaml line 85
                {
                    this.Toggle2 = (global::Windows.UI.Xaml.Controls.ToggleSwitch)(target);
                }
                break;
            case 28: // Scenario1_EventAndMenuHookup.xaml line 72
                {
                    this.AddButton1 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.AddButton1).Click += this.AddItem;
                }
                break;
            case 29: // Scenario1_EventAndMenuHookup.xaml line 73
                {
                    this.SelectButton1 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.SelectButton1).Click += this.SelectItem;
                }
                break;
            case 30: // Scenario1_EventAndMenuHookup.xaml line 74
                {
                    this.RemoveButton1 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.RemoveButton1).Click += this.RemoveItem;
                }
                break;
            case 31: // Scenario1_EventAndMenuHookup.xaml line 75
                {
                    this.Slider1 = (global::Windows.UI.Xaml.Controls.Slider)(target);
                }
                break;
            case 32: // Scenario1_EventAndMenuHookup.xaml line 76
                {
                    this.Toggle1 = (global::Windows.UI.Xaml.Controls.ToggleSwitch)(target);
                }
                break;
            case 33: // Scenario1_EventAndMenuHookup.xaml line 63
                {
                    this.AddButton0 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.AddButton0).Click += this.AddItem;
                }
                break;
            case 34: // Scenario1_EventAndMenuHookup.xaml line 64
                {
                    this.SelectButton0 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.SelectButton0).Click += this.SelectItem;
                }
                break;
            case 35: // Scenario1_EventAndMenuHookup.xaml line 65
                {
                    this.RemoveButton0 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.RemoveButton0).Click += this.RemoveItem;
                }
                break;
            case 36: // Scenario1_EventAndMenuHookup.xaml line 66
                {
                    this.Slider0 = (global::Windows.UI.Xaml.Controls.Slider)(target);
                }
                break;
            case 37: // Scenario1_EventAndMenuHookup.xaml line 67
                {
                    this.Toggle0 = (global::Windows.UI.Xaml.Controls.ToggleSwitch)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            switch(connectionId)
            {
            case 1: // Scenario1_EventAndMenuHookup.xaml line 13
                {                    
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)target;
                    Scenario1_EventAndMenuHookup_obj1_Bindings bindings = new Scenario1_EventAndMenuHookup_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                }
                break;
            }
            return returnValue;
        }
    }
}

