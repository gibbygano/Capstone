using System;
using System.Windows;

namespace com.WanderingTurtle.FormPresentation.Models
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InputValidationException"/> class.
    /// </summary>
    internal class InputValidationException : WanderingTurtleException
    {
        /// <summary>
        /// Show <see cref="InputValidationException"/> Error Message with a <see cref="string"/> <paramref name="message"/>.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="message"></param>
        public InputValidationException(FrameworkElement component, string message)
            : base(component, message, true) { }

        /// <summary>
        /// Show <see cref="InputValidationException"/> Error Message with a <see cref="string"/> <paramref name="message"/> and <see cref="string"/> <paramref name="title"/>.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="message"></param>
        /// <param name="title"></param>
        public InputValidationException(FrameworkElement component, string message, string title)
            : base(component, message, title, true) { }

        /// <summary>
        /// Show <see cref="InputValidationException"/> Error Message with an already constructed <see cref="WanderingTurtleException"/> <paramref name="exception"/>.
        /// </summary>
        /// <param name="exception"></param>
        public InputValidationException(WanderingTurtleException exception)
            : base(exception.CurrentControl, exception.Message, exception.Title) { }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WanderingTurtleException"/> class.
    /// </summary>
    internal class WanderingTurtleException : ApplicationException
    {
        /// <summary>
        /// Show <see cref="WanderingTurtleException"/> Error Message with a <see cref="string"/> <paramref name="message"/>.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="message"></param>
        /// <param name="handleException"></param>
        public WanderingTurtleException(FrameworkElement control, string message, bool handleException = true)
            : base(message)
        {
            CurrentControl = control;
            DoHandle = handleException;
        }

        /// <summary>
        /// Show <see cref="WanderingTurtleException"/> Error Message with a <see cref="string"/> <paramref name="message"/> and <see cref="Exception.InnerException"/> <paramref name="innerException"/>.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        /// <param name="handleException"></param>
        public WanderingTurtleException(FrameworkElement control, string message, Exception innerException, bool handleException = true)
            : base(message, innerException)
        {
            CurrentControl = control;
            DoHandle = handleException;
        }

        /// <summary>
        /// Show <see cref="WanderingTurtleException"/> Error Message with an <see cref="string"/> <see cref="Exception"/>.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="exception"></param>
        /// <param name="handleException"></param>
        public WanderingTurtleException(FrameworkElement control, Exception exception, bool handleException = true)
            : base(exception.Message, exception.InnerException)
        {
            CurrentControl = control;
            DoHandle = handleException;
        }

        /// <summary>
        /// Show <see cref="WanderingTurtleException"/> Error Message with an <see cref="string"/> <see cref="Exception"/> and <see cref="string"/> <paramref name="title"/>.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="exception"></param>
        /// <param name="title"></param>
        /// <param name="handleException"></param>
        public WanderingTurtleException(FrameworkElement control, Exception exception, string title, bool handleException = true)
            : base(exception.Message, exception.InnerException)
        {
            CurrentControl = control;
            DoHandle = handleException;
            Title = title;
        }

        /// <summary>
        /// Show <see cref="WanderingTurtleException"/> Error Message with a <see cref="string"/> <paramref name="message"/> and <see cref="string"/> <paramref name="title"/>.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="handleException"></param>
        public WanderingTurtleException(FrameworkElement control, string message, string title, bool handleException = true)
            : base(message)
        {
            CurrentControl = control;
            DoHandle = handleException;
            Title = title;
        }

        /// <summary>
        /// Show <see cref="WanderingTurtleException"/> Error Message with a <see cref="string"/> <paramref name="message"/> and <see cref="string"/> <paramref name="title"/> and <see cref="Exception.InnerException"/> <paramref name="innerException"/>.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="innerException"></param>
        /// <param name="handleException"></param>
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