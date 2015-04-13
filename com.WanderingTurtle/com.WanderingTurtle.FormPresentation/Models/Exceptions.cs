using System;
using System.Windows;

namespace com.WanderingTurtle.FormPresentation.Models
{
    internal class WanderingTurtleException : ApplicationException
    {
        public WanderingTurtleException(FrameworkElement control, string message)
            : base(message)
        {
            CurrentControl = control;
        }

        public WanderingTurtleException(FrameworkElement control, Exception exception)
            : base(exception.Message, exception.InnerException)
        {
            CurrentControl = control;
        }

        public WanderingTurtleException(FrameworkElement control, Exception exception, string title)
            : base(exception.Message, exception.InnerException)
        {
            CurrentControl = control;
            Title = title;
        }

        public WanderingTurtleException(FrameworkElement control, string message, string title)
            : base(message)
        {
            CurrentControl = control;
            Title = title;
        }

        public WanderingTurtleException(FrameworkElement control, string message, string title, Exception innerException)
            : base(message, innerException)
        {
            CurrentControl = control;
            Title = title;
        }

        public FrameworkElement CurrentControl { get; set; }

        public string Title { get; set; }
    }
}