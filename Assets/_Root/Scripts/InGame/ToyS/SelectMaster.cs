using System;

namespace ToyMatch
{
    public static class SelectMaster
    {
        public static event Action ToySelected;
        public static event Action ToyUnselected;

        public static bool IsSelected { get; private set; }
        public static Toy Selected { get; private set; }

        public static void OnToySelected(Toy obj)
        {
            Selected = obj;
            IsSelected = true;
            ToySelected?.Invoke();
        }
        
        public static void OnToyUnselected()
        {
            if(!IsSelected) return;
            IsSelected = false;
            ToyUnselected?.Invoke();
            Selected.Deselect();
        }
    }
}
