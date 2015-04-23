using com.WanderingTurtle.BusinessLogic;
using System.Windows.Controls;

namespace com.WanderingTurtle.FormPresentation.Views.Admin
{
    /// <summary>
    /// Interaction logic for EventType.xaml
    /// </summary>
    internal partial class EventType : UserControl
    {
        private EventManager _eventManager = new EventManager();

        public EventType()
        {
            InitializeComponent();
            var list = DataCache._currentEventTypeList;
            listbEventTypes.DataContext = list;
        }
    }
}