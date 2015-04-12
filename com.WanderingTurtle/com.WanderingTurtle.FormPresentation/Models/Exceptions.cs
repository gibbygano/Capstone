using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            : base(null, exception)
        {
            CurrentControl = control;
            Title = title;
        }

        public WanderingTurtleException(FrameworkElement control, string message, Exception innerException)
            : base(message, innerException)
        {
            CurrentControl = control;
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