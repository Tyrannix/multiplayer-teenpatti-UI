#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System;
using System.Windows.Controls;
#endif

namespace Multiplayer_Teenpatti.Assets.NoesisGUI.Loader
{
    /// <summary>
    /// Interaction logic for Loader.xaml
    /// </summary>
    public partial class Loader : UserControl
    {
        public Loader()
        {
            InitializeComponent();
        }
        #if NOESIS
            private void InitializeComponent()
            {
                NoesisUnity.LoadComponent(this);
            }
        #endif
    }
}
