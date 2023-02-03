//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/PlayerControl.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControl : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControl"",
    ""maps"": [
        {
            ""name"": ""BasicControl"",
            ""id"": ""93aa5daf-cb6e-45e4-a315-bfe9e93af69a"",
            ""actions"": [
                {
                    ""name"": ""Action"",
                    ""type"": ""Button"",
                    ""id"": ""d0d79808-774d-4d46-a0d9-bd23b1a8c041"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""acff50cf-f2a8-4e2b-87d4-4f4495acfa2d"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // BasicControl
        m_BasicControl = asset.FindActionMap("BasicControl", throwIfNotFound: true);
        m_BasicControl_Action = m_BasicControl.FindAction("Action", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // BasicControl
    private readonly InputActionMap m_BasicControl;
    private IBasicControlActions m_BasicControlActionsCallbackInterface;
    private readonly InputAction m_BasicControl_Action;
    public struct BasicControlActions
    {
        private @PlayerControl m_Wrapper;
        public BasicControlActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Action => m_Wrapper.m_BasicControl_Action;
        public InputActionMap Get() { return m_Wrapper.m_BasicControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BasicControlActions set) { return set.Get(); }
        public void SetCallbacks(IBasicControlActions instance)
        {
            if (m_Wrapper.m_BasicControlActionsCallbackInterface != null)
            {
                @Action.started -= m_Wrapper.m_BasicControlActionsCallbackInterface.OnAction;
                @Action.performed -= m_Wrapper.m_BasicControlActionsCallbackInterface.OnAction;
                @Action.canceled -= m_Wrapper.m_BasicControlActionsCallbackInterface.OnAction;
            }
            m_Wrapper.m_BasicControlActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Action.started += instance.OnAction;
                @Action.performed += instance.OnAction;
                @Action.canceled += instance.OnAction;
            }
        }
    }
    public BasicControlActions @BasicControl => new BasicControlActions(this);
    public interface IBasicControlActions
    {
        void OnAction(InputAction.CallbackContext context);
    }
}