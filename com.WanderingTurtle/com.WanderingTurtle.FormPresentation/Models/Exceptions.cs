using System;
using System.Windows;

namespace com.WanderingTurtle.FormPresentation.Models
{
    internal class InputValidationException : WanderingTurtleException
    {
        public InputValidationException(FrameworkElement component, string message)
            : base(component, message, true) { }

        public InputValidationException(FrameworkElement component, string message, string title)
            : base(component, message, title, true) { }
    }

    internal class WanderingTurtleException : ApplicationException
    {

        public WanderingTurtleException(FrameworkElement control, string message, bool handleException = true)
            : base(message)
        {
            CurrentControl = control;
            DoHandle = handleException;
        }

        public WanderingTurtleException(FrameworkElement control, Exception exception, bool handleException = true)
            : base(exception.Message, exception.InnerException)
        {
            CurrentControl = control;
            DoHandle = handleException;
        }

        public WanderingTurtleException(FrameworkElement control, Exception exception, string title, bool handleException = true)
            : base(exception.Message, exception.InnerException)
        {
            CurrentControl = control;
            DoHandle = handleException;
            Title = title;
        }

        public WanderingTurtleException(FrameworkElement control, string message, string title, bool handleException = true)
            : base(message)
        {
            CurrentControl = control;
            DoHandle = handleException;
            Title = title;
        }

        public WanderingTurtleException(FrameworkElement control, string message, string title, Exception innerException, bool handleException = true)
            : base(message, innerException)
        {
            CurrentControl = control;
            DoHandle = handleException;
            Title = title;
        }

        public FrameworkElement CurrentControl { get; set; }

        public bool DoHandle { get; set; }

        public string Title { get; set; }
    }
}