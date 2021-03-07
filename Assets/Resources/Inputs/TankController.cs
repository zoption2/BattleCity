// GENERATED AUTOMATICALLY FROM 'Assets/Resources/Inputs/TankController.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @TankController : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @TankController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""TankController"",
    ""maps"": [
        {
            ""name"": ""Tank"",
            ""id"": ""518236c7-0d28-4789-b2a8-cea7bc294bc1"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""633412fc-7810-4fb9-982a-f7d44149f53f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveUp"",
                    ""type"": ""Button"",
                    ""id"": ""74217c34-6751-4f6d-95d2-5c041ba8732b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveRight"",
                    ""type"": ""Button"",
                    ""id"": ""6f21c49f-ab86-46b6-bfb2-a35bdc796f84"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveLeft"",
                    ""type"": ""Button"",
                    ""id"": ""4bd9edf8-ea73-43fb-b317-103f88463a2d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveDown"",
                    ""type"": ""Button"",
                    ""id"": ""e2971965-cbef-42af-91b3-83ac0bfad1e1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FirstSkill"",
                    ""type"": ""Button"",
                    ""id"": ""cce04ca5-7606-460b-a6dd-b84ea0d57efb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SecondSkill"",
                    ""type"": ""Button"",
                    ""id"": ""d91fa486-ef33-49e7-9753-eab60adc94e0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ThirdSkill"",
                    ""type"": ""Button"",
                    ""id"": ""dbce0421-53f4-48ec-8552-ff77128c40f4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AutoShoot"",
                    ""type"": ""Value"",
                    ""id"": ""2fda4137-bd5a-4ecc-9f87-aabfe4668eb0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""d185a6da-6e61-4757-b973-0f815dc2708f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BoosterUI"",
                    ""type"": ""Button"",
                    ""id"": ""0cf52ee6-bb2f-49d9-a3df-37d815a5143b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""55023cf9-6c35-4aac-a72f-6a283dc21da0"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6be24135-f723-4e0a-991c-6cc1d1207e9d"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e909a194-1c07-480e-8b20-a4afbfb71ba7"",
                    ""path"": ""<HID::Twin USB Gamepad      >/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8ae82f4e-1999-415d-ba8c-e2dd90fcfd08"",
                    ""path"": ""<HID::Twin USB Gamepad      >/hat"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b20d4fb-a6df-4a36-8e12-768ddd93a4f5"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c433f59f-139e-42dd-b912-2a5ad652ab2b"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""72ddfae9-a615-4ab5-bfbe-7a1d90102126"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d1d3f57f-78ca-4a00-89f6-67e133a85b45"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f1ee19f-cac3-4eca-8b60-f1b79dec0b91"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""502c2b3c-9cc3-488c-9b79-d0c2eb6f9711"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1703515b-fca2-4cb5-a493-4beaac279599"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""44cb89f7-a4b5-40b2-b105-2e411ad124a0"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""999c0319-278b-44a0-917d-1ba8c8d8ca79"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2b4eba5b-e788-40ad-9cef-d70fa0959d02"",
                    ""path"": ""<HID::Twin USB Gamepad      >/button2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4d57a153-3814-4fb4-ba08-0ff48506959e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7b1dd6aa-f9a7-463e-9ca1-edd15331dbbf"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThirdSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ece78f90-c94b-4dee-ad19-08dd88caa93a"",
                    ""path"": ""<HID::Twin USB Gamepad      >/button6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThirdSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f5bc8ffd-d263-4de6-90b8-70519e491ee6"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThirdSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2be4ee4b-24e5-48ac-bdfc-c6078e77d983"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AutoShoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""14aceac1-f3a1-4c4b-8417-17808a1561bf"",
                    ""path"": ""<HID::Twin USB Gamepad      >/button8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AutoShoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ce8d7806-ab40-41b2-981d-8c640fda13c1"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AutoShoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e27d2456-4099-4a40-b48f-135de7a90ff4"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""95d64f3d-e78a-41de-9f4f-48bd3264e66e"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3cfd5367-e644-44a2-8048-617dbe73256c"",
                    ""path"": ""<HID::Twin USB Gamepad      >/button10"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bbe23614-88b6-45d7-856e-df80a0bb4dae"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BoosterUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3267268-4c3a-41da-939c-a9f1d2f1b6d3"",
                    ""path"": ""<HID::Twin USB Gamepad      >/button4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BoosterUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ca3853df-8c87-462a-a2bf-f7f566b252c0"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BoosterUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c4f0516b-be67-4d14-88ef-5f3cfebb4b36"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FirstSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""99ea8419-3fcc-4d9a-9cc2-89c25a6810b1"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FirstSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""537556db-c67a-4be0-83bc-a1c916512d1e"",
                    ""path"": ""<HID::Twin USB Gamepad      >/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FirstSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""7f2e3979-7a82-48b6-95e5-7b2311548e08"",
            ""actions"": [
                {
                    ""name"": ""Navigation"",
                    ""type"": ""Value"",
                    ""id"": ""9d788d62-5595-43d5-9e45-5887d18d3b2b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""5c85f633-0d81-4166-a21b-401d5df89f38"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fdbd450c-cd8d-451b-81f1-305668e07c9b"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""02597fa7-987a-4c40-a168-b377e6f09f91"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bc4dfcdf-9ab0-42d5-aa75-2fe384f83780"",
                    ""path"": ""<HID::Twin USB Gamepad      >/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""77ef2024-f1e4-4437-a940-eaf961dae6d5"",
                    ""path"": ""<HID::Twin USB Gamepad      >/hat"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""61aec5c0-a808-4ca6-85f9-ab153cebbb21"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""696e9b91-fb55-44a6-96d5-a2c8ddefce23"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""00f514da-12ba-472b-b0d6-bd920690d1ec"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3f45e882-aeeb-4b4c-881f-698afa299069"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""bec42bc4-0f3e-411b-86e8-d21968f725c9"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""60def2f7-5283-49b0-96e0-43177c991fbc"",
                    ""path"": ""<Joystick>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f0a1a354-bb8a-4754-ae65-5590a93fdd3c"",
                    ""path"": ""*/{Submit}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ed771afa-6c84-48e4-a370-549468fd10b3"",
                    ""path"": ""<HID::Twin USB Gamepad      >/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f99d4792-7ac7-4110-bef3-4595f47cc8f3"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Tank
        m_Tank = asset.FindActionMap("Tank", throwIfNotFound: true);
        m_Tank_Move = m_Tank.FindAction("Move", throwIfNotFound: true);
        m_Tank_MoveUp = m_Tank.FindAction("MoveUp", throwIfNotFound: true);
        m_Tank_MoveRight = m_Tank.FindAction("MoveRight", throwIfNotFound: true);
        m_Tank_MoveLeft = m_Tank.FindAction("MoveLeft", throwIfNotFound: true);
        m_Tank_MoveDown = m_Tank.FindAction("MoveDown", throwIfNotFound: true);
        m_Tank_FirstSkill = m_Tank.FindAction("FirstSkill", throwIfNotFound: true);
        m_Tank_SecondSkill = m_Tank.FindAction("SecondSkill", throwIfNotFound: true);
        m_Tank_ThirdSkill = m_Tank.FindAction("ThirdSkill", throwIfNotFound: true);
        m_Tank_AutoShoot = m_Tank.FindAction("AutoShoot", throwIfNotFound: true);
        m_Tank_Pause = m_Tank.FindAction("Pause", throwIfNotFound: true);
        m_Tank_BoosterUI = m_Tank.FindAction("BoosterUI", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Navigation = m_UI.FindAction("Navigation", throwIfNotFound: true);
        m_UI_Submit = m_UI.FindAction("Submit", throwIfNotFound: true);
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

    // Tank
    private readonly InputActionMap m_Tank;
    private ITankActions m_TankActionsCallbackInterface;
    private readonly InputAction m_Tank_Move;
    private readonly InputAction m_Tank_MoveUp;
    private readonly InputAction m_Tank_MoveRight;
    private readonly InputAction m_Tank_MoveLeft;
    private readonly InputAction m_Tank_MoveDown;
    private readonly InputAction m_Tank_FirstSkill;
    private readonly InputAction m_Tank_SecondSkill;
    private readonly InputAction m_Tank_ThirdSkill;
    private readonly InputAction m_Tank_AutoShoot;
    private readonly InputAction m_Tank_Pause;
    private readonly InputAction m_Tank_BoosterUI;
    public struct TankActions
    {
        private @TankController m_Wrapper;
        public TankActions(@TankController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Tank_Move;
        public InputAction @MoveUp => m_Wrapper.m_Tank_MoveUp;
        public InputAction @MoveRight => m_Wrapper.m_Tank_MoveRight;
        public InputAction @MoveLeft => m_Wrapper.m_Tank_MoveLeft;
        public InputAction @MoveDown => m_Wrapper.m_Tank_MoveDown;
        public InputAction @FirstSkill => m_Wrapper.m_Tank_FirstSkill;
        public InputAction @SecondSkill => m_Wrapper.m_Tank_SecondSkill;
        public InputAction @ThirdSkill => m_Wrapper.m_Tank_ThirdSkill;
        public InputAction @AutoShoot => m_Wrapper.m_Tank_AutoShoot;
        public InputAction @Pause => m_Wrapper.m_Tank_Pause;
        public InputAction @BoosterUI => m_Wrapper.m_Tank_BoosterUI;
        public InputActionMap Get() { return m_Wrapper.m_Tank; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TankActions set) { return set.Get(); }
        public void SetCallbacks(ITankActions instance)
        {
            if (m_Wrapper.m_TankActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_TankActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_TankActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_TankActionsCallbackInterface.OnMove;
                @MoveUp.started -= m_Wrapper.m_TankActionsCallbackInterface.OnMoveUp;
                @MoveUp.performed -= m_Wrapper.m_TankActionsCallbackInterface.OnMoveUp;
                @MoveUp.canceled -= m_Wrapper.m_TankActionsCallbackInterface.OnMoveUp;
                @MoveRight.started -= m_Wrapper.m_TankActionsCallbackInterface.OnMoveRight;
                @MoveRight.performed -= m_Wrapper.m_TankActionsCallbackInterface.OnMoveRight;
                @MoveRight.canceled -= m_Wrapper.m_TankActionsCallbackInterface.OnMoveRight;
                @MoveLeft.started -= m_Wrapper.m_TankActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.performed -= m_Wrapper.m_TankActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.canceled -= m_Wrapper.m_TankActionsCallbackInterface.OnMoveLeft;
                @MoveDown.started -= m_Wrapper.m_TankActionsCallbackInterface.OnMoveDown;
                @MoveDown.performed -= m_Wrapper.m_TankActionsCallbackInterface.OnMoveDown;
                @MoveDown.canceled -= m_Wrapper.m_TankActionsCallbackInterface.OnMoveDown;
                @FirstSkill.started -= m_Wrapper.m_TankActionsCallbackInterface.OnFirstSkill;
                @FirstSkill.performed -= m_Wrapper.m_TankActionsCallbackInterface.OnFirstSkill;
                @FirstSkill.canceled -= m_Wrapper.m_TankActionsCallbackInterface.OnFirstSkill;
                @SecondSkill.started -= m_Wrapper.m_TankActionsCallbackInterface.OnSecondSkill;
                @SecondSkill.performed -= m_Wrapper.m_TankActionsCallbackInterface.OnSecondSkill;
                @SecondSkill.canceled -= m_Wrapper.m_TankActionsCallbackInterface.OnSecondSkill;
                @ThirdSkill.started -= m_Wrapper.m_TankActionsCallbackInterface.OnThirdSkill;
                @ThirdSkill.performed -= m_Wrapper.m_TankActionsCallbackInterface.OnThirdSkill;
                @ThirdSkill.canceled -= m_Wrapper.m_TankActionsCallbackInterface.OnThirdSkill;
                @AutoShoot.started -= m_Wrapper.m_TankActionsCallbackInterface.OnAutoShoot;
                @AutoShoot.performed -= m_Wrapper.m_TankActionsCallbackInterface.OnAutoShoot;
                @AutoShoot.canceled -= m_Wrapper.m_TankActionsCallbackInterface.OnAutoShoot;
                @Pause.started -= m_Wrapper.m_TankActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_TankActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_TankActionsCallbackInterface.OnPause;
                @BoosterUI.started -= m_Wrapper.m_TankActionsCallbackInterface.OnBoosterUI;
                @BoosterUI.performed -= m_Wrapper.m_TankActionsCallbackInterface.OnBoosterUI;
                @BoosterUI.canceled -= m_Wrapper.m_TankActionsCallbackInterface.OnBoosterUI;
            }
            m_Wrapper.m_TankActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @MoveUp.started += instance.OnMoveUp;
                @MoveUp.performed += instance.OnMoveUp;
                @MoveUp.canceled += instance.OnMoveUp;
                @MoveRight.started += instance.OnMoveRight;
                @MoveRight.performed += instance.OnMoveRight;
                @MoveRight.canceled += instance.OnMoveRight;
                @MoveLeft.started += instance.OnMoveLeft;
                @MoveLeft.performed += instance.OnMoveLeft;
                @MoveLeft.canceled += instance.OnMoveLeft;
                @MoveDown.started += instance.OnMoveDown;
                @MoveDown.performed += instance.OnMoveDown;
                @MoveDown.canceled += instance.OnMoveDown;
                @FirstSkill.started += instance.OnFirstSkill;
                @FirstSkill.performed += instance.OnFirstSkill;
                @FirstSkill.canceled += instance.OnFirstSkill;
                @SecondSkill.started += instance.OnSecondSkill;
                @SecondSkill.performed += instance.OnSecondSkill;
                @SecondSkill.canceled += instance.OnSecondSkill;
                @ThirdSkill.started += instance.OnThirdSkill;
                @ThirdSkill.performed += instance.OnThirdSkill;
                @ThirdSkill.canceled += instance.OnThirdSkill;
                @AutoShoot.started += instance.OnAutoShoot;
                @AutoShoot.performed += instance.OnAutoShoot;
                @AutoShoot.canceled += instance.OnAutoShoot;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @BoosterUI.started += instance.OnBoosterUI;
                @BoosterUI.performed += instance.OnBoosterUI;
                @BoosterUI.canceled += instance.OnBoosterUI;
            }
        }
    }
    public TankActions @Tank => new TankActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Navigation;
    private readonly InputAction m_UI_Submit;
    public struct UIActions
    {
        private @TankController m_Wrapper;
        public UIActions(@TankController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigation => m_Wrapper.m_UI_Navigation;
        public InputAction @Submit => m_Wrapper.m_UI_Submit;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Navigation.started -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigation;
                @Navigation.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigation;
                @Navigation.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigation;
                @Submit.started -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
                @Submit.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
                @Submit.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Navigation.started += instance.OnNavigation;
                @Navigation.performed += instance.OnNavigation;
                @Navigation.canceled += instance.OnNavigation;
                @Submit.started += instance.OnSubmit;
                @Submit.performed += instance.OnSubmit;
                @Submit.canceled += instance.OnSubmit;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    public interface ITankActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnMoveUp(InputAction.CallbackContext context);
        void OnMoveRight(InputAction.CallbackContext context);
        void OnMoveLeft(InputAction.CallbackContext context);
        void OnMoveDown(InputAction.CallbackContext context);
        void OnFirstSkill(InputAction.CallbackContext context);
        void OnSecondSkill(InputAction.CallbackContext context);
        void OnThirdSkill(InputAction.CallbackContext context);
        void OnAutoShoot(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnBoosterUI(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnNavigation(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
    }
}
