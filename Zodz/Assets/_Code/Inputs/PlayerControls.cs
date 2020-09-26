// GENERATED AUTOMATICALLY FROM 'Assets/_Code/Inputs/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""PlayerCharacter"",
            ""id"": ""1102afe1-d846-4982-ba1e-9ffd0150aaa2"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5ecc4740-55ff-4a07-aadf-0e30b045de28"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BasicSkill"",
                    ""type"": ""Button"",
                    ""id"": ""003c967d-d39f-4ed9-b9e7-17d86cc47870"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""1ec9c999-d469-40e1-960b-d533e9cec414"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwapSkillUp"",
                    ""type"": ""Button"",
                    ""id"": ""18296f94-7b74-44d2-a9ff-88d48deb76c3"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""SwapSkillDown"",
                    ""type"": ""Button"",
                    ""id"": ""5b6efcff-e320-4044-b27b-0b92c6702e24"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""MagicSkill"",
                    ""type"": ""Button"",
                    ""id"": ""25d4a27a-128e-40a7-9dbe-1e21646a4ffd"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UltimateSkill"",
                    ""type"": ""Button"",
                    ""id"": ""4fd9ebbf-4416-4f91-91ad-c396f4cb4cb3"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""412a4299-9add-4009-8b48-94ff5c84a7c0"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""OpenMap"",
                    ""type"": ""Button"",
                    ""id"": ""ee5fcd61-bad5-4283-a5b2-3fdc003e4924"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""73b01c12-075c-48ee-8b11-db209ed1a1e7"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""af39415d-6272-4e42-b9bd-f252dbea2845"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""530b6f9a-daf0-4337-a692-5b9a470ed647"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f7123be9-e2eb-4826-b239-8934b1790e95"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""152970e6-c5f5-4e27-a701-61497cae7048"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9765ffba-616d-44ed-9e43-680b28e3dfe6"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2ab85008-ca65-4e83-85a5-518420d12f21"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""BasicSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""218b2efb-c318-45af-97ef-c712c7b63b0b"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc3ad541-7319-4e76-be2a-de53595a7f8a"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SwapSkillUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""15a0638f-7936-41d0-8fd7-dbef15870551"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SwapSkillDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8cbbdec0-a50e-4097-a517-a909f18c35af"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MagicSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""656e2037-6fa8-448a-9a22-d34ec9b2b120"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""UltimateSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cf136368-dffe-492d-8f81-a00ee8af66f8"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""79b5dca7-d0d1-45a1-a976-72c993aadd9b"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7248e21c-bb27-4893-8a18-b2aa3f0f4098"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""OpenMap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerCharacter
        m_PlayerCharacter = asset.FindActionMap("PlayerCharacter", throwIfNotFound: true);
        m_PlayerCharacter_Move = m_PlayerCharacter.FindAction("Move", throwIfNotFound: true);
        m_PlayerCharacter_BasicSkill = m_PlayerCharacter.FindAction("BasicSkill", throwIfNotFound: true);
        m_PlayerCharacter_Dash = m_PlayerCharacter.FindAction("Dash", throwIfNotFound: true);
        m_PlayerCharacter_SwapSkillUp = m_PlayerCharacter.FindAction("SwapSkillUp", throwIfNotFound: true);
        m_PlayerCharacter_SwapSkillDown = m_PlayerCharacter.FindAction("SwapSkillDown", throwIfNotFound: true);
        m_PlayerCharacter_MagicSkill = m_PlayerCharacter.FindAction("MagicSkill", throwIfNotFound: true);
        m_PlayerCharacter_UltimateSkill = m_PlayerCharacter.FindAction("UltimateSkill", throwIfNotFound: true);
        m_PlayerCharacter_Pause = m_PlayerCharacter.FindAction("Pause", throwIfNotFound: true);
        m_PlayerCharacter_OpenMap = m_PlayerCharacter.FindAction("OpenMap", throwIfNotFound: true);
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

    // PlayerCharacter
    private readonly InputActionMap m_PlayerCharacter;
    private IPlayerCharacterActions m_PlayerCharacterActionsCallbackInterface;
    private readonly InputAction m_PlayerCharacter_Move;
    private readonly InputAction m_PlayerCharacter_BasicSkill;
    private readonly InputAction m_PlayerCharacter_Dash;
    private readonly InputAction m_PlayerCharacter_SwapSkillUp;
    private readonly InputAction m_PlayerCharacter_SwapSkillDown;
    private readonly InputAction m_PlayerCharacter_MagicSkill;
    private readonly InputAction m_PlayerCharacter_UltimateSkill;
    private readonly InputAction m_PlayerCharacter_Pause;
    private readonly InputAction m_PlayerCharacter_OpenMap;
    public struct PlayerCharacterActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerCharacterActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerCharacter_Move;
        public InputAction @BasicSkill => m_Wrapper.m_PlayerCharacter_BasicSkill;
        public InputAction @Dash => m_Wrapper.m_PlayerCharacter_Dash;
        public InputAction @SwapSkillUp => m_Wrapper.m_PlayerCharacter_SwapSkillUp;
        public InputAction @SwapSkillDown => m_Wrapper.m_PlayerCharacter_SwapSkillDown;
        public InputAction @MagicSkill => m_Wrapper.m_PlayerCharacter_MagicSkill;
        public InputAction @UltimateSkill => m_Wrapper.m_PlayerCharacter_UltimateSkill;
        public InputAction @Pause => m_Wrapper.m_PlayerCharacter_Pause;
        public InputAction @OpenMap => m_Wrapper.m_PlayerCharacter_OpenMap;
        public InputActionMap Get() { return m_Wrapper.m_PlayerCharacter; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerCharacterActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerCharacterActions instance)
        {
            if (m_Wrapper.m_PlayerCharacterActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerCharacterActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerCharacterActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerCharacterActionsCallbackInterface.OnMove;
                @BasicSkill.started -= m_Wrapper.m_PlayerCharacterActionsCallbackInterface.OnBasicSkill;
                @BasicSkill.performed -= m_Wrapper.m_PlayerCharacterActionsCallbackInterface.OnBasicSkill;
                @BasicSkill.canceled -= m_Wrapper.m_PlayerCharacterActionsCallbackInterface.OnBasicSkill;
                @Dash.started -= m_Wrapper.m_PlayerCharacterActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_PlayerCharacterActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_PlayerCharacterActionsCallbackInterface.OnDash;
                @SwapSkillUp.started -= m_Wrapper.m_PlayerCharacterActionsCallbackInterface.OnSwapSkillUp;
                @SwapSkillUp.performed -= m_Wrapper.m_PlayerCharacterActionsCallbackInterface.OnSwapSkillUp;
                @SwapSkillUp.canceled -= m_Wrapper.m_PlayerCharacterActionsCallbackInterface.OnSwapSkillUp;
                @SwapSkillDown.started -= m_Wrapper.m_PlayerCharacterActionsCallbackInterface.OnSwapSkillDown;
                @SwapSkillDown.performed -= m_Wrapper.m_PlayerCharacterActionsCallbackInterface.OnSwapSkillDown;
                @SwapSkillDown.canceled -= m_Wrapper.m_PlayerCharacterActionsCallbackInterface.OnSwapSkillDown;
                @MagicSkill.started -= m_Wrapper.m_PlayerCharacterActionsCallbackInterface.OnMagicSkill;
                @MagicSkill.performed -= m_Wrapper.m_PlayerCharacterActionsCallbackInterface.OnMagicSkill;
                @MagicSkill.canceled -= m_Wrapper.m_PlayerCharacterActionsCallbackInterface.OnMagicSkill;
                @UltimateSkill.started -= m_Wrapper.m_PlayerCharacterActionsCallbackInterface.OnUltimateSkill;
                @UltimateSkill.performed -= m_Wrapper.m_PlayerCharacterActionsCallbackInterface.OnUltimateSkill;
                @UltimateSkill.canceled -= m_Wrapper.m_PlayerCharacterActionsCallbackInterface.OnUltimateSkill;
                @Pause.started -= m_Wrapper.m_PlayerCharacterActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerCharacterActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerCharacterActionsCallbackInterface.OnPause;
                @OpenMap.started -= m_Wrapper.m_PlayerCharacterActionsCallbackInterface.OnOpenMap;
                @OpenMap.performed -= m_Wrapper.m_PlayerCharacterActionsCallbackInterface.OnOpenMap;
                @OpenMap.canceled -= m_Wrapper.m_PlayerCharacterActionsCallbackInterface.OnOpenMap;
            }
            m_Wrapper.m_PlayerCharacterActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @BasicSkill.started += instance.OnBasicSkill;
                @BasicSkill.performed += instance.OnBasicSkill;
                @BasicSkill.canceled += instance.OnBasicSkill;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @SwapSkillUp.started += instance.OnSwapSkillUp;
                @SwapSkillUp.performed += instance.OnSwapSkillUp;
                @SwapSkillUp.canceled += instance.OnSwapSkillUp;
                @SwapSkillDown.started += instance.OnSwapSkillDown;
                @SwapSkillDown.performed += instance.OnSwapSkillDown;
                @SwapSkillDown.canceled += instance.OnSwapSkillDown;
                @MagicSkill.started += instance.OnMagicSkill;
                @MagicSkill.performed += instance.OnMagicSkill;
                @MagicSkill.canceled += instance.OnMagicSkill;
                @UltimateSkill.started += instance.OnUltimateSkill;
                @UltimateSkill.performed += instance.OnUltimateSkill;
                @UltimateSkill.canceled += instance.OnUltimateSkill;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @OpenMap.started += instance.OnOpenMap;
                @OpenMap.performed += instance.OnOpenMap;
                @OpenMap.canceled += instance.OnOpenMap;
            }
        }
    }
    public PlayerCharacterActions @PlayerCharacter => new PlayerCharacterActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IPlayerCharacterActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnBasicSkill(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnSwapSkillUp(InputAction.CallbackContext context);
        void OnSwapSkillDown(InputAction.CallbackContext context);
        void OnMagicSkill(InputAction.CallbackContext context);
        void OnUltimateSkill(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnOpenMap(InputAction.CallbackContext context);
    }
}
