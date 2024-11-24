//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Settings/Input/Controls.inputactions
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

public partial class @Controls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""508d022c-8dc0-4ea3-bc9c-fe5d64f93c1b"",
            ""actions"": [
                {
                    ""name"": ""CameraMove"",
                    ""type"": ""Value"",
                    ""id"": ""0330f6d8-c02d-48f2-a3a7-01de3ed6499f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""TimeStop"",
                    ""type"": ""Button"",
                    ""id"": ""19b056a8-8d93-4864-a17d-0cbdd0058bf0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TimeSpeedUp"",
                    ""type"": ""Button"",
                    ""id"": ""ed395b5e-708a-4f38-9800-7399a3841f3a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TimeSpeedDown"",
                    ""type"": ""Button"",
                    ""id"": ""7596eb70-e0ff-4123-88cf-f5c370ab9624"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Option"",
                    ""type"": ""Button"",
                    ""id"": ""a10bbc9c-9eaf-4a61-a38a-73cd5759dd8b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""HideUI"",
                    ""type"": ""Button"",
                    ""id"": ""85317c1c-57c2-437f-bf7f-2e234263aa99"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToggleLine"",
                    ""type"": ""Button"",
                    ""id"": ""d541bc36-cb35-43ce-af5e-2669d49d5102"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Car"",
                    ""type"": ""Button"",
                    ""id"": ""3187685d-c92f-4c4e-818f-b8f52a7beb2c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Truck"",
                    ""type"": ""Button"",
                    ""id"": ""ce3e0c70-d8e6-4a8c-89d7-916079e81c7c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Trailer"",
                    ""type"": ""Button"",
                    ""id"": ""0b545dc8-209e-41e6-a171-c19869fefc52"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": "" RedLine"",
                    ""type"": ""Button"",
                    ""id"": ""e86d34fb-2f59-485d-9c0f-474e9989d61b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""YellowLine"",
                    ""type"": ""Button"",
                    ""id"": ""8d396cc6-555d-4858-b33a-e28f743ede8e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""GreenLine"",
                    ""type"": ""Button"",
                    ""id"": ""470cd5a3-ef3b-4032-a91e-e7e0608ec529"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""BlueLine"",
                    ""type"": ""Button"",
                    ""id"": ""74cfd3ca-9c97-4737-88f7-2a29ee6daaff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PurpleLine"",
                    ""type"": ""Button"",
                    ""id"": ""afffb2a0-be8f-4efa-8ad2-8df7bcdb35cd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CameraScroll"",
                    ""type"": ""Value"",
                    ""id"": ""50a6fbc7-c94c-44ce-961e-b36d98ffe659"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""1a149e4d-5573-4128-b33d-6e5808482c6f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""707364c5-e13c-4f10-b709-ff0c7b4565f8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0357385a-6f6f-4c78-b468-8c750331e702"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0b754a17-00ab-49b8-9ae4-b4831514fbc8"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6f2a063d-7c8c-4987-8a75-cd7e420c69d1"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c043beee-8215-4fd0-9d3d-51cd2325e40f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TimeStop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41ea9786-5ec7-4fc9-8676-1553856c28a2"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TimeSpeedUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""86a99087-785a-411f-86a3-6ede6c85194c"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TimeSpeedDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""549e8b8e-cb92-46d2-a8d0-f965eb3a5625"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Option"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""af8d5c95-e7a3-4c04-8a7a-928e4e62fc9d"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HideUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aa903b28-1d14-4e9a-ad94-299099dca982"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleLine"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0da2281e-4393-4f5a-9979-f384b481706e"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Car"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""09b23e1e-001a-4b59-b73a-254ac15a50b4"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Truck"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""68a0c20a-04df-46a6-95bb-9351a1ee2406"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Trailer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""004d5992-1b0a-4905-9982-ebf17b1d9612"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": "" RedLine"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e8a2e7c-8ae6-4685-a884-358d010219bc"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GreenLine"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee6322dd-6402-402d-8b21-08e2e366a0d8"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""YellowLine"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c30ad08-8f8b-4d4b-b463-7471ee9ae99e"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PurpleLine"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8abb8d89-3016-4c4c-89c7-9cbd31ca11c9"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BlueLine"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e52e4d9c-d0e7-4035-a35f-77d2998c7a44"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraScroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeybroadMouse"",
            ""bindingGroup"": ""KeybroadMouse"",
            ""devices"": []
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_CameraMove = m_Player.FindAction("CameraMove", throwIfNotFound: true);
        m_Player_TimeStop = m_Player.FindAction("TimeStop", throwIfNotFound: true);
        m_Player_TimeSpeedUp = m_Player.FindAction("TimeSpeedUp", throwIfNotFound: true);
        m_Player_TimeSpeedDown = m_Player.FindAction("TimeSpeedDown", throwIfNotFound: true);
        m_Player_Option = m_Player.FindAction("Option", throwIfNotFound: true);
        m_Player_HideUI = m_Player.FindAction("HideUI", throwIfNotFound: true);
        m_Player_ToggleLine = m_Player.FindAction("ToggleLine", throwIfNotFound: true);
        m_Player_Car = m_Player.FindAction("Car", throwIfNotFound: true);
        m_Player_Truck = m_Player.FindAction("Truck", throwIfNotFound: true);
        m_Player_Trailer = m_Player.FindAction("Trailer", throwIfNotFound: true);
        m_Player_RedLine = m_Player.FindAction(" RedLine", throwIfNotFound: true);
        m_Player_YellowLine = m_Player.FindAction("YellowLine", throwIfNotFound: true);
        m_Player_GreenLine = m_Player.FindAction("GreenLine", throwIfNotFound: true);
        m_Player_BlueLine = m_Player.FindAction("BlueLine", throwIfNotFound: true);
        m_Player_PurpleLine = m_Player.FindAction("PurpleLine", throwIfNotFound: true);
        m_Player_CameraScroll = m_Player.FindAction("CameraScroll", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_CameraMove;
    private readonly InputAction m_Player_TimeStop;
    private readonly InputAction m_Player_TimeSpeedUp;
    private readonly InputAction m_Player_TimeSpeedDown;
    private readonly InputAction m_Player_Option;
    private readonly InputAction m_Player_HideUI;
    private readonly InputAction m_Player_ToggleLine;
    private readonly InputAction m_Player_Car;
    private readonly InputAction m_Player_Truck;
    private readonly InputAction m_Player_Trailer;
    private readonly InputAction m_Player_RedLine;
    private readonly InputAction m_Player_YellowLine;
    private readonly InputAction m_Player_GreenLine;
    private readonly InputAction m_Player_BlueLine;
    private readonly InputAction m_Player_PurpleLine;
    private readonly InputAction m_Player_CameraScroll;
    public struct PlayerActions
    {
        private @Controls m_Wrapper;
        public PlayerActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @CameraMove => m_Wrapper.m_Player_CameraMove;
        public InputAction @TimeStop => m_Wrapper.m_Player_TimeStop;
        public InputAction @TimeSpeedUp => m_Wrapper.m_Player_TimeSpeedUp;
        public InputAction @TimeSpeedDown => m_Wrapper.m_Player_TimeSpeedDown;
        public InputAction @Option => m_Wrapper.m_Player_Option;
        public InputAction @HideUI => m_Wrapper.m_Player_HideUI;
        public InputAction @ToggleLine => m_Wrapper.m_Player_ToggleLine;
        public InputAction @Car => m_Wrapper.m_Player_Car;
        public InputAction @Truck => m_Wrapper.m_Player_Truck;
        public InputAction @Trailer => m_Wrapper.m_Player_Trailer;
        public InputAction @RedLine => m_Wrapper.m_Player_RedLine;
        public InputAction @YellowLine => m_Wrapper.m_Player_YellowLine;
        public InputAction @GreenLine => m_Wrapper.m_Player_GreenLine;
        public InputAction @BlueLine => m_Wrapper.m_Player_BlueLine;
        public InputAction @PurpleLine => m_Wrapper.m_Player_PurpleLine;
        public InputAction @CameraScroll => m_Wrapper.m_Player_CameraScroll;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @CameraMove.started += instance.OnCameraMove;
            @CameraMove.performed += instance.OnCameraMove;
            @CameraMove.canceled += instance.OnCameraMove;
            @TimeStop.started += instance.OnTimeStop;
            @TimeStop.performed += instance.OnTimeStop;
            @TimeStop.canceled += instance.OnTimeStop;
            @TimeSpeedUp.started += instance.OnTimeSpeedUp;
            @TimeSpeedUp.performed += instance.OnTimeSpeedUp;
            @TimeSpeedUp.canceled += instance.OnTimeSpeedUp;
            @TimeSpeedDown.started += instance.OnTimeSpeedDown;
            @TimeSpeedDown.performed += instance.OnTimeSpeedDown;
            @TimeSpeedDown.canceled += instance.OnTimeSpeedDown;
            @Option.started += instance.OnOption;
            @Option.performed += instance.OnOption;
            @Option.canceled += instance.OnOption;
            @HideUI.started += instance.OnHideUI;
            @HideUI.performed += instance.OnHideUI;
            @HideUI.canceled += instance.OnHideUI;
            @ToggleLine.started += instance.OnToggleLine;
            @ToggleLine.performed += instance.OnToggleLine;
            @ToggleLine.canceled += instance.OnToggleLine;
            @Car.started += instance.OnCar;
            @Car.performed += instance.OnCar;
            @Car.canceled += instance.OnCar;
            @Truck.started += instance.OnTruck;
            @Truck.performed += instance.OnTruck;
            @Truck.canceled += instance.OnTruck;
            @Trailer.started += instance.OnTrailer;
            @Trailer.performed += instance.OnTrailer;
            @Trailer.canceled += instance.OnTrailer;
            @RedLine.started += instance.OnRedLine;
            @RedLine.performed += instance.OnRedLine;
            @RedLine.canceled += instance.OnRedLine;
            @YellowLine.started += instance.OnYellowLine;
            @YellowLine.performed += instance.OnYellowLine;
            @YellowLine.canceled += instance.OnYellowLine;
            @GreenLine.started += instance.OnGreenLine;
            @GreenLine.performed += instance.OnGreenLine;
            @GreenLine.canceled += instance.OnGreenLine;
            @BlueLine.started += instance.OnBlueLine;
            @BlueLine.performed += instance.OnBlueLine;
            @BlueLine.canceled += instance.OnBlueLine;
            @PurpleLine.started += instance.OnPurpleLine;
            @PurpleLine.performed += instance.OnPurpleLine;
            @PurpleLine.canceled += instance.OnPurpleLine;
            @CameraScroll.started += instance.OnCameraScroll;
            @CameraScroll.performed += instance.OnCameraScroll;
            @CameraScroll.canceled += instance.OnCameraScroll;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @CameraMove.started -= instance.OnCameraMove;
            @CameraMove.performed -= instance.OnCameraMove;
            @CameraMove.canceled -= instance.OnCameraMove;
            @TimeStop.started -= instance.OnTimeStop;
            @TimeStop.performed -= instance.OnTimeStop;
            @TimeStop.canceled -= instance.OnTimeStop;
            @TimeSpeedUp.started -= instance.OnTimeSpeedUp;
            @TimeSpeedUp.performed -= instance.OnTimeSpeedUp;
            @TimeSpeedUp.canceled -= instance.OnTimeSpeedUp;
            @TimeSpeedDown.started -= instance.OnTimeSpeedDown;
            @TimeSpeedDown.performed -= instance.OnTimeSpeedDown;
            @TimeSpeedDown.canceled -= instance.OnTimeSpeedDown;
            @Option.started -= instance.OnOption;
            @Option.performed -= instance.OnOption;
            @Option.canceled -= instance.OnOption;
            @HideUI.started -= instance.OnHideUI;
            @HideUI.performed -= instance.OnHideUI;
            @HideUI.canceled -= instance.OnHideUI;
            @ToggleLine.started -= instance.OnToggleLine;
            @ToggleLine.performed -= instance.OnToggleLine;
            @ToggleLine.canceled -= instance.OnToggleLine;
            @Car.started -= instance.OnCar;
            @Car.performed -= instance.OnCar;
            @Car.canceled -= instance.OnCar;
            @Truck.started -= instance.OnTruck;
            @Truck.performed -= instance.OnTruck;
            @Truck.canceled -= instance.OnTruck;
            @Trailer.started -= instance.OnTrailer;
            @Trailer.performed -= instance.OnTrailer;
            @Trailer.canceled -= instance.OnTrailer;
            @RedLine.started -= instance.OnRedLine;
            @RedLine.performed -= instance.OnRedLine;
            @RedLine.canceled -= instance.OnRedLine;
            @YellowLine.started -= instance.OnYellowLine;
            @YellowLine.performed -= instance.OnYellowLine;
            @YellowLine.canceled -= instance.OnYellowLine;
            @GreenLine.started -= instance.OnGreenLine;
            @GreenLine.performed -= instance.OnGreenLine;
            @GreenLine.canceled -= instance.OnGreenLine;
            @BlueLine.started -= instance.OnBlueLine;
            @BlueLine.performed -= instance.OnBlueLine;
            @BlueLine.canceled -= instance.OnBlueLine;
            @PurpleLine.started -= instance.OnPurpleLine;
            @PurpleLine.performed -= instance.OnPurpleLine;
            @PurpleLine.canceled -= instance.OnPurpleLine;
            @CameraScroll.started -= instance.OnCameraScroll;
            @CameraScroll.performed -= instance.OnCameraScroll;
            @CameraScroll.canceled -= instance.OnCameraScroll;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_KeybroadMouseSchemeIndex = -1;
    public InputControlScheme KeybroadMouseScheme
    {
        get
        {
            if (m_KeybroadMouseSchemeIndex == -1) m_KeybroadMouseSchemeIndex = asset.FindControlSchemeIndex("KeybroadMouse");
            return asset.controlSchemes[m_KeybroadMouseSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnCameraMove(InputAction.CallbackContext context);
        void OnTimeStop(InputAction.CallbackContext context);
        void OnTimeSpeedUp(InputAction.CallbackContext context);
        void OnTimeSpeedDown(InputAction.CallbackContext context);
        void OnOption(InputAction.CallbackContext context);
        void OnHideUI(InputAction.CallbackContext context);
        void OnToggleLine(InputAction.CallbackContext context);
        void OnCar(InputAction.CallbackContext context);
        void OnTruck(InputAction.CallbackContext context);
        void OnTrailer(InputAction.CallbackContext context);
        void OnRedLine(InputAction.CallbackContext context);
        void OnYellowLine(InputAction.CallbackContext context);
        void OnGreenLine(InputAction.CallbackContext context);
        void OnBlueLine(InputAction.CallbackContext context);
        void OnPurpleLine(InputAction.CallbackContext context);
        void OnCameraScroll(InputAction.CallbackContext context);
    }
}
