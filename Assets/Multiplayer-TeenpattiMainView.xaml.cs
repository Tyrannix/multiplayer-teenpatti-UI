#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System;
using System.Windows.Controls;
#endif

namespace Multiplayer_Teenpatti
{
    /// <summary>
    /// Interaction logic for Multiplayer_TeenpattiMainView.xaml
    /// </summary>
    public partial class Multiplayer_TeenpattiMainView : UserControl
    {
        public Multiplayer_TeenpattiMainView()
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
