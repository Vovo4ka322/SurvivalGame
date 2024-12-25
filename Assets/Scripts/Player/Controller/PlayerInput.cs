using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using Interfaces;

namespace Player.Controller
{
    public class PlayerInput : IInputActionCollection2, IDisposable
    {
        public readonly InputActionMap MPlayer;
        public readonly List<IPlayerActions> MPlayerActionsCallbackInterfaces = new List<IPlayerActions>();
        public readonly InputAction MPlayerMove;
        public readonly InputAction MPlayerLook2;
        public readonly InputAction MPlayerUseFirstAbility;
        public readonly InputAction MPlayerUseSecondAbility;
        
        private InputActionAsset _asset;
        private int _mKeyboardandmouseSchemeIndex = -1;
        private int _mPhoneSchemeIndex = -1;
        
        public PlayerInput()
        {
            _asset = InputActionAsset.FromJson(@"{ ""name"": ""PlayerInput"", ""maps"": [
            {
            ""name"": ""Player"",
            ""id"": ""9caf130e-5eaf-40a2-9bbd-91415b18e93b"",
            ""actions"": 
            [
                { 
                    ""name"": ""Move"", ""type"": ""Value"", ""id"": ""012a9772-60d3-4526-824c-8ad5cf43857d"",
                    ""expectedControlType"": ""Vector2"", ""processors"": """", ""interactions"": """", ""initialStateCheck"": true
                },
                { 
                    ""name"": ""Look2"", ""type"": ""Value"", ""id"": ""7afec11f-3b35-40aa-bc3e-bb22c63f0a90"",
                    ""expectedControlType"": ""Vector2"", ""processors"": """", ""interactions"": """", ""initialStateCheck"": true
                },
                {
                    ""name"": ""UseFirstAbility"", ""type"": ""Button"", ""id"": ""c687af4f-0513-4113-9c3f-3912ab6f46bb"",
                    ""expectedControlType"": """", ""processors"": """", ""interactions"": """", ""initialStateCheck"": false
                },
                {
                    ""name"": ""UseSecondAbility"", ""type"": ""Button"", ""id"": ""72c3425b-3bfa-4322-be03-b83b7593905b"",
                    ""expectedControlType"": """", ""processors"": """", ""interactions"": """", ""initialStateCheck"": false
                }
            ],
            ""bindings"": 
            [
                {
                    ""name"": ""2D Vector"", ""id"": ""f55a658b-531e-45d9-88a1-9e858ce122ba"", ""path"": ""2DVector"",
                    ""interactions"": """", ""processors"": """", ""groups"": """", ""action"": ""Move"", ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"", ""id"": ""e37c2471-a86c-4922-a5f6-af30bca333b3"", ""path"": ""<Keyboard>/w"",
                    ""interactions"": """", ""processors"": """", ""groups"": """", ""action"": ""Move"", ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"", ""id"": ""d2ea9701-6cff-41eb-bf34-169ca9b9ff95"", ""path"": ""<Keyboard>/s"",
                    ""interactions"": """", ""processors"": """", ""groups"": """", ""action"": ""Move"", ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"", ""id"": ""75854b25-c46c-4a80-a70c-8079677c3f5e"", ""path"": ""<Keyboard>/a"",
                    ""interactions"": """", ""processors"": """", ""groups"": """", ""action"": ""Move"",  ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"", ""id"": ""92caa51b-e776-4b56-b09d-ada045487ae2"", ""path"": ""<Keyboard>/d"",
                    ""interactions"": """", ""processors"": """", ""groups"": """", ""action"": ""Move"", ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"", ""id"": ""7b5594a3-f356-42c1-8c09-2f006faf2211"",  ""path"": ""2DVector"",
                    ""interactions"": """", ""processors"": """", ""groups"": """", ""action"": ""Move"", ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"", ""id"": ""347e35e6-11f5-4dec-a09b-00ac3fb0ad58"", ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """", ""processors"": """", ""groups"": """", ""action"": ""Move"", ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"", ""id"": ""8ce7eca3-4589-4d42-bff6-8ec0997bf3aa"", ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """", ""processors"": """", ""groups"": """", ""action"": ""Move"", ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"", ""id"": ""511f02dd-1b21-49ea-a76e-4f6bf79b01dd"", ""path"": ""<Gamepad>/leftStick/left"", 
                    ""interactions"": """", ""processors"": """", ""groups"": """", ""action"": ""Move"", ""isComposite"": false, 
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"", ""id"": ""35774447-0a13-4b02-95dc-1f96282c88b1"", ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """", ""processors"": """", ""groups"": """", ""action"": ""Move"", ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """", ""id"": ""a2e3eb66-9c11-4a75-be64-ba3639902363"", ""path"": ""<Mouse>/position"",
                    ""interactions"": """", ""processors"": """", ""groups"": """", ""action"": ""Look2"", ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """", ""id"": ""88d9a953-55ca-4485-a024-5e6679a4d4e5"", ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """", ""processors"": """", ""groups"": """", ""action"": ""Look2"", ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """", ""id"": ""64f0f31e-6e26-4091-8245-aca59e3c8af4"", ""path"": ""<Keyboard>/1"",
                    ""interactions"": ""Press(pressPoint=0.1)"", ""processors"": """", ""groups"": """", ""action"": ""UseFirstAbility"",
                    ""isComposite"": false, ""isPartOfComposite"": false
                },
                {
                    ""name"": """", ""id"": ""ec36bd1b-c68b-42c3-a990-9f80deee1132"", ""path"": ""<Keyboard>/2"",
                    ""interactions"": ""Press(pressPoint=0.1)"", ""processors"": """", ""groups"": """", ""action"": ""UseSecondAbility"",
                    ""isComposite"": false, ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and mouse"", ""bindingGroup"": ""Keyboard and mouse"", ""devices"": 
            [
                {
                    ""devicePath"": ""<Keyboard>"", ""isOptional"": false, ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"", ""isOptional"": false, ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Phone"", ""bindingGroup"": ""Phone"", ""devices"": 
            [
                {
                    ""devicePath"": ""<Gamepad>"", ""isOptional"": false, ""isOR"": false
                }
            ]
        }
    ]
}");
            
            MPlayer = _asset.FindActionMap("Player", throwIfNotFound: true);
            MPlayerMove = MPlayer.FindAction("Move", throwIfNotFound: true);
            MPlayerLook2 = MPlayer.FindAction("Look2", throwIfNotFound: true);
            MPlayerUseFirstAbility = MPlayer.FindAction("UseFirstAbility", throwIfNotFound: true);
            MPlayerUseSecondAbility = MPlayer.FindAction("UseSecondAbility", throwIfNotFound: true);
        }

        public PlayerActions Player => new PlayerActions(this);
        
        ~PlayerInput()
        {
            UnityEngine.Debug.Assert(!MPlayer.enabled, "This will cause a leak and performance issues, PlayerInput.Player.Disable() has not been called.");
        }

        public InputBinding? bindingMask
        {
            get => _asset.bindingMask;
            set => _asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => _asset.devices;
            set => _asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => _asset.controlSchemes;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return _asset.FindAction(actionNameOrId, throwIfNotFound);
        }
        
        public IEnumerator<InputAction> GetEnumerator()
        {
            return _asset.GetEnumerator();
        }
        
        public IEnumerable<InputBinding> bindings => _asset.bindings;
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(_asset);
        }
        
        public void Enable()
        {
            _asset.Enable();
        }

        public void Disable()
        {
            _asset.Disable();
        }

        public bool Contains(InputAction action)
        {
            return _asset.Contains(action);
        }
        
        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return _asset.FindBinding(bindingMask, out action);
        }
        
        public InputControlScheme KeyboardandmouseScheme
        {
            get
            {
                if(_mKeyboardandmouseSchemeIndex == -1) _mKeyboardandmouseSchemeIndex = _asset.FindControlSchemeIndex("Keyboard and mouse");
                return _asset.controlSchemes[_mKeyboardandmouseSchemeIndex];
            }
        }
        
        public InputControlScheme PhoneScheme
        {
            get
            {
                if(_mPhoneSchemeIndex == -1) _mPhoneSchemeIndex = _asset.FindControlSchemeIndex("Phone");
                return _asset.controlSchemes[_mPhoneSchemeIndex];
            }
        }
    }
}
