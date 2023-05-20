using UnityEngine;
using UnityEngine.InputSystem;

namespace ToyMatch
{
    public class Pointer : MonoBehaviour
    {
        public static Pointer Inst { get; private set; }

        public InputAction mousePointer;
        public InputAction hold;

        public static Camera Main;
        public static float MaxRayDist = 12f;

        public LayerMask toymask;

        void Awake()
        {
            if (Inst != null)
            {
                Destroy(this);
                return;
            }

            Inst = this;
            Main = Camera.main;
        }

        void OnEnable()
        {
            hold.performed += HoldOnperformed;
            mousePointer.Enable();
            hold.Enable();
        }

        void OnDisable()
        {
            hold.Disable();
            mousePointer.Disable();
            hold.performed -= HoldOnperformed;
        }

        void HoldOnperformed(InputAction.CallbackContext obj)
        {
            if (hold.IsPressed())
            {
                Select();
            }
            else
            {
                UnSelect();
            }
        }


        void Select()
        {
            Vector2 mousePos = mousePointer.ReadValue<Vector2>();
            Ray selectionRay = Main.ScreenPointToRay(mousePos);

            if(!Physics.Raycast(selectionRay, out RaycastHit hit, MaxRayDist, toymask)) return;
            
            hit.collider.GetComponent<Toy>().Select();
        }

        void UnSelect()
        {
            SelectMaster.OnToyUnselected();
        }
    }
}
