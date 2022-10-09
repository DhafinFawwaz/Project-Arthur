//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.2
//     from Assets/Code/Input/PlayerControls.inputactions
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

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Controller"",
            ""id"": ""de2a3bc1-0cb9-47ba-9158-3936dbfdc962"",
            ""actions"": [
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""ea84fcca-9aac-4535-a3bb-e6d4d1d611ad"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MoveInput"",
                    ""type"": ""Value"",
                    ""id"": ""b7b6f5c3-ac00-45a8-8063-2d9dde9aab60"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""AttackInput"",
                    ""type"": ""Button"",
                    ""id"": ""80801b92-d822-45e4-b8a5-acfc1e0604ac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ActionInput"",
                    ""type"": ""Button"",
                    ""id"": ""c618274d-311f-44de-b3e5-c8a89f6df6fd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Super1Input"",
                    ""type"": ""Button"",
                    ""id"": ""5d9220d4-f2de-4bb9-b0ac-b1d96259cf00"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""BuyInput"",
                    ""type"": ""Button"",
                    ""id"": ""f27cdc05-f67b-461b-914b-26ff84bdd7aa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""628dcbd8-3b96-4d42-af5a-b228339102be"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""0ba81a29-1ac3-4ab4-97d5-edf6fa7094e5"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveInput"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e5b9fc40-7354-4dec-b4ef-601cbc1f4cb6"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5f04720f-fe98-405e-ba62-6d8fcdfb48fe"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9accfd4b-fe3a-44fe-b29c-85ca0c6a5bd4"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""da3cf3e8-d172-4edb-bedf-34d8262b5010"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""5b3a1d57-c6eb-4e73-8506-f2c3d9757cc4"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttackInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""992d6432-7dde-4e9f-ad83-986aa7e824d5"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActionInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f188aa6c-da85-4898-a556-5f83b0088b7b"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Super1Input"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c51296b5-cd6e-4e16-b63c-702c56d5cb03"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BuyInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Editor"",
            ""id"": ""3aada8c7-3c28-445e-8ef1-ddc6e267d986"",
            ""actions"": [
                {
                    ""name"": ""Restart"",
                    ""type"": ""Button"",
                    ""id"": ""e8c9ab28-e9a4-4eed-95aa-6c2dd17899b8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ShowDebug"",
                    ""type"": ""Button"",
                    ""id"": ""4f620be7-7072-45af-9703-7c3018ce8d3f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Home"",
                    ""type"": ""Button"",
                    ""id"": ""12771de3-74ef-4b29-9b4c-c8434403d94d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""438f7e72-16b5-413e-9577-5fc5293382f4"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Restart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""14d7a6cb-ebf4-48eb-9aec-e3f4fa2f1057"",
                    ""path"": ""<Keyboard>/f1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShowDebug"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4900e240-8bfb-491a-9ab2-debfa5253a2b"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Home"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Controller
        m_Controller = asset.FindActionMap("Controller", throwIfNotFound: true);
        m_Controller_MousePosition = m_Controller.FindAction("MousePosition", throwIfNotFound: true);
        m_Controller_MoveInput = m_Controller.FindAction("MoveInput", throwIfNotFound: true);
        m_Controller_AttackInput = m_Controller.FindAction("AttackInput", throwIfNotFound: true);
        m_Controller_ActionInput = m_Controller.FindAction("ActionInput", throwIfNotFound: true);
        m_Controller_Super1Input = m_Controller.FindAction("Super1Input", throwIfNotFound: true);
        m_Controller_BuyInput = m_Controller.FindAction("BuyInput", throwIfNotFound: true);
        // Editor
        m_Editor = asset.FindActionMap("Editor", throwIfNotFound: true);
        m_Editor_Restart = m_Editor.FindAction("Restart", throwIfNotFound: true);
        m_Editor_ShowDebug = m_Editor.FindAction("ShowDebug", throwIfNotFound: true);
        m_Editor_Home = m_Editor.FindAction("Home", throwIfNotFound: true);
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

    // Controller
    private readonly InputActionMap m_Controller;
    private IControllerActions m_ControllerActionsCallbackInterface;
    private readonly InputAction m_Controller_MousePosition;
    private readonly InputAction m_Controller_MoveInput;
    private readonly InputAction m_Controller_AttackInput;
    private readonly InputAction m_Controller_ActionInput;
    private readonly InputAction m_Controller_Super1Input;
    private readonly InputAction m_Controller_BuyInput;
    public struct ControllerActions
    {
        private @PlayerControls m_Wrapper;
        public ControllerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MousePosition => m_Wrapper.m_Controller_MousePosition;
        public InputAction @MoveInput => m_Wrapper.m_Controller_MoveInput;
        public InputAction @AttackInput => m_Wrapper.m_Controller_AttackInput;
        public InputAction @ActionInput => m_Wrapper.m_Controller_ActionInput;
        public InputAction @Super1Input => m_Wrapper.m_Controller_Super1Input;
        public InputAction @BuyInput => m_Wrapper.m_Controller_BuyInput;
        public InputActionMap Get() { return m_Wrapper.m_Controller; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControllerActions set) { return set.Get(); }
        public void SetCallbacks(IControllerActions instance)
        {
            if (m_Wrapper.m_ControllerActionsCallbackInterface != null)
            {
                @MousePosition.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnMousePosition;
                @MoveInput.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnMoveInput;
                @MoveInput.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnMoveInput;
                @MoveInput.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnMoveInput;
                @AttackInput.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnAttackInput;
                @AttackInput.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnAttackInput;
                @AttackInput.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnAttackInput;
                @ActionInput.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnActionInput;
                @ActionInput.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnActionInput;
                @ActionInput.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnActionInput;
                @Super1Input.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnSuper1Input;
                @Super1Input.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnSuper1Input;
                @Super1Input.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnSuper1Input;
                @BuyInput.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnBuyInput;
                @BuyInput.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnBuyInput;
                @BuyInput.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnBuyInput;
            }
            m_Wrapper.m_ControllerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @MoveInput.started += instance.OnMoveInput;
                @MoveInput.performed += instance.OnMoveInput;
                @MoveInput.canceled += instance.OnMoveInput;
                @AttackInput.started += instance.OnAttackInput;
                @AttackInput.performed += instance.OnAttackInput;
                @AttackInput.canceled += instance.OnAttackInput;
                @ActionInput.started += instance.OnActionInput;
                @ActionInput.performed += instance.OnActionInput;
                @ActionInput.canceled += instance.OnActionInput;
                @Super1Input.started += instance.OnSuper1Input;
                @Super1Input.performed += instance.OnSuper1Input;
                @Super1Input.canceled += instance.OnSuper1Input;
                @BuyInput.started += instance.OnBuyInput;
                @BuyInput.performed += instance.OnBuyInput;
                @BuyInput.canceled += instance.OnBuyInput;
            }
        }
    }
    public ControllerActions @Controller => new ControllerActions(this);

    // Editor
    private readonly InputActionMap m_Editor;
    private IEditorActions m_EditorActionsCallbackInterface;
    private readonly InputAction m_Editor_Restart;
    private readonly InputAction m_Editor_ShowDebug;
    private readonly InputAction m_Editor_Home;
    public struct EditorActions
    {
        private @PlayerControls m_Wrapper;
        public EditorActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Restart => m_Wrapper.m_Editor_Restart;
        public InputAction @ShowDebug => m_Wrapper.m_Editor_ShowDebug;
        public InputAction @Home => m_Wrapper.m_Editor_Home;
        public InputActionMap Get() { return m_Wrapper.m_Editor; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(EditorActions set) { return set.Get(); }
        public void SetCallbacks(IEditorActions instance)
        {
            if (m_Wrapper.m_EditorActionsCallbackInterface != null)
            {
                @Restart.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnRestart;
                @Restart.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnRestart;
                @Restart.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnRestart;
                @ShowDebug.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnShowDebug;
                @ShowDebug.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnShowDebug;
                @ShowDebug.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnShowDebug;
                @Home.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnHome;
                @Home.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnHome;
                @Home.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnHome;
            }
            m_Wrapper.m_EditorActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Restart.started += instance.OnRestart;
                @Restart.performed += instance.OnRestart;
                @Restart.canceled += instance.OnRestart;
                @ShowDebug.started += instance.OnShowDebug;
                @ShowDebug.performed += instance.OnShowDebug;
                @ShowDebug.canceled += instance.OnShowDebug;
                @Home.started += instance.OnHome;
                @Home.performed += instance.OnHome;
                @Home.canceled += instance.OnHome;
            }
        }
    }
    public EditorActions @Editor => new EditorActions(this);
    public interface IControllerActions
    {
        void OnMousePosition(InputAction.CallbackContext context);
        void OnMoveInput(InputAction.CallbackContext context);
        void OnAttackInput(InputAction.CallbackContext context);
        void OnActionInput(InputAction.CallbackContext context);
        void OnSuper1Input(InputAction.CallbackContext context);
        void OnBuyInput(InputAction.CallbackContext context);
    }
    public interface IEditorActions
    {
        void OnRestart(InputAction.CallbackContext context);
        void OnShowDebug(InputAction.CallbackContext context);
        void OnHome(InputAction.CallbackContext context);
    }
}
